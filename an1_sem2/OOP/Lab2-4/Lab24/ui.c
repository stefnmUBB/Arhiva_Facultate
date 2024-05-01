#include "ui.h"

#include <stdlib.h>
#include <stdio.h>

ui* create_ui()
{
	ui* an_ui = (ui*)malloc(sizeof(ui));
	if (an_ui)
	{
		an_ui->opt_count = 0;
		return an_ui;
	}
	else return NULL;	
}

void add_ui_option(ui* ui, const char* text)
{
	ui->options[ui->opt_count++] = text;
}

int request_ui_option(ui* ui)
{
	int option = -1;
	while (0 > option || option >= ui->opt_count)
	{
		system("cls");
		for (int i = 0; i < ui->opt_count; i++)
		{
			printf("%i. %s\n", i, ui->options[i]);
		}
		printf(" >>> ");
		scanf_s("%i", &option);
	}
	return option;
}
