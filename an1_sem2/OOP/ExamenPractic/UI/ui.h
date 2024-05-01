#pragma once

#include <QWidget>
#include <QPushButton>
#include <QLayout>
#include <QLabel>
#include <QTableView>
#include <QSlider>
#include <QLineEdit>

#include "../Service/service.h"
#include "graph.h"
#include "table_model.h"

#define new_layout(layout_t, name) \
	QWidget* name = new QWidget; \
	layout_t* name##_layout = new layout_t {name};

class UI : public QWidget
{
private:
	Service& service;

	QHBoxLayout* main_layout = new QHBoxLayout{ this };

	new_layout(QVBoxLayout, left);
	new_layout(QGridLayout, right);

	QTableView* table = new QTableView;
	TableModel* model = new TableModel{ service.sorted() };

	QLabel* title_label = new QLabel{"Title : "};
	QLabel* rank_label = new QLabel{"Rank : "};

	QLineEdit* title_field = new QLineEdit;
	QSlider* rank_field = new QSlider{ Qt::Orientation::Horizontal };
	
	QPushButton* update_btn = new QPushButton{ "Update" };
	QPushButton* remove_btn = new QPushButton{ "Remove" };

	Graph* graph = new Graph;

	void do_connects();
public:
	UI(Service& s);
	void update_graph();
};

