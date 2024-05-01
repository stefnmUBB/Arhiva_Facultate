#include "number_plate.h"
#include <iostream>
#include <chrono>
#include <random>

NumberPlate::NumberPlate(string value)
{
	this->value = value;
}

NumberPlate::NumberPlate(const NumberPlate& nb)
{
	this->value = nb.value;
	std::cout << "!!! A number plate copy was made\n";
}

string NumberPlate::get_value() const
{
	return value;
}

bool NumberPlate::operator == (const NumberPlate& np)
{
	return value == np.get_value();
}

bool NumberPlate::operator != (const NumberPlate& np)
{
	return value != np.get_value();
}

void NumberPlate::check_valid() const 
{
	if (value.size() != 6 && value.size() != 7)
		throw number_plate_exception("Wrong number plate length");
	if (value.size() == 6)
	{
		if (!isalpha(value.at(0)) || !isdigit(value.at(1)) || !isdigit(value.at(2))
			|| !isalpha(value.at(3)) || !isalpha(value.at(4)) || !isalpha(value.at(5)))
			throw number_plate_exception("Wrong number plate code ");
		if (value.at(0) != 'B')
			throw number_plate_exception("6-length number palts must start with B");
	}
	else if (value.size() == 7)
	{
		if (value.at(0) == 'B')
		{
			if (!isalnum(value.at(1)) || !isdigit(value.at(2)) || !isdigit(value.at(3))
				|| !isalpha(value.at(4)) || !isalpha(value.at(5)) || !isalpha(value.at(6)))
				throw number_plate_exception("Wrong number plate code ");
		}
		else
		{
			if (!isalpha(value.at(0)) || !isalpha(value.at(1)) || !isdigit(value.at(2)) || !isdigit(value.at(3))
				|| !isalpha(value.at(4)) || !isalpha(value.at(5)) || !isalpha(value.at(6)))
				throw number_plate_exception("Wrong number plate code ");
		}
	}

}

string JJ[] = {
	"AB", "AR", "AG", "BC", "BH", "BN", "BT", "BV", "BR", "BZ", "CS", "CL", "CJ",
	"CT", "CV", "DB", "DJ", "GL", "GR", "GJ", "HR", "HD", "IL", "IS", "IF", "MM",
	"MH", "MS", "NT", "OT", "PH", "SM", "SJ", "SB", "SV", "TR", "TM", "TL", "VS", "VL", "VN"
};


NumberPlate NumberPlate::generate_random()
{	
	const string jj = JJ[std::rand() % 41];
	string nn = to_string(std::rand() % 99 + 1);
	string lll = "";
	for (int i = 3; i--;)
	{		
		char c = 'A' + (rand() % 26);
		lll += c;
	}
	if (nn.size() == 1) nn = "0" + nn;
	return NumberPlate(jj + nn + lll);
}