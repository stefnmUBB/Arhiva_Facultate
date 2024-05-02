#pragma once

template<typename T>
struct TCPResponse
{
	bool ok;
	T value;	

	static TCPResponse success(const T& value) { return { true, value }; }
	static TCPResponse fail() { return { false, default(T) }; }

	operator T() { return value; }
};