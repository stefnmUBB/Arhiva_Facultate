// Lab24.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#define _CRTDBG_MAP_ALLOC 
#include <stdlib.h>
#include <crtdbg.h>

#define TEST

#ifdef TEST

#include "tests.h"

int main()
{
    test_all();
    //int* x = malloc(100); // <-- Test CRT
    _CrtDumpMemoryLeaks();
    return 0;
}

#else

#include <stdio.h>
#include <string.h>

#include "service.h"
#include "ui.h"

void print_header()
{
    printf("%5s %20s %20s %20s %6s %6s\n", "Id", "Type", "Producer", "Model", "Price", "Quan.");
}

void print_product(Product* p)
{
    printf("%5i %20s %20s %20s %6i %6i\n", p->id, p->type, p->producer, p->model, p->price, p->quantity);
}

void populate(List* l)
{    
    for (int i = 0; i < 10; i++)
    {
        char* type = (char*)malloc(3);
        char* prod = (char*)malloc(3);
        char* model = (char*)malloc(3);
        if (model && prod && type)
        {
            type[0] = 't';
            prod[0] = 'p';
            model[0] = 'm';
            type[1] = '0' + i%3;
            prod[1] = '0' + i%4;
            model[1] = '0' + i % 5;
            type[2] = prod[2] = model[2] = 0;
            Product* p = create_product(i, type, prod, model, (i % 5 + 1) * 10, (i % 3 + 1) * 20);
            add_product(l, p);
        }
        free(type);
        free(prod);
        free(model);
    }
}

int cmp_prod_price_le(Product* p1, Product* p2) { return p1->price - p2->price; }

int cmp_prod_price_ge(Product* p1, Product* p2) { return p2->price - p1->price; }

int cmp_prod_quant_le(Product* p1, Product* p2) { return p1->quantity - p2->quantity; }

int cmp_prod_quant_ge(Product* p1, Product* p2) { return p2->quantity - p1->quantity; }

