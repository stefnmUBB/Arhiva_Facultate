#include "washlist_crud_gui.h"
#include <QMessageBox>

CrudGUIWashListObserver::CrudGUIWashListObserver(WashListCrudGUI* gui)
{
	this->gui = gui;
}

void CrudGUIWashListObserver::update()
{
	gui->wl_refresh();
}


WashListCrudGUI::WashListCrudGUI(WashList& wash_list) : wash_list(wash_list) 
{
	setWindowTitle("Washlist CRUD");
	main_layout->addWidget(list);
	main_layout->addWidget(control_panel);

	wash_list_layout->addWidget(wash_list_clear_btn);
	wash_list_populate_layout->addWidget(wash_list_populate_btn);

	wash_list_number_input->setMinimum(1);
	wash_list_number_input->setMaximum(2000);
	wash_list_number_input->setAlignment(Qt::AlignRight);
	wash_list_populate_layout->addWidget(wash_list_number_input);

	wash_list_layout->addWidget(wash_list_add_btn);
	wash_list_layout->addWidget(wash_list_populate_panel);
	wash_list_layout->addWidget(wash_list_cnt_label);

	//connect(wash_list_add_btn, &QPushButton::clicked, this, &WashListCrudGUI::btn_wl_add);
	connect(wash_list_clear_btn, &QPushButton::clicked, this, &WashListCrudGUI::btn_wl_clear);
	connect(wash_list_populate_btn, &QPushButton::clicked, this, &WashListCrudGUI::btn_wl_populate);
	//connect(wash_list_view_btn, &QPushButton::clicked, this, &WashListCrudGUI::btn_wl_view);
	//connect(wash_list_export_btn, &QPushButton::clicked, this, &WashListCrudGUI::btn_wl_export);

	observer = new CrudGUIWashListObserver(this);
	wash_list.addObserver(observer);
	wl_refresh();
}

void WashListCrudGUI::update_wl_count()
{
	const string msg = "Cars in wash list : ";
	wash_list_cnt_label->setText(QString::fromStdString(msg + std::to_string(wash_list.count())));
}

void WashListCrudGUI::btn_wl_clear()
{
	wash_list.clear();
	update_wl_count();
	wl_refresh();
}

void WashListCrudGUI::btn_wl_populate()
{
	int count = wash_list_number_input->value();
	try
	{
		wash_list.populate(count);
		update_wl_count();
		wl_refresh();
	}
	catch (const exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);		
	}	
}

void WashListCrudGUI::wl_refresh()
{
	list->clear();
	for (auto c : wash_list.get_cars())
	{		
		list->addItem(QString::fromStdString(c.get_number_plate().get_value()));
	}
}

WashListCrudGUI::~WashListCrudGUI()
{
	wash_list.removeObserver(observer);
	delete observer;
}