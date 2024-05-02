% Monte Carlo 2

function int = MC2(g,a,b,n)
  u = unifrnd(a,b,1,n);
  int = (b-a)* mean(g(u));
end
