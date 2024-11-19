package org.example.pages;

import net.serenitybdd.core.annotations.findby.FindBy;
import net.serenitybdd.core.pages.WebElementFacade;
import net.thucydides.core.annotations.DefaultUrl;
import net.thucydides.core.pages.PageObject;
import org.example.utils.Configuration;
import org.openqa.selenium.By;

@DefaultUrl(Configuration.BASE_URL)
public class LoginPage extends PageObject {
    @FindBy(id = "user-name")
    private WebElementFacade usernameField;
    @FindBy(id = "password")
    private WebElementFacade passwordField;
    @FindBy(id = "login-button")
    private WebElementFacade loginButton;

    public void setUsernameField(String username) {
        waitFor(usernameField);
        typeInto(usernameField, username);
    }

    public void setPasswordField(String password) {
        waitFor(passwordField);
        passwordField.waitUntilClickable();
        typeInto(passwordField, password);
    }

    public void clickLoginButton() {
        loginButton.click();
    }

    public boolean loginErrorMessageIsVisible() {
        WebElementFacade loginErrorMessage = find(By.cssSelector("[data-test='error']"));
        return loginErrorMessage.isVisible();
    }
}
