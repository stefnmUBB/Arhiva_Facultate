#pragma once

enum class CommMessageType
{
	Connect,
	Lines20,
	ReadEnd,
	FileFragment,
	FileEnd = -1,
	RequestCountriesRanking,
	CountriesRanking,
	RequestFullRanking,
	FullRanking,
	Close
};