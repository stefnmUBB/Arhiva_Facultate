#include "test_product.h"

#include "product.h"
#include <assert.h>
#include <string.h>

void test_create_product()
{
	Product* product = create_product(1, "eg_type", "eg_producer", "eg_model", 100, 25);
	assert(product->id == 1);
	assert(!strcmp(product->type, "eg_type"));
	assert(!strcmp(product->producer, "eg_producer"));
	assert(!strcmp(product->model, "eg_model"));
	assert(product->price == 100);
	assert(product->quantity == 25);
	destroy_product(product);
}