int main()
{    
    List* list = create_list();
    populate(list);
    ui* ui = create_ui();
    add_ui_option(ui, "Exit");
    add_ui_option(ui, "Add product");
    add_ui_option(ui, "Edit product");
    add_ui_option(ui, "Remove product");
    add_ui_option(ui, "View products sorted");
    add_ui_option(ui, "Filter products");
        
    for(int running_flag=1;running_flag;)
    {
        int option = request_ui_option(ui);
        switch (option)
        {
            case 0: // exit
            {
                free_list(list);
                free(ui);
                running_flag = 0;
                break;
            }
            case 1: // add product
            {
                Product* p = create_product(0, "", "", "", 0, 0);
                if (!p)
                {
                    printf("FATAL: Failed to create product instance");
                    running_flag = 0;
                    break;
                }

                printf("  Id = ");  scanf_s("%i", &p->id);
                printf(" Type = "); scanf_s("%s", p->type, 100);
                printf(" Producer = "); scanf_s("%s", p->producer, 100);
                printf(" Model = "); scanf_s("%s", p->model, 100);
                printf(" Price = "); scanf_s("%i", &p->price);
                printf(" Quantity = "); scanf_s("%i", &p->quantity);
                op_result r = add_product(list, p);
                if (r == OP_INCONSISTENT_PRICE)
                {
                    printf("The same product must not have different prices. Operation aborted.\n");
                    break;
                }
                else if (r & OP_OK)
                {
                    printf("Operation succeeded.\n");
                    if (r & OP_QUANTITY_UPDATED)
                    {
                        destroy_product(p);
                        printf("Product quantity increased.\n");
                        break;
                    }
                    break;
                }
                else if (r == OP_ID_PRODUCT_ALREADY_EXISTS)
                {
                    printf("Product id already exists\n");
                    break;
                }
                printf("[DEBUG] Unknown result code\n");
                break;
            }

            case 2: // edit product
            {
                int id;
                printf("Edit product price/quantity\n");
                printf("Type -1 if you want the value unchanged\n");
                printf("Product Id = "); scanf_s("%d", &id);
                Product* prod = find_product_by_id(list, id);
                if (prod == NULL)
                {
                    printf("Product not found.\n");
                    break;
                }
                int new_price, new_quantity;
                printf("New price = "); scanf_s("%d", &new_price);
                printf("New quantity = "); scanf_s("%d", &new_quantity);
                op_result r = edit_product(list, prod, new_price, new_quantity);
                if (r == OP_PRODUCT_NOT_FOUND)
                {
                    printf("Product not found. (*)\n");
                    break;
                }
                else if (r == OP_OK)
                {
                    printf("Operation succeeded.\n");
                    break;
                }
                printf("[DEBUG] Unknown result code\n");
                break;
            }

            case 3: // remove product
            {
                int id;
                printf("Edit product price/quantity\n");
                printf("Type -1 if you want the value unchanged\n");
                printf("Product Id = "); scanf_s("%d", &id);
                Product* prod = find_product_by_id(list, id);
                if (prod == NULL)
                {
                    printf("Product not found.\n");
                    break;
                }
                op_result r = remove_product(list, prod);
                if (r == OP_PRODUCT_NOT_FOUND)
                {
                    printf("Product not found. No changes were made.\n");
                    break;
                }
                else if (r == OP_OK)
                {
                    printf("Operation succeeded.\n");
                    break;
                }
                printf("[DEBUG] Unknown result code\n");
                break;
            }

            case 4: // view sorted products
            {
                Product* result_buffer[100];
                char opt_price, opt_quant, dummy;
                printf("Sort\n");
                printf("By price asc/desc (A/D) : ");                
                dummy = getchar(); // \n
                opt_price = getchar();               
                dummy = getchar(); // \n
                printf("By quantity asc/desc (A/D) : ");
                opt_quant = getchar();

                if ('a' <= opt_price && opt_price <= 'z') opt_price += 'A' - 'a';
                if ('a' <= opt_quant && opt_quant <= 'z') opt_quant += 'A' - 'a';
                
                /*sort_products(list, result_buffer,
                    (opt_price == 'A' ? BY_PRICE_ASCENDING : BY_PRICE_DESCENDING) |
                    (opt_quant == 'A' ? BY_QUANTITY_ASCENDING : BY_QUANTITY_DESCENDING)
                    );*/                

                if (opt_quant == 'A')
                    sort_products_with_func(list, result_buffer, cmp_prod_quant_le);
                else if (opt_quant == 'D')
                    sort_products_with_func(list, result_buffer, cmp_prod_quant_ge); 
                else if (opt_price == 'A')
                    sort_products_with_func(list, result_buffer, cmp_prod_price_le);
                else if (opt_price == 'D')
                    sort_products_with_func(list, result_buffer, cmp_prod_price_ge);

                print_header();
                for (int i = 0; i < list->count; i++)
                {
                    print_product(result_buffer[i]);
                }
                break;
            }

            case 5: // filter
            {
                printf("Filter\n");
                char producer[300];
                int price, quantity;
                printf("Producer : "); 
                //scanf_s("%s ", producer, 300);                
                fgets(producer, 300, stdin);
                fgets(producer, 300, stdin);
                producer[strlen(producer) - 1] = '\0';
                printf("Price : "); scanf_s("%d", &price);
                printf("Quantity : "); scanf_s("%d", &quantity);
                Product* result_buffer[100];
                int k = filter_products(list, result_buffer, producer, price, quantity);
                print_header();
                for (int i = 0; i < k; i++)
                {
                    print_product(result_buffer[i]);
                }
                break;
            }

        }

        printf("Press Enter to continue...\n");
        char c = getchar();
        c = getchar();
    }
    _CrtDumpMemoryLeaks();
    return 0;
}

#endif