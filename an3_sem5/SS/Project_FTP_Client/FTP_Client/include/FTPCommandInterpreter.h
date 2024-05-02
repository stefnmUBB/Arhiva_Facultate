#pragma once

#include "CommandInterpreter.h"
#include "FTPClient.h"

class FTPCommandInterpreter : public CommandInterpreter
{
private:
	FTPClient* ftp;
public:
	FTPCommandInterpreter(FTPClient* ftp);
	
};