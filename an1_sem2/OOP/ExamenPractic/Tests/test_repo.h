#pragma once

#include "../repo/repo.h"
#include <fstream>
#include <cassert>

inline void cpy(string src, string dest)
{
	std::ifstream f{ src };
	std::ofstream g{ dest };

	string line;
	while (getline(f, line))
	{
		g << line << '\n';
	}

	f.close();
	g.close();
}

inline void test_repo()
{
	// read
	cpy("Tests/items_rd.txt", "Tests/tmp.txt");
	Repo r{ "Tests/tmp.txt" };
	assert(r.getAll().size() == 50);

	// update
	r.update(5, "new_title", 8);
	assert(r.getAll()[5].get_title()=="new_title");
	assert(r.getAll()[5].get_rank() == 8);

	// remove
	r.remove(5);
	assert(r.getAll().size() == 49);

	r.save();

	Repo r2{ "Tests/tmp2.txt" };
	assert(r2.getAll().size() == 0);

}

