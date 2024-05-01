#pragma once

#include "../Repo/repo.h"

class Service
{
private:
	Repo& repo;
public:
	Service(Repo& repo);

	/// <summary>
	/// get a list of all melodies
	/// </summary>
	/// <returns>vector of all melodies</returns>
	const vector<Melody>& getAll() const;

	/// <summary>
	/// get a list of all melodies, sorted by rank ascendingly
	/// </summary>
	/// <returns>sorted vector of all melodies</returns>
	vector<Melody> sorted() const;

	/// <summary>
	/// update melody by id
	/// </summary>
	/// <param name="id">id of target melody</param>
	/// <param name="title">new melody title</param>
	/// <param name="rank">new melody rank</param>
	void update(int id, string title, int rank);	

	/// <summary>
	/// remove melody by id
	/// nothing happens if id not found
	/// </summary>
	/// <param name="id">melody id</param>
	void remove(int id);
};

