#pragma once

#include <QtWidgets/QWidget>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QPushButton>
#include "../../Users/Stefan/Desktop/an-1-sem-2/OOP/Lab10-11/Lab10-11/Domain/car.h"

class FilterResults : public QWidget
{
private:
	QVBoxLayout* layout;
	QListWidget* list;
	QPushButton* close_btn;
public:

	FilterResults();

	void add_car(const Car& car);
};

