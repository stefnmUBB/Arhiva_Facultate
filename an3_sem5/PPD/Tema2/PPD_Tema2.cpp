#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include <sstream>
#include <fstream>
#include <chrono>
#include <functional>
#include <thread>
#include <barrier>

using namespace std;


struct Matrix
{
	int n, m;
	int* items;

    Matrix(int n, int m)
    {
        this->n = n;
        this->m = m;
        items = new int[n * m];
    }

	__forceinline
		int operator() (int i, int j) const { return items[i * m + j]; }		

	__forceinline
		int& operator() (int i, int j) { return items[i * m + j]; }	

	friend ostream& operator << (ostream& s, const Matrix& a)
	{
		s << a.n << " " << a.m << "\n";
		for (int i = 0; i < a.n; i++)
		{
			for (int j = 0; j < a.m; j++)
				s << a(i, j) << " ";
			s << "\n";
		}
		return s;
	}    

	string to_string() const
	{
		stringstream ss;
		ss << (*this);
		return ss.str();
	}

    ~Matrix()
    {
        delete[] items;
    }

    void copy_line(int i, int* dest) const
    {
        memcpy(dest, &items[i * m], m * sizeof(int));
    }
};

__forceinline constexpr int clamp(int x, int a, int b) { return x <= a ? a : x >= b ? b : x; }

static void convolveInPlace(Matrix* K, Matrix* A, int i0, int i1, barrier<>* b=nullptr) {
    int* prevLine = new int[A->m];
    int* crtLine = new int[A->m];
    int* lastBorder = new int[A->m];
    
    A->copy_line(clamp(i1, 0, A->n - 1), lastBorder);

    for (int j = 0; j < A->m; j++) prevLine[j] = (*A)(clamp(i0 - 1, 0, A->n - 1), j);

    if (b)
    {
        b->arrive_and_wait();
    }

    for (int i = i0; i < i1; i++) {
        A->copy_line(i, crtLine);        
        for (int j = 0; j < A->m; j++) {
            int lk0 = (*K)(0, 0) * prevLine[clamp(j - 1, 0, A->m - 1)]
                + (*K)(0, 1) * prevLine[clamp(j, 0, A->m - 1)]
                + (*K)(0, 2) * prevLine[clamp(j + 1, 0, A->m - 1)];
            int lk1 = (*K)(1, 0) * crtLine[clamp(j - 1, 0, A->m - 1)]
                + (*K)(1, 1) * crtLine[clamp(j, 0, A->m - 1)]
                + (*K)(1, 2) * crtLine[clamp(j + 1, 0, A->m - 1)];
            int lk2 = 0;
            if (i == i1 - 1) {
                lk2 = (*K)(2, 0) * lastBorder[clamp(j - 1, 0, A->m - 1)]
                    + (*K)(2, 1) * lastBorder[clamp(j, 0, A->m - 1)]
                    + (*K)(2, 2) * lastBorder[clamp(j + 1, 0, A->m - 1)];
            }
            else {
                int ni = clamp(i + 1, 0, (*A).n - 1);
                lk2 = (*K)(2, 0) * (*A)(ni, clamp(j - 1, 0, A->m - 1))
                    + (*K)(2, 1) * (*A)(ni, clamp(j, 0, A->m - 1))
                    + (*K)(2, 2) * (*A)(ni, clamp(j + 1, 0, A->m - 1));
            }
            (*A)(i, j) = lk0 + lk1 + lk2;
        }

        memcpy(prevLine, crtLine, A->m * sizeof(int));        
    }

    delete[] prevLine;
    delete[] crtLine;
    delete[] lastBorder;
}

void convert(const string& path)
{
    cout << "Converting";
    ifstream f(path);
    ofstream g("D:\\date2.bin", ios::binary);
    
    int x;    
    while (f >> x)
    {
        g.write(reinterpret_cast<const char*>(&x), sizeof(x));        
    }
    f.close();
    g.close();

    cout << "Done.";
}

void load(const string& path, Matrix& a, Matrix& k)
{
    ifstream f("D:\\date2.bin", ios::binary);

    int x[25];
    
    f.read(reinterpret_cast<char*>(x), sizeof(x));    
    memcpy(k.items, x, 9 * sizeof(int));
    f.read(reinterpret_cast<char*>(a.items), sizeof(int) * a.n * a.m);
    f.close();    
}


int int_arg(int argc, char** argv, int i, int defaultValue)
{
    if (i >= argc) return defaultValue;
    int r;
    stringstream ss(argv[i]);
    ss >> r;
    return ss.fail() ? defaultValue : r;    
}

template<class Fn, class... Types>
void measure(Fn&& f, Types&&... args)
{
    auto t_start = std::chrono::high_resolution_clock::now();
    f(args...);
    auto t_end = std::chrono::high_resolution_clock::now();
    double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
    cout << ">>Measured time = " << elapsed_time_ms << "\n";
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

void run_threads(int p, Matrix* K, Matrix* A, barrier<>* b)
{
    thread* threads = new thread[p];

    thread* t = threads;
    interval_splitter(A->n, p, [&t, A, K, b](int s, int e)
        {
            *(t++) = thread(convolveInPlace, K, A, s, e, b);
        });

    for (int i = 0; i < p; i++) threads[i].join();
    delete[] threads;
}


int main(int argc, char** argv)
{
    int p = int_arg(argc, argv, 1, 2);
    int N = int_arg(argc, argv, 2, 10);
    int M = int_arg(argc, argv, 3, 10);

    Matrix K(3, 3);
    Matrix A(N, M);    

    //convert("D:\\date2.txt");
    load("D:\\date2.txt", A, K);

    if (p == 0) 
    {
        measure(convolveInPlace, &K, &A, 0, A.n, nullptr);
    }
    else
    {                
        barrier b(p);
        measure(run_threads, p, &K, &A, &b);
    }
    
    ofstream g("test_result.out");
    g << A;	
    g.close();

    return 0;
}
