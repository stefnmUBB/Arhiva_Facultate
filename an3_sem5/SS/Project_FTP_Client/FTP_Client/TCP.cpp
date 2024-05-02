#include "TCP.h"

#define _WIN32_WINNT 0x601 // Windows 7 or later
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#include <ws2tcpip.h>
#include <ws2spi.h>
#include <sys/types.h>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <exception>
#include <string>

#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")

#include "bufferf.h"
#include "tcp_exception.h"
#include <bout.h>

class TCP::__privates__
{
private:
	SOCKET sockd = INVALID_SOCKET;	
	addrinfo* server = nullptr;
	int port = 0;
	char ip[100]={};
public:

	__privates__()
	{		
		WSADATA wsaData;
		int iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
		if (iResult != 0) 
		{
			throw std::exception(bout() << "WSAStartup failed with error:" << iResult << bfin);
		}
	}

	void set_timeout(int seconds)
	{		
		DWORD timeout = seconds * 1000;
		setsockopt(sockd, SOL_SOCKET, SO_RCVTIMEO, (const char*)&timeout, sizeof(timeout));
		setsockopt(sockd, SOL_SOCKET, SO_RCVTIMEO, (const char*)&timeout, sizeof(timeout));
	}	

	void connect(const char* host, int port) 
	{		
		close();
		sockd = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
		addrinfo hints;

		ZeroMemory(&hints, sizeof(hints));
		hints.ai_family = AF_UNSPEC;
		hints.ai_socktype = SOCK_STREAM;
		hints.ai_protocol = IPPROTO_TCP;		

		char port_str[20] = { 0 };
		if (_itoa_s(port, port_str, 10) != 0)
		{			
			throw std::exception(bout() << "Failed to convert port number to string: " << port << bfin);
		}		
		addrinfo* result = nullptr;		

		int iResult = getaddrinfo(host, port_str, &hints, &result);
		if (iResult != 0) 
		{
			WSACleanup();
			throw std::exception(bout() << "getaddrinfo failed with error:" << iResult << bfin);
		}


		// Attempt to connect to an address until one succeeds
		for (addrinfo* ptr = result; ptr != NULL; ptr = ptr->ai_next) {

			printf("Trying..\n");
			// Create a SOCKET for connecting to server
			sockd = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
			if (sockd == INVALID_SOCKET) {
				WSACleanup();
				throw std::exception("socket failed with error: %ld\n", WSAGetLastError());											
			}			
			// Connect to server.
			int iResult = ::connect(sockd, ptr->ai_addr, (int)ptr->ai_addrlen);
			if (iResult == SOCKET_ERROR) {
				closesocket(sockd);
				sockd = INVALID_SOCKET;
				continue;
			}
			server = ptr;

			sockaddr_in struc_ = {};
			int struc_len = sizeof(struc_);
			if (getsockname(sockd, (sockaddr*)&struc_, &struc_len))
			{
				printf("getname failed\n");
			}
			else
			{
				strncpy_s(ip, inet_ntoa(((sockaddr_in*)&struc_)->sin_addr), sizeof(ip));
				port = ntohs(((sockaddr_in*)&struc_)->sin_port);
			}

			return;
		}
		

		throw std::exception("Connection failed");
	}	

	int get_port() const { return port; }
	const char* get_ip() const { return ip; }

	int send(const char* buffer, size_t size)
	{
		return ::send(sockd, buffer, (int)size, 0);
	}

	int recv(char* buffer, size_t size)
	{
		return ::recv(sockd, buffer, (int)size, 0);			
	}


	void close()
	{
		if (sockd != INVALID_SOCKET)
		{
			closesocket(sockd);
			sockd = INVALID_SOCKET;
		}
	}

	~__privates__()
	{
		close();
	}
};


TCP::TCP()
{
	privates = new __privates__();
}

void TCP::connect(const char* host, int port)
{
	privates->connect(host, port);
}

TCPResult TCP::send(const void* buffer, size_t size)
{
	int sent_result = privates->send((const char*)buffer, size);

	if (sent_result < 0)
		return TCPResult::fail(WSAGetLastError());
	else
		return TCPResult::success(sent_result);
}

TCPResult TCP::recv(void* buffer, size_t size)
{
	int recv_result = privates->recv((char*)buffer, size);

	if (recv_result < 0)
		return TCPResult::fail(WSAGetLastError());
	else
		return TCPResult::success(recv_result);
}

void TCP::ensure_send(void* buffer, size_t size)
{
	send(buffer, size).validate_send(size);	
}

void TCP::ensure_recv(void* buffer, size_t size)
{
	recv(buffer, size).validate_recv(size);	
}

TCPResult TCP::send_i32(int n)
{
	int x = htonl(n);
	return send(&x, sizeof(int));
}

TCPResponse<int> TCP::recv_i32()
{
	int x;
	ensure_recv(&x, sizeof(int));
	return TCPResponse<int>::success(ntohl(x));
}

TCPResult TCP::send_u8(unsigned char n)
{
	return send(&n, sizeof(unsigned char));
}

TCPResponse<unsigned char> TCP::recv_u8()
{
	unsigned char x;
	ensure_recv(&x, sizeof(unsigned char));
	return TCPResponse<unsigned char>::success(x);
}

TCPResult TCP::send_i8(char n)
{
	return send(&n, sizeof(char));
}

TCPResponse<char> TCP::recv_i8()
{
	char x;
	ensure_recv(&x, sizeof(char));
	return TCPResponse<char>::success(x);
}

void TCP::set_timeout(int seconds) { privates->set_timeout(seconds); }


int TCP::get_port() const { return privates->get_port(); }
const char* TCP::get_ip() const { return privates->get_ip(); }

void TCP::close()
{
	privates->close();
}

TCP::~TCP()
{
	delete privates;
}