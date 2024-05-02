package com.helpinghands.client_admin;

import com.helpinghands.domain.Admin;
import com.helpinghands.domain.Utilizator;
import com.helpinghands.service.IService;
import com.helpinghands.service.ServiceException;
import com.helpinghands.service.data.UserInfo;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

import java.io.IOException;
import java.util.Objects;

public class LoginPageController {
    private IService service;

    @FXML
    TextField loginUsername;

    @FXML
    PasswordField loginPassword;

    @FXML
    Button loginButton;

    public void setService(IService service){
        this.service = service;
    }

    @FXML
    void loginButtonClicked() throws IOException {
        String username = loginUsername.getText();
        String password = loginPassword.getText();

        if( loginUsername.getText().isEmpty() || loginPassword.getText().isEmpty()){
            MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info","Introduceti datele corespunzatoare!");
            return;
        }

        try {
            UserInfo userInfo = service.login(username, password);
            if(Objects.equals(userInfo.getType(), "Voluntar")){
                MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info","Accesul este restrictionat! Trebuie introduse credentialele unui administrator!");
            }else {
                Utilizator utilizator = userInfo.getUtilizator();
                FXMLLoader loader = new FXMLLoader();
                loader.setLocation(getClass().getResource("/com/helpinghands/client_admin/adminPage.fxml"));
                AnchorPane root = loader.load();

                AdminPageController adminPageController = loader.getController();
                adminPageController.setServer(service);
                adminPageController.setAdmin(userInfo);

                Stage stage = new Stage();
                stage.setScene(new Scene(root, 900, 700));
                stage.setTitle("Hello, " + utilizator.getPrenume()+" "+ utilizator.getNume() +"!");
                stage.show();

                Stage thisStage = (Stage) loginButton.getScene().getWindow();
                thisStage.close();
            }
        }
        catch (Exception e){
            MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info", e.getMessage());
            return;
        }
        /*catch (RuntimeException e){
            MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info", e.getMessage());
            return;
        }*/

    }

}

