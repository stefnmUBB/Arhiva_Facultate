#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <string.h>

int main(int argc, char** argv) {
    int a2b, b2a;

    a2b = open("a2b", O_WRONLY);
	b2a = open("b2a", O_RDONLY);


	int n=argc-1;
	write(a2b, &n, sizeof(int));
	
	int totlen = 0;
	for(int i=1;i<=n;i++) {
		int len=strlen(argv[i])+1;
		totlen+=len-1;
		write(a2b, &len, sizeof(int));
		write(a2b, argv[i], len);
	}

	totlen+=1; // '\0'
	char* result=malloc(totlen);
	result[0]='\0';
	for(int i=1;i<=n;i++) {
		int len=strlen(argv[i])+1;
		read(b2a, argv[i], len);
		printf("%s\n",argv[i]);
		strcat(result, argv[i]);
	}
	printf("%s\n", result);
	free(result);
	
	close(a2b);
	close(b2a);
	return 0;
}
