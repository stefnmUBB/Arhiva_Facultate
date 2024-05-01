#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include<unistd.h>
#include <sys/wait.h>
#include <time.h>

int main(int argc, char** argv)
{
        int nr, nrb, c2p[2], p2c[2];
        int res1 = pipe(c2p);
        int res2 = pipe(p2c);
        if(res1 == -1)
        {
                perror("pipe(c2p)");
                exit(EXIT_FAILURE);
        }
        if(res2 == -1)
        {
                perror("pipe(p2c)");
                exit(EXIT_FAILURE);
        }
        int pid = fork();
        if(pid == -1)
        {
                perror("fork()");
                exit(EXIT_FAILURE);
        }

        if(pid == 0)
        {//copilul incearca sa ghiceasca
                close(c2p[0]);
                close(p2c[1]);
				sleep(1);
                srand(time(NULL));
                while(1)
                {
                        char sign;
                        nrb = rand()% 100;
                        printf("%d", nrb);
                        write(c2p[1], &nrb, sizeof(int));
                        read(p2c[0], &sign, sizeof(char));
                        if(sign == '=')
                                break;
                        printf("%c\n", sign);
                }
                close(c2p[1]);
                close(p2c[0]);
                exit(EXIT_SUCCESS);
        }
        close(c2p[1]);
        close(p2c[0]);
        srand(time(NULL));
        nr = rand()%100;
        while(1)
        {
                read(c2p[0], &nrb, sizeof(int));
                char sign;
                if(nrb == nr)
                {
                        sign = '=';
                        write(p2c[1], &sign, sizeof(char));
                        break;
                }
                if(nrb < nr)
                        sign = '<';
                if(nrb > nr)
                        sign = '>';
                write(p2c[1], &sign, sizeof(char));
        }
        printf("nr %d, nrb %d", nr, nrb);
        int status;
        wait(&status);
        close(c2p[0]);
        close(p2c[1]);
        return 0;
}
