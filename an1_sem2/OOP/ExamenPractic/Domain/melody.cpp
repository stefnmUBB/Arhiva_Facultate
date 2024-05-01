#include "melody.h"

Melody::Melody(int id, string title, string artist, int rank)
	: id{ id }, title{ title }, artist{ artist }, rank{ rank }
{}

int Melody::get_id() const { return id; }
string Melody::get_title() const { return title; }
string Melody::get_artist() const { return artist; }
int Melody::get_rank() const { return rank; }

void Melody::set_title(string title) { this->title = title; }
void Melody::set_rank(int rank) { this->rank = rank; }

#include <sstream>

istream& operator >> (istream& i, Melody& m)
{
	string _id, _rank;
	std::getline(i, _id);
	std::getline(i, m.title);
	std::getline(i, m.artist);
	std::getline(i, _rank);

	std::stringstream ss;
	ss << _id << " " << _rank;
	ss >> m.id >> m.rank;
	return i;
}

ostream& operator << (ostream& o, const Melody& m)
{
	o << m.id << '\n' << m.title << '\n' << m.artist << '\n' << m.rank << '\n';
	return o;
}