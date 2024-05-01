#include "service.h"
#include <algorithm>

class ValidatorRem
{
private:
	Repo& repo;
public:
	ValidatorRem(Repo& repo) : repo{repo} {}
	void validate(int id)
	{
		string artist = "";
		for(const auto& m:repo.getAll())
			if (m.get_id() == id)
			{
				artist = m.get_artist();
				break;
			}
		if (artist == "") return;
		int cnt = 0;
		for (const auto& m : repo.getAll())
		{
			cnt += m.get_artist() == artist;
		}
		if (cnt == 1)
			throw std::exception{ "Each artist must have at least one melody" };
	}
};

Service::Service(Repo& repo) : repo{ repo }
{
	
}

const vector<Melody>& Service::getAll() const
{
	return repo.getAll();
}

vector<Melody> Service::sorted() const
{
	vector<Melody> items=repo.getAll();

	std::sort(items.begin(), items.end(), [](const Melody& m1, const Melody& m2)
		{
			return m1.get_rank() < m2.get_rank();
		});

	return items;
}

void Service::update(int id, string title, int rank)
{
	repo.update(id, title, rank);
	repo.save();
}

void Service::remove(int id)
{
	ValidatorRem(repo).validate(id);
	repo.remove(id);
	repo.save();
}