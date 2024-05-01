#include "ui.h"
#include <iostream>
#include <stdlib.h>

int read_int(std::istream& in)
{
	int x;
	if (in >> x)
		return x;
	else
	{
		throw read_com_exception("Your option must be a number");
	}
}

ui_node::ui_node(const std::string& _text, std::function<void()> _action)
{
	text = _text;
	action = _action;
	parent = nullptr;
}

void ui_node::add(ui_node* node)
{
	children.push_back(node);
	node->parent = this;
}

ui_node* ui_node::get(size_t index) const noexcept
{
	if (index > children.size())
	{
		return nullptr;
	}	
	if (index == children.size())
		return parent;
	return children.at(index);
}

bool ui_node::can_execute() const noexcept
{
	return action != NULL;
}

void ui_node::execute() const
{
	action();
}

void ui_node::print() const
{
	for (size_t i = 0; i < children.size(); i++)
	{
		if (children.at(i) != nullptr)
		{
			std::cout << i << ". " << children.at(i)->text << '\n';
		}		
	}	
	if (parent != nullptr)
		std::cout << children.size() << ". Go back\n";
}

UI::UI(ui_node* _root) noexcept
{
	current = _root;
}

void UI::select_option()
{
	if (!current->can_execute())
	{
		system("cls");
		current->print();
		size_t option = 0xFFFFFFFF; 
		std::cout << ">>> ";
		option = read_int(std::cin);		
		ui_node* next = current->get(option);
		if (next == nullptr)
		{
			std::cout << "Invalid option.\nPress any key to continue";			
			int c = getchar();
			c = getchar();			
			return;
		}
		if (next->can_execute())
		{
			next->execute();
			int c = getchar();
			c = getchar();
			std::cout << "Press any key to continue";
			return;
		}
		else current = next;
	}
}