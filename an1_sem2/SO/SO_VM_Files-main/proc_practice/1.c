#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

int main() {
	int n;
	int ch[101];
	printf("n (1..100) =");
	scanf("%i", &n);
	if(n<1 || n>100) 
	{
		printf("Wrong number of processes.");
		return 1;
	}
	for(int i=1;i<=n;i++)
	{
		ch[i] = fork();
		if(ch[i]<0)
		{
			printf("ERROR!\n");
			return 2;
		}
		if(ch[i]==0)
		{
			// child code
			printf("Child %03i, PID = %i, PPID = %i\n",
				i,
				getpid(),	
				getppid()
			);
			exit(0);
		}
	}
	for(int i=1;i<=n;i++)
		wait(0);
	printf("Parent PID = %i\nChildren: ", getpid());
	for(int i=1;i<=n;i++) {
		printf("%i ", ch[i]);
	}
	printf("\n");	

	return 0;
}
