#include "repo.h"

#include<fstream>


Repo::Repo(string fn) : filename{ fn }
{
	load();
}

void Repo::load()
{
	items.clear();
	std::ifstream f{ filename };

	if (!f.is_open() || f.bad() || f.fail())
	{
		f.close();
		return;
	}

	Melody m;	
	while (f >> m)
		add(m);

	f.close();
}

void Repo::save()
{	
	std::ofstream f{ filename };

	if (!f.is_open() || f.bad() || f.fail())
	{
		f.close();
		return;
	}

	for (const auto& m : items)
		f << m;

	f.close();
}

void Repo::add(const Melody& m)
{
	items.push_back(m);
}

vector<Melody>& Repo::getAll() 
{
	return items;
}

void Repo::update(int id, string title, int rank)
{
	for (Melody& m : items)
	{
		if (m.get_id() == id)
		{
			m.set_title(title);
			m.set_rank(rank);
		}
	}
}

void Repo::remove(int id)
{
	vector<Melody> new_items;

	for (Melody& m : items)
	{
		if (m.get_id() != id)
		{
			new_items.push_back(m);
		}
	}

	items = (vector<Melody>&&)new_items;
}