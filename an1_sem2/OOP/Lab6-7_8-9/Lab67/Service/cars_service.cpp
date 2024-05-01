#include "cars_service.h"

#include <vector>
#include <queue>
#include <algorithm>

#include <random>
#include <chrono>

#include <iomanip>
#include <memory>

std::map<string, std::map<string, string>> car_categories
{
	{
		"Audi",
		{
			{"A4 Allroad", "Station Wagon"},
			{"A5", "Coupe"}
		}
	},
	{
		"Chevrolet",
		{
			{"Corvette", "Coupe"}
		}
	},
	{
		"Dacia",
		{
			{"1300", "Large family car"},
			{"1310", "Large family car"},
			{"Pick-Up", "Pick up truck"},
		}
	},
	{
		"Ford",
		{
			{"Mustang", "Coupe"}
		}
	},

};

void CarsService::debug_populate_random(size_t cnt)
{
	cars_list.get_container().clear();
	while (cnt > 0)
	{
		NumberPlate np = NumberPlate::generate_random();
		auto item = car_categories.begin();
		std::advance(item, rand() % car_categories.size());
		string prod = item->first;
		auto lst = item->second;
		auto jtem = lst.begin();

		std::advance(jtem, rand() % lst.size());

		string model = jtem->first;
		string type = jtem->second;
		
		try
		{
			add_car(np.get_value(), prod, model, type);
		}
		catch (exception e)
		{
			continue;
		}		
		cnt--;
	}
}

size_t CarsService::wash_list_count() const noexcept
{
	return wash_list.size();
}

void CarsService::clear_wash_list() noexcept
{
	wash_list.clear();
}

void CarsService::add_to_wash_list(string number_plate)
{
	NumberPlate(number_plate).check_valid();
	try
	{
		find_car(number_plate, "*", "*", "*");
	}
	catch (car_not_found_exception ex)
	{
		throw car_not_found_exception(("No car with numer plate " + number_plate + " has been registered.").c_str());
	}
	if (std::find(wash_list.begin(), wash_list.end(), number_plate) != wash_list.end())
	{
		throw car_already_in_wash_list_exception(("Car with number plate" + number_plate + " already in wash list").c_str());
	}
	wash_list.push_back(number_plate);
}

void CarsService::populate_wash_list(size_t count)
{
	if (count > get_cars_count())
	{
		throw out_of_range("Too many cars");
	}
	
	vector<string> copy_plates;
	const vector<Car>& container = cars_list.get_container_const();
	std::for_each(container.begin(), container.end(), [&copy_plates](const Car& c)
		{
			copy_plates.push_back(c.get_number_plate().get_value());
		});

	const uint32_t seed = static_cast<uint32_t>(std::chrono::system_clock::now().time_since_epoch().count());
	std::shuffle(copy_plates.begin(), copy_plates.end(), std::default_random_engine(seed));

	wash_list.clear();

	std::copy(copy_plates.begin(), copy_plates.begin() + count, std::back_inserter(wash_list));
}

void CarsService::add_car(string plate, string producer, string model, string type)
{
	Car new_car(NumberPlate(plate), producer, model, type);
	validate(new_car, "add");
	cars_list.add(new_car);		
	undo_list.push(make_unique<UndoAdd>(cars_list, new_car));
}

bool CarsService::remove_car(int id)
{
	int pos;
	if ((pos=cars_list.find_by_id(id)) != -1)
	{
		undo_list.push(make_unique<UndoRemove>(cars_list, cars_list[pos]));
	}
	return cars_list.remove_by_id(id);
}

void CarsService::edit_car(int id, string plate, string producer, string model, string type)
{
	const Car& old_car = get_car_by_id(id);
	Car updated_car(
		NumberPlate(plate != "" ? plate : old_car.get_number_plate()),
		producer != "" ? producer : old_car.get_producer(),
		model != "" ? model : old_car.get_model(),
		type != "" ? type : old_car.get_type());
	validate(updated_car, "edit" + to_string(id));
	//int pos;
	//if ((pos = cars_list.find_by_id(id)) != -1)
	{		
		undo_list.push(make_unique<UndoEdit>(cars_list, old_car));
	}	
	cars_list.update(id, updated_car);
}

void CarsService::for_each(ParseCarFunc do_with_car)
{
	cars_list.for_each(do_with_car);
}

size_t CarsService::get_cars_count() const noexcept
{
	return cars_list.size();
}

const Car& CarsService::get_car_by_id(int id) const
{
	const int index = cars_list.find_by_id(id);
	if (index < 0)
	{
		throw id_not_found_exception("There is no record with that car ID.");
	}
	return cars_list[index];
}

