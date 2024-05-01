#pragma once
#include "product.h"
#include <stddef.h>

typedef struct
{
	Product** products;
	int count;
	int capacity;
} List;

/// <summary>
/// Creates an empty products list
/// </summary>
/// <returns>Pointer to List instance, or NULL if list failed to create</returns>
List* create_list();

int redim_list(List* list, size_t new_capacity);

void free_list(List* list);