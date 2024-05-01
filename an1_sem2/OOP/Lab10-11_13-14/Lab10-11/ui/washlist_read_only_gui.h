#pragma once

#include <QtWidgets/QWidget>
#include <QTimer>
#include <QMessageBox>
#include <qdebug.h>
#include "../../Users/Stefan/Desktop/an-1-sem-2/OOP/Lab10-11/Lab10-11/Service/wash_list.h"

class CarSprite
{
public:
	int x = 0;
	int y = 0;
	QImage image;
	int t = 0;
	int speed = 1;

	CarSprite(int x, int y)
	{
		this->x = x;
		this->y = y;
		t = rand() % 360;
		speed = rand() % 5 + 2;
		image.load("./car_gfx.png");
		paint();
	}
	void update(int w, int h)
	{
		x += speed * cos(t * M_PI / 180);
		y += speed * sin(t * M_PI / 180);
		if (x < 0 || y < 0 || x >= w || y >= h)
		{
			x = w / 2;
			y = h / 2;
			t += (rand() % 5 - 1) * 45;
			t %= 360;
		}
	}

	void paint()
	{
		uint cl = 0xFF;;
		cl <<= 8; cl |= rand() & 255;
		cl <<= 8; cl |= rand() & 255;
		cl <<= 8; cl |= rand() & 255;
				
		for(int y=0;y<120;y++)
			for (int x = 0; x < 160; x++)
			{								
				if (image.pixel(x, y) == 0xFFFF00FF)
				{					
					image.setPixel(x, y, cl);
				}
			}		
	}
};

class ReadOnlyGuiWashListObserver;

class WashListReadOnlyGUI : public QWidget
{
private:
	WashList& wash_list;
	ReadOnlyGuiWashListObserver* observer;
	std::vector<CarSprite> car_sprites;
	QTimer* timer = new QTimer{ this };

public:
	WashListReadOnlyGUI(WashList& wash_list);
	~WashListReadOnlyGUI();
	void refresh_wl();
	void paintEvent(QPaintEvent* ev) override;

	void on_timer();
	friend class ReadOnlyGuiWashListObserver;
};


class ReadOnlyGuiWashListObserver : public Observer
{
private:
	WashListReadOnlyGUI* gui;
public:
	ReadOnlyGuiWashListObserver(WashListReadOnlyGUI* gui);
	void update() override;
};