package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;


import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client.ServiceObjectProxy;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

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
        Angajat angajat=null;
        Stage window=null;
        try {
            window = Utils.createSpectacoleWindow(appService);
            SpectacoleViewController controller = Utils.getController(window);
            angajat = appService.loginAngajat(username, password, controller);
        }
        catch (ServiceException e){

        }
        if(angajat==null){
            displayException(new RuntimeException("Wrong username or password"));
            Platform.exit();
        }
        else {
            Angajat thisAngajat = angajat;
            window.setOnCloseRequest(t -> {
                appService.logout(thisAngajat);
                /*if(appService instanceof ServiceObjectProxy) {
                    ((ServiceObjectProxy)appService).closeConnection();
                }*/
                Platform.exit();
                System.exit(0);
            });

            window.show();
            loginButton.getScene().getWindow().hide();
        }
    }
}
