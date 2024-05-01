#pragma once

#include <string>
#include "../Domain/report_item.h"

using namespace std;

class NumberPlate
{
private:
	string value;
public:

	NumberPlate() noexcept = default;

	/// <summary>
	/// Creates a number plate from a string
	/// </summary>
	/// <param name="value">Number plate value</param>
	NumberPlate(string value);

	NumberPlate(const NumberPlate& nb);

	/// <summary>
	/// Checks if two number plates are identical
	/// </summary>
	/// <param name="np">Number plate to compare to</param>
	/// <returns>true if number plates are equal, false otherwise</returns>
	bool operator == (const NumberPlate& np);

	/// <summary>
	/// Checks if two number plates are different
	/// </summary>
	/// <param name="np">Number plate to compare to</param>
	/// <returns>true if number plates are not equal, false otherwise</returns>
	bool operator != (const NumberPlate& np);
	
	/// <returns>Number plate value</returns>
	string get_value() const;

	void check_valid() const;

	static NumberPlate generate_random();
};

class number_plate_exception : public std::exception
{
public: number_plate_exception(const char* msg) noexcept : std::exception(msg) { }
};

