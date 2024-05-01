#include "test_all.h"

#include "test_car.h"
#include "test_list_item.h"
#include "test_number_plate.h"
#include "test_list.h"
#include "test_list_storage.h"
#include "test_service.h"

void test_all()
{	
	test_list_item_create();

	test_number_plate_create();
	test_number_plate_equals();
	test_number_plate_valid();

	test_car_eq();
	test_get_number_plate();
	test_get_producer();
	test_get_model();
	test_get_type();

	test_list_add();
	test_list_find_by_id();
	test_list_remove_at();
	test_list_remove_by_id();
	test_for_each();

	test_list_storage_create();

	test_service_add();
	test_service_edit();
	test_service_remove();
	test_service_get_car_by_id();
	
	test_service_filter();
	test_service_sort();
	test_service_find();
	test_service_for_each();

	test_service_wash_list_clear();
	test_service_wash_list_add();
	test_service_wish_list_populate();
	test_service_debug_populate_random();
	test_service_report();
	test_service_undo();
	test_service_html();
	test_service_storage();
	
}

