#pragma once

#include "../Domain/melody.h"

#include <vector>
using std::vector;

class Repo
{
private:
	vector<Melody> items;
	string filename;
public:
	Repo(string fn);

	/// <summary>
	/// load items from filename
	/// </summary>
	void load();

	/// <summary>
	/// save items to filename
	/// </summary>
	void save();

	/// <summary>
	/// add melody to repo
	/// </summary>
	/// <param name="m">new melody</param>
	void add(const Melody& m);

	/// <summary>
	/// get all melodies
	/// </summary>
	/// <returns>vector of all melodies</returns>
	vector<Melody>& getAll();

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

