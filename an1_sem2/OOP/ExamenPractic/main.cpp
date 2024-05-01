#include "repo/repo.h"
#include "service/service.h"
#include "ui/ui.h"

//#define TESTS

#ifdef TESTS
#include "Tests/tests.h"
int main()
{    
    test_all();

    /*Repo r{"items_ok.txt"};

    for (int i = 0; i < 50; i++)
    {
        r.add(Melody
            {
                i,
                "title1" + std::to_string(rand() % 15),
                "artist1" + std::to_string(rand() % 10),
                1 + std::rand() % 10
            });
    }
    r.save();*/
}
#else
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    Repo r{ "items.txt" };
    Service s{ r };
    UI* ui = new UI{ s };
    ui->show();
    return a.exec();
}
#endif
