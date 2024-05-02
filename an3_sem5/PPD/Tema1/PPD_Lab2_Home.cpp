#include<iostream>
#include<thread>
#include <cstdlib>
#include <cmath>
#include<string>
#include<fstream>
#include<sstream>
#include<functional>

using namespace std;

struct Matrix 
{
	int n, m;
	int* items;

	__forceinline
	int elem(int i, int j) const { return items[i * m + j]; }

	__forceinline
	int& elem(int i, int j) { return items[i * m + j]; }

	string to_string() const 
	{
		stringstream ss;
		ss << n << " " << m << "\n";
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
				ss << elem(i, j) << " ";
			ss << "\n";
		}
		return ss.str();
	}
};

__forceinline int clamp(int x, int a, int b) { return x <= a ? a : x >= b ? b : x; }

__forceinline void convolve_at(const Matrix& k, const Matrix& a, Matrix& r, int i, int j)
{
	int ai0 = i - k.n / 2, aj0 = j - k.m / 2;
	int result = 0;
	for (int ki = 0; ki < k.n; ki++)
		for (int kj = 0; kj < k.m; kj++) {
			result += k.elem(ki, kj) * a.elem(clamp(ai0 + ki, 0, a.n - 1), clamp(aj0 + kj, 0, a.m - 1));
		}
	r.elem(i, j) = result;
}

__forceinline void convolve_line(const Matrix& k, const Matrix& a, Matrix& r, int i) {
	for (int j = 0; j < a.m; j++) 
		convolve_at(k, a, r, i, j);
}

void convolve_lines(const Matrix& k, const Matrix& a, Matrix& r, int i0, int i1) {
	for (int i = i0; i < i1; i++)
		convolve_line(k, a, r, i);
}

__forceinline
static void convolve_col(const Matrix& k, const Matrix& a, Matrix& r, int j) {
	for (int i = 0; i < a.n; i++)
		convolve_at(k, a, r, i, j);
}

void convolve_cols(const Matrix& k, const Matrix& a, Matrix& r, int j0, int j1) {
	for (int j = j0; j < j1; j++)
		convolve_col(k, a, r, j);
}

__forceinline void convolve_linear(const Matrix& k, const Matrix& a, Matrix& r, int k0,int k1) {
	for (int _k = k0; _k < k1; _k++)
	{
		int i = _k / a.m, j = _k % a.m;
		convolve_at(k, a, r, i, j);
	}		
}

__forceinline void convolve_cyclic(const Matrix& k, const Matrix& a, Matrix& r, int k0, int p) {
	int len = a.n * a.m;
	for (int _k = k0; _k < len; _k+=p)
	{
		int i = _k / a.m, j = _k % a.m;
		convolve_at(k, a, r, i, j);
	}
}

__forceinline void convolve_area(const Matrix& k, const Matrix& a, Matrix& r, int i0,int j0, int i1, int j1) 
{
	for (int i = i0; i <= i1; i++)
		for (int j = j0; j <= j1; j++)
			convolve_at(k, a, r, i, j);
}


void measure(function<void()> f)
{
	auto t_start = std::chrono::high_resolution_clock::now();
	f();
	auto t_end = std::chrono::high_resolution_clock::now();
	double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
	cout << ">>Measured time = " << elapsed_time_ms << "\n";
}



int get_int_arg(int argc, char** args, unsigned int i, int defaultValue)
{
	return i < argc ? atoi(args[i]) : defaultValue;
}

string get_string_arg(int argc, char** args, unsigned int i, string defaultValue)
{
	return i < argc ? string(args[i]) : defaultValue;
}


int eA[1000000], eR[1000000], eK[25];


void load(const string& path, Matrix& a, Matrix& k)
{
	ifstream f(path);

	for (int i = 0; i < 25; i++)
	{
		if (i < k.m * k.n)
			f >> k.items[i];
		else
		{
			int _; f >> _;
		}
	}

	for (int i = 0; i < a.m * a.n; i++)
		f >> a.items[i];

	f.close();
}

void save(const string& path, const Matrix& r)
{
	ofstream g(path);
	g << r.to_string();
	g.close();
}


void run_sequencial(const Matrix& k, const Matrix& a, Matrix& r)
{
	convolve_area(k, a, r, 0, 0, a.n - 1, a.m - 1);
}

