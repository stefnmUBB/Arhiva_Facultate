#pragma once

#include "../../Users/Stefan/Desktop/an-1-sem-2/OOP/Lab10-11/Lab10-11/Service/cars_service.h"
#include <QtWidgets/QWidget>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QLabel>
#include <QtWidgets/QGroupBox>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QSpinBox>
#include <QtWidgets/QListView>
#include <QtWidgets/QTableView>
#include "./observer.h"
#include "car_list_model.h"

class BodyWashListObserver;

class Body : public QWidget
{
private:
	QHBoxLayout* main_layout;

	QWidget* left_panel;
	QVBoxLayout* left_layout;

	QTableView* cars_list_box;
	CarListModel* model;

	QWidget* list_actions_panel;
	QHBoxLayout* list_actions_layout;

	QPushButton* btn_remove;
	QComboBox* sort_options;
	QPushButton* btn_sort_reg_plate;
	QPushButton* btn_sort_prod_mod;

	QWidget* right_panel;
	QVBoxLayout* right_layout;

	QWidget* car_form;
	QGridLayout* car_form_layout;

	QLineEdit* car_plate_box;
	QLineEdit* car_producer_box;
	QLineEdit* car_model_box;
	QLineEdit* car_type_box;

	QWidget* car_actions_panel;
	QHBoxLayout* car_actions_layout;

	QPushButton* car_add_button;
	QPushButton* car_update_button;

	QPushButton* car_filter_button;
	QPushButton* car_undo_button;

	QGroupBox* wash_list_panel;
	QVBoxLayout* wash_list_layout;

	QPushButton* wash_list_clear_btn;

	QWidget* wash_list_populate_panel;
	QHBoxLayout* wash_list_populate_layout;
	QSpinBox* wash_list_number_input;
	QPushButton* wash_list_populate_btn;
	QPushButton* wash_list_add_btn;
	QPushButton* wash_list_view_btn;
	QPushButton* wash_list_view_rdo_btn;

	QPushButton* wash_list_export_btn;
	QLabel* wash_list_cnt_label;

	CarsService& service;
	WashList& wash_list;

	BodyWashListObserver* observer;
public:
	Body(CarsService& service, WashList& wash_list);

private:
	static QLabel* make_label(const QString& text);
	void update_list();

	void sort_options_changed();

	void btn_wl_view();
	void btn_wl_view_rdo();
	void btn_wl_clear();
	void btn_wl_populate();
	void btn_wl_add();
	void btn_wl_export();
	void update_wl_count();
	
	void btn_remove_click();	
	void cars_list_item_selection_changed();
	Car get_selected_car();

	void btn_update_click();
	void btn_add_click();
	void btn_filter_click();
	void sort_by_plates();
	void sort_by_prod_mod();		
	void btn_undo_click();

	void sort_by(const CarCompFunc& cmp);
	
public:
	static void set_item_car_data(QListWidgetItem* item, const Car& car);

	~Body();

	friend class BodyWashListObserver;
};

class BodyWashListObserver : public Observer
{
private:
	Body* body;
public:
	BodyWashListObserver(Body* body);
	void update() override;	
};