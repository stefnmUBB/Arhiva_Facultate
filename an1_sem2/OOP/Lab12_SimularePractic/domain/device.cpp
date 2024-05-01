#include "device.h"

Device::Device(std::string _type, std::string _model, std::string _color, int _price, int _year)
	:type(_type), model(_model), color(_color), price(_price), year(_year)
{

}

std::string Device::get_type() const { return type; }

std::string Device::get_model() const { return model; }
std::string Device::get_color() const { return color; }
int Device::get_price() const { return price; }
int Device::get_year() const { return year; }

void Device::set_type(std::string _type) { type = _type; }
void Device::set_model(std::string _model) { model = _model; }
void Device::set_color(std::string _color) { color = _color; }
void Device::set_price(int _price) { price = _price; }
void Device::set_year(int _year) { year = _year; }

std::istream& operator >> (std::istream& in, Device& d)
{	
	std::string sprice, syear;
	std::getline(in, d.type);
	std::getline(in, d.model);
	std::getline(in, d.color);			
	std::getline(in, sprice);			
	std::getline(in, syear);			
	d.set_price(std::atoi(sprice.c_str()));
	d.set_year(std::atoi(syear.c_str()));	
	return in;
}