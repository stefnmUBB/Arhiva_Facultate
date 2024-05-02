  % g2 = @(x) abs(sin(exp(x))), a = -1, b = 3, M = 1
  % g3 = @(x)  x .^2 ./ (1 + x .^ 2) .* (x <= 0) + sqrt(2 * x - x .^ 2) .* (x > 0), a = -1, b = 2, M = 1

function lab6_2(n=500)

  g = @(x) exp( - x .^ 2), a = -2, b = 2, M = 1

  clf; hold on;

  for i = 1 : n
    x = unifrnd(a,b);
    y = unifrnd(0,M);

    if(g(x) >= y)
      plot(x,y,"*r","MarkerSize",8)
    else
      plot(x,y,"*c","MarkerSize",8)
    endif
  endfor

  t = linspace(a,b,1000);
  plot(t,g(t),'-k','LineWidth',2);

  % X = unifrnd(a,b,1,n)
  % Y = unifrnd(0,M,1,n)
  % plot(X(g(X)<=Y),Y(g(X)<=Y),'*r');
endfunction

