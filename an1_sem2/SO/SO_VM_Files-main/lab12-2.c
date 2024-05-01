#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <semaphore.h>
#include <time.h>
#include <unistd.h>
sem_t sem;

void* f(void* a)
{
    int *id=(int*)a;
	sem_wait(&sem);
	printf("%i\n",*id);
	sleep(1);
	sem_post(&sem);
	free(a);
	return NULL;
}

int main(int argc, char** argv)
{
	pthread_t t[10];
	sem_init(&sem, 0, 3);
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
	sem_destroy(&sem);
	return 0;

}
