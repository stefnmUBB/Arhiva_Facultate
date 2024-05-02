/*
Un client trimite unui server un sir de numere. Serverul va returna clientului suma numerelor primite.
*/
#include <sys/types.h>
#include <sys/socket.h>
#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>

int main(){
   int n;
   struct sockaddr_in server;
   int c;
   c = socket(AF_INET, SOCK_STREAM, 0);
   if(c < 0){
       printf("Eroare la crearea socket-ului client/n");
       return 1;
   }
   
   memset(&server, 0, sizeof(server));
   server.sin_port = htons(1234);
   server.sin_family = AF_INET;
   server.sin_addr.s_addr = inet_addr("172.30.117.26");
  
   if (connect(c, (struct sockaddr *) &server, sizeof(server)) < 0) {
     printf("Eroare la conectarea la server\n");
     return 1;
   }
   printf("n = ");
   scanf("%d", &n);
   int m = n;
   n = htons(n);
   send(c, &n, sizeof(n), 0);
   printf("%d", n);
   
   /*int ndublu;
   recv(c, &ndublu, sizeof(ndublu), MSG_WAITALL);
   ndublu = ntohs(ndublu);
   printf("Am primit dublul lui n, care este %d", ndublu);*/
   
   int nr;
   while(m>0){
       scanf("%d", &nr);
       nr = htons(nr);
       send(c, &nr, sizeof(nr), 0);
       m--;
   }
   printf("Suma nr este");
   int suma;
   recv(c, &suma, sizeof(suma), MSG_WAITALL);
   suma = ntohs(suma);
   printf("Suma nr este %d", suma);
   
   close(c);
   
}
