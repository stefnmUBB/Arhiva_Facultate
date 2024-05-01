#include "body.h"

#include <QtWidgets/QLabel>
#include <QtWidgets/QMessageBox>
#include <QtWidgets/QFileDialog>
#include "filter_results.h"
#include "washlist_crud_gui.h"
#include "washlist_read_only_gui.h"

BodyWashListObserver::BodyWashListObserver(Body* body)
{
	this->body = body;
}

void BodyWashListObserver::update()
{	
	body->update_wl_count();
}

#include "car_list_model.h"

QString default_style = "QWidget{background-color: #97d0e8;} QPushButton, QCombobox { background-color: #b9deed; } QLineEdit, QSpinBox, QListWidget {  background-color: white; }";

Body::Body(CarsService& service, WashList& wash_list) : service(service), wash_list(wash_list)
{	
	this->setStyleSheet(default_style);
	main_layout = new QHBoxLayout(this);

	left_panel = new QWidget();
	left_layout = new QVBoxLayout(left_panel);
	
	cars_list_box = new QTableView();
	model = new CarListModel(cars_list_box);
	cars_list_box->setModel(model);
	left_layout->addWidget(cars_list_box);

	list_actions_panel = new QWidget();
	list_actions_layout = new QHBoxLayout(list_actions_panel);

	btn_remove = new QPushButton();
	btn_remove->setText("Remove");

	QLabel* sort_label = new QLabel();
	sort_label->setText("Sort by ");

	sort_options = new QComboBox();
	sort_options->addItem("(default)");
	sort_options->addItem("registration plate");
	sort_options->addItem("producer & model");
	
	

	list_actions_layout->addWidget(btn_remove);
	list_actions_layout->addStretch();
	list_actions_layout->addWidget(sort_label);
	list_actions_layout->addWidget(sort_options);

	left_layout->addWidget(list_actions_panel);

	main_layout->addWidget(left_panel);

	right_panel = new QWidget();
	right_layout = new QVBoxLayout(right_panel);

	car_form = new QWidget();
	car_form_layout = new QGridLayout(car_form);

	car_plate_box = new QLineEdit();
	car_producer_box = new QLineEdit();
	car_model_box = new QLineEdit();
	car_type_box = new QLineEdit();
	car_plate_box->setStyleSheet("background-color: white");
	car_producer_box->setStyleSheet("background-color: white");
	car_model_box->setStyleSheet("background-color: white");
	car_type_box->setStyleSheet("background-color: white");

	car_form_layout->addWidget(make_label("Number plate : "), 0, 0);
	car_form_layout->addWidget(make_label("Producer : "), 1, 0);
	car_form_layout->addWidget(make_label("Model : "), 2, 0);
	car_form_layout->addWidget(make_label("Type : "), 3, 0);

	car_form_layout->addWidget(car_plate_box, 0, 1);
	car_form_layout->addWidget(car_producer_box, 1, 1);
	car_form_layout->addWidget(car_model_box, 2, 1);
	car_form_layout->addWidget(car_type_box, 3, 1);
	
	right_layout->addWidget(car_form);

	car_actions_panel = new QWidget();
	car_actions_layout = new QHBoxLayout(car_actions_panel);

	car_add_button = new QPushButton();
	car_add_button->setText("Add");
	car_actions_layout->addWidget(car_add_button);

	car_update_button = new QPushButton();
	car_update_button->setText("Update"); 
	car_actions_layout->addWidget(car_update_button);

	car_filter_button = new QPushButton();
	car_filter_button->setText("Filter...");
	car_actions_layout->addWidget(car_filter_button);

	car_undo_button = new QPushButton();
	car_undo_button->setText("Undo");
	car_actions_layout->addWidget(car_undo_button);


	wash_list_panel = new QGroupBox();		
	wash_list_panel->setTitle("Wash List");
	wash_list_panel->setContentsMargins(20, 20, 20, 0);

	wash_list_layout = new QVBoxLayout(wash_list_panel);

	wash_list_clear_btn = new QPushButton();
	wash_list_clear_btn->setText("Clear Wash List");
	wash_list_layout->addWidget(wash_list_clear_btn);

	wash_list_populate_panel = new QWidget();
	wash_list_populate_layout = new QHBoxLayout(wash_list_populate_panel);

	wash_list_populate_btn = new QPushButton();
	wash_list_populate_btn->setText("Populate Wash List");
	wash_list_populate_layout->addWidget(wash_list_populate_btn);

	wash_list_number_input = new QSpinBox();
	wash_list_number_input->setMinimum(1);
	wash_list_number_input->setMaximum(2000);
	wash_list_number_input->setAlignment(Qt::AlignRight);
	wash_list_number_input->setStyleSheet("background-color: white");
	wash_list_populate_layout->addWidget(wash_list_number_input);	

	wash_list_layout->addWidget(wash_list_populate_panel);

	wash_list_add_btn = new QPushButton();
	wash_list_add_btn->setText("Add to Wash List");
	wash_list_layout->addWidget(wash_list_add_btn);

	wash_list_view_btn = new QPushButton();
	wash_list_view_btn->setText("View wash list");
	wash_list_layout->addWidget(wash_list_view_btn);

	wash_list_view_rdo_btn = new QPushButton("View wash list (Read-only)");
	wash_list_layout->addWidget(wash_list_view_rdo_btn);

	wash_list_export_btn = new QPushButton();
	wash_list_export_btn->setText("Export Wash List");
	wash_list_layout->addWidget(wash_list_export_btn);
	
	wash_list_cnt_label = new QLabel();
	wash_list_cnt_label->setText("Cars in wash list : 0");
	wash_list_layout->addWidget(wash_list_cnt_label);

	right_layout->addWidget(car_actions_panel);
	right_layout->addWidget(wash_list_panel);
	right_layout->addStretch();

	main_layout->addWidget(right_panel);	

	connect(btn_remove, &QPushButton::clicked, this, &Body::btn_remove_click);
	//connect(cars_list_box, &QListWidget::itemSelectionChanged, this, &Body::cars_list_item_selection_changed);

	connect(car_update_button, &QPushButton::clicked, this, &Body::btn_update_click);
	connect(car_add_button, &QPushButton::clicked, this, &Body::btn_add_click);
	connect(car_filter_button, &QPushButton::clicked, this, &Body::btn_filter_click);
	connect(car_undo_button, &QPushButton::clicked, this, &Body::btn_undo_click);

	connect(sort_options, &QComboBox::currentTextChanged, this, &Body::sort_options_changed);

	connect(wash_list_add_btn, &QPushButton::clicked, this, &Body::btn_wl_add);
	connect(wash_list_clear_btn, &QPushButton::clicked, this, &Body::btn_wl_clear);
	connect(wash_list_populate_btn, &QPushButton::clicked, this, &Body::btn_wl_populate);
	connect(wash_list_view_btn, &QPushButton::clicked, this, &Body::btn_wl_view);
	connect(wash_list_view_rdo_btn, &QPushButton::clicked, this, &Body::btn_wl_view_rdo);
	connect(wash_list_export_btn, &QPushButton::clicked, this, &Body::btn_wl_export);

	wash_list_add_btn->setEnabled(false);
	QObject::connect(cars_list_box->selectionModel(), &QItemSelectionModel::selectionChanged, [&]() 
		{
			wash_list_add_btn->setEnabled(!cars_list_box->selectionModel()->selectedRows().isEmpty());
		});

	QObject::connect(cars_list_box->selectionModel(), &QItemSelectionModel::selectionChanged, [&]()
		{
			for (const auto& index : cars_list_box->selectionModel()->selectedIndexes())
			{
				if (index.column() == 0)
				{
					string dat = index.data(Qt::UserRole).value<Car>().get_number_plate().get_value();
					car_plate_box->setText(QString::fromStdString(dat));
					continue;
				}
				if (index.column() == 1)
				{
					string dat = index.data(Qt::UserRole).value<Car>().get_producer();
					car_producer_box->setText(QString::fromStdString(dat));
					continue;
				}
				if (index.column() == 2)
				{
					string dat = index.data(Qt::UserRole).value<Car>().get_model();
					car_model_box->setText(QString::fromStdString(dat));					
					continue;
				}
				if (index.column() == 3)
				{
					string dat = index.data(Qt::UserRole).value<Car>().get_type();
					car_type_box->setText(QString::fromStdString(dat));
					continue;
				}
			}
		});

	observer = new BodyWashListObserver(this);
	wash_list.addObserver(observer);

	update_list();
}

