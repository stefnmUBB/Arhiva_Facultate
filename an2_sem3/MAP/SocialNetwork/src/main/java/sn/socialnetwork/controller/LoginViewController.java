package sn.socialnetwork.controller;

import javafx.fxml.FXML;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.stage.Stage;
import sn.socialnetwork.domain.User;
import sn.socialnetwork.service.Auth;

import java.io.IOException;
import java.util.Objects;

public class LoginViewController extends SocialNetworkController {
    @FXML
    TextField emailTextField;

    @FXML
    TextField passwordTextField;

    @FXML
    Button loginButton;
    @FXML
    Hyperlink registerButton;

    @FXML
    Label errorLabel;

    public void loginButtonClicked() {
        String email = emailTextField.getText();
        String password = passwordTextField.getText();

        User user = new Auth(getNetwork()).login(email, password);
        System.out.println(user);
        if(user==null) {
            displayError("Incorrect username or password");
            return;
        }

        Stage userViewStage = createStage("user-view.fxml",800,600);
        UserViewController userViewController = (UserViewController)userViewStage.getUserData();
        userViewController.setUser(user);
        userViewStage.show();
        getStage(loginButton).close();
    }

    private void displayError(String text) {
        errorLabel.setText(text);
        errorLabel.setVisible(!Objects.equals(text, ""));
    }

    public void setCredentials(String email, String password) {
        emailTextField.setText(email);
        passwordTextField.setText(password);
    }
}
