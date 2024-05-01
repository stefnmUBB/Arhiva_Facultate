#pragma once

#include <QAbstractTableModel>
#include "../Service/service.h"

class TableModel : public QAbstractTableModel
{
private:
	vector<Melody> items;
	int n_ranks[11];
public:
	virtual int rowCount(const QModelIndex& parent = QModelIndex()) const override;
	virtual int columnCount(const QModelIndex& parent = QModelIndex()) const override;
	virtual QVariant data(const QModelIndex& index, int role = Qt::DisplayRole) const override;
	virtual QVariant headerData(int section, Qt::Orientation orientation,int role = Qt::DisplayRole) const override;

	TableModel(const vector<Melody>& items);

	void update(const vector<Melody> items);
};

