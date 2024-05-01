#pragma once
#include "../../Users/Stefan/Desktop/an-1-sem-2/OOP/Lab10-11/Lab10-11/Service/wash_list.h"
#include <QtWidgets/QWidget>
#include <QtWidgets/QLayout>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QSpinBox>
#include <QtWidgets/QLabel>

class CrudGUIWashListObserver;

class WashListCrudGUI : public QWidget
{
private:
	CrudGUIWashListObserver* observer;

	WashList& wash_list;

	QHBoxLayout* main_layout = new QHBoxLayout(this);

	QListWidget* list = new QListWidget();

	QWidget* control_panel = new QWidget();
	QVBoxLayout* wash_list_layout = new QVBoxLayout(control_panel);

	QPushButton* wash_list_clear_btn = new QPushButton("Clear Wash List");		

	QWidget* wash_list_populate_panel = new QWidget();
	QHBoxLayout* wash_list_populate_layout = new QHBoxLayout(wash_list_populate_panel);

	QPushButton* wash_list_populate_btn = new QPushButton("Populate Wash List");	

	QSpinBox* wash_list_number_input = new QSpinBox();		

	QPushButton* wash_list_add_btn = new QPushButton("Add to Wash List");		

	QLabel* wash_list_cnt_label = new QLabel("Cars in wash list : 0");		
public:
	WashListCrudGUI(WashList& wash_list);

private:
	//void btn_wl_view();
	void btn_wl_clear();
	void btn_wl_populate();
	//void btn_wl_add();
	//void btn_wl_export();
	void update_wl_count();

	void wl_refresh();

	~WashListCrudGUI();

	friend class CrudGUIWashListObserver;
};

class CrudGUIWashListObserver : public Observer
{
private:
	WashListCrudGUI* gui;
public:
	CrudGUIWashListObserver(WashListCrudGUI* gui);
	void update() override;
};