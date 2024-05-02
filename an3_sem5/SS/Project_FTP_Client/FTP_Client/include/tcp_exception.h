#pragma once

#include <exception>

class tcp_exception : public std::exception
{
public:
	tcp_exception() : std::exception() { }
	tcp_exception(const char* message) : std::exception(message) { }
};