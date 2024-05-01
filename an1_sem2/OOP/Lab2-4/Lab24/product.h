#pragma once

typedef struct
{
	int id;
	char* type;
	char* producer;
	char* model;
	unsigned int price;
	unsigned int quantity;
} Product;

/// <summary>
/// Creates new Product instance
/// </summary>
/// <param name="id">Product id</param>
/// <param name="type">Product type</param>
/// <param name="producer">Producer name</param>
/// <param name="model">Product model</param>
/// <param name="price">Product price</param>
/// <param name="quantity">Product quantity</param>
/// <returns>Pointer to product instance featuring requested data, or NULL if structure failed to create</returns>
Product* create_product(int id, const char* type, const char* producer, const char* model,
	unsigned int price, unsigned int quantity);

void destroy_product(Product* prod);