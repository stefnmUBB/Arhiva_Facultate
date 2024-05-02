function lab5_2 ()
  N=1000;
  lambda = 1/12;

  x1 = -log(1-rand(1,N))/lambda
  x2 = exprnd(12,1,N)

  m1 = mean(x1)
  m2 = mean(x2)

  s1 = std(x1)
  s2 = std(x2)

endfunction

