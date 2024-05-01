#pragma once

#include "../Domain/melody.h"
#include <cassert>

inline void test_melody()
{
	Melody m{ 2, "t1","a1", 3 };
	assert(m.get_id() == 2);
	assert(m.get_title() == "t1");
	assert(m.get_artist() == "a1");
	assert(m.get_rank() == 3);

	m.set_rank(5);
	assert(m.get_rank() == 5);
	m.set_title("t2");
	assert(m.get_title() == "t2");

}

