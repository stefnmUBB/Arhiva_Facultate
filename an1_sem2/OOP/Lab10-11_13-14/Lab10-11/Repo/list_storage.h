#pragma once

#include "list.h"

#include <string>
#include <fstream>

using std::ifstream;
using std::ofstream;
using std::string;

template<typename TItem> class ListStorage : public List<TItem>
{
private:
	string filename;
public:
	ListStorage(string fn="");
	void set_file(string fn);
	void save_to_file();	

	static void save_callback(void*);

};

template<typename TItem>
void ListStorage<TItem>::save_callback(void* lst)
{
	//ListStorage<TItem>& lstorage = static_cast<ListStorage<TItem>&>(lst);
	((ListStorage<TItem>*)lst)->save_to_file();
}


template<typename TItem> ListStorage<TItem>::ListStorage(string fn)
{
	if (fn != "")
		set_file(fn);
}

template<typename TItem> void ListStorage<TItem>::set_file(string fn)
{
	filename = fn;
	ifstream in(filename);
	if (in.fail())
	{
		in.close();
		ofstream out(filename);
		out.close();
		return;
	}
	TItem item;
	while (in >> item)
	{
		this->container.push_back(item);
	}
	in.close();
	this->set_callback(save_callback);
}

template<typename TItem> void ListStorage<TItem>::save_to_file()
{
	ofstream out(filename);	
	for (size_t i = 0; i < this->get_container().size(); i++)
	{
		out << this->get_container().at(i) << '\n';
	}
	/*this->for_each([&out](const TItem& item)
		{
			out << item << '\n';
		});*/
	out.close();
}
