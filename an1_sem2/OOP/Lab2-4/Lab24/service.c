#include "service.h"
#include <string.h>
#include <stdlib.h>

int equals(Product* p1, Product* p2)
{
	if (p1->id != p2->id) return 0;
	if (strcmp(p1->type, p2->type)) return 0;
	if (strcmp(p1->model, p2->model)) return 0;
	if (strcmp(p1->producer, p2->producer)) return 0;
	return 1;
}

Product* find_product(List* list, Product* product)
{
	for (int i = 0; i < list->count; i++)
	{
		if (equals(product, list->products[i]))
		{
			return list->products[i];
		}
	}
	return NULL;
}

Product* find_product_by_id(List* list, int id)
{
	for (int i = 0; i < list->count; i++)
	{
		if (list->products[i]->id == id)
		{
			return list->products[i];
		}
	}
	return NULL;
}

int find_product_index(List* list, Product* product)
{
	for (int i = 0; i < list->count; i++)
	{
		if (equals(product, list->products[i]))
		{
			return i;
		}
	}
	return -1;
}

op_result add_product(List* list, Product* product)
{
	Product* existing;
	if (existing = find_product(list, product))
	{
		if (existing->price != product->price)
		{
			return OP_INCONSISTENT_PRICE;
		}
		existing->quantity += product->quantity;
		return OP_QUANTITY_UPDATED | OP_OK;
	}
	else
	{
		if (find_product_by_id(list, product->id))
		{
			return OP_ID_PRODUCT_ALREADY_EXISTS;
		}
		if (list->count + 1 == list->capacity)
		{
			redim_list(list, 2 * list->capacity);
		}
		list->products[list->count++] = product;
	}
	return OP_OK;
}

op_result edit_product(List* list, Product* product, int price, int quantity)
{
	Product* item = find_product(list, product);
	if (item == NULL)
	{
		return OP_PRODUCT_NOT_FOUND;
	}
	if (price >= 0) item->price = price;
	if (quantity >= 0) item->quantity = quantity;
	return OP_OK;
}

op_result remove_product(List* list, Product* product)
{
	int index = find_product_index(list, product);
	if (index < 0)
	{
		return OP_PRODUCT_NOT_FOUND;
	}
	destroy_product(list->products[index]);
	for (int i = index; i < list->count - 1; i++)   
	{
		list->products[i] = list->products[i + 1];
	}
	list->count--;
	return OP_OK;
}

int compare_products(Product* p1, Product* p2, sort_options options)
{
	if (p1->price < p2->price) return (options & BY_PRICE_ASCENDING) ? -1 : 1;
	if (p1->price > p2->price) return (options & BY_PRICE_ASCENDING) ? 1 : -1;

	if (p1->quantity < p2->quantity) return (options & BY_QUANTITY_ASCENDING) ? -1 : 1;
	if (p1->quantity > p2->quantity) return (options & BY_QUANTITY_ASCENDING) ? 1 : -1;
	
	return 0;
}

void sort_products(List* list, Product** result_buffer, sort_options options)
{
	for (int i = 0; i < list->count; i++)
		result_buffer[i] = list->products[i];

	for (int i = 0; i < list->count - 1; i++)
	{
		for (int j = i + 1; j < list->count; j++)
		{
			if (compare_products(result_buffer[i], result_buffer[j], options) >= 0)
			{
				Product* aux = result_buffer[i];
				result_buffer[i] = result_buffer[j];
				result_buffer[j] = aux;
			}
		}
	}
}

void sort_products_with_func(List* list, Product** result_buffer, int (*cmp)(Product*, Product*))
{
	for (int i = 0; i < list->count; i++)
		result_buffer[i] = list->products[i];

	for (int i = 0; i < list->count - 1; i++)
	{
		for (int j = i + 1; j < list->count; j++)
		{
			if (cmp(result_buffer[i], result_buffer[j]) >= 0)
			{
				Product* aux = result_buffer[i];
				result_buffer[i] = result_buffer[j];
				result_buffer[j] = aux;
			}
		}
	}
}

int filter_products(List* list, Product** result_buffer, const char* producer, int price, int quantity)
{
	int count = 0;
	for (int i = 0; i < list->count; i++)
	{
		char choose = 1;
		if (producer != NULL && strlen(producer) > 0)
		{
			if (strcmp(list->products[i]->producer, producer))
				choose = 0;
		}
		if (price >= 0 && list->products[i]->price != price)
		{
			choose = 0;
		}
		if (quantity >= 0 && list->products[i]->quantity != quantity)
		{
			choose = 0;
		}
		if (choose)
		{
			result_buffer[count++] = list->products[i];
		}
	}
	return count;
}