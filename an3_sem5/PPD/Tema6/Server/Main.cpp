//Proiect pe echipe: Neacsu Stefan, Condor Andrada
#include <iostream>
#include "TCPServer.h"
#include "../commons/ParticipantRecord.h"
#include "ProducerConsumerPipe.h"
#include <functional>
#include <fstream>
#include "ThreadPool.h"
#include "LinkedList.h"
#include "ContestantsProcessor.h"
#include <future>
#include <chrono>
#include <sstream>

// if DISABLE_COUT is defined, my_log<<X prints nothing, but std::cout<<X still prints ok
#define DISABLE_COUT

#include "../commons/log.h"

using namespace std;

using CountriesLeaderboard = std::vector<CountryRecord>;

ContestantsProcessor contestants_processor;

bool partial_leaderboard_request = false;
vector<promise<CountriesLeaderboard>*> partial_leaderboard_promises; // fiecare thread are promise-ul lui (nu putem face de 2x get/set)
mutex partial_leaderboard_mx;

bool close_server = false;
atomic_int finished_clients = 0;

void send_close_request()
{
    unique_lock<mutex> lock{ partial_leaderboard_mx };
    close_server = true;
}


future<CountriesLeaderboard> get_partial_leaderboard()
{    
    unique_lock<mutex> lock{ partial_leaderboard_mx };
    partial_leaderboard_request = true;
    promise<CountriesLeaderboard>* p = new promise<CountriesLeaderboard>();
    partial_leaderboard_promises.push_back(p);
    return p->get_future();
}

int compare(const ParticipantRecordTuple& t1, const ParticipantRecordTuple& t2)
{
    if (t1.points != t2.points) return t2.points - t1.points;
    return t1.id_part - t2.id_part;
}


class Pipe : public ProducerConsumerPipe<ParticipantRecordTuple>
{
private:
    ContestantsProcessor* contestants_processor;
public:
    Pipe(ContestantsProcessor* contestants_processor): contestants_processor{ contestants_processor } { }

    virtual void process(const ParticipantRecordTuple& item)
    {        
        contestants_processor->processContestant(item);                
    }
};

class Server : public TCPServer
{
private:    
    int p_w = 2;
    int p_r = 2;
    ThreadPool* thread_pool;
public:
    Pipe pipe{ &contestants_processor };
    std::vector<thread*> workers;

    void read_data(int country_id, Connection conn)
    {
        CommMessageType message = conn.recv<CommMessageType>();
        if (message == CommMessageType::Lines20)
        {
            read_lines20(country_id, conn);
        }
        else if (message == CommMessageType::FileFragment)
        {
            read_bytes(country_id, conn);
        }
        else
            throw std::exception("Invalid message, expected Lines20 or FileFragment");
    }    

    void read_bytes(int country_id, Connection conn)
    {
        CommMessageType message;
        std::vector<char> buffer;
        int cnt = 0;
        do // FileFragment, len <= L=128, data[0:len]
        {
            buffer.clear();
            int len = 0;
            while ((len = conn.recv<int>()) > 0)
            {
                my_log << "Receiving " << len << " bytes.\n";
                char* tmp = new char[len+1];                
                conn.recv(tmp, len);
                tmp[len] = 0;                               
                buffer.insert(buffer.end(), tmp, tmp + len);
                delete[] tmp;
            }
                        
            //buffer.push_back('\0');  
            cnt++;
            string path = "cache/R" + to_string(country_id) + "_" + to_string(cnt) + ".txt";
            ofstream g(path, ios::binary);
            g.write(buffer.data(), buffer.size());            
            g.close();

            ifstream f(path);

            ParticipantRecordPair line;
            while (f >> line)
            {
                ParticipantRecordTuple tuple(country_id, line);
                my_log << "Tuple: " << tuple << "\n";                
                thread_pool->put([tuple, this]() { this->pipe.put(tuple); }); // reader
            }
        } while ((message = conn.recv<CommMessageType>()) != CommMessageType::ReadEnd);
    }

    void read_lines20(int country_id, Connection conn)
    {    
        CommMessageType message;
        do
        {
            Pairs20 p20 = conn.recv<Pairs20>();
            my_log << "Received " << p20.count << "\n";
            for (int i = 0; i < p20.count; i++)
            {
                ParticipantRecordTuple tuple(country_id, p20.pairs[i]);
                thread_pool->put([tuple, this]() { this->pipe.put(tuple); }); // reader
            }
        } while ((message = conn.recv<CommMessageType>()) == CommMessageType::Lines20); // != ReadEnd
    }

    thread pipe_runner{ [&] {
        pipe.run();
    } };

    thread pool_runner{ [&] {         
        thread_pool->run();         
    } };

    atomic_int clients_count = 0;

    condition_variable cv_wait_all_clients;
    mutex mx_wait_all_clients;

