#define CLIENT
#include "common.h"

#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>
#include <arpa/inet.h>


void run_client(int c)
{	
	string a = make_string("aacdgllox");	
	string b = make_string("bcdhjmwxx");
	print_string(a); printf("\n");
	print_string(b); printf("\n");
	send_string(c, a);	
	send_string(c, b);
	
	string r = recv_string(c);
	print_string(r);
	printf("\n");
}
 
int main() {
  int c;
  struct sockaddr_in server;  
  
  c = socket(AF_INET, SOCK_STREAM, 0);
  if (c < 0) {
    printf("Eroare la crearea socketului client\n");
    return 1;
  }  
  
  memset(&server, 0, sizeof(server));
  server.sin_port = htons(1234);
  server.sin_family = AF_INET;
  server.sin_addr.s_addr = inet_addr("127.0.0.1");
  
  if (connect(c, (struct sockaddr *) &server, sizeof(server)) < 0) {
    printf("Eroare la conectarea la server\n");
    return 1;
  }
 
  run_client(c);
  close(c);
}