Body::~Body()
{
	wash_list.removeObserver(observer);
	delete observer;
}

void Body::update_wl_count()
{
	const string msg = "Cars in wash list : ";
	wash_list_cnt_label->setText(QString::fromStdString(msg + std::to_string(wash_list.count())));
}

void Body::btn_wl_clear()
{
	wash_list.clear();	
	update_wl_count();
}

void Body::btn_wl_populate()
{
	int count = wash_list_number_input->value();
	try
	{
		wash_list.populate(count);
		update_wl_count();
	}	
	catch (const exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::btn_wl_add()
{
	try
	{
		const int sel_id = get_selected_car().get_id();
		const Car& sel_car = service.get_car_by_id(sel_id);
		wash_list.add(sel_car.get_number_plate().get_value());		
		update_wl_count();
	}
	catch (const exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::btn_wl_export()
{
	string filename = QFileDialog::getSaveFileName(this, tr("Export..."),
		"untitled.html", "HTML document (*.html);; CSV document (*.csv)").toStdString();
	try
	{
		wash_list.export_html(filename);
	}
	catch (const exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::btn_undo_click()
{
	try
	{
		service.undo();
		update_list();
	}
	catch (const std::exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::btn_remove_click()
{			
	try
	{
		Car target = get_selected_car();		
		service.remove_car(get_selected_car().get_id());		
		wash_list.remove(target.get_number_plate().get_value());
		update_wl_count();
		update_list();		
	}
	catch (const std::exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}	
}

void Body::btn_update_click()
{	
	try
	{				
		int id = get_selected_car().get_id();
		string plate = car_plate_box->text().toStdString();
		string producer = car_producer_box->text().toStdString();
		string model = car_model_box->text().toStdString();
		string type = car_type_box->text().toStdString();

		service.edit_car(id, plate, producer, model, type);
		update_list();		
	}
	catch (const std::exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::cars_list_item_selection_changed()
{
	/*if (cars_list_box->selectedItems().size() == 1)
	{
		QListWidgetItem* item = cars_list_box->selectedItems()[0];

		Car target = item->data(Qt::UserRole).value<Car>();
		car_plate_box->setText(QString::fromStdString(target.get_number_plate().get_value()));
		car_producer_box->setText(QString::fromStdString(target.get_producer()));
		car_model_box->setText(QString::fromStdString(target.get_model()));
		car_type_box->setText(QString::fromStdString(target.get_type()));
	}
	else
	{
		car_plate_box->setText("");
		car_producer_box->setText("");
		car_model_box->setText("");
		car_type_box->setText("");
	}*/
}

QLabel* Body::make_label(const QString& text)
{
	QLabel* label = new QLabel();
	label->setText(text);
	return label;
}

void Body::update_list()
{	
	model->set_cars(service.get_repo().get_container_const());
}

void Body::set_item_car_data(QListWidgetItem* item, const Car& car)
{
	item->setText(QString::fromStdString(car.get_number_plate().get_value() + " " +
		car.get_producer() + " " +
		car.get_model() + " " +
		car.get_type()));
	item->setData(Qt::UserRole, QVariant::fromValue<Car>(car));
}

Car Body::get_selected_car()
{
	if (cars_list_box->selectionModel()->selectedRows().size() != 1)
	{
		throw exception("Wrong selection");
	}
	return cars_list_box->selectionModel()->selectedIndexes()[0].data(Qt::UserRole).value<Car>();		
}

void Body::btn_add_click()
{
	string plate = car_plate_box->text().toStdString();
	string producer = car_producer_box->text().toStdString();
	string model = car_model_box->text().toStdString();
	string type = car_type_box->text().toStdString();

	try
	{
		service.add_car(plate, producer, model, type);		
		update_list();
	}
	catch (const std::exception& e)
	{
		QMessageBox::warning(this, "Error", e.what(), QMessageBox::Ok);
	}
}

void Body::sort_by(const CarCompFunc& cmp)
{		
	vector<Car> sorted_cars;
	service.sort(cmp, [&sorted_cars](const Car& car)
		{						
			sorted_cars.push_back(car);			
		});
	model->set_cars(sorted_cars);
}

void Body::sort_options_changed()
{
	if (sort_options->currentText() == "registration plate")
	{
		sort_by(CarsService::compare_by_number_plate);
	}
	else if (sort_options->currentText() == "producer & model")
	{
		sort_by(CarsService::compare_by_prod_model);
	}
	else if (sort_options->currentText() == "(default)")
	{
		update_list();
	}
}

void Body::sort_by_plates()
{
	sort_by(CarsService::compare_by_number_plate);
}

void Body::sort_by_prod_mod()
{
	sort_by(CarsService::compare_by_prod_model);
}

void Body::btn_filter_click()
{	

	string plate = car_plate_box->text().toStdString();
	string producer = car_producer_box->text().toStdString();
	string model = car_model_box->text().toStdString();
	string type = car_type_box->text().toStdString();

	if (plate == "") plate = "*";
	if (producer == "") producer = "*";
	if (model == "") model = "*";
	if (type == "") type = "*";

	
	FilterResults* filter = new FilterResults();	

	service.filter([&plate, &producer, &model, &type](const Car& car)
		{
			bool ok = true;
			if (plate != "*")
			{
				ok &= car.get_number_plate().get_value() == plate;
			}
			if (producer != "*")
			{
				ok &= car.get_producer() == producer;
			}
			if (model != "*")
			{
				ok &= car.get_model() == model;
			}
			if (type != "*")
			{
				ok &= car.get_type() == type;
			}
			return ok;
		}, 
		[filter](const Car& car) {filter->add_car(car); });

	filter->show();
}

void Body::btn_wl_view()
{	
	WashListCrudGUI* wl_crud = new WashListCrudGUI(wash_list);
	wl_crud->setStyleSheet(default_style);
	wl_crud->show();
}

void Body::btn_wl_view_rdo()
{
	WashListReadOnlyGUI* wl_rdo = new WashListReadOnlyGUI(wash_list);
	wl_rdo->setStyleSheet(default_style);
	wl_rdo->show();
}