void CarsService::filter(const CarFilterFunc& filter_func, const ParseCarFunc& do_with_car)
{
	vector<Car>& container = cars_list.get_container();
	std::vector<Car> filter_result;
	std::copy_if(container.begin(), container.end(), std::back_inserter(filter_result), filter_func);
	std::for_each(filter_result.begin(), filter_result.end(), do_with_car);
}

void CarsService::sort(const CarCompFunc& comp_func, const ParseCarFunc& do_with_car)
{	
	vector<Car>& container = cars_list.get_container();
	std::vector<Car> sort_list;

	std::copy(container.begin(), container.end(), std::back_inserter(sort_list));	

	std::sort(sort_list.begin(), sort_list.end(), comp_func);

	std::for_each(sort_list.begin(), sort_list.end(), do_with_car);
}

const Car& CarsService::find_car(string number_plate, string producer, string model, string type)
{	
	vector<Car>& container = cars_list.get_container();
	const std::vector<Car>::iterator& iter = std::find_if(container.begin(), container.end(), 
		[&number_plate, &producer, &model, &type](const Car& car) 
		{
			bool found = true;
			if (number_plate != "*")
				found &= car.get_number_plate().get_value() == number_plate;
			if (producer != "*")
				found &= car.get_producer() == producer;
			if (model != "*")
				found &= car.get_model() == model;
			if (type != "*")
				found &= car.get_type() == type;
			return found;
		});

	std::cout << (iter - container.begin())<<'\n';
	if (iter != container.end()) // valid
	{
		return *iter; // ?
	}
	throw car_not_found_exception("Car with given properties was not found");
}

bool CarsService::compare_by_number_plate(const Car& c1, const Car& c2)
{
	return c1.get_number_plate().get_value() < c2.get_number_plate().get_value();
}

bool CarsService::compare_by_type(const Car& c1, const Car& c2) noexcept
{
	return c1.get_type() < c2.get_type();
}

bool CarsService::compare_by_prod_model(const Car& c1, const Car& c2) noexcept
{
	if (c1.get_producer() != c2.get_producer())
	{
		return c1.get_producer() < c2.get_producer();
	}
	return c1.get_model() < c2.get_model();
}

void CarsService::validate(const Car& c, const string& scope) const
{
	c.check_valid();
	if (scope == "add")
	{
		const vector<Car>& container = cars_list.get_container_const();
		std::for_each(container.begin(), container.end(), [&c](const Car& item)
			{
				if (item.get_number_plate().get_value() == c.get_number_plate().get_value())
				{
					throw std::exception("Duplicate number plates");
				}
			});
	}
	else if (scope.substr(0, 4) == "edit")
	{
		int id = stoi(scope.substr(4));

		const vector<Car>& container = cars_list.get_container_const();
		std::for_each(container.begin(), container.end(), [&c, &id](const Car& item)
			{
				if (item.get_id() != id && item.get_number_plate().get_value() == c.get_number_plate().get_value())
				{
					throw std::exception("Duplicate number plates");
				}
			});
	}
}

CarsService::~CarsService()
{
	//cars_list.custom_delete();
}

ReportMap CarsService::report_types()
{
	ReportMap report;

	const vector<Car>& container = cars_list.get_container_const();
	std::for_each(container.begin(), container.end(), [&report](const Car& c)
		{			
			report.add(c.get_type());
		});
	return report;
}

CarsService::CarsService(string fname) 
{
	cars_list.set_file(fname);
}

UndoAdd::UndoAdd(List<Car>& cars_list, const Car& target_car) : cars_list(cars_list), target_car(target_car) {}

void UndoAdd::do_undo() 
{
	cars_list.remove_by_id(target_car.get_id());
}

UndoEdit::UndoEdit(List<Car>& cars_list, const Car& target_car) : cars_list(cars_list), target_car(target_car) {}

void UndoEdit::do_undo()
{	
	cars_list.update(target_car.get_id(), target_car);
}

UndoRemove::UndoRemove(List<Car>& cars_list, const Car& target_car) : cars_list(cars_list), target_car(target_car) {}

void UndoRemove::do_undo()
{	
	cars_list.add(target_car);
}


void CarsService::undo()
{
	if (undo_list.empty())
	{
		throw undo_exception("Undo list is empty");
	}
	auto undo_act = std::move(undo_list.top());
	undo_list.pop();
	undo_act.get()->do_undo();
}

void CarsService::export_html(string fname)
{
	if (fname == "") fname = "cars_list_dump.html";
	ofstream html(fname);
	html << "<h1>List of registered cars</h1>";
	html << "<table>";
	for_each([&html](const Car& car)
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