#pragma once

#include "TCP.h"
#include <functional>

class TelNetClient
{
private:
	TCP tcp;	
	std::function<void(char*)> line_received_callback = [](char*) {};
	const char* ip = nullptr;
	int port = 21;
	bool is_connected = false;
public:	

	TelNetClient(const char* ip, int port, std::function<void(char*)> line_received_callback = [](char*) {});
	int send_command(const char* command);
	int recv_response();

	void close();

	void reconnect();

};
