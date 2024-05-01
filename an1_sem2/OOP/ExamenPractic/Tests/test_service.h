#pragma once

#include "../Service/service.h"
#include "test_repo.h"

inline void test_service()
{
	// read
	cpy("Tests/items_rd.txt", "Tests/tmp.txt");
	Repo r{ "Tests/tmp.txt" };
	Service s{ r };
	assert(s.getAll().size() == 50);

	// update
	s.update(5, "new_title", 8);
	assert(s.getAll()[5].get_title() == "new_title");
	assert(s.getAll()[5].get_rank() == 8);

	// remove
	s.remove(5);
	assert(s.getAll().size() == 49);

	// sort
	vector<Melody> srt = s.sorted();
	for (int i = 0; i < srt.size() - 1; i++)
		assert(srt[i].get_rank() <= srt[i + 1].get_rank());


	// remove only artist
	cpy("Tests/empty.txt", "Tests/tmp.txt");
	r.load();
	r.add(Melody{ 0,"t1","a1",1 });
	r.add(Melody{ 2,"t2","a2",1 });
	r.add(Melody{ 3,"t3","a1",1 });
	assert(s.getAll().size() == 3);
	try
	{
		s.remove(2); assert(false);
	}
	catch (...) { assert(true); }

}
