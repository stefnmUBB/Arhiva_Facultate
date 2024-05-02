package Lab1_Complex.model.Expressions;

import Lab1_Complex.model.ComplexExpression;
import Lab1_Complex.model.ComplexNumber;
import Lab1_Complex.model.Operation;

public class Subtraction extends ComplexExpression {
    public Subtraction(ComplexNumber[] args)
    {
        super(Operation.SUBTRACTION, args);
    }
    @Override
    protected ComplexNumber executeOneOperation(ComplexNumber a, ComplexNumber b) {
        return a.sub(b);
    }
}
