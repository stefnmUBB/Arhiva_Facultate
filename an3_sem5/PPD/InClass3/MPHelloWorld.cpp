#include<stdio.h>
#include<mpi.h> 
#include<stdlib.h> 
#include<iostream>

using namespace std;

void printVector(int v[], int n) {
	printf("%i %i %i %i %i %i %i %i %i %i\n", v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9]);	
}

int main()
{
	int myid, numprocs, namelen;
	char processor_name[MPI_MAX_PROCESSOR_NAME];

	const int N = 10;
	int A[N], B[N], C[N];
	int start=0, end=0;
	

	MPI_Status status;


	MPI_Init(NULL, NULL);
	MPI_Comm_rank(MPI_COMM_WORLD, &myid);  // get current process id
	MPI_Comm_size(MPI_COMM_WORLD, &numprocs);      // get number of processeser
	MPI_Get_processor_name(processor_name, &namelen);

	int auxLen = N / numprocs;
	int maxChunkSize = auxLen + 1;
	int* auxA = new int[maxChunkSize];
	int* auxB = new int[maxChunkSize];
	int* auxC = new int[maxChunkSize];

	if (myid == 0)
	{
		for (int i = 0; i < N; i++)
		{
			A[i] = rand() % 10;
			B[i] = rand() % 10;
		}
	}
	
	int* pstart = new int[numprocs];
	int* pcnt = new int[numprocs];
	int q = N / numprocs, r = N % numprocs;

	for (int p = 0; p < numprocs; p++)
	{
		end = start + q + (r > 0), r--;
		pstart[p] = start;
		pcnt[p] = end - start;
		start = end;

		//cout<<"cnt "<<p<<":"<<pstart
	}

	MPI_Scatterv(A, pcnt, pstart, MPI_INT, auxA, maxChunkSize, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Scatterv(B, pcnt, pstart, MPI_INT, auxB, maxChunkSize, MPI_INT, 0, MPI_COMM_WORLD);

	//MPI_Scatter(A, auxLen, MPI_INT, auxA, auxLen, MPI_INT, 0, MPI_COMM_WORLD);
	//MPI_Scatter(B, auxLen, MPI_INT, auxB, auxLen, MPI_INT, 0, MPI_COMM_WORLD);

	for (int i = 0; i < pcnt[myid]; i++) auxC[i] = auxA[i] + auxB[i];

	//MPI_Gather(auxC, auxLen, MPI_INT, C, auxLen, MPI_INT, 0, MPI_COMM_WORLD);

	MPI_Gatherv(auxC, pcnt[myid], MPI_INT, C, pcnt, pstart, MPI_INT, 0, MPI_COMM_WORLD);

	if (myid == 0)
	{
		printVector(A, N);
		printVector(B, N);
		printVector(C, N);
	}


	MPI_Finalize();
}

int main2(int argc, char* argv[])
{
	int myid, numprocs, namelen;
	char processor_name[MPI_MAX_PROCESSOR_NAME];

	const int N = 10;
	int A[N], B[N], C[N];
	int start, end;

	MPI_Status status;


	MPI_Init(NULL, NULL);
	MPI_Comm_rank(MPI_COMM_WORLD, &myid);  // get current process id
	MPI_Comm_size(MPI_COMM_WORLD, &numprocs);      // get number of processeser
	MPI_Get_processor_name(processor_name, &namelen);

	if (myid == 0)
	{		
		for (int i = 0; i < N; i++) 
		{
			A[i] = rand() % 10;
			B[i] = rand() % 10;
		}

		start = 0, end = 0;
		int q = N / (numprocs - 1), r = N % (numprocs - 1);

		for (int p = 1; p < numprocs; p++)
		{
			end = start + q + (r > 0), r--;			
			MPI_Send(&start, /*count*/ 1, MPI_INT, p, 0, MPI_COMM_WORLD);
			MPI_Send(&end, /*count*/ 1, MPI_INT, p, 0, MPI_COMM_WORLD);

			MPI_Send(A + start, (end - start), MPI_INT, p, 0, MPI_COMM_WORLD);
			MPI_Send(B + start, (end - start), MPI_INT, p, 0, MPI_COMM_WORLD);

			start = end;
		}

		for (int p = 1; p < numprocs; p++)
		{
			MPI_Recv(&start, /*count*/ 1, MPI_INT, /*source*/ p, /*tag*/ 0, MPI_COMM_WORLD, &status);
			MPI_Recv(&end, /*count*/ 1, MPI_INT, /*source*/ p, /*tag*/ 0, MPI_COMM_WORLD, &status);
			MPI_Recv(C + start, (end - start), MPI_INT, p, 0, MPI_COMM_WORLD, &status);
		}

		printVector(A, N);
		printVector(B, N);
		printVector(C, N);

	}
	else // copil
	{		
		MPI_Recv(&start, /*count*/ 1, MPI_INT, /*source*/ 0, /*tag*/ 0, MPI_COMM_WORLD, &status);
		MPI_Recv(&end, /*count*/ 1, MPI_INT, /*source*/ 0, /*tag*/ 0, MPI_COMM_WORLD, &status);
		cout << "Pentru p=" << myid << " s,e = " << start << ", " << end << "\n";

		MPI_Recv(A + start, (end - start), MPI_INT, 0, 0, MPI_COMM_WORLD, &status);
		MPI_Recv(B + start, (end - start), MPI_INT, 0, 0, MPI_COMM_WORLD, &status);

		for (int i = start; i < end; i++) C[i] = A[i] + B[i];


		MPI_Send(&start, /*count*/ 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
		MPI_Send(&end, /*count*/ 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
		MPI_Send(C + start, (end - start), MPI_INT, 0, 0, MPI_COMM_WORLD);

		//printVector(A, N);

	}	

	MPI_Finalize();
	return 0;
}