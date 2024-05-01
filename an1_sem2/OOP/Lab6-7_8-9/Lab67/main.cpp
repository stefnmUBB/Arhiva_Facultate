#include "defines.h"

#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

#ifdef TEST

#include "./Tests/test_all.h"

int main()
{
	test_all();
	_CrtDumpMemoryLeaks();
}

#else

#include "./UI/ui.h"
#include "./Service/cars_service.h"

#include <iostream>
#include <random>
#include <algorithm>
using namespace std;

void add_car(CarsService& service)
{
	string number_plate;
	string producer;
	string model;
	string type;
	cout << "Number plate : ";
	cin >> number_plate;

	cout << "Producer     : ";
	cin >> producer;

	cout << "Model        : ";
	cin >> model;

	cout << "Type         : ";
	cin >> type;

	service.add_car(number_plate, producer, model, type);
	cout << "Car successfully added\n";
}

void edit_car(CarsService& service)
{
	int id;
	cout << "Id of car      : ";
	cin >> id;	

	string number_plate;
	string producer;
	string model;
	string type;
	cout << "Number plate : ";
	cin >> number_plate;

	cout << "Producer     : ";
	cin >> producer;

	cout << "Model        : ";
	cin >> model;

	cout << "Type         : ";
	cin >> type;

	try
	{
		service.edit_car(id, number_plate, producer, model, type);
		cout << "Operation successful\n";
	}
	catch (id_not_found_exception e)
	{
		cout << e.what();
	}
}

void remove_car(CarsService& service)
{
	int id;
	cout << "Id of car      : ";
	cin >> id;
	
	if (service.remove_car(id))
	{
		cout << "Operation successful\n";
	}
	else
	{
		cout << "Car ID not found\n";
	}	
}

function<void(const Car&)> print_car_func = [](const Car& car)
{
	printf("%5i %15s %15s %15s %15s\n", 
		car.get_id(),
		car.get_number_plate().get_value().c_str(),
		car.get_producer().c_str(),
		car.get_model().c_str(),
		car.get_type().c_str());
};

void print_cars(CarsService& service)
{	
	service.for_each(print_car_func);
	std::cout << "\n--- REPORT ---\n";
	ReportMap report = service.report_types();
	std::for_each(report.begin(), report.end(), [](const auto& kv)
		{
			std::cout << kv.second << '\n';
		});
}

#define filter_function(prop) [&service]() { \
	string val; \
	string field = #prop ; \
	cout << field <<" name : "; \
	cin >> val; \
	service.filter([&val](const Car& car) { return car.get_##prop##() == val; }, print_car_func); \
} 

#define sort_function(prop) [&service]() { service.sort(CarsService::compare_by_##prop##, print_car_func);} 

void search_car(CarsService& service)
{
	string number_plate;
	string producer;
	string model;
	string type;
	cout << "Input search parameters (\"*\" if field not included in search):\n";
	cout << "Number plate : ";
	cin >> number_plate;

	cout << "Producer     : ";
	cin >> producer;

	cout << "Model        : ";
	cin >> model;

	cout << "Type         : ";
	cin >> type;

	try
	{
		Car car = service.find_car(number_plate, producer, model, type);
		cout << "Car succsessfully found:\n";
		print_car_func(car);
	}
	catch (car_not_found_exception e)
	{
		cout << e.what() << '\n';
	}	
}

#define wash_list_op(f) \
	[&service]() { f(service); std::cout<<"Cars in wash list: "<<service.wash_list_count()<<'\n'; }

void wl_clear(CarsService& service)
{
	service.clear_wash_list();
}

void wl_generate(CarsService& service)
{
	std::cout << "Generate wash list of length : ";
	size_t len;
	std::cin >> len;	
	service.populate_wash_list(len);
}

void wl_add(CarsService& service)
{
	std::cout << "Add to wash list car with number plate : ";
	string nps;
	std::cin >> nps;
	service.add_to_wash_list(nps);
}

void run()
{
	cout << "Lab 8-9\n";
	CarsService service;
	//service.debug_populate_random(50);

	ui_node root("[ROOT]", NULL);

	ui_node opt_add_car("Add car", [&service]() {add_car(service); });
	ui_node opt_edit_car("Edit car", [&service]() {edit_car(service); });
	ui_node opt_remove_car("Remove car", [&service]() {remove_car(service); });
	ui_node opt_print_cars("Print cars", [&service]() {print_cars(service); });
	ui_node opt_find_car("Find car", [&service]() {search_car(service); });

	ui_node opt_filter("Filter cars", NULL);

	ui_node opt_filter_producer("Filter cars by producer", filter_function(producer));
	ui_node opt_filter_type("Filter cars by type", filter_function(type));

	opt_filter.add(&opt_filter_producer);
	opt_filter.add(&opt_filter_type);

	ui_node opt_sort("Sort cars", NULL);

	ui_node opt_sort_number_plate("Sort by number plate", sort_function(number_plate));
	opt_sort.add(&opt_sort_number_plate);

	ui_node opt_sort_type("Sort by type", sort_function(type));
	opt_sort.add(&opt_sort_type);

	ui_node opt_sort_prod_model("Sort by producer & model", sort_function(prod_model));
	opt_sort.add(&opt_sort_prod_model);


	ui_node opt_wash_list("Wash cars", NULL);

	ui_node opt_wash_list_clear("Clear wash list", wash_list_op(wl_clear));
	ui_node opt_wash_list_gen("Generate wash list", wash_list_op(wl_generate));
	ui_node opt_wash_list_add("Add car to wash list", wash_list_op(wl_add));

	opt_wash_list.add(&opt_wash_list_clear);
	opt_wash_list.add(&opt_wash_list_gen);
	opt_wash_list.add(&opt_wash_list_add);

	ui_node opt_undo("Undo", [&service]() {service.undo(); cout << "Complete!\n"; });
	ui_node opt_html("Export as HTML", [&service]() {service.export_html(); cout << "Complete!\n"; });
	ui_node opt_exit("Exit", []() noexcept { exit(0); });

	root.add(&opt_add_car);
	root.add(&opt_edit_car);
	root.add(&opt_remove_car);
	root.add(&opt_print_cars);
	root.add(&opt_filter);
	root.add(&opt_sort);
	root.add(&opt_find_car);
	root.add(&opt_wash_list);
	root.add(&opt_undo);
	root.add(&opt_html);
	root.add(&opt_exit);

	UI ui(&root);

	while (1)
	{
		try
		{
			ui.select_option();
		}
		catch (const read_com_exception& e)
		{
			cin.clear();
			cin.ignore();
			cout << "An error has occured :\n" << e.what() << "\n";
		}
		catch (const std::out_of_range& e)
		{
			cout << "An error has occured :\n" << e.what() << "\n";
			getchar();
			getchar();
		}
		catch (const exception& e)
		{
			cout << "An error has occured :\n" << e.what() << "\n";
			getchar();
			getchar();
		}	
	}
}

int main()
{
	run();
	_CrtDumpMemoryLeaks();

	return 0;
}

#endif