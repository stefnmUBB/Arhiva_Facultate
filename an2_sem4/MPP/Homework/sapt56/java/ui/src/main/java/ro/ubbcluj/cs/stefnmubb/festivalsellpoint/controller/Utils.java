package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.stage.Stage;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.Application;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

import java.io.IOException;

public class Utils {
    public static Stage createLoginWindow(IAppService appService){
        return createWindow("loginView.fxml", 500, 240
            , "Conectare Angajat", appService);
    }

    public static Stage createSpectacoleWindow(IAppService appService){
        return createWindow("spectacoleView.fxml", 800, 600
                , "Spectacole", appService);
    }

    public static Stage createWindow(String fxml, int width, int height) {
        try {
            var stage = new Stage();
            FXMLLoader fxmlLoader = getLoader(fxml);
            Scene scene = new Scene(fxmlLoader.load(), width, height);
            stage.setUserData(fxmlLoader);
            stage.setResizable(false);
            stage.setScene(scene);
            return stage;
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static Stage createWindow(String fxml, int width, int height
        ,String title, IAppService appService){
        var stage = createWindow(fxml, width, height);
        Controller controller = getController(stage);
        controller.setAppService(appService);
        stage.setTitle(title);
        return stage;
    }



    public static <C extends Controller> C getController(Stage stage){
        return ((FXMLLoader)stage.getUserData()).getController();
    }
    public static FXMLLoader getLoader(String name) {
        return new FXMLLoader(Application.class.getResource(name));
    }

    public static void showErrorBox(String message) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("Error");
        alert.setHeaderText("Something wrong happened");
        alert.setContentText(message);
        alert.showAndWait().ifPresent(rs -> {
            if (rs == ButtonType.OK) {
                System.out.println("Pressed OK.");
            }
        });
    }
}
