#include "device_repo.h"

#include <fstream>
#include <exception>

DeviceRepo::DeviceRepo(std::string _filename)
{
	filename = _filename;
	std::ifstream in(filename);
	if (in.fail())
	{
		in.close();
		throw std::exception("Cannot open repo file");				
	}
	Device d;
	while (in >> d)
	{
		items.push_back(d);
	}

	in.close();
}

const std::vector<Device>& DeviceRepo::get_items() const
{
	return items;
}