#pragma once

#include <QWidget>
#include <QPaintEvent>

class Graph : public QWidget
{
private:
	int n_ranks[11];
public:
	void paintEvent(QPaintEvent* e) override;
	void set_ranks(int rks[11]);
};

