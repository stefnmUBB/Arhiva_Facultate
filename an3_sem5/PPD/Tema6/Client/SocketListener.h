#pragma once
#include "../commons/Connection.h"
#include<thread>
#include<functional>
#include "../commons/CommMessageType.h"
#include "../commons/ParticipantRecord.h"


class SocketListener
{
private:
	Connection conn;
	std::atomic_bool is_running = true;
public:
	SocketListener(Connection conn) : conn(conn) { }

	void run()
	{
		try
		{
			while (is_running.load())
			{
				std::cout << "Waiting for message\n";
				CommMessageType message = conn.recv<CommMessageType>();
				switch (message)
				{
				case CommMessageType::CountriesRanking:
				{
					std::cout << "CountriesRanking response\n";
					
					int len = conn.recv<int>();
					CountryRecord* data = new CountryRecord[len];
					conn.recv(data, len * sizeof(CountryRecord));

					for (int i = 0; i < len; i++)
					{
						std::cout << data[i].country_id << " " << data[i].p << "\n";
					}
					std::cout << "\n";

					delete[] data;

					break;
				}
				case CommMessageType::FullRanking:
				{
					std::cout << "FullRanking response\n";

					int len = conn.recv<int>();
					ParticipantRecordTuple* data = new ParticipantRecordTuple[len];
					conn.recv(data, len * sizeof(ParticipantRecordTuple));

					for (int i = 0; i < len; i++)
					{
						std::cout << data[i] << "\n";
					}
					std::cout << "\n";

					delete[] data;

					int len2 = conn.recv<int>();
					CountryRecord* data2 = new CountryRecord[len2];
					conn.recv(data2, len2 * sizeof(CountryRecord));
					
					for (int i = 0; i < len2; i++)
					{
						std::cout << data2[i].country_id << " " << data2[i].p << "\n";
					}
					std::cout << "\n";

					delete[] data2;

					conn.send(CommMessageType::Close);

					return;

					break;
				}
				default:
					throw std::exception(("Invalid message type received:" + std::to_string((int)message)).c_str());
				}
			}
		}
		catch (std::exception& e) { std::cout << e.what() << "\n"; }
	}

	void close()
	{
		is_running = false;
	}


};