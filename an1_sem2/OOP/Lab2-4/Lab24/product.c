#include "product.h"
#include <malloc.h>
#include <string.h>

Product* create_product(int id, const char* type, const char* producer, const char* model,
	unsigned int price, unsigned int quantity)
{
	Product* product = (Product*)malloc(sizeof(Product));
	if (product)
	{
		product->type = (char*)malloc(100 * sizeof(char));
 		product->producer = (char*)malloc(100 * sizeof(char));
		product->model = (char*)malloc(100 * sizeof(char));
		if (product->type && product->producer && product->model)
		{
			product->id = id;
			strcpy_s(product->type, 100, type);
			strcpy_s(product->producer, 100, producer);
			strcpy_s(product->model, 100, model);
			product->price = price;
			product->quantity = quantity;
		}
		else return NULL;
	}
	else
	{
		return NULL;
	}
	return product;
}

void destroy_product(Product* prod)
{
	free(prod->type);
	free(prod->producer);
	free(prod->model);
	free(prod);
}