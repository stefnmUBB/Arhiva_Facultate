#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>


int main()
{
	int r, n, k=10;
	r = open("xyz", O_WRONLY);
	n = write(r, &k, sizeof(int));
	printf("%i\n",n);

	/*char* s[3]={"A","B","C"};
	for(int i=0;i<3;i++) {
		if(fork()==0) {
			execl("/bin/echo","bin/echo",s[i],NULL);
		}
	}
	sleep(1);*/
	return 0;
}

