#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib,"WS2_32") 

#include "../commons/WSAException.h"
#include "../commons/Connection.h"


class TCPClient
{
private:
	const char* servername{};
	int port{};
	SOCKET conn = -1;
	WSADATA wsaData = {};
	struct sockaddr_in server = {};

	void initialize()
	{
		int ret = 0;
		if ((ret = WSAStartup(0x101, &wsaData)) != 0)
			throw WSAException(ret);
		if ((conn = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)) == INVALID_SOCKET)
			throw WSAException(WSAGetLastError());
		hostent* hp;
		unsigned long addr;
		if (inet_addr(servername) == INADDR_NONE)
		{
			hp = gethostbyname(servername);
		}
		else
		{
			addr = inet_addr(servername);
			hp = gethostbyaddr((char*)&addr, sizeof(addr), AF_INET);
		}
		if (hp == NULL)
		{
			closesocket(conn);
			conn = -1;
			throw std::exception("Could not resolve host address");
		}

		server.sin_addr.s_addr = *((unsigned long*)hp->h_addr);
		server.sin_family = AF_INET;
		server.sin_port = htons(port);
		
		if ((ret = ::connect(conn, (struct sockaddr*)&server, sizeof(server))) != 0)		
			throw WSAException(); 
	}
public:
	TCPClient(const char* servername, int port=8128) : servername(servername), port(port) {}

	void connect()
	{
		initialize();
		client_connected(Connection(conn));
	}

	virtual void client_connected(Connection conn)
	{

	}

	void close()
	{
		if (conn >= 0)
			closesocket(conn);
	}

	virtual ~TCPClient()
	{
		close();
	}
};
