#include "washlist_read_only_gui.h"

#include <QPaintEvent>
#include <QPainter>
#include <QPixmap>
#include <QMessageBox>

WashListReadOnlyGUI::WashListReadOnlyGUI(WashList& wash_list) : wash_list(wash_list)
{
	setWindowTitle("Washlist ReadOnly");
	observer = new ReadOnlyGuiWashListObserver(this);
	wash_list.addObserver(observer);
	refresh_wl();
	timer->setInterval(50);
	connect(timer, &QTimer::timeout, this, &WashListReadOnlyGUI::on_timer);
	timer->start();
}

WashListReadOnlyGUI::~WashListReadOnlyGUI()
{
	wash_list.removeObserver(observer);
	delete observer;
}

void WashListReadOnlyGUI::on_timer()
{	
	for (auto& sprite : car_sprites)
	{
		//QMessageBox::warning(this, "123", "456");
		sprite.update(width(), height());
	}
	this->update();
}

void WashListReadOnlyGUI::refresh_wl()
{	
	car_sprites.clear();
	for (auto car : wash_list.get_cars())
	{		
		CarSprite sprite(rand() % width(), rand() % height());
		car_sprites.push_back(sprite);
	}
	this->update();
}

#include<QMessageBox>

void WashListReadOnlyGUI::paintEvent(QPaintEvent* ev)
{
	QPainter p{ this };	
	for (auto sprite : car_sprites)
	{		
		p.drawImage(sprite.x - 80, sprite.y - 60, sprite.image.scaled(160, 120));
	}
	
}

ReadOnlyGuiWashListObserver::ReadOnlyGuiWashListObserver(WashListReadOnlyGUI* gui) : gui{ gui } {}

void ReadOnlyGuiWashListObserver::update()
{
	gui->refresh_wl();
}