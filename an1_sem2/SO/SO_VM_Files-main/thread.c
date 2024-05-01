#include <stdio.h>
#include <pthread.h>
#include <stdlib.h>
#include <string.h>
#include <errno.h>

pthread_rwlock_t rwl;

typedef struct{
        int lit, cif, spec;
        char *str;
} data;

int tot_lit, tot_cif, tot_spec;

void *f(void *arg)
{
        data* dt = (data *)arg;
        int l = 0, d = 0, s = 0, i, len = strlen(dt->str);
    for(i = 0; i < len; i++){
        if((dt->str[i] >= 'a' && dt->str[i] <= 'z') || (dt->str[i] >= 'A' && dt->str[i] <= 'Z'))
                l++;
        else if(dt->str[i] >= '0' && dt->str[i] <= '9')
            d++;
        else
            s++;
     }
     if(l > 0){
        pthread_rwlock_wrlock(&rwl);
        dt->lit += l;
        pthread_rwlock_unlock(&rwl);
      }
     if (d > 0) {
        pthread_rwlock_wrlock(&rwl);
        dt->cif += d;
        pthread_rwlock_unlock(&rwl);
    }
    if (s > 0) {
        pthread_rwlock_wrlock(&rwl);
        dt->spec += s;
        pthread_rwlock_unlock(&rwl);
    }
	
    return NULL;
}

void *p()
{
        pthread_rwlock_rdlock(&rwl);
        printf("Lit:%d\nCif:%d\nSpec:%d\n", tot_lit, tot_cif, tot_spec);
        pthread_rwlock_unlock(&rwl);
        return NULL;
}

int main(int argc, char *argv[])
{
    pthread_t *thrds = malloc(sizeof(pthread_t) * (argc - 1));
    data *args = malloc(sizeof(data) * (argc-1));
    pthread_rwlock_init(&rwl, NULL);
    int i;
    for(i = 0; i < argc-1; i++)
    {
        args[i].lit = tot_lit;
        args[i].cif = tot_cif;
        args[i].spec = tot_spec;
        args[i].str = argv[i+1];
        if(0 > pthread_create(&thrds[i], NULL, f, (void *)&args[i]))
        { errno=1; perror("Error create thread 1");  }
        }
        pthread_t rez;
        if(0 > pthread_create(&rez,NULL, p, NULL))
        {       perror("Error create thread read"); };
        for(i = 0; i < argc - 1; i++)
                if(pthread_join(thrds[i], NULL)) {      perror("Error waiting for thread");}
        pthread_join(rez,NULL);
        pthread_rwlock_destroy(&rwl);
        free(thrds);
        free(args);
        return 0;
}
