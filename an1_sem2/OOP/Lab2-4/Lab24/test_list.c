#include "test_list.h"

#include "list.h"
#include "service.h"
#include <assert.h>


void test_populate(List* l)
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
			type[1] = '0' + i % 3;
			prod[1] = '0' + i % 4;
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

void test_create_list()
{
	List* list = create_list();
	assert(list != NULL);
	assert(list->count == 0);
	free_list(list);
}