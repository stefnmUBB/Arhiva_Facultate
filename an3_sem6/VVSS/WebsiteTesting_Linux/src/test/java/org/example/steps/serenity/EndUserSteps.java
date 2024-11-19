package org.example.steps.serenity;

import groovy.lang.Tuple;
import net.thucydides.core.annotations.Step;
import org.example.pages.HomePage;
import org.example.pages.LoginPage;
import org.example.pages.ShoppingCartPage;
import org.example.utils.Configuration;
import org.junit.Assert;

import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import static net.thucydides.core.webdriver.ThucydidesWebDriverSupport.getDriver;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.hasItem;

public class EndUserSteps {

    private LoginPage loginPage;
    private HomePage homePage;
    private ShoppingCartPage shoppingCartPage;

    @Step
    public  void logsIn(String username, String password) {
        loginPage.open();
        loginPage.setUsernameField(username);
        loginPage.setPasswordField(password);
        loginPage.clickLoginButton();
    }

    @Step
    public void checkLoginSuccessful() {
        Assert.assertTrue(homePage.isVisible());
        Assert.assertEquals(Configuration.BASE_URL + "inventory.html", getDriver().getCurrentUrl());
    }

    @Step
    public void checkLoginFailed() {
        Assert.assertTrue(loginPage.loginErrorMessageIsVisible());
    }

    @Step
    public void addItemToCart(int itemIndex) {
        homePage.clickAddToCartButtonOnItem(itemIndex);
    }

    @Step
    public void checkAddItemToCartSuccessful(int expectedBadgeCount) {
        navigateHome();
        Assert.assertEquals(expectedBadgeCount, homePage.getNumberOfAddedItemsBadgeCount());
        homePage.clickShoppingCartButton();
        Assert.assertEquals(expectedBadgeCount, shoppingCartPage.getQuantityOfShoppingCart());
    }

    @Step
    public void removeItemFromCart(int itemIndex) {
        shoppingCartPage.clickRemoveButton(itemIndex);
    }

    @SafeVarargs
    public final void checkShoppingCart(Tuple<String>... expectedItems){
        checkAddItemToCartSuccessful(expectedItems.length);
        List<Tuple<String>> items = shoppingCartPage.getShoppinCart();
        items.forEach(System.out::println);
        Assert.assertEquals(Arrays.stream(expectedItems).collect(Collectors.toList()), items);
    }

    public void navigateHome(){
        homePage.clickAllItemsButton();
    }

    @Step
    public void logsOut() {
        homePage.clickLogoutButton();
    }

    @Step
    public void checkLogoutSuccessful() {
        Assert.assertEquals(Configuration.BASE_URL, getDriver().getCurrentUrl());
    }

}