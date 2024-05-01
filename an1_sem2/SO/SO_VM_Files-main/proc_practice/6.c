#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <time.h>

int main(int argc, char** argv)
{
	int p2c[2], c2p[2];
	srand(time(NULL));
	int n=atoi(argv[1]);
	int* vec = malloc(n*sizeof(int));

	for(int i=0;i<n;i++)
		vec[i]=rand()%100;

	pipe(p2c);
	pipe(c2p);

	int f=fork();
	if(f<0) {
		printf("Error!");
		exit(1);
	}
	if(f==0) {
		close(p2c[1]);
		close(c2p[0]);

		int cnt=0, sum=0;
		read(p2c[0], &cnt, sizeof(int));

		for(int q=0,i=0;i<cnt;i++) {
			read(p2c[0], &q, sizeof(int));
			printf("Child read %i\n",q);
			sum+=q;
		}
		double avg = 1.0 * sum / cnt;

		printf("AVG  %f\n",avg);

		close(p2c[0]);

		write(c2p[1], &avg, sizeof(double));

		close(c2p[1]);

		exit(0);
	}
	else {
		close(c2p[1]);
		close(p2c[0]);	

		write(p2c[1], &n, sizeof(int));

		for(int i=0;i<n;i++)
			write(p2c[1], &vec[i], sizeof(int));

		close(p2c[1]);

		double avg=-1;
		read(c2p[0], &avg, sizeof(double));

		close(c2p[0]);
	
		wait(0);

		printf("Average = %f\n", avg);
		free(vec);
		exit(0);
	}

	return 0;
}
