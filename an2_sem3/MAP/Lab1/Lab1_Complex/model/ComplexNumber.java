package Lab1_Complex.model;

public class ComplexNumber {
    public float re;
    public float im;

    /**
     * creates a ComplexNumber instance
     * @param re real part
     * @param im imaginary part
     */
    public ComplexNumber(float re, float im) {
        this.re=re;
        this.im=im;
    }

    /**
     * Creates a ComplexNumber instance from a real nunmber
     * @param re real number
     */
    public ComplexNumber(float re) { this(re,0); }

    /**
     * Default constructor (z=0)
     */
    public ComplexNumber() { this(0); }

    /**
     * ComplexNumber z = new ComplexNumber(2,3);
     * System.out.println(z); // calls z.toString() => "2+3*i;
     * @return Pretty-print complex number
     */
    public String toString() {
        if(im==0) return ""+re;
        if(re==0)
        {
            return im+"i";
        }
        if(im<0)
            return re+""+im+"*i";
        return re+"+"+im+"*i";
    }

    /**
     * adds a number to the complex number instance
     * @param other number to add
     * @return result of operation
     */
    public ComplexNumber add(ComplexNumber other) {
        return new ComplexNumber(this.re+other.re, this.im+other.im);
    }

    /**
     * subtracts a number from the complex number instance
     * @param other number to subtract
     * @return result of operation
     */
    public ComplexNumber sub(ComplexNumber other) {
        return new ComplexNumber(this.re-other.re, this.im-other.im);
    }

    /**
     * multiplies a number with the complex number instance
     * @param other number to multiply with
     * @return result of operation
     */
    public ComplexNumber mul(ComplexNumber other) {
        return new ComplexNumber(
                this.re*other.re-this.im*other.im,
                this.re*other.im+this.im*other.re);
    }

    /**
     * divides a number to the complex number instance
     * @param other number to divide to
     * @return result of operation
     */
    public ComplexNumber div(ComplexNumber other) {
        // (a*b^)/|b|^2
        float l = other.re*other.re+other.im*other.im;
        float re = (this.re*other.re+this.im*other.im)/l;
        float im = (this.im*other.re-this.re*other.im)/l;
        return new ComplexNumber(re,im);
    }

}
