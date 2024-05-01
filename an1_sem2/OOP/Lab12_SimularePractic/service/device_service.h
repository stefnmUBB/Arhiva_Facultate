#pragma once

#include "../Users/Stefan/source/repos/practic-app/practic-app/repo/device_repo.h"

#include <functional>

class DeviceService
{
private:
	DeviceRepo& repo;
public:
	DeviceService(DeviceRepo& _repo);

	const std::vector<Device>& get_items() const;

	std::vector<Device> filter(std::function<bool(const Device&)> sel);	
};

