package pizzashop.gui;

import javafx.event.EventHandler;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;
import pizzashop.controller.OrdersGUIController;
import pizzashop.service.PizzaService;

import java.io.IOException;

public class OrdersGUI {
    protected int tableNumber;
    public int getTableNumber() {
        return tableNumber;
    }
    public void setTableNumber(int tableNumber) { this.tableNumber = tableNumber; }
    private PizzaService service;

    public void displayOrdersForm(PizzaService service){
     VBox vBoxOrders = null;
        try {
            FXMLLoader loader = new FXMLLoader(getClass().getResource("/fxml/OrdersGUIFXML.fxml"));

            //vBoxOrders = FXMLLoader.load(getClass().getResource("/fxml/OrdersGUIFXML.fxml"));
            vBoxOrders = loader.load();
            OrdersGUIController ordersCtrl= loader.getController();
            ordersCtrl.setService(service, tableNumber);

        } catch (IOException e) {
            e.printStackTrace();
        }
        catch (NumberFormatException e){
            // in repo cand inputul in menu.txt nu e corect
            System.out.println(e.getMessage());
        }

     Stage stage = new Stage();
     stage.setTitle("Table"+getTableNumber()+" order form");
     stage.setResizable(false);
     // disable X on the window
     stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
      @Override
     public void handle(WindowEvent event) {
         // consume event
         event.consume();
            }
        });
     stage.setScene(new Scene(vBoxOrders));
     stage.show();
    }
}