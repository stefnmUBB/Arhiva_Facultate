#include <QtWidgets/QApplication>

#include "ui/ui.h"

int main(int argc, char *argv[])
{    
    QApplication a(argc, argv);

    DeviceRepo repo("devices.txt");
    DeviceService service(repo);

    Body* body = new Body(service);
    body->show();

    return a.exec();
}
