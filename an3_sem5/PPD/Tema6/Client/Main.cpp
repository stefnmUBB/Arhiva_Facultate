//Proiect pe echipe: Neacsu Stefan, Condor Andrada
#include <iostream>
#include "TCPClient.h"
#include "SocketListener.h"
#include "../commons/ParticipantRecord.h"

// if DISABLE_COUT is defined, my_log<<X prints nothing, but std::cout<<X still prints ok
#define DISABLE_COUT

#include "../commons/log.h"

#include <fstream>
using namespace std;

class Client : public TCPClient
{
private:
    int id_country;
    int deltax;
    const int problems_count = 10;    
    const char* send_method;
public:
    Client(int id_country, int deltax, const char* send_method) : TCPClient("127.0.0.1"), 
        id_country(id_country), deltax(deltax), send_method(send_method) { }

    void send_problems20(Connection conn)
    {
        Pairs20 pairs;
        pairs.count = 0;        

        for (int p = 1; p <= 10; p++)
        {
            std::string path = "data/C" + std::to_string(id_country) + "_P" + std::to_string(p) + ".txt";
            ifstream f(path);
            std::cout << path << "\n";
            if (f.fail())
            {
                std::cout << "Failed?" << "\n";
                continue;
            }

            ParticipantRecordPair line;
            while (f >> line)
            {
                pairs.pairs[pairs.count++] = line;
                if (pairs.count == 20)
                {
                    my_log << "Sending\n";
                    Sleep(deltax * 1000);
                    conn.send_message(CommMessageType::Lines20, pairs);                    
                    pairs.count = 0;
                }
            }
            f.close();
        }        

        if (pairs.count > 0)
        {
            my_log << "Sending\n";
            Sleep(deltax * 1000);
            conn.send_message(CommMessageType::Lines20, pairs);
            //CommMessageType::FileFragment
        }        
        conn.send(CommMessageType::ReadEnd);        
    }

    void send_bytes(Connection conn)
    {
        for (int p = 1; p <= 10; p++)
        {
            std::string path = "data/C" + std::to_string(id_country) + "_P" + std::to_string(p) + ".txt";
            ifstream f(path, ios::binary);
            std::cout << path << "\n";
            if (f.fail())
            {
                std::cout << "Failed?" << "\n";
                continue;
            }
            f.seekg(0, ios_base::end);
            int length = (int)f.tellg();
            f.seekg(0, ios_base::beg);
            char* buffer = new char[length + 1];
            f.read(buffer, length);
            f.close();

            conn.send(CommMessageType::FileFragment);

            int fixed_buffer_size = 128;

            for (int i = 0; i < length; i += fixed_buffer_size)
            {
                int send_len = min(length - i, fixed_buffer_size);
                my_log << "Sending " << send_len << " bytes\n";
                conn.send<int>(send_len);
                conn.send(buffer + i, send_len);
            }
            conn.send<int>(-1); // end of file
            delete[] buffer;
        }
        conn.send(CommMessageType::ReadEnd);
    }


    virtual void client_connected(Connection conn) override
    {        
        auto run_start = std::chrono::high_resolution_clock::now();

        SocketListener listener(conn);
        std::thread listener_run(&SocketListener::run, &listener);

        conn.send_message(CommMessageType::Connect, id_country);
                
        if (strcmp(send_method, "send20") == 0)
            send_problems20(conn);
        else
            send_bytes(conn);

        conn.send(CommMessageType::RequestCountriesRanking); // cerere de informare        

        conn.send(CommMessageType::RequestFullRanking);
        
        // am primit cl final ==> listener.close()

        listener_run.join();       

        auto run_end = std::chrono::high_resolution_clock::now();
        double elapsed_time_ms = std::chrono::duration<double, std::milli>(run_end - run_start).count();
        std::cout << ">>>> " << elapsed_time_ms << "\n";
    }
};

using namespace std;

int main(int argc, char** argv)
{    
    int id_country = (argc == 1 ? 0 : (argv[1][0] - '0'));    
    int delta_x = (argc <= 2 ? 1 : (argv[2][0] - '0')); 
    const char* send_method = (argc <= 3 ? "send20" : argv[3]);

    Client client(id_country, delta_x, send_method);
    try
    {
        client.connect();
    }
    catch (std::exception& e)
    {
        std::cout << "Error" << "\n";
        std::cout << e.what() << "\n";
    }
    
}