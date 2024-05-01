#include "test_service.h"
#include "test_list.h"
#include "list.h"
#include "product.h"
#include "service.h"
#include <assert.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void test_sort_products_with_func();

void test_add_product()
{
	List* list = create_list();

	add_product(list, create_product(0, "type1", "prod1", "model1_add", 10, 5));
	assert(list->count == 1);
	assert(list->products[0]->id == 0);
	assert(!strcmp(list->products[0]->type, "type1"));
	assert(list->products[0]->price == 10);

	add_product(list, create_product(1, "type2", "prod2", "model2_add", 1, 1));
	assert(list->count == 2);
	assert(list->products[1]->id == 1);
	assert(!strcmp(list->products[1]->type, "type2"));
	assert(list->products[1]->price == 1);

	Product* prod = create_product(0, "type1", "prod1", "model1_add", 7, 8);
	op_result result = add_product(list, prod);
	assert(result & OP_INCONSISTENT_PRICE);
	destroy_product(prod);

	prod = create_product(0, "type1", "prod1", "model1_add", 10, 8);
	result = add_product(list, prod);
	assert(result & OP_OK);
	assert(list->products[0]->quantity == 13);
	destroy_product(prod);

	prod = create_product(0, "type3", "prod2", "model4", 100, 50);
	result = add_product(list, prod);
	assert(result & OP_ID_PRODUCT_ALREADY_EXISTS);
	destroy_product(prod);

	free_list(list);
}

void test_edit_product()
{
	List* list = create_list();
	add_product(list, create_product(0, "type1", "prod1", "model1_edit", 10, 5));
	add_product(list, create_product(1, "type2", "prod2", "model2_edit", 1, 1));
	add_product(list, create_product(2, "type3", "prod3", "model3_edit", 2, 3));

	edit_product(list, list->products[0], 50, 10);
	assert(list->products[0]->price == 50);
	assert(list->products[0]->quantity == 10);

	Product* prod_new = create_product(0, "type1", "prod1", "model1_edit", 0, 0);
	op_result result = edit_product(list, prod_new, 50, 10);
	assert(result == OP_OK);
	destroy_product(prod_new);
	
	prod_new = create_product(8, "type9", "prod9", "model9", 0, 0);
	result = edit_product(list, prod_new, 50, 10);
	assert(result == OP_PRODUCT_NOT_FOUND);
	destroy_product(prod_new);
	free_list(list);
}

void test_remove_product()
{
	List* list = create_list();
	add_product(list, create_product(0, "type1", "prod1", "model1", 10, 5));
	add_product(list, create_product(1, "type2", "prod2", "model2", 1, 1));
	add_product(list, create_product(2, "type3", "prod3", "model3", 2, 3));

	remove_product(list, list->products[0]);
	assert(list->count == 2);

	remove_product(list, list->products[0]);
	assert(list->count == 1);

	Product* prod_x = create_product(8, "type9", "prod9", "model9", 0, 0);
	op_result result = remove_product(list, prod_x);
	assert(result == OP_PRODUCT_NOT_FOUND);
	destroy_product(prod_x);

	free_list(list);
}

void test_sort_products()
{
	List* l = create_list();
	test_populate(l);
	add_product(l, l->products[0]);

	Product* buffer[100];
	sort_options sopts = BY_PRICE_ASCENDING | BY_QUANTITY_DESCENDING;
	sort_products(l, buffer, sopts);
	for (int i = 0; i < l->count - 1; i++)
	{
		assert(compare_products(buffer[i], buffer[i + 1], sopts) <= 0);
	}

	free_list(l);

	test_sort_products_with_func();
}

void test_compare_products()
{
	Product* p1 = create_product(0, "t", "p", "m", 10, 20);
	Product* p2 = create_product(0, "t", "p", "m", 15, 25);
	assert(compare_products(p1, p2, BY_PRICE_ASCENDING | BY_QUANTITY_ASCENDING) == -1);
	p2->price = 10;
	p2->quantity = 20;
	assert(compare_products(p1, p2, BY_PRICE_ASCENDING | BY_QUANTITY_ASCENDING) == 0);

	destroy_product(p1);
	destroy_product(p2);
}

void test_filter_products()
{
	List* l = create_list();
	test_populate(l);

	Product* buffer[100];	
	assert(filter_products(l, buffer, "p1", -1, -1) == 3);
	for (int i = 0; i < 3; i++)
		assert(!strcmp(buffer[i]->producer, "p1"));

	assert(filter_products(l, buffer, "", 20, -1) == 2);
	for (int i = 0; i < 2; i++)
		assert(buffer[i]->price == 20);

	assert(filter_products(l, buffer, "", 20, 40) == 1);
	assert(filter_products(l, buffer, "p3", 20, 10) == 0);

	free_list(l);

}

void test_find_product_by_id()
{
	List* l = create_list();
	test_populate(l);

	Product* p = find_product_by_id(l, 5);
	assert(p->id == 5);

	p = find_product_by_id(l, 200);
	assert(p == NULL);

	free_list(l);
}

void test_find_product_index()
{
	List* l = create_list();
	test_populate(l);

	int index = find_product_index(l, l->products[0]);
	assert(index == 0);

	Product* prod = create_product(15, "t", "p", "m", 0, 0);
	assert(find_product_index(l, prod) == -1);
	destroy_product(prod);

	free_list(l);
}

void test_find_product()
{
	List* l = create_list();
	test_populate(l);

	Product* prod = create_product(0, "t0", "p0", "m0", 1000, 20);
	Product* p = find_product(l, prod);
	assert(p == l->products[0]);
	destroy_product(prod);

	prod = create_product(1000, "t0", "p0", "m0", 1000, 20);
	p = find_product(l, prod);
	assert(p == NULL);
	destroy_product(prod);

	free_list(l);
}

int compare_prices_le(Product* p1, Product* p2)
{
	return p1->price - p2->price;
}

int compare_prices_ge(Product* p1, Product* p2)
{
	return p2->price - p1->price;
}

void test_sort_products_with_func()
{
	List* l = create_list();
	test_populate(l);
	Product* buffer[100];
	sort_products_with_func(l, buffer, compare_prices_le);
	for (int i = 0; i < l->count - 1; i++)
	{		
		assert(buffer[i]->price <= buffer[i + 1]->price);
	}

	sort_products_with_func(l, buffer, compare_prices_ge);
	for (int i = 0; i < l->count - 1; i++)
	{
		assert(buffer[i]->price >= buffer[i + 1]->price);
	}
	free_list(l);
}