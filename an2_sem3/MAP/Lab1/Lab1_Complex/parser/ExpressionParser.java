package Lab1_Complex.parser;

import Lab1_Complex.exceptions.InvalidOperatorSymbolException;
import Lab1_Complex.exceptions.InvalidTokenCountException;
import Lab1_Complex.exceptions.InvalidTokenException;
import Lab1_Complex.exceptions.OperatorException;
import Lab1_Complex.factory.ExpressionFactory;
import Lab1_Complex.model.ComplexExpression;
import Lab1_Complex.model.Expressions.Addition;
import Lab1_Complex.model.ComplexNumber;
import Lab1_Complex.model.Operation;

import java.util.Objects;

// Command line arguments parser

public class ExpressionParser {
    /**
     * elements : shadow of main's args
     */
    private final String[] elements;

    /**
     * Creates a new expression parser
     * @param elems a_1 op a_2 op ... op a_n
     */
    public ExpressionParser(String[] elems)
    {
        this.elements = elems;
    }

    /**
     * checks if a given token is a + sign
     * @param token string to be checked
     * @return true if token is +, false otherwise
     */
    private boolean isPlusSign(String token) {
        return Objects.equals(token, "+");
    }

    /**
     * checks if a given token is a - sign
     * @param token string to be checked
     * @return true if token is -, false otherwise
     */
    private boolean isMinusSign(String token) {
        return Objects.equals(token, "-");
    }


    /**
     * checks if a given token is a +/- sign
     * @param token string to be checked
     * @return true if token is + or -, false otherwise
     */
    private boolean isSign(String token) {
        return isPlusSign(token) || isMinusSign(token);
    }


    /**
     * Converts +- sign to its algebraic unit counterpart
     * @param token string "+" or "-"
     * @return +1 if token is "+", -1 if token is "-"
     */

    private int getSign(String token) {
        return isPlusSign(token) ? +1 : -1;
    }

    /**
     * Checks if a token only contains digits
     * @param token string to be checked
     * @return true if token contains only digits, false otherwise
     */
    private boolean isNumber(String token)
    {
        for(int i=0;i<token.length();i++){
            if(!Character.isDigit(token.charAt(i)))
                return false;
        }
        return true;
    }

    /**
     * checks if a token is the imaginary part (i)
     * @param token string to be checked
     * @return true if token is "i", false otherwise
     */
    private boolean isI(String token) {
        return Objects.equals(token, "i");
    }

    /**
     * checks if a token is the * sign before "i"
     * @param token string to be checked
     * @return true if token is "*", false otherwise
     */
    private boolean isMul(String token) {
        return Objects.equals(token, "*");
    }

    /**
     * converts an argument string to its ComplexNumber counterpart
     * "2+3*i" --> ComplexNumber(2,3)
     * "-5i"   --> ComplexNumber(0,-5)
     * @param str string to be parsed
     * @return
     * @throws InvalidTokenException
     */
    private ComplexNumber parseComplex(String str) throws InvalidTokenException {
        int re=0;
        int im=0;

        // IDEA : pad operators with spaces
        // "1+3*i" => "1 + 3 * i"
        str = str.replace("+"," + ");
        str = str.replace("-"," - ");
        str = str.replace("*"," * ");

        // split argument by spaces
        // "1 + 13 * i" => "1", "+", "13", "*", "i"
        // BEWARE : " - 5 + i" => "", "-", "5", "+", "i"
        //             redundant, ^
        //             we don't want this
        String[] tokens = str.split(" ");

        // remove empty strings resulted from split
        // BEWARE : " - 5 + i" => "-", "5", "+", "i" (without "")
        int len = tokens.length;
        for(int i=0;i<tokens.length;i++){
            if(tokens[i].length()==0){
                // mark unwanted targets ("") with null for easier
                // identifying. It is optional tho
                tokens[i]=null;
                len--;
            }
        }

        // Can't .remove() from arrays, create a new one instead with
        // the remaining elements
        String[] tmp_tokens = new String[len];
        int k=0;
        for (String token : tokens) {
            if (token != null) {
                tmp_tokens[k++] = token;
            }
        }
        tokens=tmp_tokens;


        // Try to detect a complex number pattern
        // Rudimentary method: analyze each possible
        // combination of tokens (number, sign, i etc)
        switch(tokens.length) {
            case 1: return parse_1_args(tokens);
            case 2: return parse_2_args(tokens);
            case 3: return parse_3_args(tokens);
            case 4: return parse_4_args(tokens);
            case 5: return parse_5_args(tokens);
            case 6: return parse_6_args(tokens);
            default: throw new InvalidTokenException();
        }
    }

