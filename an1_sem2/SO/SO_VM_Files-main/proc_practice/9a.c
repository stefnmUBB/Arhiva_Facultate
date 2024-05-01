#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <string.h>

int main() {
	mkfifo("a2b", 0600);
	mkfifo("b2a", 0600);
    int a2b, b2a;
	a2b = open("a2b", O_WRONLY);
    b2a = open("b2a", O_RDONLY);

	char* cmd=malloc(101);
    char res[10024];

	size_t clen=101;

	getline(&cmd,&clen,stdin);
	printf("HERE\n");	

	printf("%s\n",cmd);

    FILE* fp = popen(cmd, "r");
	fread(res, 1, 10023, fp);
	fclose(fp);

	printf("%s\n",res);

	int len = strlen(res)+1;
	write(a2b, &len, sizeof(int));
	write(a2b, res, len);
	

    close(a2b);
    close(b2a);

	unlink("a2b");
	unlink("b2a");
	return 0;
}

