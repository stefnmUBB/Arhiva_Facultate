#pragma once

#include <stdio.h>

/// <summary>
/// wrapper around snprintf for quick inline concatenations
/// </summary>
/*class bufferf final
{
private:
	char* buffer = nullptr;	
public:
	template<typename... Args>
	bufferf(const char* format, Args... tail)
	{
		int N = 2048;
		buffer = new char[N]; // *
		snprintf(buffer, N, format, tail...);
	}

	operator const char* () const { return buffer; }

	~bufferf() { delete[] buffer; }
};*/