    void process_client(Connection conn)
    {        
        std::cout << "Client accepted\n";
        CommMessageType message;

        if (!conn.recv_expect(CommMessageType::Connect))
            throw std::exception("Invalid message, expected Connect");
        int country = conn.recv<int>();
        std::cout << "Country id = " << country << "\n";
        read_data(country, conn);

        std::cout << "EndRead\n";

        if (!conn.recv_expect(CommMessageType::RequestCountriesRanking))
            throw std::exception("Invalid message, expected RequestCountriesRanking");

        future<CountriesLeaderboard> countries_lb = get_partial_leaderboard(); // future primit de pe main din while(1)        
        countries_lb.wait();

        CountriesLeaderboard countries = countries_lb.get();

        conn.send(CommMessageType::CountriesRanking);
        conn.send<int>(countries.size());
        conn.send(&countries[0], countries.size() * sizeof(CountryRecord));

        if (!conn.recv_expect(CommMessageType::RequestFullRanking))
            throw std::exception("Invalid message, expected RequestFullRanking");
        
        clients_count++;

        unique_lock<mutex> lock{ mx_wait_all_clients };

        int bak_clients_count = clients_count.load();

        while (clients_count.load() < ClientsCount)
            cv_wait_all_clients.wait(lock);
        
        cv_wait_all_clients.notify_one();

        {
            vector<ParticipantRecordTuple> final_result = contestants_processor.getContestantsLeaderboard(compare);            
            

            conn.send(CommMessageType::FullRanking);
            conn.send<int>(final_result.size());
            conn.send(&final_result[0], final_result.size() * sizeof(ParticipantRecordTuple));

            future<CountriesLeaderboard> countries_lb = get_partial_leaderboard();
            countries_lb.wait();

            CountriesLeaderboard countries = countries_lb.get(); // vector<CountryRecord>

            conn.send<int>(countries.size());
            conn.send(&countries[0], countries.size() * sizeof(CountryRecord)); 
            
            if (!conn.recv_expect(CommMessageType::Close))
                throw std::exception("Invalid message, expected Close");

        }
        finished_clients++;
    }    

    virtual void client_accepted(Connection conn) override
    {        
        try
        {
            process_client(conn);
        }
        catch (std::exception& e) { std::cout << e.what() << "\n"; }        
    }

    Server(int p_w=2, int p_r=2) : p_w(p_w), p_r(p_r)
    {
        for (int p = 0; p < p_w; p++)
            workers.push_back(new thread(&Pipe::run, &pipe));
        thread_pool = new ThreadPool(p_r);
    }

    ~Server()
    {
        printf("threadpool stop\n");
        thread_pool->stop();
        printf("pipe stop\n");
        pipe.stop();        
        printf("pipe join\n");
        pipe_runner.join();
        printf("pool join\n");
        pool_runner.join();        
        printf("workers join\n");
        for (int p = 0; p < p_w; p++)
            workers[p]->join();
        printf("after all\n");
        delete thread_pool;
    }
};

int main(int argc, char** argv)
{   
    int p_w = (argc == 1 ? 4 : (argv[1][0] - '0'));
    int dt = (argc <= 2 ? 1 : (argv[2][0] - '0'));
    int p_r = (argc <=3 ? 2 : (argv[3][0] - '0'));

    Server server(p_w, p_r);
    try
    {
        auto run_start = std::chrono::high_resolution_clock::now();

        server.start(); // <-- creeaza un thread separat care da accept()

        auto t_start = std::chrono::high_resolution_clock::now();
        CountriesLeaderboard countries;

        while (1) // pe main thread facem clasamentul pe tari
        {
            unique_lock<mutex> lock{ partial_leaderboard_mx };
            if (partial_leaderboard_request)
            {
                auto t_end = std::chrono::high_resolution_clock::now();
                double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
                if (elapsed_time_ms > dt)
                {
                    countries = contestants_processor.getCountriesLeaderboard();
                    t_start = t_end;
                }
                for (auto* p : partial_leaderboard_promises) // iteram promise-urile si le setam valoarea countries
                    p->set_value(countries);
                partial_leaderboard_promises.clear();                               
            }
            if (finished_clients == ClientsCount)
                break;
        }
        // isi incheie toti clientii executia

       
        ofstream g("participants.txt");        
        vector<ParticipantRecordTuple> final_result = contestants_processor.getContestantsLeaderboard(compare);
        for (auto t : final_result)
            g << t << "\n";
        g.close();
        printf("parts.txt\n");
        g = ofstream("countries.txt");
        for (auto c : countries)
            g << c.country_id << " " << c.p << "\n";
        g.close();
        printf("countries.txt\n");

        auto run_end = std::chrono::high_resolution_clock::now();
        double elapsed_time_ms = std::chrono::duration<double, std::milli>(run_end - run_start).count();
        std::cout << ">>>> " << elapsed_time_ms << "\n";
    }    
    catch (std::exception& e)
    {
        std::cout << e.what() << "\n";
    }
}
