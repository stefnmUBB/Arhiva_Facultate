#include <iostream>
#include <exception>

#include "FTPClient.h"
#include "FTPCommandInterpreter.h"

#include <string>
#include "tcp_exception.h"
#include "utils.h"
#include "ArgsParser.h"

using namespace std;


void run_client(const char* ip, int port)
{    
    FTPClient ftp_client(ip, port, printf);

    FTPCommandInterpreter ci(&ftp_client);        

    std::string cmd;
    while (1)
    {
        std::cout << ">> ";        
        std::getline(cin, cmd);

        try
        {
            ci.execute(cmd.c_str());
        }
        catch (exception& e)
        {
            cout << Utils::Color::Red() << e.what() << Utils::Color::White() << "\n";
        }
    }

    return;
}


int main(int argc, const char** argv)
{    
    try
    {
        ArgsParser args(argc, argv);
        const char* ip = args.get_arg<const char*>(1, "127.0.0.1");
        int port = args.get_arg(2, 21);  

        if (Utils::get_str_bound(ip, 20) < 0)
            throw std::exception("Invalid IP");

        run_client(ip, port);
    }
    catch (exception& e)
    {
        cout << e.what() << "\n";
    }   
}
