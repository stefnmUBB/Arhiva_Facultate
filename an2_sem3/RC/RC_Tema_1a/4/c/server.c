#define SERVER
#include "common.h"

#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>
#include <arpa/inet.h>

void serve_client(int c)
{
	// deservirea clientului
	string a = recv_string(c);
	string b = recv_string(c);
	
	string r = create_string(a.len+b.len);
	
	int k=0, i=0, j=0;
	while(i<a.len && j<b.len)
	{
		if(a.buff[i]<b.buff[j])
		{
			r.buff[k++] = a.buff[i++];
		}
		else 
		{
			r.buff[k++] = b.buff[j++];
		}
	}
	while(i<a.len) r.buff[k++] = a.buff[i++];
	while(j<b.len) r.buff[k++] = b.buff[j++];
	
	send_string(c, r);	
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