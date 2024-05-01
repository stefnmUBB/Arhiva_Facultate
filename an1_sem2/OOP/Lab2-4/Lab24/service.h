#pragma once

#include "product.h"
#include "list.h"
#include "error.h"

/// <summary>
/// adds a new product to the list, or updates the quantity if the product already exists
/// </summary>
/// <param name="list">products list</param>
/// <param name="product">product to be added</param>
/// <returns>Result of the operation. OP_OK if everything went well, otherwise an error code is returned</returns>
op_result add_product(List* list, Product* product);

/// <summary>
/// Changes product specifications (price/quantity).
/// </summary>
/// <param name="list">Products list</param>
/// <param name="product">Product to be changed</param>
/// <param name="price">New price (-1 if this parameter doesn't change)</param>
/// <param name="quantity">New quantity (-1 if this parameter doesn't change)</param>
/// <returns>Result of the operation. OP_OK if everything went well, otherwise an error code is returned</returns>
op_result edit_product(List* list, Product* product, int price, int quantity);

/// <summary>
/// Gets product from list having the specified id
/// </summary>
/// <param name="list">Products list</param>
/// <param name="id">Product id</param>
/// <returns>Product with the specified id, or NULL if product not found</returns>
Product* find_product_by_id(List* list, int id);

/// <summary>
/// Removes a product from the list
/// </summary>
/// <param name="list">Products list</param>
/// <param name="product">Product to be removed</param>
/// <returns>Result of the operation. OP_OK if everything went well, otherwise an error code is returned</returns>
op_result remove_product(List* list, Product* product);

/// <summary>
/// Finds product in a list
/// </summary>
/// <param name="list">Products list</param>
/// <param name="product">Product instance carrying target properties</param>
/// <returns>Product pointer in list, or NULL if product not found</returns>
Product* find_product(List* list, Product* product);

/// <summary>
/// Finds index of a certain product in list
/// </summary>
/// <param name="list">Products list</param>
/// <param name="product">Product to look for</param>
/// <returns>Index of product if it exists in the list, otherwise -1</returns>
int find_product_index(List* list, Product* product);

/// <summary>
/// Sort option used when sorting products list
/// </summary>
typedef enum
{

	BY_PRICE_ASCENDING = 1 << 0,
	BY_PRICE_DESCENDING = 0 << 0,
	BY_QUANTITY_ASCENDING = 1 << 1,
	BY_QUANTITY_DESCENDING = 0 << 1
} sort_options;

/// <summary>
/// Compares 2 products based on selected sort options
/// </summary>
/// <param name="p1">first product</param>
/// <param name="p2">second product</param>
/// <param name="options">sort options</param>
/// <returns>-1 if p1 comes before p2, 1 if p1 comes after p2, 0 if products are on the same rank</returns>
int compare_products(Product* p1, Product* p2, sort_options options);

/// <summary>
/// Sorts products list into an external buffer based on specified options
/// </summary>
/// <param name="list">Products list</param>
/// <param name="result_buffer">where sorted list appears</param>
/// <param name="options">Sort options (e.g BY_PRICE_ASCENDING | BY_QUANTITY_DESCENDING)</param>
void sort_products(List* list, Product** result_buffer, sort_options options);

/// <summary>
/// Filters products which carries certain values in certain fields
/// </summary>
/// <param name="list">Products list</param>
/// <param name="result_buffer">Where filtered products appear</param>
/// <param name="producer">Producer name filter ("" if ignore)</param>
/// <param name="price">Price filter (-1 if ignore)</param>
/// <param name="quantity">Quantity filter (-1 if ignore)</param>
/// <returns>Number of filtered elements</returns>
int filter_products(List* list, Product** result_buffer, const char* producer, int price, int quantity);


/// <summary>
/// sort products by a specified compare function
/// </summary>
/// <param name="list">Products list</param>
/// <param name="result_buffer">Where sorted products appear</param>
/// <param name="cmp">compare function (returns -n if p1 in front of p2, 1 if p1 after p2, 0 if equal positions)</param>
void sort_products_with_func(List* list, Product** result_buffer, int (*cmp)(Product*, Product*));
