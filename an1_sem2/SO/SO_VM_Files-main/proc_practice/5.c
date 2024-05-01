#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <time.h>


int master;
int p[100][2];

int n;

int* get_read_fd(int i) {
	return p[i];
	if(i==0)
		return p[n-1];
	return p[i-1];
}

int* get_write_fd(int i) {
	if(i==n-1)
		return p[0];
	return p[i+1];
}

int is_bolz(int x){
	if(x%7==0) return 1;
	while(x>0) {
		if(x%10==7) return 1;
		x/=10;
	}
	return 0;
}

int fail_bolz() {
	return rand()%3==0;
}

int main(){
	srand(time(NULL));
	master = getpid();
	printf("n=");
	scanf("%i",&n);
	for(int i=0;i<n;i++) {			
		pipe(p[i]);
	}
	int x=1;
	write(get_write_fd(0)[1],&x, sizeof(int));
	printf("Parent 0 started the game: 1\n");
	int i;
	for(i=1;i<n;i++) {
		int f = fork();
		if(f<-1) {
			printf("ERROR!\n");
			return -1;
		}		
		if(f==0) { // child code
	        printf("Child %i joined the game\n",i);		
		}
		else 
		{
			break; // parent code, stop procreating
		}
	}
	i--;	

	for(int j=0;j<n;j++)
	{
		if(p[j]!=get_read_fd(i) && p[j]!=get_write_fd(i))
		{
			printf("Closing %i from %i\n",j,i);
			close(p[j][0]);
			close(p[j][1]);
		}
	}
	close(get_read_fd(i)[1]);
	close(get_write_fd(i)[0]);
	
	sleep(1); // wait other children to join
	while(1) {
		printf("-- %i running\n",i);
		int nr;
		printf("Process %i reads...\n",i);
		int rd =read(get_read_fd(i)[0], &nr, sizeof(int));
		if(rd==0) {
			printf("Process %i failed to read\n",i);
			break;
		}
		if(nr==-1) { // previous process vanished
			write(get_write_fd(i)[1], &nr, sizeof(int));
			close(get_write_fd(i)[1]);
            close(get_read_fd(i)[0]);
			break;
		}
		nr++;		
		printf("Process %i writes...\n",i);
		int wr=write(get_write_fd(i)[1], &nr, sizeof(int));
		if(wr==0) {
			printf("Process %i failed to write\n",i);
			break;
		}
		if(!is_bolz(nr)) {
			printf("Process %i said %i\n",i,nr);
		}
		else {
			if(fail_bolz()){
				printf("Process %i forgot to say BOLZ\n",i);
				nr=-1;
				write(get_write_fd(i)[1],&nr, sizeof(int));
				close(get_write_fd(i)[1]);
				close(get_read_fd(i)[0]);
				break;
			}
			printf("Process %i said BOLZ\n",i);
		}
		sleep(1);
	}
	printf("Process %i terminating\n",i);
//	close(get_read_fd(i)[0]);
//	printf("Closed R%i\n",i);
//	close(get_write_fd(i)[1]);
//	printf("Close W%i\n",(i+1)%n);
//	close(get_read_fd(i)[0]);
  //  printf("Closed R%i\n",i);

	//close(p[i][1]);
	wait(0);
	printf("Process %i terminated\n",i);
	exit(0);
}




















