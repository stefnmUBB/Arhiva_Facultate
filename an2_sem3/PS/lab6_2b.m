  % g2 = @(x) abs(sin(exp(x))), a = -1, b = 3, M = 1
  % g3 = @(x)  x .^2 ./ (1 + x .^ 2) .* (x <= 0) + sqrt(2 * x - x .^ 2) .* (x > 0), a = -1, b = 2, M = 1

function lab6_2b(n=500)
  g = @(x) exp( - x .^ 2), a = -2, b = 2, M = 1

  MC1 = MC1(g,a,b,M,n)
  MC2 = MC2(g,a,b,n)
  int = integral(g,a,b)
endfunction

