package org.example.features.function;

import net.serenitybdd.junit.runners.SerenityParameterizedRunner;
import net.thucydides.core.annotations.Managed;
import net.thucydides.core.annotations.Steps;
import net.thucydides.junit.annotations.UseTestDataFrom;
import org.example.steps.serenity.EndUserSteps;
import org.example.utils.Configuration;
import org.junit.Before;
import org.junit.Ignore;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.openqa.selenium.WebDriver;

@RunWith(SerenityParameterizedRunner.class)
@UseTestDataFrom("features/function/loginValidData.csv")
public class LoginValidDataTest {
    public LoginValidDataTest(){
        System.setProperty("webdriver.gecko.driver", Configuration.GECKO_DRIVER_PATH);
    }

    @Managed(uniqueSession = true)
    public WebDriver webdriver;

    @Steps
    public EndUserSteps user;

    public String username, password;

    @Before
    public void maximize() {
        webdriver.manage().window().maximize();
    }

    @Test
    //@Ignore
    public void test_login_with_valid_username_and_password() {
        user.logsIn(username,password);
        user.checkLoginSuccessful();
    }
}
