//#define TESTS

#ifdef TESTS
#include "Tests/test_all.h"
#else
#include <QtWidgets/QApplication>
#include "ui/body.h"
#endif // !TESTS

int main(int argc, char* argv[])
{
#ifndef TESTS
    CarsService service;
    WashList wash_list(service.get_repo());
    QApplication a(argc, argv);
    Body main_body(service, wash_list);
    main_body.show();
    return a.exec();
#else
    test_all();
    return 0;
#endif // !TESTS
}
