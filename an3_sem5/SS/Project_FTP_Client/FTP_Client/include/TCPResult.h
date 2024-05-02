#pragma once

#include "bufferf.h"

struct TCPResult
{
	bool ok;
	int bytes_count;
	int error_code;

	static TCPResult success(int sent_bytes) { return { true, sent_bytes, 0 }; }
	static TCPResult fail(int error_code) { return { false, 0, error_code }; }	

	const char* get_error_message();

	void validate_send(size_t desired_size);
	void validate_recv(size_t desired_size);
};