void interval_splitter(int n, int p, function<void(int, int)> callback)
{
	for (int i = 0, q = n / p, r = n % p, _s = 0; i < p; i++)
	{
		volatile int s = _s;
		volatile int e = (_s += q + ((i < r) ? 1 : 0));
		callback(s, e);
	}
}

void area_slicer(int i, int j, int n, int m, int p, function<void(int,int,int,int)> callback)
{
	if (p == 1)
	{
		callback(i, j, n, m);
		return;
	}
	if (n > m) {
		area_slicer(i, j, n / 2, m, p / 2 + p % 2, callback);
		area_slicer(i + n / 2, j, n - n / 2, m, p / 2, callback);		
	}
	else {
		area_slicer(i, j, n, m / 2, p / 2 + p % 2, callback);
		area_slicer(i, j + m / 2, n, m - m / 2, p / 2, callback);
	}
}

void th_create_lines(thread* t, int p, const Matrix& k, const Matrix& a, Matrix& r)
{		
	interval_splitter(a.n, p, [&t, a, k, &r](int s, int e)
		{
			*(t++) = thread(convolve_lines, k, a, std::ref(r), s, e);
		});
}

void th_create_cols(thread* t, int p, const Matrix& k, const Matrix& a, Matrix& r)
{	
	interval_splitter(a.m, p, [&t, a, k, &r](int s, int e)
		{
			*(t++) = thread(convolve_cols, k, a, std::ref(r), s, e);
		});
}

void th_create_linear(thread* t, int p, const Matrix& k, const Matrix& a, Matrix& r)
{	
	interval_splitter(a.m*a.n, p, [&t, a, k, &r](int s, int e)
		{
			*(t++) = thread(convolve_linear, k, a, std::ref(r), s, e);
		});
}

void th_create_cyclic(thread* t, int p, const Matrix& k, const Matrix& a, Matrix& r)
{
	interval_splitter(a.m * a.n, p, [&t, a, k, &r](int s, int e)
		{
			*(t++) = thread(convolve_cyclic, k, a, std::ref(r), s, e);
		});
}

void th_create_area(thread* t, int p, const Matrix& k, const Matrix& a, Matrix& r)
{
	area_slicer(0,0,a.n, a.m, p, [&t, a, k, &r](int i0, int j0,int i1,int j1)
		{
			*(t++) = thread(convolve_area, k, a, std::ref(r), i0, j0, i1, j1);
		});
}


int main(int argc, char** argv)
{		
	int p = get_int_arg(argc, argv, 1, 4);
	string worker = get_string_arg(argc, argv, 2, "lines");
	int n = get_int_arg(argc, argv, 3, 5);
	int m = get_int_arg(argc, argv, 4, 5);
	int N = get_int_arg(argc, argv, 5, 1000);
	int M = get_int_arg(argc, argv, 6, 1000);
	int dynamic = get_int_arg(argc, argv, 7, 0);

	cout << (p == 0 ? "Sequencial" : ("Parallel " + to_string(p) + " threads")) << "\n";
	cout << "Allocation: " << (dynamic == 0 ? "static" : "dynamic") << "\n";
	if (p > 0)
		cout << "Worker = " << worker << "\n";
	cout << "Kernel size = " << n << "x" << m << "\n";
	cout << "Matrix size = " << N << "x" << M << "\n";

	Matrix A, K, R;

	A.n = R.n = N;
	A.m = R.m = M;
	K.m = m;
	K.n = n;

	if (dynamic)
	{
		A.items = new int[M * N];
		R.items = new int[M * N];
		K.items = new int[m * n];
	}
	else
	{		
		A.items = eA;
		R.items = eR;
		K.items = eK;
	}

	load("D:\\date.txt", A, K);

	if (p == 0)
	{
		measure([&]() { run_sequencial(K, A, R); });
	}
	else
	{
		thread* threads = new thread[p];
		measure([&]()
			{				
				if (worker == "lines") th_create_lines(threads, p, K, A, R);
				else if (worker == "cols") th_create_cols(threads, p, K, A, R);
				else if (worker == "lind") th_create_linear(threads, p, K, A, R);
				else if (worker == "cyc") th_create_cyclic(threads, p, K, A, R);
				else if (worker == "blocks") th_create_area(threads, p, K, A, R);


				for (int i = 0; i < p; i++) threads[i].join();				
			});

		delete[] threads;
	}	

	save("test_result.out", R);		
	
	return 0;
}
