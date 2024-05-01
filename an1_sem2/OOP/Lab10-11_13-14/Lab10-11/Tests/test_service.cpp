#include "test_service.h"

#include "../Service/cars_service.h"
#include <cassert>
#include <stdexcept> 
#include <stdio.h> 

void _clear_file(string fname)
{
	ofstream g(fname);
	g.close();
	remove(fname.c_str());
}

void test_service_storage()
{
	CarsService cs("test_cars_storage.txt");
	assert(cs.get_cars_count() == 47);
	try{CarsService cs2("test_cars_wrong.txt"); assert(false);}catch (car_read_exception) {}
}

void test_service_add()
{
	_clear_file("cars_test_add.txt");
	CarsService cs("cars_test_add.txt");

	cs.add_car("AA01ZZZ", "P1", "M1", "T1");
	assert(cs.get_cars_count() == 1);

	cs.add_car("BB01ZZZ", "P2", "M2", "T2");
	assert(cs.get_cars_count() == 2);

	try { cs.add_car("BB01ZZZ", "P2", "M2", "T2"); assert(false); } catch (exception) { assert(true); }

	try { cs.add_car("BB01AZZ", "", "M2", "T2"); assert(false); } catch (exception) { assert(true); }

	try { cs.add_car("BB01BZZ", "P2", "", "T2"); assert(false); } catch (exception) { assert(true); }

	try { cs.add_car("BB01BZZ", "P2", "M2", ""); assert(false); } catch (exception) { assert(true); }
}

void test_service_edit()
{
	_clear_file("cars_test_edit.txt");
	CarsService cs("cars_test_edit.txt");

	cs.add_car("AA01ZZZ", "P1", "M1", "T1");
	cs.add_car("BB01ZZZ", "P2", "M2", "T2");

	cs.edit_car(0, "CC00DDD", "", "", "Tx");
	assert(cs.get_car_by_id(0).get_number_plate().get_value() == "CC00DDD");

	try { cs.edit_car(5, "", "P1", "", "T3");	assert(false); }
	catch (id_not_found_exception) { assert(true); }

	try { cs.edit_car(0, "BB01ZZZ", "P2", "M2", "T2"); assert(false); }
	catch (exception) { assert(true); }
	
}

void test_service_remove()
{
	_clear_file("cars_test_rem.txt");
	CarsService cs("cars_test_rem.txt");

	cs.add_car("AA01ZZZ", "P1", "M1", "T1");	
	cs.add_car("BB01ZZZ", "P2", "M2", "T2");	

	assert(cs.remove_car(0));
	assert(cs.get_cars_count() == 1);
	assert(cs.remove_car(1));
	assert(!cs.remove_car(0));
}

void test_service_get_car_by_id()
{
	_clear_file("cars_test_get_by_id.txt");
	CarsService cs{ "cars_test_get_by_id.txt" };

	cs.add_car("AA01ZZZ", "P1", "M1", "T1");
	cs.add_car("BB01ZZZ", "P2", "M2", "T2");

	assert(cs.get_car_by_id(0).get_number_plate().get_value() == "AA01ZZZ");

	try { cs.get_car_by_id(5);	assert(false); }
	catch (id_not_found_exception) { assert(true); }
}

void test_service_filter()
{
	_clear_file("cars_test_filter.txt");
	CarsService cs{ "cars_test_filter.txt" };

	cs.add_car("AA01ZZZ", "P1", "M1", "T1");
	cs.add_car("BB01ZZZ", "P2", "M2", "T2");
	cs.add_car("CC01ZZZ", "P3", "M2", "T2");
	cs.add_car("DD01ZZZ", "P2", "M2", "T2");
	cs.add_car("EE01ZZZ", "P2", "M2", "T2");
	cs.add_car("FF01ZZZ", "P1", "M2", "T2");

	int cnt = 0;
	string prod = "P2";
	cs.filter([&prod](const Car& car) noexcept { return car.get_producer() == prod; }, [&cnt](const Car&) noexcept {cnt++; });
	assert(cnt == 3);

}

void test_service_sort()
{
	_clear_file("cars_test_sort.txt");
	CarsService cs{ "cars_test_sort.txt" };

	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	string ids = "";
	cs.sort(
		CarsService::compare_by_number_plate,
		[&ids](const Car& c) {ids += c.get_number_plate().get_value().at(3); });
	assert(ids == "123456");

	ids = "";
	cs.sort(
		CarsService::compare_by_type,
		[&ids](const Car& c) {ids += c.get_number_plate().get_value().at(3); });
	assert(ids == "346215");

	ids = "";
	cs.sort(
		CarsService::compare_by_prod_model,
		[&ids](const Car& c) {ids += c.get_number_plate().get_value().at(3); });
	assert(ids == "534162");
}

