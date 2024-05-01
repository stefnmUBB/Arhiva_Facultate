#include "list.h"
#include <malloc.h>

List* create_list()
{
	List* list = (List*)malloc(sizeof(List));
	if (list)
	{
		list->count = 0;
		list->capacity = 1;
		list->products = (Product**)malloc(list->capacity * sizeof(Product*));
	}
	else
	{
		return NULL;
	}
	return list;
}

int redim_list(List* list, size_t new_capacity)
{
	Product** buff = (Product**)malloc(new_capacity * sizeof(Product*));
	if (buff)
	{
		int finlen = new_capacity < ((size_t)list->count) ? new_capacity : list->count;
		for (int i = 0; i < finlen; i++)
			buff[i] = list->products[i];

		for (int i = finlen; i < list->count; i++)
			destroy_product(list->products[i]);
		free(list->products);

		list->capacity = new_capacity;
		list->count = finlen;
		list->products = buff;
		return 0;
	}
	else return -1;	
}

void free_list(List* list)
{
	for (int i = 0; i < list->count; i++)
	{
		destroy_product(list->products[i]);		
	}
	free(list->products);
	free(list);
}