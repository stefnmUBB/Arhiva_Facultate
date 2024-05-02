#pragma once

#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib,"WS2_32") 

#include "../commons/WSAException.h"
#include "../commons/Connection.h"
#include <thread>
#include <functional>
#include <vector>
#include "ClientsCount.h"

class TCPServer
{
private:
	SOCKET server;
	WSADATA wsaData;
	sockaddr_in local;

	void initialize()
	{
		int ret = 0;
		if ((ret = WSAStartup(0x101, &wsaData)) != 0)
			throw WSAException(ret);

		local.sin_family = AF_INET;
		local.sin_addr.s_addr = INADDR_ANY;
		local.sin_port = htons((u_short)8128);


		if ((server = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
			throw WSAException(WSAGetLastError());

		if ((ret = bind(server, (sockaddr*)&local, sizeof(local))) != 0)
			throw WSAException(ret);

		if ((ret = listen(server, 10)) != 0)
			throw WSAException(ret);
	}

	std::atomic<bool> accepts_clients = true;

	void run()
	{
		SOCKET client;
		sockaddr_in from;
		int fromlen = sizeof(from);
		int clients_count = 0;
		while (clients_count < ClientsCount) //(accepts_clients.load())
		{			
			printf("    [Waiting for clients...]\n");
			client = accept(server, (struct sockaddr*)&from, &fromlen);
			clients_count++;

			char buffer[30];
			const char* ip;			

			if ((ip = inet_ntop(AF_INET, &(from.sin_addr), buffer, INET_ADDRSTRLEN)) == nullptr)
				throw WSAException();
			
			printf("    [Connected client %s]\n", ip);

			Connection conn(client); // client_running[i] <-> client_threads[i]
			client_running.push_back(new std::atomic<bool>{ true });
			client_threads.push_back(new std::thread(&TCPServer::serve_client, this, conn, client_running.back()));			

			// sterge threadurile care s-au terminat deja
			for (int i = 0; i < client_running.size(); i++)
			{
				if (client_running[i]->load() == false && client_threads[i] != nullptr)
				{
					client_threads[i]->join();
					delete client_threads[i];
					client_threads[i] = nullptr;
				}
			}
		}
		printf("Server loop ended\n");
	}

	std::thread run_thread;
	std::vector<std::atomic<bool>*> client_running;
	std::vector<std::thread*> client_threads;

	void serve_client(Connection conn, std::atomic<bool>* running)
	{
		printf("    [Interact with client %lli...]\n", conn.socket());
		client_accepted(conn);
		printf("    [Client %lli terminated...]\n", conn.socket());
		conn.close();
		running->store(false);
		printf("    [Client %lli terminated... (x2)]\n", conn.socket());
	}

public:
	void start()
	{
		initialize();
		run_thread = std::thread(std::bind(&TCPServer::run, this));
	}

	virtual void client_accepted(Connection conn) = 0;	

	void close()
	{		
		accepts_clients = false;
		if (run_thread.joinable())
			run_thread.join();
		for (auto* t : client_threads)
			if (t)
				t->join();
	}

	
	virtual ~TCPServer()
	{
		close();
		closesocket(server);
		WSACleanup();
	}

};