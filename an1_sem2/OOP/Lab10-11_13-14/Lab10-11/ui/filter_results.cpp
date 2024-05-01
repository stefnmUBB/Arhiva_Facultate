#include "filter_results.h"

#include "body.h"

FilterResults::FilterResults()
{
	layout = new QVBoxLayout(this);
	list = new QListWidget();
	layout->addWidget(list);

	close_btn = new QPushButton();	
	close_btn->setText("Close");
	layout->addWidget(close_btn);

	connect(close_btn, &QPushButton::clicked, this, &FilterResults::close);

	setWindowModality(Qt::ApplicationModal);
}

void FilterResults::add_car(const Car& car)
{
	QListWidgetItem* item = new QListWidgetItem();
	Body::set_item_car_data(item, car);
	list->addItem(item);
};