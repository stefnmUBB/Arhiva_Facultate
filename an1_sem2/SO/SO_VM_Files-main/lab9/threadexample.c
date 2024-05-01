#include <stdlib.h>
#include <stdio.h>
#include <sys/types.h>
#include <unistd.h>
#include <pthread.h>

void* hardWorkFunction(void* p) {
    printf("I'm a thread and I am going to do some hard work now!\n");
    sleep(3);
    printf("Finished work!\n");
}
     
int main() {
    pthread_t t[10];
    int i;
     
    for(i=0; i<10; i++) {
	    pthread_create(&t[i], NULL, hardWorkFunction, NULL);
    }
    for(i=0; i<10; i++) {
        pthread_join(t[i], NULL);
    }

    return 0;
}

