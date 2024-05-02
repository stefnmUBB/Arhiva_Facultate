#pragma once


#include <stdlib.h>
#include <stdio.h>
#include <string.h>

/// string wrapper around char*
typedef struct 
{
	int len;
	char* buff;
} string;

/// creates string struct from const char*
static inline string make_string(const char* msg)
{	
	string str;
	str.len = strlen(msg);
	str.buff = malloc(str.len+1);
	strcpy(str.buff, msg);
	return str;
}

/// creates string struct of given length buffer
static inline string create_string(int len)
{
	string str;
	str.len = len;
	str.buff = malloc(str.len+1);
	str.buff[0]=0;
	return str;
}

/// frees string buffer
static inline void destroy_string(string s)
{
	free(s.buff);	
}


#ifdef DEBUG
#define dbg(...) printf(__VA_ARGS__)
#else
#define dbg(...)
#endif


/*****************************************************************

	send/receive methods for primitives

******************************************************************/

#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>


#ifdef SERVER
	#define _recv(A, B, C) recv(A, B, C, MSG_WAITALL);
#elif defined(CLIENT)
	#define _recv(A, B, C) recv(A, B, C, 0);
#endif

static inline void send_int(int c, int x)
{
	x = htonl(x); 	
	send(c, &x, sizeof(x), 0);	
	dbg("Am trimis %i.\n",ntohl(x));
}

static inline int recv_int(int c)
{
	int x;
	_recv(c, &x, sizeof(x));
	x=ntohl(x);
	dbg("Am primit %i.\n",x);
	return x;
}

static inline int send_char(int c, char ch)
{	
	send(c, &ch, sizeof(ch), 0);
	dbg("Am trimis chr %i.\n", ch);
}

static inline void send_chars(int c, char* buff, int len)
{
	send(c, buff, sizeof(char)*len, 0);
	dbg("Am trimis %i octeti.\n", len);
}

static inline void recv_chars(int c, char* buff, int len)
{
	_recv(c, buff, sizeof(char)*len);
	dbg("Am primit %i octeti.\n", len);
}

static inline void send_string(int c, string s)
{	
	send_int(c, s.len);
	send_chars(c, s.buff, s.len + 1);
	dbg("Am trimis sirul \"%s\" de lungime %i.\n", s.buff, s.len);
}

static inline string recv_string(int c)
{		
	int len = recv_int(c);	
	string str = create_string(len);
	recv_chars(c, str.buff, str.len+1);
	return str;
}

static inline void print_string(string s)
{
	printf("%s", s.buff);
}
