#include <unistd.h>
#include <sys/types.h>
#include <stdio.h>
#include <sys/wait.h>
#include <stdlib.h>

int main() {
	int i,j;
  	for(i=0;i<3;i++) {
		if(fork()==0) {
			printf("%d %d\n",getppid(),getpid());
			for(j=0;j<3;j++) {
				if(fork()==0) {
					printf("%d %d\n",getppid(),getpid());
				}
			}
			for(j=0;j<3;j++) {
				wait(0);
			}
			printf("\n");
			exit(0);
		}
	}
	for(i=0;i<3;i++)
		wait(0);
	return 0;
}
