package pizzashop.repository;
import java.util.logging.Logger;


import pizzashop.model.Order;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.StringTokenizer;

public class MenuRepository {
    private String filename;
    private List<Order> listMenu;

    public MenuRepository(String filename){
        this.filename = filename;
    }

    private void readMenu(){
        //ClassLoader classLoader = MenuRepository.class.getClassLoader();
        File file = new File(filename);
        this.listMenu= new ArrayList();
        try(BufferedReader br = new BufferedReader(new FileReader(file))) {
            String line = null;
            while((line=br.readLine())!=null){
                Order menuItem=getMenuItem(line);
                listMenu.add(menuItem);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    Logger logger = Logger.getLogger(getClass().getName());
    private Order getMenuItem(String line) throws IllegalArgumentException {
        try {
            Order item = null;
            if (line == null || line.equals("")) return null;
            StringTokenizer st = new StringTokenizer(line, ",");

            String name = st.nextToken();
            double price = Double.parseDouble(st.nextToken());
            item = new Order(name, 0, price);
            return item;
        }
        catch (Exception e){
            throw new IllegalArgumentException(line+" => "+e.getMessage());
        }
    }

    public List<Order> getMenu(){
        readMenu();//create a new menu for each table, on request
        return listMenu;
    }
}
