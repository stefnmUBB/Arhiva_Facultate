#pragma once

#include <string>
#include <iostream>

using std::string;
using std::istream;
using std::ostream;


class Melody
{
private:
	int id = 0;
	string title = "";
	string artist = "";
	int rank = 0;
public:
	Melody() = default;
	Melody(int id, string title, string artist, int rank);

	/// <summary>
	/// get melody id
	/// </summary>
	/// <returns>melody id</returns>
	int get_id() const;

	/// <summary>
	/// get melody title
	/// </summary>
	/// <returns>melody title</returns>
	string get_title() const;

	/// <summary>
	/// get melody artist
	/// </summary>
	/// <returns>melody artist</returns>
	string get_artist() const;

	/// <summary>
	/// get melody rank
	/// </summary>
	/// <returns>melody rank</returns>
	int get_rank() const;

	/// <summary>
	/// set melody title
	/// </summary>
	/// <param name="title">new melody title</param>
	void set_title(string title);

	/// <summary>
	/// set melody rank
	/// </summary>
	/// <param name="rank">new melody rank</param>
	void set_rank(int rank);

	friend istream& operator >> (istream& i, Melody& m);
	friend ostream& operator << (ostream& o, const Melody& m);

};

