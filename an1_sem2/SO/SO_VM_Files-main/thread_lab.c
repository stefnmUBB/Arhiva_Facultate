#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

void* f(void* a)
{
    int *id=(int*)a;
	printf("%i\n",*id);
	free(a);
	return NULL;
}

int main(int argc, char** argv)
{
	pthread_t t[10];
	for(int i=0;i<10;i++)
	{
		int* ids = malloc(sizeof(int));
		*ids=i;
		pthread_create(&t[i], NULL, f, ids);
	}
	for(int i=0;i<10;i++)
	{
		pthread_join(t[i], NULL);
	}
	return 0;

}
