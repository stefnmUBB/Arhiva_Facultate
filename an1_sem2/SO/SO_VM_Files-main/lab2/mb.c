#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>

int main(int argc,char** argv)
{
	int** m;
	int rows, cols, i, j, fd;
	FILE* f;

	f = fopen(argv[1],"r");
	fscanf(f,"%d %d ", &rows, &cols);
	
	m = (int**)malloc(rows*sizeof(int*));
	for(i=0;i<rows;i++)
	{
		m[i]=(int*)malloc(cols*sizeof(int));
		for(j=0;j<cols;j++)
		{
			fscanf(f,"%d",&m[i][j]);
		}
	}
	fd = open(argv[2], O_CREAT | O_WRONLY, 00600);
	write(fd,&rows,sizeof(int));
	write(fd,&cols,sizeof(int));
	for(i=0;i<rows;i++)
	{
		for(j=0;j<cols;j++)
		{
			write(fd,&m[i][j],sizeof(int));
		}
	}
	close(fd);
	
	for(i=0;i<rows;i++)
		free(m[i]);

	free(m);
	return 0;
}
