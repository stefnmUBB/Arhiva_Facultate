#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

void create(int n) {
	if(n==0) return;
	int p = fork();
	if(p<0) {
		printf("ERROR!\n");
		exit(1);
	}
	if(p==0) { // child code
		printf("Child %i created by %i\n",getpid(), getppid());
		create(n-1);
		exit(0);
	}
	wait(0);
}

int main() {
	create(1000);
	return 0;
}
