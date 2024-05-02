package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationReceiver;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

public class Controller {

    IAppService appService;
    INotificationReceiver notificationReceiver;

    public INotificationReceiver getNotificationReceiver() {
        return notificationReceiver;
    }

    public void setNotificationReceiver(INotificationReceiver notificationReceiver) {
        this.notificationReceiver = notificationReceiver;
    }

    public IAppService getAppService() {
        return appService;
    }

    public void setAppService(IAppService appService) {
        this.appService = appService;
    }

    public static void displayException(Exception e){
        Utils.showMessageBox(e.getMessage());
    }
}
