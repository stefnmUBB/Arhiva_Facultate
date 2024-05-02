#pragma once

#include <vector>
#include <string>
#include "utils.h"

struct _bhex {};
struct _bdec {};
struct _bfin {};

inline _bhex bhex;
inline _bdec bdec;
inline _bfin bfin;

class bout
{
private:
	inline static constexpr int STR_ARG_MAX_SIZE = 1024;
	std::vector<char> buffer;
	inline static constexpr const char* digits = "0123456789ABCDEF";


	bool int_mode_hex = false;

public:
	bout& operator <<(const char* str)
	{
		const char* it = str;

		int k = Utils::get_str_bound(str, STR_ARG_MAX_SIZE);
		if(k<0)
			throw std::exception("bout failed: invalid char*: '\\0' not found");					

		if (k > 0) // k==0 means "" so no need to append anything
		{
			std::vector<char> result(k);
			memcpy(result.data(), str, k);
			buffer.insert(buffer.end(), result.begin(), result.end());
		}
		return *this;
	}	

	//template<int N> // why doesn't compile...
	bout& operator <<(wchar_t s[256])
	{
		std::wstring wstr{s};
		std::string str{};
		str.assign(wstr.begin(), wstr.end());		
		return *this << str.c_str();
	}


	bout& operator << (const _bhex&) { int_mode_hex = true; return *this; }
	bout& operator << (const _bdec&) { int_mode_hex = false; return *this; }

	bout& operator << (char c)
	{
		if (isgraph(c))
		{
			buffer.push_back(c);
			return *this;
		}

		buffer.push_back('\\');
		buffer.push_back('0');
		buffer.push_back('x');
		buffer.push_back(digits[((unsigned)c & 0xF0) >> 4]);
		buffer.push_back(digits[((unsigned)c & 0x0F)]);
		return *this;
	}

	bout& operator << (long long x)
	{
		if (x < 0)
		{
			x = -x;
			buffer.push_back('-');
		}	
		return *this << (unsigned long long)x;
	}


	bout& operator << (unsigned long long x)
	{		
		if (x == 0)
		{
			buffer.push_back(digits[0]);
			return *this;
		}

		int base = int_mode_hex ? 16 : 10;
		std::vector<char> result;
		for (; x > 0; x /= base)
			result.push_back(digits[x % base]);

		buffer.insert(buffer.end(), result.rbegin(), result.rend());
		return *this;
	}

	bout& operator << (int x) { return *this << (long long)x; }
	bout& operator << (unsigned int x) { return *this << (long long)x; }

	const char* operator << (const _bfin&) 
	{ 
		buffer.push_back('\0');

		for (char c : buffer)
		{
			printf("%02X ", c);
		}
		printf("\n");

		char* output = new char[buffer.size()];
		memcpy(output, buffer.data(), buffer.size());
		buffer.clear();
		return output;
	}
};