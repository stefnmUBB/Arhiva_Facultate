#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

pthread_t threads[10];
pthread_mutex_t mut;

void * threadFunction(void * var) {
    int *y = (int *)var;
    int threadVar = *y;
    
    printf("THR. %d:I'm trying to block access!\n", threadVar);
    pthread_mutex_lock(&mut);
    printf("I'm a thread number %d and I'm starting to sleep...\n", threadVar);
    sleep(3*threadVar);
    printf("THR. %d: Unlock access\n", threadVar);
    pthread_mutex_unlock(&mut);
   free(y);
	return 0;
}

int main() {

    pthread_mutex_init(&mut, NULL);
    int i;
    for(i = 1; i <  5; i++) {
        int * c;
        c = malloc(sizeof(int));
        *c = i;
        pthread_create(&threads[i], NULL, threadFunction, c);
    }

    for (i = 1; i <  5; i++) {
        pthread_join(threads[i], NULL);

    }
    pthread_mutex_destroy(&mut);
	return 1;
}

