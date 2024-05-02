/*
Un client trimite unui server un sir de caractere. Serverul va returna clientului numarul de caractere spatiu din sir.
*/
#include <sys/types.h>
#include <sys/socket.h>
#include <stdio.h>
#include <netinet/in.h>
#include <netinet/ip.h>
#include <string.h>

int main(){
    int s;
    struct sockaddr_in server, client;
    
    s = socket(AF_INET, SOCK_STREAM, 0);
    if(s < 0){
        printf("Eroare la crearea socket-ului server!\n");
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
    int l = sizeof(client);
    memset(&client, 0, sizeof(client));
    
    int nr_spatii=0;
	while(1)
	{		
		int c = accept(s, (struct sockaddr *) &client, &l);
		printf("S-a conectat clientul.\n");
		nr_spatii=0;
		char caracter = ' ';
		while(caracter != '\0'){
			recv(c, &caracter, sizeof(caracter), MSG_WAITALL);
			printf("%c", caracter);
			if(caracter == ' ')
				nr_spatii++;
		}
		printf("NRSPATII = %i\n",nr_spatii);
		nr_spatii = ntohs(nr_spatii);
		send(c, &nr_spatii, sizeof(nr_spatii), 0);
		close(c);
	}
	close(s);
}
 
