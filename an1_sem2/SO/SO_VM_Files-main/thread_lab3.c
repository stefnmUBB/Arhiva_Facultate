#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

int x=0;
pthread_mutex_t m;

void* f(void* a)
{ 
	for(int i=0;i<10000;i++)
	{
		pthread_mutex_lock(&m);	
		x++;
		pthread_mutex_unlock(&m);
		if(i%1000==0)
			printf("%i\n",*(int*)a);
	}
	free(a);
	return NULL;
}

int main(int argc, char** argv)
{
	pthread_t t[10];

	pthread_mutex_init(&m,NULL);

	for(int i=0;i<10;i++)
	{
		int* id = malloc(sizeof(int));
		*id=i+1;
		pthread_create(&t[i], NULL, f, id);
	}
	for(int i=0;i<10;i++)
	{
		pthread_join(t[i], NULL);
	}

	pthread_mutex_destroy(&m);

	printf("%i\n",x);
	return 0;

}
