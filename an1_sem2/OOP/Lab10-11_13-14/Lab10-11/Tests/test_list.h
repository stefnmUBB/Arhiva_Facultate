#pragma once

#include "test_list_item.h"

class TestList : public List<TestListItem>
{

};

void test_list_add();
void test_list_find_by_id();
void test_list_remove_at();
void test_list_remove_by_id();
void test_for_each();