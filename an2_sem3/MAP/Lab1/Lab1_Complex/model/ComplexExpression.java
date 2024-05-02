package Lab1_Complex.model;

import Lab1_Complex.model.ComplexNumber;
import Lab1_Complex.model.Operation;

public abstract class ComplexExpression {
    private final Operation operation;
    private final ComplexNumber[] args;

    public ComplexNumber[] getArgs(){return args;}
    public Operation getOperation() {return operation;}

    /**
     * create ComplexExpression instance featuring an operation
     * and arguments
     */
    public ComplexExpression(Operation op, ComplexNumber[] args)
    {
        this.operation=op;
        this.args=args;
    }


    /**
     * abstract method for custom operations
     * @param a first operand
     * @param b second operand
     * @return result of a op b
     */
    protected abstract ComplexNumber executeOneOperation(ComplexNumber a, ComplexNumber b);

    /**
     * core expression solver, performs n1 op n2 op ... op nk
     * @return the result of operatiosn
     */
    public final ComplexNumber execute()
    {
        ComplexNumber result=null;
        for(ComplexNumber z: this.getArgs())
        {
            if(result==null)
                result=new ComplexNumber(z.re,z.im);
            else
                result = executeOneOperation(result, z);
        }
        return result;
    }
}
