#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <time.h>

int a2b[2], b2a[2];

int give_a_number() {
	return rand()%10+1;
}

void do_write(int pw, int pr, int number) {
	if(write(pw, &number, sizeof(int)) < 0)
	{
		printf("Couldn't write number to pipe\n");
		close(pw);
		close(pr);
		exit(-1);
	}
	if(pw==a2b[1])
		printf("A writes %i\n", number);
	else 
		printf("B writes %i\n", number);
}

int do_read(int pr, int pw) {
	int result;
	if(read(pr, &result, sizeof(int))<0)
	{
		printf("Couldn't read number from pipe\n");
		close(pw);
		close(pr);
		exit(-1);
	}
	if(pr==a2b[0]) 
		printf("B reads %i\n", result);	
	else 
		printf("A reads %i\n", result);
	return result;
}

void child_routine(int reads_from[2], int writes_to[2], int first) {
	close(reads_from[1]);
	close(writes_to[0]);

	if(first) {
		do_write(writes_to[1], reads_from[0], give_a_number());
	}	

	int x=0;
	while(x!=10)
	{
		x=do_read(reads_from[0], writes_to[1]);
		if(x!=10) {
		int y=give_a_number();
		do_write(writes_to[1], reads_from[0], y);
		if(y==10) break;
		}
	}
	printf("Child terminates\n");

	close(writes_to[1]);
	close(reads_from[0]);
}

int main() {
	srand(time(NULL));
	pipe(a2b);
	pipe(b2a);

	int a = fork();
	if(a<0) {
		printf("Failed to create child a\n");
		exit(-1);
	}

	if(a==0) {
		child_routine(b2a, a2b, 1);
	}
	else {
		int b=fork();
		if(b<0) {
			printf("Failed to create child b\n");
			exit(-1);
		}
		if(b==0) {
			child_routine(a2b, b2a, 0);
		}
		else {
			wait(0);
			wait(0);
			exit(0);
		}

	}
	
}

