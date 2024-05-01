#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <time.h>


int main(int argc, char** argv)
{
	clock_t start = clock();
	if(fork()==0) {
		execvp(argv[1], argv+1);
		return 1;
	}
	else{
		int exitcode=0;
		wait(&exitcode);
		clock_t end = clock();
		if(exitcode!=0)	{
			printf("Command returned error status\n");
		}
		printf("Time: %f seconds\n",((double)(end-start))/CLOCKS_PER_SEC);
		
	}
	return 0;
}
