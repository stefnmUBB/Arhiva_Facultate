package com.helpinghands.client_admin;

import com.helpinghands.service.ServiceException;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;

public class HelloApplication extends Application {
    @Override
    public void start(Stage stage) throws IOException {
        var service=new ClientServiceImpl();
        /*try {
            var a = service.login("user1","0001");
            System.out.println(a);
        } catch (ServiceException e) {
            throw new RuntimeException(e);
        }*/

        FXMLLoader loader = new FXMLLoader(
                getClass().getClassLoader().getResource("com/helpinghands/client_admin/loginPage.fxml"));
        Parent root=loader.load();

        LoginPageController loginPageController = loader.<LoginPageController>getController();
        loginPageController.setService(service);

        stage.setTitle("Helping Hands Administration");
        stage.setScene(new Scene(root,788,518));
        stage.show();
    }

    public static void main(String[] args) {
        launch();
    }
}