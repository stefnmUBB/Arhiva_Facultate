#include "FTPClient.h"
#include <iostream>
#include "utils.h"
#include "bout.h"

FTPClient::FTPClient(const char* ip, int port, std::function<void(const char*)> print_line)
{
	this->print_line = print_line;
	std::function<void(const char*)> line_rec_cb = std::bind(&FTPClient::line_received_callback, this, std::placeholders::_1);
	telnet_client = new TelNetClient(ip, port, line_rec_cb);
	connected = true;
	filesystem = new VirtualFS("vfs_root");
}

void FTPClient::line_received_callback(const char* line)
{	
	strncpy_s(line_buffer, line, sizeof(line_buffer));

	std::cout << Utils::Color::Yellow();
	print_line(line);
	std::cout << Utils::Color::White();
}

int FTPClient::send_command_wrapper(const char* cmd)
{
	std::cout << Utils::Color::Blue() << cmd << Utils::Color::White() << "\n";
	return telnet_client->send_command(cmd);
}

void FTPClient::login(const char* user, const char* pass)
{	
	if (!connected)
	{
		telnet_client->reconnect();
		connected = true;
	}
	if (send_command_wrapper(bout() << "USER " << user << bfin) != 331)
	{
		throw std::exception("login failed");				
	}	
	if (send_command_wrapper(bout() << "PASS " << pass << bfin) != 230)
	{
		throw std::exception("login failed");
	}	
}

void FTPClient::logout()
{	
	if (send_command_wrapper("QUIT") != 221)
	{
		throw std::exception("logout failed");
	}
	connected = false;	
	telnet_client->close();
}


void FTPClient::list(const char* path)
{
	int resp = path == nullptr ? send_command_wrapper("LIST") : send_command_wrapper(bout() << "LIST " << path << bfin);
	if (resp != 150)
		throw std::exception("Failed");

	//int data_size = get_data_size(line_buffer);
	//printf("Data size = %i\n", data_size);


	std::vector<char> tmp_buffer(1024);
	std::vector<char> buffer;
	int tmp_effective_size = 0;

	while ((tmp_effective_size = data_port.recv(tmp_buffer.data(), tmp_buffer.size()).bytes_count) > 0)
	{
		buffer.insert(buffer.end(), tmp_buffer.begin(), tmp_buffer.begin() + tmp_effective_size);
	}
	//data_port.recv(buffer.data(), buffer.size());	
	data_port.close();	

	buffer.push_back('\0');

	printf(buffer.data());

	if (telnet_client->recv_response() != 226)
		throw std::exception("Failed transfer");
}

void FTPClient::mode_binary()
{
	if(send_command_wrapper("TYPE I")!=200)
		throw std::exception("Failed");
}

void FTPClient::mode_ascii()
{
	if (send_command_wrapper("TYPE A") != 200)
		throw std::exception("Failed");
}

void FTPClient::stor(const char* path)
{
	std::vector<char> buffer;
	
	try
	{
		buffer = filesystem->read(path);
	}
	catch (const std::exception& e)
	{
		data_port.close();
		throw e;
	}

	if (send_command_wrapper(bout() << "STOR " << path << bfin) != 150)
		throw std::exception("Failed");
	
	data_port.send(buffer.data(), buffer.size());
	data_port.close();

	if (telnet_client->recv_response() != 226)
		throw std::exception("Failed transfer");
}

void FTPClient::retr(const char* path)
{
	if (send_command_wrapper(bout() << "RETR " << path << bfin) != 150)
		throw std::exception("Failed");

	//int data_size = get_data_size(line_buffer);
	//printf("Data size = %i\n", data_size);
	
	std::vector<char> tmp_buffer(1024);
	std::vector<char> buffer;
	int tmp_effective_size = 0;

	while ((tmp_effective_size = data_port.recv(tmp_buffer.data(), tmp_buffer.size()).bytes_count) > 0)
	{
		buffer.insert(buffer.end(), tmp_buffer.begin(), tmp_buffer.begin() + tmp_effective_size);
	}	
	
	//data_port.recv(buffer.data(), buffer.size());	
	data_port.close();

	filesystem->write(path, buffer);

	if (telnet_client->recv_response() != 226)
		throw std::exception("Failed transfer");
}


namespace
{
	// buff = "192,168,56,1,244,12)" --> int[] {192, 168, 56, 1, 244, 12};	
	void parse_pasv_addr(const char* buff, int a[6])
	{
		constexpr int maxStrLen = 4 * 6;
		int i = 0, k = 0;

		for (; buff[k] != ')' && k < maxStrLen; k++)
		{
			if ('0' <= buff[k] && buff[k] <= '9')
			{
				a[i] = a[i] * 10 + (buff[k] - '0');
				continue;
			}
			if (buff[k] == ',')
			{
				if (i >= 6)
					throw std::exception("Failed to parse PASV address: too many numbers");
				i++;
				continue;
			}
			throw std::exception(bout() << "Failed to parse PASV address: invalid character '0x" << bhex << buff[i] << "'" << bfin);
		}

		if (buff[k] != ')')
			throw std::exception("Failed to parse PASV address: input too long");
		else
		{
			if (i >= 6)
				throw std::exception("Failed to parse PASV address: too many numbers");
			i++;
		}

		if (i < 6)
			throw std::exception("Failed to parse PASV address: insufficient numbers");
	}
}

void FTPClient::pasv()
{
	if (send_command_wrapper("PASV") != 227)
		throw std::exception("Entering passive mode failed");	
	const char* line_pfx = "227 Entering Passive Mode (";
	if (strncmp(line_pfx, line_buffer, strlen(line_pfx)) != 0)
		throw std::exception("Invalid passive response message");
	char* buff = line_buffer + strlen(line_pfx);		

	int a[6]{};
	parse_pasv_addr(buff, a);

	const char* ip = bout() << a[0] << "." << a[1] << "." << a[2] << "." << a[3] << bfin;	
	int port = a[4] * 256 + a[5];	

	data_port.connect(ip, port);
	printf("Opened data port on %s:%i.\n", (const char*)ip, port);
}

FTPClient::~FTPClient()
{
	delete telnet_client;
	delete filesystem;
}