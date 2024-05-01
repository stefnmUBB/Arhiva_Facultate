#pragma once

#include <QtWidgets/QWidget>
#include <QtWidgets/QLayout>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QLabel>
#include <QtWidgets/QPushButton>

#include "../Users/Stefan/source/repos/practic-app/practic-app/service/device_service.h"

class Body : public QWidget
{
private:
	QHBoxLayout* body_layout = new QHBoxLayout(this);
	QListWidget* list = new QListWidget();


	QWidget* info_panel = new QWidget();
	QGridLayout* info_layout = new QGridLayout(info_panel);
	
	QLabel* model_label = new QLabel("Model : ");
	QLabel* year_label = new QLabel("Year : ");

	QLineEdit* model_box = new QLineEdit();
	QLineEdit* year_box = new QLineEdit();

	QPushButton* fil_model_btn = new QPushButton("Filtrare Model");
	QPushButton* fil_year_btn = new QPushButton("Filtrare An");
	QPushButton* reload_btn = new QPushButton("Reincarcare");


	DeviceService& service;
public:
	Body(DeviceService& _service);
	void populate(const std::vector<Device>& source);

	void fil_model_click();
	void fil_year_click();
	void reload_click();

	void item_sel_changed();
};

