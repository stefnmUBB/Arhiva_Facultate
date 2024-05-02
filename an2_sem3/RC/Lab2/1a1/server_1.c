#include <sys/types.h>
#include <sys/socket.h>
#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>
#include <stdlib.h>

int recv_int(int c) {
	int x;
	recv(c, &x, sizeof(x), MSG_WAITALL);
	return ntohs(x);
}

void send_int(int c, int x) {
	printf("Sending... %i\n", x);
	int* a= malloc(sizeof(int));
	*a = htons(x);	
	send(c, a, sizeof(int), 0);
	free(a);
}

void serve(int c) {
	int n = recv_int(c);
	printf("n = %d\n", n);
	int sum = 0;
	for(int i=0;i<n;i++) {
		int nr = recv_int(c);
		printf("nr = %d\n", nr);
		sum += nr;
	}
	send_int(c, sum);
}

int init_socket(int* s) {
	*s = socket(AF_INET, SOCK_STREAM, 0);
	if(s<0) {
		printf("Eroare la creare server");
		return 0;
	}
	return 1;
}

void init_server(struct sockaddr_in* server, int port) {
	memset(server, 0, sizeof(server));
	server->sin_port = htons(port);
	server->sin_family = AF_INET;
	server->sin_addr.s_addr = INADDR_ANY;
}


int main() 
{
	int s, c, l;
	struct sockaddr_in server, client;	
	
	if(!init_socket(&s)) {
		return 1;
	}
	
	init_server(&server, 1234);
	
	if (bind(s, (struct sockaddr *) &server, sizeof(server)) < 0) {
		printf("Eroare la bind\n");
		return 1;
	}
	
	listen(s, 5);
	
	l=sizeof(client);
	memset(&client, 0, l);
	
	while(1) {
		c = accept(s, (struct sockaddr*) &client, &l);
		printf("S-a conectat clientul");
		serve(c);
		close(c);
	}		
  
  
}