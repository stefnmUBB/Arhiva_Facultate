#pragma once

#include <exception>
#include <string>
#include <locale>
#include <codecvt>

#include <winsock2.h>
#pragma comment(lib,"WS2_32") 

struct WSAException : public std::exception
{
private:
	static const wchar_t* get_message(int err)
	{
		LPTSTR Error = 0;
		if (FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
			NULL, err, 0, (LPTSTR)&Error, 0, NULL) == 0
			)
			return L"[Translating error failed]";
		return Error;
	}

#if _MSC_VER >= 1900

	std::string utf16_to_utf8(std::u16string utf16_string)
	{
		std::wstring_convert<std::codecvt_utf8_utf16<int16_t>, int16_t> convert;
		auto p = reinterpret_cast<const int16_t*>(utf16_string.data());
		return convert.to_bytes(p, p + utf16_string.size());
	}

#else

	std::string utf16_to_utf8(std::u16string utf16_string)
	{
		std::wstring_convert<std::codecvt_utf8_utf16<char16_t>, char16_t> convert;
		return convert.to_bytes(utf16_string);
	}

#endif


public:
	WSAException(int err) : std::exception(utf16_to_utf8(std::u16string((const char16_t*)get_message(err))).c_str())
	{

	}

	WSAException() : WSAException(WSAGetLastError()) { }
};