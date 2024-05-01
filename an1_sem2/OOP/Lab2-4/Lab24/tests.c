#include "tests.h"

#include "test_product.h"
#include "test_list.h"
#include "test_service.h"

void test_all()
{
	test_create_product();
	test_create_list();
	test_add_product();
	test_edit_product();
	test_remove_product();

	test_sort_products();
	test_filter_products();
	test_find_product_by_id();
	test_find_product();
	test_find_product_index();
	test_compare_products();
}