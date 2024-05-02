#pragma once

#include "TCPResult.h"
#include "TCPResponse.h"

class TCP final
{
private:
	class __privates__;
	__privates__* privates;	

public:
	TCP();

	void connect(const char* host, int port);	

	TCPResult send(const void* buffer, size_t size);
	TCPResult recv(void* buffer, size_t size);

	void ensure_send(void* buffer, size_t size);
	void ensure_recv(void* buffer, size_t size);

	TCPResult send_i32(int n);
	TCPResponse<int> recv_i32();

	TCPResult send_u8(unsigned char n);
	TCPResponse<unsigned char> recv_u8();

	TCPResult send_i8(char n);
	TCPResponse<char> recv_i8();

	void set_timeout(int seconds);

	int get_port() const;
	const char* get_ip() const;

	void close();

	~TCP();
};