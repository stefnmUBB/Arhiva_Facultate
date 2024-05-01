#include "test_ui.h"

#include "../UI/ui.h"
#include <cassert>

void test_ui()
{
	ui_node node("Sample text", NULL);
	assert(!node.can_execute());

	int x = 0;
	ui_node child("A suboption", [&x]() { x = 1; });
	node.add(&child);

	assert(node.get(0) == &child);
	assert(child.get(0) == &node);
	assert(node.get(2) == NULL);

	child.execute();
	assert(x == 1);
}