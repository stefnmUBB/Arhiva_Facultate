package org.example.features.scenarios;

import groovy.lang.Tuple;
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
public class FullScenarioTest {
    public FullScenarioTest(){
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
    // @Ignore
    public void test_valid_login_add_to_card_remove_from_cart_logout() throws InterruptedException {
        // Login
        user.logsIn(username,password);
        user.checkLoginSuccessful();

        // Add to cart
        user.addItemToCart(0);
        user.checkShoppingCart(CartItem_Backpack);

        // Remove from cart
        user.removeItemFromCart(0);
        user.checkShoppingCart();

        // (Optional) Combined Add/Remove
        user.navigateHome();
        user.addItemToCart(0);
        user.addItemToCart(1);
        user.checkShoppingCart(CartItem_Backpack, CartItem_BikeLight);

        System.out.println("Step 1!!!!!!!!!!!!!!!!1");
        //Thread.sleep(60000);
        user.removeItemFromCart(0);
        System.out.println("Step 2!!!!!!!!!!!!!!!!1");
        //Thread.sleep(60000);
        user.checkShoppingCart(CartItem_BikeLight);

        user.navigateHome();
        user.addItemToCart(0);
        user.addItemToCart(2);
        user.checkShoppingCart(CartItem_BikeLight, CartItem_Backpack, CartItem_TShirt);

        user.removeItemFromCart(2);
        user.checkShoppingCart(CartItem_BikeLight, CartItem_Backpack);
        user.removeItemFromCart(0);
        user.checkShoppingCart(CartItem_Backpack);
        user.removeItemFromCart(0);
        user.checkShoppingCart();

        // Logout
         user.logsOut();
         user.checkLogoutSuccessful();
    }

    private static final Tuple<String> CartItem_Backpack = new Tuple<>("Sauce Labs Backpack", "$29.99");
    private static final Tuple<String> CartItem_BikeLight = new Tuple<>("Sauce Labs Bike Light", "$9.99");
    private static final Tuple<String> CartItem_TShirt = new Tuple<>("Sauce Labs Bolt T-Shirt", "$15.99");
}