void test_service_for_each()
{
	_clear_file("cars_test_for_each.txt");
	CarsService cs{ "cars_test_for_each.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	int cnt = 0;
	cs.for_each([&cnt](const Car&) noexcept { cnt++; });
	assert(cnt == 6);
}

void test_service_find()
{
	_clear_file("cars_test_find.txt");
	CarsService cs{ "cars_test_find.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	assert(cs.find_car("AA05ZZZ", "*", "*", "*").get_number_plate().get_value() == "AA05ZZZ");
	assert(cs.find_car("*", "P1", "M2", "*").get_number_plate().get_value() == "AA03ZZZ");
	assert(cs.find_car("*", "*", "*", "T1").get_number_plate().get_value() == "AA03ZZZ");

	try { cs.find_car("*", "*", "*", "T7"); assert(false); }
	catch (car_not_found_exception) { assert(true); }
}

void test_service_wash_list_clear()
{
	_clear_file("cars_test_wl_clear.txt");
	CarsService cs{ "cars_test_wl_clear.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	WashList wash_list(cs.get_repo());

	wash_list.clear();	

	assert(wash_list.count() == 0);
}

void test_service_wash_list_add()
{
	_clear_file("cars_test_wl_add.txt");
	CarsService cs{ "cars_test_wl_add.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	WashList wash_list(cs.get_repo());
	wash_list.add("AA05ZZZ");
	
	assert(wash_list.count() == 1);

	try { wash_list.add("AA05ZZZ"); assert(false); } catch (car_already_in_wash_list_exception) { assert(true); }
	try { wash_list.add("ZZ05ZZZ"); assert(false); } catch (car_not_found_exception) { assert(true); }

}

void test_service_wish_list_populate()
{
	_clear_file("cars_test_wl_populate.txt");
	CarsService cs{ "cars_test_wl_populate.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T6");
	cs.add_car("AA01ZZZ", "P2", "M2", "T5");
	cs.add_car("AA02ZZZ", "P3", "M2", "T4");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	WashList wash_list(cs.get_repo());

	try {wash_list.populate(10); assert(false); } catch (std::out_of_range) { assert(true); }

	wash_list.populate(5);
	assert(wash_list.count() == 5);
}

void test_service_debug_populate_random()
{
	_clear_file("cars_test_populate_random.txt");
	CarsService cs{ "cars_test_populate_random.txt" };
	cs.debug_populate_random(10);
	assert(cs.get_cars_count() == 10);
}

void test_service_report()
{
	_clear_file("cars_test_service_report.txt");
	CarsService cs{ "cars_test_service_report.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T5");
	cs.add_car("AA01ZZZ", "P2", "M2", "T3");
	cs.add_car("AA02ZZZ", "P3", "M2", "T3");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	std::map<std::string, ReportItem> rep = cs.report_types();
	assert(rep.size() == 4);
	assert(rep["T1"].second == 1);
	assert(rep["T2"].second == 1);
	assert(rep["T3"].second == 3);
	assert(rep["T5"].second == 1);
}

void test_service_undo()
{
	_clear_file("cars_test_undo.txt");
	CarsService cs{ "cars_test_undo.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T5");
	cs.add_car("AA01ZZZ", "P2", "M2", "T3");
	cs.add_car("AA02ZZZ", "P3", "M2", "T3");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");

	cs.undo();	
	assert(cs.get_cars_count() == 5);
	
	try { cs.find_car("AA03ZZZ", "*", "*", "*"); assert(false); }
	catch (car_not_found_exception) { assert(true); }

	cs.edit_car(0, "AA07AAA", "P5", "M5", "T6");
	cs.remove_car(1);
	assert(cs.get_cars_count() == 4);
	cs.undo();
	cs.find_car("AA01ZZZ", "*", "*", "*");
	cs.undo();
	try { cs.find_car("AA07AAA", "*", "*", "*"); assert(false); }
	catch (car_not_found_exception) { assert(true); }
	cs.find_car("AA05ZZZ", "*", "*", "*");

	cs.undo();
	cs.undo();
	cs.undo();
	cs.undo();
	cs.undo();
	try { cs.undo(); assert(false); }catch (undo_exception) { assert(true); }
}

void test_service_html()
{
	_clear_file("cars_test_html.txt");
	CarsService cs{ "cars_test_html.txt" };
	cs.add_car("AA05ZZZ", "P1", "M1", "T5");
	cs.add_car("AA01ZZZ", "P2", "M2", "T3");
	cs.add_car("AA02ZZZ", "P3", "M2", "T3");
	cs.add_car("AA06ZZZ", "P2", "M3", "T3");
	cs.add_car("AA04ZZZ", "P2", "M1", "T2");
	cs.add_car("AA03ZZZ", "P1", "M2", "T1");


	/*cs.add_to_wash_list("AA05ZZZ");
	cs.add_to_wash_list("AA01ZZZ");
	cs.add_to_wash_list("AA04ZZZ");
	cs.export_html("test_html.html");*/
	// manually check for the 3 cars in test_html.html
}
	