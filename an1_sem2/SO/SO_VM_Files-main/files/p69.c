#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

int main()
{
	for(int i=0;i<4;i++) 
		if(fork() && i%2==1)
			break;
	printf("x\n");
	sleep(1);
	return 0;
}

