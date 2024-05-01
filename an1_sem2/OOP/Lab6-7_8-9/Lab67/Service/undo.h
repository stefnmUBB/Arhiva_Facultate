#pragma once

#include <exception>

class UndoAction
{	
public:
	virtual void do_undo() = 0;
	virtual ~UndoAction() = default;
};

class undo_exception : public std::exception
{
public:
	undo_exception(const char* msg) noexcept : std::exception(msg) {}
};