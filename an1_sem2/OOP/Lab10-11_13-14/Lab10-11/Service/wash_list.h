#pragma once
#include "../Repo/list_storage.h"
#include "../Domain/car.h"
#include "../ui/observer.h"

#include <vector>

class WashList : public Observable
{
private:
	List<Car>& repo;
	std::vector<std::string> list;
public:
	WashList(List<Car>& repo);
	size_t count() const noexcept;

	void clear() noexcept;

	void add(std::string number_plate);

	void populate(size_t count);

	void remove(std::string number_plate);

	std::vector<Car> get_cars();

	void export_html(string fname = "");
};

class car_already_in_wash_list_exception :public exception
{
public:
	car_already_in_wash_list_exception(const char* msg) noexcept : exception(msg) {}
};
