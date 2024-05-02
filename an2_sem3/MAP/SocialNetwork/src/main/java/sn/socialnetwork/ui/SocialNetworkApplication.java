package sn.socialnetwork.ui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import sn.socialnetwork.controller.Globals;
import sn.socialnetwork.controller.LoginViewController;

import java.io.IOException;

public class SocialNetworkApplication extends Application {

    @Override
    public void start(Stage stage) throws IOException {
        FXMLLoader fxmlLoader = getLoader("login-view.fxml");
        Scene scene = new Scene(fxmlLoader.load(), 600, 400);

        LoginViewController controller = fxmlLoader.getController();
        controller.setNetwork(Globals.getNetwork());

        stage.setResizable(false);
        stage.setTitle("Social Network");
        stage.setScene(scene);
        stage.show();
    }

    public static FXMLLoader getLoader(String name) {
        return new FXMLLoader(SocialNetworkApplication.class.getResource(name));
    }


    public static void main(String[] args) {
        launch();
    }
}
