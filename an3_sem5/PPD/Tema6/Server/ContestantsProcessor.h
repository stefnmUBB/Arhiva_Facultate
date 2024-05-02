#pragma once
#include "../commons/ParticipantRecord.h"
#include "LinkedList.h"
#include <unordered_set>
#include <map>
#include <algorithm>

class ContestantsProcessor
{
private:
	LinkedList<ParticipantRecordTuple> contestantsLeaderboard;

	std::mutex cheater_mx;

	std::unordered_set<int> cheaterIds;

	void markCheater(int contestantId)
	{
		std::unique_lock<std::mutex> lock{ cheater_mx };
		cheaterIds.insert(contestantId);
	}

	bool isCheater(int contestant_id)
	{				
		std::unique_lock<std::mutex> lock{ cheater_mx };			
		return cheaterIds.find(contestant_id) != cheaterIds.end();
	}

	void deleteContestant(int id)
	{
		contestantsLeaderboard.remove(contestantHasId(id));
	}

	void updateOrAddСontestant(const ParticipantRecordTuple& contestant)
	{
		bool updated = contestantsLeaderboard.update(contestantHasId(contestant.id_part), [&](const ParticipantRecordTuple& contestantToCheck)
			{
				int new_score = contestantToCheck.points + contestant.points;
				return ParticipantRecordTuple{ contestantToCheck.id_country, contestantToCheck.id_part, new_score };
			});
		if (!updated)
			addContestant(contestant);
	}

	void addContestant(const ParticipantRecordTuple& contestant)
	{
		contestantsLeaderboard.insert(contestant);
	}


	void updateLeaderboard(const ParticipantRecordTuple& contestant)
	{
		if (contestant.points == -1)
		{
			markCheater(contestant.id_part);
			deleteContestant(contestant.id_part);
			return;
		}
		updateOrAddСontestant(contestant);
	}

	inline static std::function<bool(const ParticipantRecordTuple&)> contestantHasId(int id)
	{
		return [id](const ParticipantRecordTuple& t) { return t.id_part == id; };
	}

	std::map<int, std::mutex*> id_mx; // map id->mutex
	std::mutex id_mx_lock;

	std::mutex* get_id_mx(int id) // wrapper peste map <-- thread unsafe  id_mx.get(id)
	{
		std::unique_lock<std::mutex> lock{ id_mx_lock };
		if (id_mx.count(id) == 0)
			id_mx[id] = new std::mutex();		
		return id_mx[id];
	}


	std::mutex global_mx;
public:

	void processContestant(const ParticipantRecordTuple& contestant)
	{
		std::unique_lock<std::mutex> glock{ global_mx };
		if (isCheater(contestant.id_part))
			return;
		std::unique_lock<std::mutex> lock{ *get_id_mx(contestant.id_part) };
		updateLeaderboard(contestant);
	}

	std::vector<ParticipantRecordTuple> getContestantsLeaderboard(std::function<int(const ParticipantRecordTuple&, const ParticipantRecordTuple&)> compare) 
	{ 
		std::unique_lock<std::mutex> glock{ global_mx };
		contestantsLeaderboard.sort(compare);
		std::vector<ParticipantRecordTuple> v;
		contestantsLeaderboard.iterate_sync([&v](const ParticipantRecordTuple& x) { v.push_back(x); });
		return v;
	}

	std::vector<CountryRecord> getCountriesLeaderboard()
	{
		std::unique_lock<std::mutex> glock{ global_mx };
		std::map<int, int> result;
		// for (ParticipantRecordTuple it : contestantsLeaderboard)
		contestantsLeaderboard.iterate_sync([&](const ParticipantRecordTuple& it) { result[it.id_country] += it.points; });
		std::vector<CountryRecord> countries;
		for (auto p : result)
			countries.push_back(CountryRecord{ p.first, p.second });
		
		std::sort(countries.begin(), countries.end(), [](const CountryRecord& c1, const CountryRecord& c2) -> bool
			{
				if (c1.p != c2.p) return c1.p > c2.p;
				return c1.country_id < c2.country_id;
			});

		return countries;
	}
};
