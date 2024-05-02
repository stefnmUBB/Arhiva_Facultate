package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

public class Controller {

    IAppService appService;

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
