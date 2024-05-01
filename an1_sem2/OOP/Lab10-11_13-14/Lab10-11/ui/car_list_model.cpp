#include "car_list_model.h"
#include <QMessageBox>

CarListModel::CarListModel(QObject* parent) : QAbstractTableModel(parent)
{
	cars_list.push_back(Car(NumberPlate("AA01BBB"),"Skoda","Octavia","lalal"));
}

QVariant CarListModel::data(const QModelIndex& index, int role) const
{
	int row = index.row();
	int column = index.column();
	if (role == Qt::DisplayRole) {
		switch (column)
		{
		case 0: return QString::fromStdString(cars_list[row].get_number_plate().get_value());
		case 1: return QString::fromStdString(cars_list[row].get_producer());
		case 2: return QString::fromStdString(cars_list[row].get_model());
		case 3: return QString::fromStdString(cars_list[row].get_type());

		}
		return QString("col %1").arg(column);
		return  QString("R%1, C%2").arg(index.row() + 1).arg(index.column() + 1);	
	}
	if (role == Qt::FontRole) {
		QFont f;
		/*f.setItalic(row % 4 == 1);
		f.setBold(row % 2 == 1);*/
		return f;
	}
	if (role == Qt::BackgroundRole) 
	{
		if (row % 2 == 0)
			return QBrush(QColor::fromRgb(220,220,220));
		else return QBrush(Qt::white);
	}
	if (role == Qt::UserRole)
	{
		return QVariant::fromValue(cars_list[row]);
	}
	return QVariant();

}

int CarListModel::rowCount(const QModelIndex& /*parent*/) const {
	return cars_list.size();
}
int CarListModel::columnCount(const QModelIndex& /*parent*/) const {
	return 4;
}

QVariant CarListModel::headerData(int section, Qt::Orientation orientation, int role) const 
{
	if (role == Qt::DisplayRole) {
		if (orientation == Qt::Horizontal) {
			switch (section)
			{
				case 0: return QString("Number plate");
				case 1: return QString("Producer");
				case 2: return QString("Model");
				case 3: return QString("Type");

			}
			return QString("col %1").arg(section);
		}
		else {
			return QString("%1").arg(section);
		}
	}
	return QVariant();
}

void CarListModel::set_cars(const vector<Car>& cars_list)
{	
	this->cars_list = cars_list;
	QModelIndex topLeft = createIndex(0, 0);	
	QModelIndex bottomRight = createIndex(rowCount(), columnCount());	
	emit dataChanged(topLeft, bottomRight);
	emit layoutChanged();
}