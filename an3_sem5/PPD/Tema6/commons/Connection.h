#pragma once

#include <winsock2.h>
#pragma comment(lib,"WS2_32") 
#include "./WSAException.h"
#include "./CommMessageType.h"

class Connection
{
private:
	SOCKET s;
public:
	Connection(SOCKET s) : s(s) { }

	int send(const void* buffer, int len)
	{
		int result = ::send(s, (const char*)buffer, len, 0);
		return result > 0 ? result : throw WSAException();
	}

	int recv(void* buffer, int len)
	{
		int result = ::recv(s, (char*)buffer, len, 0);
		return result > 0 ? result : throw WSAException();
	}	

	template<typename T> 
	void send(const T& data) 
	{
		if (send((const char*)&data, sizeof(T)) != sizeof(T))
			throw std::exception("Data did not send completely");
	}

	template<typename T>
	T recv()
	{
		T buffer{};
		if(recv((char*)&buffer, sizeof(T))!=sizeof(T))
			throw std::exception("Data was not received completely");
		return buffer;
	}

	template<typename T>
	bool recv_expect(const T& requirement)
	{
		return recv<T>() == requirement;
	}

	template<typename T>
	void send_message(CommMessageType m, const T& data)
	{
		send(m);
		send(data);
	}

	void close()
	{
		if (s < 0) return;
		closesocket(s);
		s = -1;
	}	

	SOCKET socket() const { return s; }
};
