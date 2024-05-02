#include "TCPResult.h"

#include <winsock.h>
#include "tcp_exception.h"
#include <bout.h>


const char* TCPResult::get_error_message()
{    
    wchar_t msgbuf[256]{};
    msgbuf[0] = '\0';

    FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        error_code,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        msgbuf,
        sizeof(msgbuf),
        NULL);

    return bout() << "Socket error " << error_code << ": " << msgbuf << bfin;
}

void TCPResult::validate_send(size_t desired_size)
{
    if (!ok)
        throw tcp_exception(get_error_message());
    if (bytes_count != desired_size)
        throw tcp_exception(bout() << "Not all bytes were sent (" << bytes_count << "/" << desired_size << ")" << bfin);
}

void TCPResult::validate_recv(size_t desired_size)
{
    if (!ok)
        throw tcp_exception(get_error_message());
    if (bytes_count == 0)
        throw tcp_exception("Connection interrupted during recv");
    if (bytes_count != desired_size)
        throw tcp_exception(bout() << "Not all bytes were sent (" << bytes_count << "/" << desired_size << ")" << bfin);        
}