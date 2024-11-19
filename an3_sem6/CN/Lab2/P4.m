"P4";

function [s, m, e] = peek_double(x)
  hex = [
    "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111","1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111",
  ];

  internal_repr = num2hex(x)
  bin_repr = "";
  for nb = internal_repr
    n = hex2dec(nb);
    bin_repr = [bin_repr, hex(4*n+1: 4*n+4)];
  endfor


  bin_repr
  sign = bin_repr(1)
  exponent = bin_repr(2:12)
  mantissa = bin_repr(13:64)

  sign = (-1)^bin2dec(sign)
  exponent_shifted = bin2dec(exponent)
  exponent_real = exponent_shifted-1023
  mantissa = bin2dec(mantissa)/(2^52)

  reconstructed = sign*(1+mantissa)*(2^exponent_real)

end

peek_double(1.2)

#>> P4
#internal_repr = 3ff3333333333333
#bin_repr = 0011111111110011001100110011001100110011001100110011001100110011
#sign = 0
#exponent = 01111111111
#mantissa = 0011001100110011001100110011001100110011001100110011
#sign = 1
#exponent_shifted = 1023
#exponent_real = 0
#mantissa = 0.2000
#reconstructed = 1.2000
#>> peek_double(3.5)
#internal_repr = 400c000000000000
#bin_repr = 0100000000001100000000000000000000000000000000000000000000000000
#sign = 0
#exponent = 10000000000
#mantissa = 1100000000000000000000000000000000000000000000000000
#sign = 1
#exponent_shifted = 1024
#exponent_real = 1
#mantissa = 0.7500
#reconstructed = 3.5000
