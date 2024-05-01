#include "ui.h"
#include <QMessageBox>
#include <map>
#include <algorithm>

std::map<std::string, std::pair<QBrush, QBrush>> colors =
{
	{"rosu", {Qt::red, Qt::white}},
	{"albastru",{Qt::blue,Qt::white}},
	{"negru",{Qt::black,Qt::white}},
	{"galben",{Qt::yellow,Qt::white}}
};

Body::Body(DeviceService& _service): service(_service) 
{

	body_layout->addWidget(list);

	info_layout->addWidget(model_label,0,0);
	info_layout->addWidget(year_label,1,0);

	info_layout->addWidget(model_box,0,1);
	info_layout->addWidget(year_box, 1, 1);

	model_box->setReadOnly(true);
	year_box->setReadOnly(true);

	info_layout->addWidget(fil_model_btn, 2, 0, 1, 2);	
	info_layout->addWidget(fil_year_btn, 3, 0, 1, 2);	
	info_layout->addWidget(reload_btn, 4, 0, 1, 2);		


	body_layout->addWidget(info_panel);

	connect(list, &QListWidget::itemSelectionChanged, this, &Body::item_sel_changed);
	connect(fil_model_btn, &QPushButton::clicked, this, &Body::fil_model_click);
	connect(fil_year_btn, &QPushButton::clicked, this, &Body::fil_year_click);
	connect(reload_btn, &QPushButton::clicked, this, &Body::reload_click);


	populate(service.get_items());
}

void Body::fil_model_click()
{
	std::string _model = model_box->text().toStdString();
	if (_model == "")
	{
		QMessageBox::warning(this, "Error", "Nothing to filter");
		return;
	}
	const std::vector<Device>& source = service.filter([&_model](const Device& dev)
		{
			return dev.get_model() == _model;
		});
	populate(source);
}

void Body::fil_year_click()
{
	std::string _year = year_box->text().toStdString();
	if (_year == "")
	{
		QMessageBox::warning(this, "Error", "Nothing to filter");
		return;
	}
	int yy = atoi(_year.c_str());
	const std::vector<Device>& source = service.filter([&yy](const Device& dev)
		{
			return dev.get_year() == yy;
		});
	populate(source);

}

void Body::reload_click()
{
	populate(service.get_items());
}


void Body::item_sel_changed()
{
	if (list->selectedItems().size() == 0)
	{
		return;
	}
	Device dev = list->selectedItems()[0]->data(Qt::UserRole).value<Device>();
	model_box->setText(QString::fromStdString(dev.get_model()));
	year_box->setText(QString::fromStdString(std::to_string(dev.get_year())));

	//QMessageBox::warning(this, "123", "456");
}

void Body::populate(const std::vector<Device>& source)
{	
	list->clear();
	for (const Device& dev : source)
	{
		QListWidgetItem* item = new QListWidgetItem();
		item->setText(QString::fromStdString(
			dev.get_type() + " " + dev.get_model() + " (" + std::to_string(dev.get_price()) + ")"));

		if (2022 - dev.get_year() <= 3)
		{
			item->setBackground(colors[dev.get_color()].first);
			item->setForeground(colors[dev.get_color()].second);
		}

		item->setData(Qt::UserRole, QVariant::fromValue(dev));
		list->addItem(item);
	}
}