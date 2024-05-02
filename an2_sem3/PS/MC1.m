% Monte Carlo 1

function int = MC1(g,a,b,M,n)
    x = unifrnd(a,b,1,n);
    y = unifrnd(0,M,1,n);
    int = (b-a)*M*mean(y <= g(x));
end

