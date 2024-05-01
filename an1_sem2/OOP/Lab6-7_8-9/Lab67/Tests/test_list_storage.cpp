#include "test_list_storage.h"

#include "../Repo/list_storage.h"
#include <cassert>

void clear_file(std::string fn)
{
	std::ofstream g(fn);
	g.close();
}


void test_list_storage_create()
{
	clear_file("test_storage_int_0.txt");
	TestStorageList* lst = new TestStorageList("test_storage_int_0.txt");

	TestStorageListItem a; a.set_value(1);
	TestStorageListItem b; b.set_value(2);
	TestStorageListItem c; c.set_value(3);

	lst->add(a);
	lst->add(b);
	lst->add(c);

	lst->save_to_file();	

	std::ifstream in("test_storage_int_0.txt");
	for (int id, v, i = 1; in >> id >> v; i++)
	{
		assert(v == i);
		assert(id == v - 1);
	}
	in.close();

	lst->remove_at(1);
	lst->save_to_file();

	delete lst;

	lst = new TestStorageList("test_storage_int_0.txt");
	assert(lst->size() == 2);
	lst->save_to_file();

	delete lst;

	in = std::ifstream("test_storage_int_0.txt");
	for (int id, v, i = 1; in >> id >> v; i++)
	{
		std::cout << id << '\n';
		assert(id == v - 1);
	}
	in.close();


}