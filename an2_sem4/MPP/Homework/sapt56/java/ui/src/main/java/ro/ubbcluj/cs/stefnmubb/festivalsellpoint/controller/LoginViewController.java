package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;


import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

public class LoginViewController extends Controller {
    @FXML
    TextField usernameBox;
    @FXML
    TextField passwordBox;
    @FXML
    Button loginButton;

    public void onLoginButtonClicked() {
        var username = usernameBox.getText();
        var password = passwordBox.getText();
        try {
            /*var angajat = appService.loginAngajat(username, password);
            if(angajat==null){
                throw new RuntimeException("Wrong username or password");
            }*/

            Utils.createSpectacoleWindow(appService).show();
        } catch (Exception e) {
            throw new RuntimeException(e);
            //displayException(e);
        }
    }

}
