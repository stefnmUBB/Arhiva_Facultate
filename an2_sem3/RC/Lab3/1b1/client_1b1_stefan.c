#include <sys/types.h>
#include <sys/socket.h>
#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <stdlib.h>
#include <string.h>

void send_string(int c, const char* str) {
	while(*str)
		send(c, str++, 1, 0);
	send(c, str, 1, 0);
}

char* recv_string(int c) {
	char* rec = (char*)malloc(256);
	do {
		recv(c, rec, 1, 0);
	} while(*rec != '\0');
	return rec;
}

int recv_int(int c) {
	int x;
	recv(c, &x, sizeof(x), 0);
	return ntohs(x);
}

int main(int argc, char** argv) {	
	
	int c;
	struct sockaddr_in server;
	
	c = socket(AF_INET, SOCK_STREAM, 0);
	if(c<0) {
		printf("Eroare la creare socket\n");
		return 1;
	}
	
	 memset(&server, 0, sizeof(server));
	 server.sin_port = htons(atoi(argv[2]));
	 server.sin_family = AF_INET;
	 server.sin_addr.s_addr = inet_addr(argv[1]);
	
	if(connect(c, (struct sockaddr*)&server, sizeof(server))<0) {
		printf("Eroare la conectare server\n");
		return 1;
	}	
		
	char* str = "Ana are mere\0";
	printf("%s\n",str);	
	send_string(c,str);
	
	int result = recv_int(c);
	
	printf("Numarul de spatii este %i\n", result);

	close(c);
}
