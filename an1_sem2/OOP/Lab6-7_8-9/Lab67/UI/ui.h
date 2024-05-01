#pragma once

#include <string>
#include <vector>

#include <functional>
#include <utility>

int read_int(std::istream& in);
class ui_node
{
private:
	ui_node* parent;
	std::string text;
	std::function<void()> action;	
	std::vector<ui_node*> children;
public:
	ui_node(const std::string& _text, std::function<void()> _action);
	void add(ui_node* node);
	ui_node* get(size_t index) const noexcept;
	bool can_execute() const noexcept;
	void print() const;
	void execute() const;
};

class UI
{
private:	
	ui_node* current;
public:
	UI(ui_node* _root) noexcept;
	void select_option();
};

class read_com_exception : public std::exception
{
public: read_com_exception(const char* msg) noexcept : std::exception(msg) { }
};