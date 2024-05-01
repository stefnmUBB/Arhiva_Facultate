#pragma once

#include "../Domain/list_item.h"

class TestListItem : public ListItem
{
public:
	TestListItem(int _id = -1);

	int $get_id() { return get_id(); } // protected method exposure
};

void test_list_item_create();

