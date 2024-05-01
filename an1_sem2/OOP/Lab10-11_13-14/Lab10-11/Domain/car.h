#pragma once

#include <string>
#include "number_plate.h"
#include "list_item.h"

using namespace std;

class Car : public ListItem
{
private:
	NumberPlate number_plate;
	string producer;
	string model;
	string type;
public:
	/// <summary>
	/// Creates new car instance
	/// </summary>
	/// <param name="number_plate">Car number plate</param>
	/// <param name="producer">Car producer</param>
	/// <param name="model">Car model</param>
	/// <param name="type">Car type</param>
	Car(const NumberPlate& number_plate, const string& producer, const string& model, const string& type);

	Car() = default;

	Car(const Car& car);

	~Car() = default;

	/// <returns>Car number plate</returns>
	const NumberPlate& get_number_plate() const noexcept;

	/// <returns>Car producer</returns>
	const string& get_producer() const noexcept;

	/// <returns>Car model</returns>
	const string& get_model() const noexcept;

	/// <returns>Car type</returns>
	const string& get_type() const noexcept;

	/// <summary>
	/// Checks if two car entities are identical
	/// </summary>
	/// <param name="car">Car to compare to</param>
	/// <returns>true if the cars are identical</returns>
	bool operator == (const Car& car);

	void check_valid() const;	
};

istream& operator >> (istream& i, Car& c);
ostream& operator << (ostream& o, const Car& c);

class car_read_exception : public std::exception
{
public: car_read_exception(const char* msg) noexcept;
};

class empty_string_exception : public std::exception
{
public: empty_string_exception(const char* msg) noexcept : std::exception(msg)  {}
};
