#define SERVER
#include "common.h"

#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>
#include <arpa/inet.h>

void serve_client(int c)
{
	string str = recv_string(c);
	int i = recv_int(c);
	int l = recv_int(c);
	
	string res = create_string(l);	
	for(int j=0;j<l;j++)
		res.buff[j] = str.buff[i+j];
	res.buff[l]=0;
	send_string(c, res);
}
 
int main() {
  int s;
  struct sockaddr_in server, client;
  int c, l;
  
  s = socket(AF_INET, SOCK_STREAM, 0);
  if (s < 0) {
    printf("Eroare la crearea socketului server\n");
    return 1;
  }
  
  memset(&server, 0, sizeof(server));
  server.sin_port = htons(1234);
  server.sin_family = AF_INET;
  server.sin_addr.s_addr = INADDR_ANY;
  
  if (bind(s, (struct sockaddr *) &server, sizeof(server)) < 0) {
    printf("Eroare la bind\n");
    return 1;
  }   
 
  listen(s, 5);
  
  l = sizeof(client);
  memset(&client, 0, sizeof(client));  
  
  while (1) {	
    c = accept(s, (struct sockaddr *) &client, &l);
    printf("S-a conectat un client.\n");
	
    serve_client(c);
	
    close(c);
    // sfarsitul deservirii clientului;
  }
}