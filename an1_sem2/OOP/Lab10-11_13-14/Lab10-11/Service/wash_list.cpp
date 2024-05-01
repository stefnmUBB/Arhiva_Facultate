#include "wash_list.h"
#include <chrono>
#include <fstream>
#include <random>

WashList::WashList(List<Car>& repo) : repo(repo) { }

size_t WashList::count() const noexcept
{
	return list.size();
}

void WashList::clear() noexcept
{
	list.clear();
	notify();
}

void WashList::add(std::string number_plate)
{
	NumberPlate(number_plate).check_valid();
	if (std::find(list.begin(), list.end(), number_plate) != list.end())
	{
		throw car_already_in_wash_list_exception(("Car with number plate" + number_plate + " already in wash list").c_str());
	}
	// throws if car not found:
	repo.find([&number_plate](const Car& _c) {return _c.get_number_plate().get_value() == number_plate; });

	list.push_back(number_plate);
	notify();
}

void WashList::populate(size_t count)
{
	if (count > repo.size())
	{
		throw out_of_range("Too many cars");
	}

	vector<string> copy_plates;
	const vector<Car>& container = repo.get_container_const();
	std::for_each(container.begin(), container.end(), [&copy_plates](const Car& c)
		{
			copy_plates.push_back(c.get_number_plate().get_value());
		});

	const uint32_t seed = static_cast<uint32_t>(std::chrono::system_clock::now().time_since_epoch().count());
	std::shuffle(copy_plates.begin(), copy_plates.end(), std::default_random_engine(seed));

	list.clear();

	std::copy(copy_plates.begin(), copy_plates.begin() + count, std::back_inserter(list));
	notify();
}

std::vector<Car> WashList::get_cars()
{		
	vector<Car> result;

	for (const string& plate : list)
	{
		Car c = repo.find([&plate](const Car& _c) {return _c.get_number_plate().get_value() == plate; });
		result.push_back(c);
	}

	return result;
}

void WashList::remove(std::string number_plate)
{
	std::remove(list.begin(), list.end(), number_plate);
}

void WashList::export_html(string fname)
{
	if (fname == "") fname = "cars_list_dump.html";
	ofstream html(fname);
	html << "<h1>List of registered cars</h1>";
	html << "<table>";
	auto res = get_cars();
	std::for_each(res.begin(), res.end(), [&html, this](const Car& car)
		{			
			html << "<tr>";
			html << "<td>" << car.get_id() << "</td>";
			html << "<td>" << car.get_number_plate().get_value() << "</td>";
			html << "<td>" << car.get_producer() << "</td>";
			html << "<td>" << car.get_model() << "</td>";
			html << "<td>" << car.get_type() << "</td>";
			html << "</tr>";
		});
	html << "</table>";
	html.close();
}