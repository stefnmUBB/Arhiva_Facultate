#include "device_service.h"

DeviceService::DeviceService(DeviceRepo& _repo) : repo(_repo) { }

const std::vector<Device>& DeviceService::get_items() const
{
	return repo.get_items();
}

std::vector<Device> DeviceService::filter(std::function<bool(const Device&)> sel)
{
	std::vector<Device> result;
	const std::vector<Device>& items = get_items();
	for (const Device& dev : items)
	{
		if (sel(dev))
		{
			result.push_back(dev);
		}
	}
	return result;
}