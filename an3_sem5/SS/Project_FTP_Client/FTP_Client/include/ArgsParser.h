#pragma once

#include <string>
#include <vector>
#include "utils.h"

class ArgsParser
{
private:
	std::vector<std::string> args;
public:
	ArgsParser(int argc, const char** argv) 
	{
		for (int i = 0; i < argc; i++)
		{
			args.push_back(argv[i]);
		}
	}

	const char* get_arg(int i)
	{
		if (i < 0 || i >= args.size())
			return nullptr;
		return args[i].c_str();
	}

	template<typename T> T get_arg(int i, T default_value)
	{		
		if(get_arg(i)==nullptr)
			return default_value;
		throw std::exception("Not implemented: get_arg<T>(int, T)");
	}	

	template<>
	const char* get_arg<const char*>(int i, const char* default_value)
	{
		const char* arg = get_arg(i);
		return arg ? arg : default_value;
	}

	template<>
	int get_arg<int>(int i, int default_value)
	{
		const char* arg = get_arg(i);
		return arg ? Utils::my_atoi(arg) : default_value;
	}



};