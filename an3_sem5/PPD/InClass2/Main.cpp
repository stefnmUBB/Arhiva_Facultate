#include<iostream>
#include<thread>
#include <cstdlib>
#include <cmath>

using namespace std;

const int N = 2000000;
const int P = 6;

// switch this to change methods:
//#define LINEAR
//#define STATIC

#ifdef STATIC 
	int A[N], B[N], C[N];
#else 
	int* A = new int[N], *B = new int[N], *C = new int[N];
#endif

void init()
{
	for (int i = 0; i < N; i++)
		A[i] = rand() % 10, B[i] = rand() % 10;
}

ostream& operator << (ostream& o, int* x)
{
	//for (int i = 0; i < N; i++) o << x[i] << " "; 
	return o;
}

void f_lin(int id, int* A, int* B, int* C, int s, int e)
{	
	for (int i = s; i < e; i++)
		C[i] = (int)sqrt(A[i] * A[i] * A[i] + B[i] * B[i] * B[i]);
}

void f_cyc(int id, int* A, int* B, int* C)
{
	for (int i = id; i < N; i += P)
		C[i] = A[i] + B[i];
}

int main()
{
	init();
	cout << "A = " << A << "\n";
	cout << "B = " << B << "\n";
	
	int s[P], e[P];
	thread threads[P];	
	
	for (int i = 0, q = N / P, r = N % P, _s = 0; i < P; i++)
	{
		s[i] = _s;
		e[i] = (_s += q + ((i < r) ? 1 : 0));
	}	
	
	auto t_start = std::chrono::high_resolution_clock::now();

#ifdef LINEAR
	for (int i = 0; i < P; i++)	threads[i] = thread(f_lin, i, A, B, C, s[i], e[i]);
#else
	for (int i = 0; i < P; i++)	threads[i] = thread(f_cyc, i, A, B, C);
#endif	
	for (int i = 0; i < P; i++) threads[i].join();

	auto t_end = std::chrono::high_resolution_clock::now();
	double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
	cout << "elapsed_time_ms = " << elapsed_time_ms << "\n";
	
	cout << "C = " << C << "\n";

	return 0;
}

/*******************

C=A+B

DEBUG
                 A+B               | sqrt(A^3+B^3)
LINEAR STATIC -  9ms               | 25ms
LINEAR DYNAMIC - 6ms    -33.3%     | 23ms  -0.08%
CYCLIC STATIC  - 17ms              | 15ms
CYCLIC DYNAMIC - 14ms   -17.64%    | 14ms  -0.06%

RELEASE

LINEAR STATIC -  6ms               | 8ms
LINEAR DYNAMIC - 5.8ms  +0.033%    | 7ms  -0.12%
CYCLIC STATIC  - 15.5ms            | 18ms
CYCLIC DYNAMIC - 14ms   0.0967%    | 16ms -0.11%


			LINEAR    CYCLIC        |  LINEAR  CYLIC
DEBUG        7.5ms    15.5ms  +53%  |   24ms   14.5ms -39%
RELEASE      5.9ms    14.75ms +59%  |   7.5ms  17ms   +126%
             -21%     -0.04%        |   -54%   -17%

*********************/