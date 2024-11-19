package org.example.pages;

import groovy.lang.Tuple;
import net.serenitybdd.core.annotations.findby.FindBy;
import net.serenitybdd.core.pages.WebElementFacade;
import net.thucydides.core.pages.PageObject;
import org.junit.Assert;
import org.openqa.selenium.By;

import java.util.ArrayList;
import java.util.List;

public class ShoppingCartPage extends PageObject {
    @FindBy(css = "[data-test='inventory-item']")
    private List<WebElementFacade> shoppingCartItems;

    public void clickRemoveButton(int itemIndex){
        waitFor(shoppingCartItems.get(itemIndex));
        WebElementFacade item = shoppingCartItems.get(itemIndex);
        WebElementFacade removeButton = item.find(By.className("cart_button"));
        Assert.assertEquals("Remove", removeButton.getText());
        removeButton.click();
    }

    public int getQuantityOfShoppingCart() {
        return shoppingCartItems.size();
    }

    public List<Tuple<String>> getShoppinCart() {
        List<Tuple<String>> items = new ArrayList<>();
        for (WebElementFacade shoppingCartItem : shoppingCartItems) {
            waitFor(shoppingCartItem);
            String itemName = shoppingCartItem.find(By.className("inventory_item_name")).getText();
            String price = shoppingCartItem.find(By.className("inventory_item_price")).getText();
            items.add(new Tuple<>(itemName, price));
        }
        return items;
    }

}
