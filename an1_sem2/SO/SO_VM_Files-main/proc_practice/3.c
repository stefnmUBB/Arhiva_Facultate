#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <signal.h>

int child;

void ph(int sg) {
	printf("Parent kills itself\n");
	kill(child, SIGUSR1);
	wait(0);
	exit(0);
}

void ch(int sg) {
	printf("Child kills itself\n");
	exit(0);
}

void zh(int sg) {
	printf("Parent waits for child to kill itself\n");
	wait(0);
	exit(0);
}

int main()
{
	child = fork();
	if(child<0) {
		printf("ERROR\n");
		return -1;
	}
	if(child==0) { // child code
		signal(SIGUSR1, ch);
		while(1) {
			printf("Child %i working...\n", getpid());
			sleep(2);
		}
	}
	else {
		signal(SIGUSR1, ph);
		signal(SIGCHLD, zh);
		while(1) {
			printf("Parent %i working...\n", getpid());
			sleep(2);
		}
	}
}
