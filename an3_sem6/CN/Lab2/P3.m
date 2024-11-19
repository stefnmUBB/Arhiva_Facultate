"P3";

function [ret_a, ret_b, ret_r] = pade(f, x, k, m)
    c = zeros(1, m+k+1);

    xx = sym('xx', 'positive');
    macl = taylor(f, x, 0, 'order', m+k+1);
    macl = subs(macl, x, xx);

    [coeff, t] = coeffs(macl);
    coeff = eval(coeff);
    t = eval(expand(log(t)) ./ log(xx)); #[x^5, x^3, x^1] --> [5 3 1]
    coeff
    c(1+t) = coeff;
    c

    A = zeros(k, k);
    C = zeros(k, 1);

    for i = 1:k
      for j = 1:k
        c_index = m+(i-j);
        if(c_index>0)
          A(i,j) =c(c_index+1);
        endif
      endfor
      C(i) = -c(m+i+1);
    endfor
    A
    C

    B = linsolve(A,C);
    b = [1; B]

    a = zeros(1, m+1);
    for j = 0:m
      for L = 0:j
          if (j<L || L>k) continue endif
          a(j+1) += c(j-L+1) * b(L+1);
      endfor
    endfor
    a

    ret_a = flip(a);
    ret_b = flip(b);
    ret_r = simplify(poly2sym(sym(ret_a), x)/poly2sym(sym(ret_b), x));
end

syms x

[a, b, r_sin] = pade(e^x, x, 4, 4);
[_, _, r_cos] = pade(cos(x), x, 4, 4);

disp(r_sin)
disp(r_cos)

disp(["sin(10*pi) = ", num2str(eval(subs(r_sin, x, 10*sym(pi))))])
disp(["cos(10*pi) = ", num2str(eval(subs(r_cos, x, 10*sym(pi))))])

#    4       3        2
#   x  + 20*x  + 180*x  + 840*x + 1680
#   ----------------------------------
#    4       3        2
#   x  - 20*x  + 180*x  - 840*x + 1680
#      /        4          2        \
#   60*\450*pi*x  - 31165*x  + 68292/
#   ---------------------------------
#          /    4        2        \
#      271*\13*x  + 660*x  + 15120/
# sin(10*pi) = 3.5508
# cos(10*pi) = 22.3633

