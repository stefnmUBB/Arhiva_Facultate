#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

int n = 0;
int Index = 0;

pthread_mutex_t m;
pthread_rwlock_t rw_lock;

int xsleep(int ms);

void* f(void* a) {
	int* id=(int*)a;
	printf("%i\n", *id);
	free(a);
	while(1)
	{

		pthread_rwlock_rdlock(&rw_lock);
		if(Index%2==0) {
			pthread_mutex_lock(&m);
			n++;
			pthread_mutex_unlock(&m);
		}
		pthread_rwlock_unlock(&rw_lock);
		xsleep(1000);
	}
	return NULL;
}

#include <time.h>

int xsleep(int ms) {
	struct timespec req={
		(int)(ms/1000),
		(ms%1000)*1000000
	};
	return nanosleep(&req, NULL);
	
}

void* update(void* a) {
	int* y=(int*)a;
	printf("This\n");
	while(1) {
		xsleep(2000);
		pthread_rwlock_wrlock(&rw_lock);
		printf("Thread %i locks write: %i\n",*y,Index);
		Index++;
		pthread_rwlock_unlock(&rw_lock);
	}
}




int main(int argc, char** argv) {
	pthread_t t[10];
	pthread_mutex_init(&m,NULL);
	pthread_rwlock_init(&rw_lock, NULL);
	
	for(int i=0;i<10;i++) {
		int* ids = malloc(sizeof(int));
		*ids = i;
		pthread_create(&t[i], NULL, f, ids);
	}

	pthread_t t1, t2;
	int var1=11, var2=22;
    pthread_create(&t1, NULL, update, &var1);
	pthread_create(&t2, NULL, update, &var2);

	for(int i=0;i<10;i++)
		pthread_join(t[i], NULL);

	pthread_join(t1, NULL);
	pthread_join(t2, NULL);
	pthread_mutex_destroy(&m);
	pthread_rwlock_destroy(&rw_lock);
	printf("%i\n",n);
	return 0;
}


