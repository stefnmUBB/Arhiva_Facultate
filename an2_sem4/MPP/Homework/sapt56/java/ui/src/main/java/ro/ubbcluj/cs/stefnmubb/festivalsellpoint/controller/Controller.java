package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.stage.Stage;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.Application;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

import java.io.IOException;

public class Controller {

    IAppService appService;

    public IAppService getAppService() {
        return appService;
    }

    public void setAppService(IAppService appService) {
        this.appService = appService;
    }

    public static void displayException(Exception e){
        Utils.showErrorBox(e.getMessage());
    }
}
