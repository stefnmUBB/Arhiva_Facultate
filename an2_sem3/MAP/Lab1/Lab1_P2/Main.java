package Lab1_P2;
// 2. Read a string, integer and float from the console

import java.util.InputMismatchException;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        try {
            System.out.println("string s = ");
            String s = scan.nextLine();
            System.out.println("Set s = \"" + s + "\"");

            System.out.print("int x = ");
            int x = scan.nextInt();
            System.out.println("Set x=" + x);

            System.out.println("float f = ");
            ;
            float f = scan.nextFloat();
            System.out.printf("Set f=" + f);
        }
        catch(InputMismatchException e)
        {
            System.out.println("Invalid input");
        }



    }
}
