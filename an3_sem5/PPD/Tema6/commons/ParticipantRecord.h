#pragma once

#include <iostream>

struct ParticipantRecordPair
{
	int id;
	int points;

	friend std::istream& operator >> (std::istream& i, ParticipantRecordPair& p)
	{
		return i >> p.id >> p.points;
	}

	friend std::ostream& operator << (std::ostream& o, const ParticipantRecordPair& p)
	{
		return o << p.id << " " << p.points;
	}


};

struct ParticipantRecordTuple
{
	int id_country;
	int id_part;
	int points;

	ParticipantRecordTuple(int id_country=0, int id_part=0, int points=0):id_country(id_country), id_part(id_part), points(points) { }
	ParticipantRecordTuple(int id_country, const ParticipantRecordPair& p) : ParticipantRecordTuple(id_country, p.id, p.points) { }

	friend std::ostream& operator << (std::ostream& o, const ParticipantRecordTuple& p)
	{
		return o << p.id_country << " " << p.id_part << " " << p.points;
	}
};

struct Pairs20
{
	int count = 0;
	ParticipantRecordPair pairs[20];
};

struct CountryRecord
{
	int country_id;
	int p;
};