#include "graph.h"

#include <QPainter>

void Graph::paintEvent(QPaintEvent* e)
{
	QPainter p{ this };

	p.fillRect(0, 0, width(), height(), QBrush{ QColor{255,255,255} });

	int stride = width() / 11;
	int h = height();

	int mx = 5;
	for (int i = 0; i <= 10; i++)
		mx = n_ranks[i] > mx ? n_ranks[i] : mx;

	int r1 = 0x00;
	int g1 = 0x5A;
	int b1 = 0x9C;

	int r2 = 0;
	int g2 = 255;
	int b2 = 255;

	for (int i = 0; i <= 10; i++)
	{
		int y = n_ranks[i] * h / mx;
		int r = (r1 * i + r2 * (11 - i)) / 11;
		int g = (g1 * i + g2 * (11 - i)) / 11;
		int b = (b1 * i + b2 * (11 - i)) / 11;
		p.fillRect(
			stride * i+2,
			h - y,
			stride-2,
			y,
			QBrush{ QColor{r, g, b} }
		);
	}
}

void Graph::set_ranks(int rks[11])
{
	for(int i=0;i<11;i++)
		n_ranks[i] = rks[i];
	update();
}