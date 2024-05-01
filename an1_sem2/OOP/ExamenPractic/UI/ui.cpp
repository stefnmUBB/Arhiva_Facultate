#include "ui.h"
#include <QMessageBox>

UI::UI(Service& s) : service(s)
{
	main_layout->addWidget(left);
	main_layout->addWidget(right);

	left_layout->addWidget(table);
	table->setModel(model);
	table->setMinimumWidth(600);
	table->setMinimumHeight(400);

	left_layout->addWidget(graph);
	graph->setFixedHeight(100);

	right_layout->setRowMinimumHeight(4, 300);
	right_layout->addWidget(title_label, 0, 0);
	right_layout->addWidget(rank_label, 1, 0);

	right_layout->addWidget(title_field, 0, 1);
	right_layout->addWidget(rank_field, 1, 1);

	rank_field->setMinimum(0);
	rank_field->setMaximum(10);
	rank_field->setEnabled(false);	
	title_field->setEnabled(false);
	rank_field->setValue(1);
	title_field->setText("Select a line...");

	right_layout->addWidget(update_btn, 2, 0);
	right_layout->addWidget(remove_btn, 3, 0);

	do_connects();
	update_graph();
	
}

void UI::do_connects()
{
	connect(table->selectionModel(), &QItemSelectionModel::selectionChanged, [&]()
		{
			if (table->selectionModel()->selectedRows().size() != 1)
			{				
				rank_field->setEnabled(false);
				title_field->setEnabled(false);
				rank_field->setValue(1);
				title_field->setText("Select a line...");
				return;
			}
			Melody mel = table->selectionModel()->selectedRows()[0].data(Qt::UserRole).value<Melody>();			
			title_field->setText(mel.get_title().c_str());
			rank_field->setValue(mel.get_rank());
			rank_field->setEnabled(true);
			title_field->setEnabled(true);
		});
	connect(update_btn, &QPushButton::clicked, [&]()
		{
			string title = title_field->text().toStdString();
			int rank = rank_field->value();
			int id = table->selectionModel()->selectedRows()[0].data(Qt::UserRole).value<Melody>().get_id();
			service.update(id, title, rank);
			model->update(service.sorted());
			update_graph();
		});
	connect(remove_btn, &QPushButton::clicked, [&]()
		{
			int id = table->selectionModel()->selectedRows()[0].data(Qt::UserRole).value<Melody>().get_id();
			try
			{
				service.remove(id);
				model->update(service.sorted());
				update_graph();
			}
			catch (std::exception& e)
			{
				QMessageBox::warning(this, "Error", e.what());
			}
		});
}

void UI::update_graph()
{
	int rks[11];
	for (int i = 0; i < 11; i++) rks[i] = 0;

	for (const auto& m : service.getAll())
	{
		rks[m.get_rank()]++;
	}

	graph->set_ranks(rks);
}