#include "test_list_item.h"
#include <assert.h>

TestListItem::TestListItem(int _id)
{
	set_id(_id);
}

void test_list_item_create()
{
	TestListItem tli(5);

	assert(tli.$get_id() == 5);
}