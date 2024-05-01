#include "car.h"
#include <iostream>
#include <sstream>

Car::Car(const NumberPlate& number_plate, const string& producer, const string& model, const string& type)
{
	this->number_plate = number_plate;
	this->producer = producer;
	this->model = model;
	this->type = type;
}

Car::Car(const Car& car)
{
	this->set_id(car.get_id());
	this->number_plate = car.number_plate;
	this->producer = car.producer;
	this->model = car.model;
	this->type = car.type;
	std::cout << "!!! A car copy was made\n";
}

const NumberPlate& Car::get_number_plate() const noexcept { return number_plate; }

const string& Car::get_producer() const noexcept { return producer; }

const string& Car::get_model() const noexcept { return model; }

const string& Car::get_type() const noexcept { return type; }

bool Car::operator == (const Car& car)
{
	return number_plate == car.number_plate
		&& producer == car.producer
		&& model == car.model
		&& type == car.type
		&& get_id() == car.get_id();
}

void Car::check_valid() const
{
	number_plate.check_valid();
	if (model == "")
		throw empty_string_exception("Car model cannot be empty");
	if (producer == "")
		throw empty_string_exception("Car producer cannot be empty");
	if (type == "")
		throw empty_string_exception("Car type cannot be empty");
}


ostream& operator << (ostream& o, const Car& c)
{
	o << "[" << c.get_id() << "," << c.get_number_plate().get_value() << "," << c.get_producer() << ","
		<< c.get_model() << "," << c.get_type() << "]";
	return o;
}

istream& operator >> (istream& i, Car& c)
{	
	string input;
	getline(i, input);
	if (input == "")
	{
		i.setstate(std::ios_base::failbit);
		return i;
	}
	//for (char ch; i.peek() != '['; (i >> ch));
	/*if (i.peek() != '[')
	{
		i.setstate(std::ios_base::failbit);
		return i;
		//throw car_read_exception("Failed to read car: [ not found");
	}
	for (char ch=0; (i.peek() != '\n' && i.peek() != ']') && (i >> ch);)
	{
		input += ch;
	}*/
	//char ch; i >> ch;
	//if (ch != ']')
		//throw car_read_exception("Failed to read car: ] not found");
	std::cout << input << '\n';
	
	input = input.substr(1, input.size() - 2);

	stringstream ss(input);
	vector<string> fields;

	while (ss.good())
	{
		string substr;
		getline(ss, substr, ',');
		fields.push_back(substr);
	}

	if (fields.size() != 5)
		throw car_read_exception("Failed to read car: Wrong number of fields");

	c = Car(NumberPlate(fields.at(1)), fields.at(2), fields.at(3), fields.at(4));
	c.set_id(stoi(fields.at(0), nullptr));
	return i;
}

car_read_exception::car_read_exception(const char* msg) noexcept : std::exception(msg) {}
