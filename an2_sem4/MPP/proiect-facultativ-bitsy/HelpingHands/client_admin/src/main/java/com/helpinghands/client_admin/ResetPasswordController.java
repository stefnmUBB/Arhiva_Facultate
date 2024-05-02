package com.helpinghands.client_admin;

import com.helpinghands.service.IService;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;

import javafx.scene.control.TextField;

import java.io.IOException;


public class ResetPasswordController {

    @FXML
    Button resetButton2;

    @FXML
    TextField usernameField;
    @FXML
    PasswordField passwordField;
    @FXML
    PasswordField confirmPasswordField;

    private IService service;

    public void setService(IService service){
        this.service=service;
    }

    @FXML
    public void handleResetPassword()throws IOException {
        String username = usernameField.getText();
        String password = passwordField.getText();
        String confirmPassword = confirmPasswordField.getText();
        if (!password.equals(confirmPassword)) {
            MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info", "Parolele nu coincid!");
        }
        else {
            try {
                service.resetPassword(username, password);
                MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info", "Parola a fost resetata cu succes!");
            } catch (Exception e) {
                MessageAlert.showMessage(null, Alert.AlertType.INFORMATION, "Info", e.getMessage());
            }
        }
    }

}
