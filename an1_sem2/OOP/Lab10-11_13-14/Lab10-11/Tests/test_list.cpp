#include "test_list.h"
#include <cassert>

void test_list_add()
{
	TestList tl;

	TestListItem item1;
	tl.add(item1);
	
	assert(tl[0].get_id() == 0);

	TestListItem item2;
	tl.add(item2);
	assert(tl[1].get_id() == 1);

	assert(tl.size() == 2);

}

void test_list_find_by_id()
{
	TestList tl;

	TestListItem item0; tl.add(item0); // 0	
	TestListItem item1; tl.add(item1); // 1
	TestListItem item2; tl.add(item2); // 2	
	TestListItem item3; tl.add(item3); // 3	
	TestListItem item4; tl.add(item4); // 4	
	TestListItem item5; tl.add(item5); // 5	

	assert(tl.find_by_id(2) == 2);
	assert(tl.find_by_id(3) == 3);
	assert(tl.find_by_id(5) == 5);
	assert(tl.find_by_id(100) == -1);

	tl.remove_at(3);

	assert(tl.find_by_id(2) == 2);
	assert(tl.find_by_id(3) == -1);
	assert(tl.find_by_id(5) == 4);
}

void test_list_remove_at()
{
	TestList tl;

	TestListItem item0; tl.add(item0); // 0	
	TestListItem item1; tl.add(item1); // 1
	TestListItem item2; tl.add(item2); // 2	
	TestListItem item3; tl.add(item3); // 3	

	tl.remove_at(2);

	assert(tl.size() == 3);
	assert(tl[2].get_id() == 3);
}

void test_list_remove_by_id()
{
	TestList tl;

	TestListItem item0; tl.add(item0); // 0	
	TestListItem item1; tl.add(item1); // 1
	TestListItem item2; tl.add(item2); // 2	
	TestListItem item3; tl.add(item3); // 3	

	assert(tl.remove_by_id(2));

	assert(tl.size() == 3);
	assert(tl[2].get_id() == 3);

	assert(!tl.remove_by_id(2));

	assert(tl.remove_by_id(3));
	assert(tl.size() == 2);
}

void test_for_each()
{
	TestList tl;	

	TestListItem item0; tl.add(item0); // 0	
	TestListItem item1; tl.add(item1); // 1
	TestListItem item2; tl.add(item2); // 2	
	TestListItem item3; tl.add(item3); // 3	

	int sum = 0;
	const auto sum_ids = [&sum](const TestListItem& item) { sum += item.get_id(); };

	tl.for_each(sum_ids);

	assert(sum == 6);
		
	std::cout << &tl.container;
}