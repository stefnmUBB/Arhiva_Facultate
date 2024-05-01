#pragma once

#include <string>
#include <iostream>
#include <fstream>
#include <string>

class Device
{
private:
	std::string type;
	std::string model;
	std::string color;
	int year = 0;
	int price = 0;
public:
	Device(std::string _type, std::string _model, std::string _color, int _price, int _year);

	Device() = default;
	
	std::string get_type() const;
	std::string get_model() const;
	std::string get_color() const;
	int get_price() const;
	int get_year() const;

	void set_type(std::string _type);
	void set_model(std::string _model);
	void set_color(std::string _color);
	void set_price(int _price);
	void set_year(int _price);

	friend std::istream& operator >> (std::istream& in, Device& d);
};



