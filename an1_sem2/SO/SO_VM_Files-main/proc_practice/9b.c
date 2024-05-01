#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

int main() {
	int a2b, b2a;
	a2b=open("a2b", O_RDONLY);
	b2a=open("b2a", O_WRONLY);

	char res[10024];
	int len;
	
	read(a2b, &len, sizeof(int));
	read(a2b, res, len);

	printf("%s\n",res);

	

	close(a2b);
	close(b2a);
	return 0;
}
