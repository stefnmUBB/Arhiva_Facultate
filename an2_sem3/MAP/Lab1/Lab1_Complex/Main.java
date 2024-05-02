package Lab1_Complex;

import Lab1_Complex.exceptions.InvalidOperatorSymbolException;
import Lab1_Complex.exceptions.InvalidTokenCountException;
import Lab1_Complex.exceptions.InvalidTokenException;
import Lab1_Complex.exceptions.OperatorException;
import Lab1_Complex.model.ComplexExpression;
import Lab1_Complex.model.ComplexNumber;
import Lab1_Complex.parser.ExpressionParser;

public class Main {
    public static void main(String[] args)
    {
        ComplexNumber a = new ComplexNumber(1,2);
        ComplexNumber b = new ComplexNumber(1,2);

        for(String arg : args){
            System.out.println(arg);
        }
        System.out.println(String.join(" ",args));
        ExpressionParser parser = new ExpressionParser(args);
        try {
            ComplexExpression expression = parser.parse();

            ComplexNumber result = expression.execute();
            System.out.println(result);
        }
        catch (Exception e){
            throw new RuntimeException();
            //System.out.println("General exception");
        }
        catch (InvalidTokenException e) {
            System.out.println("Could not parse token");
        } catch (InvalidTokenCountException e) {
            System.out.println("Invalid tokens count");
        } catch (OperatorException e) {
            System.out.println("Multiple operators detected");
        } catch (InvalidOperatorSymbolException e) {
            System.out.println("Invalid operator symbol");
        }
    }
}
