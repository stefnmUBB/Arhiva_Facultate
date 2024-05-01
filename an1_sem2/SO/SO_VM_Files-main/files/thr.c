#include <stdio.h>
#include <pthread.h>

int n=0;
pthread_mutex_t m[3];

pthread_cond_t cond[2];

void* f(void*p) {	
	int id = (int)p;
	pthread_mutex_lock(&m[id]);	
	
	n+=id;

	if(id == 0)
	{
		pthread_mutex_lock(&m[0]);
		n = 1;
	}else{
		if( id == 1)pthread_mutex_unlock(&m[0]);
		pthread_mutex_lock(&m[id]);
		n = 3;
	}

	printf("%d ",n);

	pthread_mutex_unlock(&m[(id+1)%3]);
	return NULL;
}


int main()
{
	pthread_t t[3];
	for(int i=0;i<3;i++) pthread_mutex_init(&m[i],NULL);
	
	pthread_cond_init(&cond[0], NULL);
	pthread_cond_init(&cond[1], NULL);

	for(int i=0;i<3;i++) {
		pthread_create(&t[i], NULL, f, (void*)i);
	}
	for(int i=0;i<3;i++) pthread_join(t[i],NULL);
	for(int i=0;i<3;i++) pthread_mutex_destroy(&m[i]);
	
	pthread_cond_destroy(&cond[0]);
	pthread_cond_destroy(&cond[1]);
	printf("\n");
	return 0;
}
