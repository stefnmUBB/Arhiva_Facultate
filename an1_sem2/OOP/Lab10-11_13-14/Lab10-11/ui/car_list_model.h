#pragma once

#include <QtWidgets/QListWidget>
#include <QAbstractListModel>
#include <QAbstractTableModel>
#include "../Domain/car.h"

class CarListModel : public QAbstractTableModel
{
private:
	std::vector<Car> cars_list;	 
public:
	CarListModel(QObject* parent);
	QVariant data(const QModelIndex& index, int role) const;
	int rowCount(const QModelIndex& parent = QModelIndex()) const;
	int columnCount(const QModelIndex& parent = QModelIndex()) const;
	QVariant headerData(int section, Qt::Orientation orientation, int role) const;
	void set_cars(const vector<Car>& cars_list);
};

