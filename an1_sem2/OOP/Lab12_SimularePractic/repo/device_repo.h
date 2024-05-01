#pragma once

#include "../Users/Stefan/source/repos/practic-app/practic-app/domain/device.h"

#include <vector>

class DeviceRepo
{
private:
	std::string filename;
	std::vector<Device> items;
public:
	DeviceRepo(std::string _filename);
	const std::vector<Device>& get_items() const;
};