    /**
     * parses one token complex candidates (eg. 15, i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_1_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isNumber(tokens[0])) {
            // 5
            re = Integer.parseInt(tokens[0]);
        }
        else if(isI(tokens[0]))
        {
            // i
            im = 1;
        }
        else throw new InvalidTokenException(); // if pattern can't match
        return new ComplexNumber(re,im);
    }

    /**
     * parses two token complex candidates (eg. -15, +i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_2_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isSign(tokens[0]) && isNumber(tokens[1])) {
            // - 3
            re = getSign(tokens[0])*Integer.parseInt(tokens[1]);
            // while matching the patterns try building the component numbers
        }
        else if(isSign(tokens[0]) && isI(tokens[1]))
        {
            // - i
            im = getSign(tokens[0]);
        }
        else throw new InvalidTokenException();
        return new ComplexNumber(re,im);
    }

    /**
     * parses three token complex candidates (eg. 15*i, 1-i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_3_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isNumber(tokens[0]) && isSign(tokens[1]) && isI(tokens[2])) {
            // 2 + i
            re = Integer.parseInt(tokens[0]);
            im = getSign(tokens[1]);

        }
        else if(isNumber(tokens[0]) && isMul(tokens[1]) && isI(tokens[2])) {
            // 6 * i
            im = Integer.parseInt(tokens[0]);
        }
        else throw new InvalidTokenException();
        return new ComplexNumber(re,im);
    }

    /**
     * parses four token complex candidates (eg. +15*i, -1-i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_4_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isSign(tokens[0]) && isNumber(tokens[1]) && isMul(tokens[2]) && isI(tokens[3])) {
            // + 7 * i
            im = getSign(tokens[0]) * Integer.parseInt(tokens[1]);
        }
        else if(isSign(tokens[0]) && isNumber(tokens[1]) && isSign(tokens[2]) && isI(tokens[3])) {
            // - 5 - i
            re = getSign(tokens[0]) * Integer.parseInt(tokens[1]);
            im = getSign(tokens[2]);
        }
        else throw new InvalidTokenException();
        return new ComplexNumber(re,im);
    }

    /**
     * parses five token complex candidates (eg. 10+4*i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_5_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isNumber(tokens[0]) && isSign(tokens[1]) && isNumber(tokens[2]) &&
                isMul(tokens[3]) && isI(tokens[4])) {
            // 10 + 4 * i
            re = Integer.parseInt(tokens[0]);
            im = getSign(tokens[1]) * Integer.parseInt(tokens[2]);
        }
        else throw new InvalidTokenException();
        return new ComplexNumber(re,im);
    }

    /**
     * parses six token complex candidates (eg. -10+4*i)
     * @param tokens
     * @return parsed complex number instance
     * @throws InvalidTokenException
     */
    private ComplexNumber parse_6_args(String[] tokens) throws InvalidTokenException {
        int re=0, im=0;
        if(isSign(tokens[0]) && isNumber(tokens[1]) && isSign(tokens[2]) && isNumber(tokens[3]) &&
                isMul(tokens[4]) && isI(tokens[5])) {
            // - 10 + 4 * i
            re = getSign(tokens[0]) * Integer.parseInt(tokens[1]);
            im = getSign(tokens[2]) * Integer.parseInt(tokens[3]);
        }
        else throw new InvalidTokenException();
        return new ComplexNumber(re,im);
    }

    /**
     * Detects and retrieves the element's operation
     * @return Corresponded Operation enum value
     * @throws OperatorException
     * @throws InvalidOperatorSymbolException
     */
    private Operation parseOperation() throws OperatorException, InvalidOperatorSymbolException {
        String op = null;
        for(int i=1;i<elements.length;i+=2) {
            if(op==null) {
                // identify first operator encountered
                op=elements[i];
                continue;
            }
            if(!Objects.equals(elements[i], op)) {
                // check if it the same operator all over expression
                throw new OperatorException();
            }
        }

        // operator string of length >1 is a sign that an operation
        // has been possibly missed eg: 1+2*i 5-3*i // where +, -, *, / ?
        if(op.length()!=1) throw new InvalidOperatorSymbolException();

        // if all good, match the operator char with its Operation
        switch(op.charAt(0))
        {
            case '+': return Operation.ADDITION;
            case '-': return Operation.SUBTRACTION;
            case '*': return Operation.MULTIPLICATION;
            case '/': return Operation.DIVISION;
            default: throw new InvalidOperatorSymbolException();
        }
    }

    /**
     * Converts the input arguments to a solvable ComplexExpression, if possible
     * @return a complex expression
     * @throws InvalidTokenException
     * @throws InvalidTokenCountException
     * @throws OperatorException
     * @throws InvalidOperatorSymbolException
     */
    public ComplexExpression parse() throws InvalidTokenException, InvalidTokenCountException, OperatorException, InvalidOperatorSymbolException {
        // Don't throw exception if no arguments are provided, assume the result is 0
        if(elements.length==0) {
            ComplexNumber[] dummy = new ComplexNumber[1];
            dummy[0] = new ComplexNumber(0);
            return new Addition(dummy);
        }
        // Also if there is a single number and no operation,
        // just return the number
        if(elements.length==1) {
            ComplexNumber[] dummy = new ComplexNumber[1];
            dummy[0] = parseComplex(elements[0]);
            return new Addition(dummy);
        }

        // avoid hanging operators cases eg. 2+3i * 5 *
        if(elements.length%2==0){
            throw new InvalidTokenCountException();
        }

        // get the operator
        var operation = parseOperation();

        // parse the complex numbers
        ComplexNumber[] list = new ComplexNumber[elements.length/2+1];
        for(int i=0;i<elements.length;i+=2)
        {
            //System.out.println(elements[i]);
            list[i/2] = parseComplex(elements[i]);
            //System.out.println(list[i/2]);
        }

        // evaluate the expression
        return ExpressionFactory.getInstance().createExpression(operation, list);
    }
}
