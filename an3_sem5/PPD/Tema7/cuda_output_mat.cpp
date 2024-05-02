//Proiect pe echipe: Neacsu Stefan, Condor Andrada
//17.01.2024

%%cu
#include <cstdio>
#include <iostream>
#include <fstream>
#include <chrono>

#include <cuda_runtime_api.h>
#include <cuda.h>
#include <cooperative_groups.h>

using namespace std;

// __device__ because it is called from the GPU
__device__ int clamp(int x, int a, int b) { return x<=a ? a : x>=b ? b: x; }

__global__ void convolution(int* matrix, int* kernel, int n, int k, int* rezultat) // kernel -> used by every thread
{
	int border[9];
	int thread_id = blockIdx.x*blockDim.x + threadIdx.x;
    if(thread_id>=n*n) return;
	int i = thread_id / n;
	int j = thread_id % n;
	for(int ii=0;ii<3;ii++)
          for(int jj=0;jj<3;jj++)
            border[3*ii+jj] = matrix[n*clamp(i+ii-1, 0, n-1) + clamp(j+jj-1, 0, n-1)];
	int s = 0;
	for(int i=0;i<9;i++) s+=kernel[i]*border[i];
  rezultat[n*i+j] = s;
}

int main()
{
    int n = 10000;
    int k = 3;

    int* matrix = new int[n*n];
    int* rezultat = new int[n*n];
    int kernel[9] = { 2, 1, 1, 1, 1, 0, 0, 1, 2};

    auto t_read_start = std::chrono::high_resolution_clock::now();

    ifstream f("/input_10000.txt");

    for(int i=0;i<n;i++)
    {
        for(int j=0;j<n;j++) f>>matrix[i*n+j];
    }

    f.close();

    auto t_read_end = std::chrono::high_resolution_clock::now();
    double read_time = std::chrono::duration<double, std::milli>(t_read_end - t_read_start).count();

  	int* cuda_matrix;
    int* cuda_kernel;
	int* cuda_rezultat;

    // masuram timpul
    cudaEvent_t start, end;

    // Allocate memory on the GPU
    cudaMalloc(&cuda_matrix, n*n*sizeof(int));
  	cudaMalloc(&cuda_kernel, k*k*sizeof(int));
  	cudaMalloc(&cuda_rezultat, n*n*sizeof(int));

    // Copy vectors to the device
    cudaMemcpy(cuda_matrix, matrix, n*n*sizeof(int), cudaMemcpyHostToDevice);
  	cudaMemcpy(cuda_kernel, kernel, k*k*sizeof(int), cudaMemcpyHostToDevice);

  	int blockSize, gridSize;
    blockSize = 1024;
    gridSize = n*n / blockSize+1;

    cudaEventCreate(&start);
    cudaEventCreate(&end);
    cudaEventRecord(start);

    convolution<<<gridSize, blockSize>>>(cuda_matrix, cuda_kernel, n, k, cuda_rezultat);

  	// Copy matrix back to host
    cudaMemcpy(rezultat, cuda_rezultat, n*n*sizeof(int), cudaMemcpyDeviceToHost);
    cudaMemcpy(kernel, cuda_kernel, k*k*sizeof(int), cudaMemcpyDeviceToHost);

    cudaEventRecord(end);
    cudaEventSynchronize(end);

    float time = 0;
    cudaEventElapsedTime(&time, start, end);

    auto t_write_start = std::chrono::high_resolution_clock::now();

    ofstream g("/output.txt");
    for(int i=0;i<n;i++)
    {
        for(int j=0;j<n;j++)
        {
            g<<rezultat[i*n+j]<<" ";
        }
        g<<"\n";
    }
    g.close();

    auto t_write_end = std::chrono::high_resolution_clock::now();

    double write_time = std::chrono::duration<double, std::milli>(t_write_end - t_write_start).count();

		// Release device memory
    cudaFree(cuda_matrix);
  	cudaFree(cuda_kernel);

    // Release host memory
    delete[] matrix;
    delete[] rezultat;

    cout << "Read time : "<<read_time<<"ms \n";
    cout << "CUDA time : "<<time<<"ms \n";
    cout << "Write time : "<<write_time<<"ms \n";
    cout << "Total time : "<<read_time+time+write_time<<"ms \n";

    return 0;
}

