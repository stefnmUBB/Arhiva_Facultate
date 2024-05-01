#include <stdio.h>
#include <stdlib.h>

int main(int argc, char** argv)
{
    int **m;
    int rows, cols, i, j;
    FILE* f;

    f  = fopen(argv[1],"r");
    fscanf(f,"%d %d",&rows, &cols);
    m = (int**)malloc(rows*sizeof(int*));
    for(i=0;i<rows;i++)
    {
        m[i] = (int*)malloc(cols*sizeof(int));
        for(j=0;j<cols;j++)
        {
            fscanf(f,"%d", &m[i][j]);
        }
    }
    fclose(f);
    for(i=0;i<rows;i++)
    {
        for(j=0;j<cols;j++)
        {
            printf("%3d ",m[i][j]);
        }
        printf("\n");
    }

	for(i=0;i<rows;i++)
		free(m[i]);
	free(m);
	return 0;
}
