package sn.socialnetwork.controller;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Control;
import javafx.stage.Stage;
import sn.socialnetwork.service.Network;
import sn.socialnetwork.ui.SocialNetworkApplication;

import java.io.IOException;

public class SocialNetworkController {
    private Network network = null;

    public Network getNetwork() {
        return network;
    }

    public void setNetwork(Network network) {
        this.network = network;
    }

    public Stage createStage(String view, int width, int height) {
        try {
            Stage stage = new Stage();
            FXMLLoader fxmlLoader = SocialNetworkApplication.getLoader(view);
            Scene scene = new Scene(fxmlLoader.load(), width, height);
            scene.setUserData(fxmlLoader);

            SocialNetworkController controller = fxmlLoader.getController();
            controller.setNetwork(Globals.getNetwork());

            stage.setUserData(controller);
            stage.setTitle("Social Network");
            stage.setScene(scene);

            return stage;
        }
        catch (IOException e){
            throw new RuntimeException(e);
        }
    }

    Stage getStage(Control control) {
        return (Stage)control.getScene().getWindow();
    }

    void showErrorBox(String message) {
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
