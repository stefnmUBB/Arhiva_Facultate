package Lab1_Complex.factory;

import Lab1_Complex.model.ComplexExpression;
import Lab1_Complex.model.Expressions.Addition;
import Lab1_Complex.model.Expressions.Division;
import Lab1_Complex.model.Expressions.Multiplication;
import Lab1_Complex.model.Expressions.Subtraction;
import Lab1_Complex.model.ComplexNumber;
import Lab1_Complex.model.Operation;

/**
 * Singleton instance for creating expressions
 */
public class ExpressionFactory {
    private static ExpressionFactory instance = new ExpressionFactory();

    private ExpressionFactory() {}

    public static ExpressionFactory getInstance() { return instance; }

    /**
     * Returns expression based on a given operation and complex arguments
     * @param op Desired operation
     * @param args list of complex numbers
     * @return Complex expression that applies op on the complex numbers list
     */
    public ComplexExpression createExpression(Operation op, ComplexNumber[] args)
    {
        switch(op)
        {
            case ADDITION: return new Addition(args);
            case SUBTRACTION: return new Subtraction(args);
            case MULTIPLICATION: return new Multiplication(args);
            case DIVISION: return new Division(args);
            default: return null;
        }
    }
}
