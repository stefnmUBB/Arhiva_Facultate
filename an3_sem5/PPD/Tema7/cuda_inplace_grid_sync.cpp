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

__global__ void convolution(int* matrix, int* kernel, int n, int k, int blocks_count, int threads_per_block) // kernel -> used by every thread
{
    const int max_elems_per_thread = 3000;
    int line[max_elems_per_thread];
    int col[max_elems_per_thread];
    int border[max_elems_per_thread][9];

    int thread_id = blockIdx.x*threads_per_block+threadIdx.x;

    int elems_count = n*n/(blocks_count*threads_per_block)+1;
    int elem_start = thread_id*elems_count;

    for(int i=0;i<elems_count;i++)
    {
        if(elem_start+i>=n*n) break;
        line[i] = (elem_start+i)/n;
        col[i] = (elem_start+i)%n;
        for(int ii=0;ii<3;ii++)
          for(int jj=0;jj<3;jj++)
            border[i][3*ii+jj] = matrix[n*clamp(line[i]+ii-1, 0, n-1) + clamp(col[i]+jj-1, 0, n-1)];
    }

    cooperative_groups::grid_group grid = cooperative_groups::this_grid();
    grid.sync();

    for(int k=0;k<elems_count;k++)
    {
        if(elem_start+k>=n*n) break;
        int sum = 0;
        for(int i=0;i<9;i++)
          sum += border[k][i] * kernel[i];

        matrix[n*line[k]+col[k]] = sum;
    }
}

int main()
{
    int n = 10000;
    int k = 3;

    int* matrix = new int[n*n];
    //int* kernel = new int[k*k];
    int kernel[9] = { 2, 1, 1, 1, 1, 0, 0, 1, 2};

    auto t_read_start = std::chrono::high_resolution_clock::now();


    //ifstream f("/test_case_1.txt");
    ifstream f("/input_10000.txt");

    for(int i=0;i<n;i++)
    {
        for(int j=0;j<n;j++) f>>matrix[i*n+j];
    }

  	/*for(int i=0;i<k;i++)
    {
        for(int j=0;j<k;j++) f>>kernel[i*k+j];
    }*/

    f.close();

    auto t_read_end = std::chrono::high_resolution_clock::now();

    for(int i=0;i<k;i++)
    {
        for(int j=0;j<k;j++)
        {
            cout<<kernel[i*k+j]<<" ";
        }
        cout<<"\n";
    }


    double read_time = std::chrono::duration<double, std::milli>(t_read_end - t_read_start).count();

  	int* cuda_matrix;
    int* cuda_kernel;

    // masuram timpul
    cudaEvent_t start, end;

    // Allocate memory on the GPU
    cudaMalloc(&cuda_matrix, n*n*sizeof(int));
  	cudaMalloc(&cuda_kernel, k*k*sizeof(int));

    // Copy vectors to the device
    cudaMemcpy(cuda_matrix, matrix, n*n*sizeof(int), cudaMemcpyHostToDevice);
  	cudaMemcpy(cuda_kernel, kernel, k*k*sizeof(int), cudaMemcpyHostToDevice);

  	int blockSize, gridSize;
    blockSize = n;
    gridSize = n; // (n*n) /n

    cudaEventCreate(&start);
    cudaEventCreate(&end);
    cudaEventRecord(start);

    //convolution<<<gridSize, blockSize>>>(cuda_matrix, cuda_kernel, n, k);

    int dev = 0;
    cudaDeviceProp deviceProp;
    cudaGetDeviceProperties(&deviceProp, dev);
    int blocks_count = deviceProp.multiProcessorCount;
    int threads_per_block = 1024;

    void* args[] = { &cuda_matrix, &cuda_kernel, &n, &k, &blocks_count, &threads_per_block};

    cout<<"MP count ="<<deviceProp.multiProcessorCount<<"\n";

    cudaLaunchCooperativeKernel((void*)convolution, blocks_count, threads_per_block, (void**)args);

  	// Copy matrix back to host
    cudaMemcpy(matrix, cuda_matrix, n*n*sizeof(int), cudaMemcpyDeviceToHost);
    cudaMemcpy(kernel, cuda_kernel, k*k*sizeof(int), cudaMemcpyDeviceToHost);

    cudaEventRecord(end);
    cudaEventSynchronize(end);

    cout<<"Count = "<<kernel[0]<<"\n";

    float time = 0;
    cudaEventElapsedTime(&time, start, end);

    auto t_write_start = std::chrono::high_resolution_clock::now();

    ofstream g("/output.txt");
    for(int i=0;i<n;i++)
    {
        for(int j=0;j<n;j++)
        {
            g<<matrix[i*n+j]<<" ";
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
    //delete[] kernel;

    cout << "Read time : "<<read_time<<"ms \n";
    cout << "CUDA time : "<<time<<"ms \n";
    cout << "Write time : "<<write_time<<"ms \n";
    cout << "Total time : "<<read_time+time+write_time<<"ms \n";

    return 0;
}

