package Lab1_P3;
// 3. Find Largest and Smallest Number in an Array

import java.util.InputMismatchException;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int n;
        int[] arr;
        try
        {
            System.out.print("n = ");
            n=in.nextInt();
            if(n<=0)
                throw new Exception();
            arr=new int[n];
            for(int i=0;i<n;i++)
                arr[i]=in.nextInt();

            int min=arr[0];
            int max=arr[0];
            for(int i=1;i<n;i++) {
                min = Math.min(arr[i], min);
                max = Math.max(arr[i], max);
            }
            System.out.println("Min="+min);
            System.out.println("Max="+max);
        }
        catch (InputMismatchException e)
        {
            System.out.println("Invalid input");
        }
        catch(Exception e)
        {
            System.out.println("Array length must be >0");
        }
    }
}
