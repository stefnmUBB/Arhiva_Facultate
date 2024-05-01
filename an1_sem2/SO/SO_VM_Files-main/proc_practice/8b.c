#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

int main() {
	int a2b, b2a;

	a2b = open("a2b", O_RDONLY);
	b2a = open("b2a", O_WRONLY);

	int n;
	read(a2b, &n, sizeof(int));

	printf("N=%i\n",n);

	char** args = malloc(n*sizeof(char*));
	
	int len;
	for(int i=0;i<n;i++) {
		read(a2b, &len, sizeof(int));
		args[i] = malloc(len*sizeof(char));
		read(a2b, args[i], len);
		printf("%s\n",args[i]);
		for(int j=0;j<len-1;j++) {
			if('a'<=args[i][j]&&args[i][j]<='z') {
				args[i][j]+='A'-'a';
			}
		}
		write(b2a, args[i], len);
	}

	for(int i=0;i<n;i++) {
		free(args[i]);
	}
	free(args);
	
		
	
	close(a2b);
	close(b2a);
	return 0;
}
