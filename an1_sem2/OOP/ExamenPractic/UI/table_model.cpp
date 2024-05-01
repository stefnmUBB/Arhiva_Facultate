#include "table_model.h"

int TableModel::rowCount(const QModelIndex& parent) const
{
    return items.size();
}

int TableModel::columnCount(const QModelIndex& parent) const
{
    return 5;
}

QVariant TableModel::data(const QModelIndex& index, int role) const
{
    int row = index.row();
    int col = index.column();
    if (role == Qt::DisplayRole)
    {
        switch (col)
        {
        case 0: // id
            return items[row].get_id();
        case 1: // title
            return items[row].get_title().c_str();
        case 2: // artist
            return items[row].get_artist().c_str();
        case 3: // rank
            return items[row].get_rank();
        case 4: // nr. mel.
            return n_ranks[items[row].get_rank()];
        }
    }
    else if (role == Qt::UserRole)
    {
        return QVariant::fromValue<Melody>(items[row]);
    }
    return QVariant();
}

QVariant TableModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if (orientation == Qt::Orientation::Horizontal && role==Qt::DisplayRole)
    {
        switch (section)
        {
        case 0: // id
            return "Id";
        case 1: // title
            return "Title";
        case 2: // artist
            return "Artist";
        case 3: // rank
            return "Rank";
        case 4: // nr. mel.
            return "# rank";
        }
    }
    else if (orientation == Qt::Orientation::Vertical && role == Qt::DisplayRole)
        return section;
    return QVariant();
}

TableModel::TableModel(const vector<Melody>& items) : items{items}
{
    update(items);
}

void TableModel::update(const vector<Melody> items)
{
    this->items = items;

    for (int i = 0; i <= 10; i++) n_ranks[i] = 0;
    for (const auto& m : items)
    {
        n_ranks[m.get_rank()]++;
    }

    auto topLeft = index(0, 0);
    auto bottomRight = index(rowCount(), columnCount());
    emit dataChanged(topLeft, bottomRight);
    emit layoutChanged();
}