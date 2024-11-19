"P1";

function retval = my_eps
  eps = 1;
  while(1.0 + (eps/2) > 1.0)
    eps = eps / 2;
  end
  retval = eps;
  return
end

disp(["eps = ", num2str(eps), "; my_eps = ", num2str(my_eps)])

function retval = my_realmin # smallest unnormalized non-0 number
  rmin = 1;
  k = 0;
  while(rmin/2>0)
    rmin = rmin/2;
    k = k+1;
  end
  retval = rmin;
  k
  return
end

disp(["realmin = ", num2str(realmin), "; my_realmin = ", num2str(my_realmin), "; 2^-1022 (smallest normalized double) = ", num2str(2^-1022)])

function retval = my_realmax
  r = 1;
  k=0;
  while(!isinf(r*2))
    r = r*2;
    k = k+1;
  end
  retval = r * (2-eps); # because 2*r becomes inf
  k
  return
end

disp(["realmax = ", num2str(realmax), "; my_realmax = ", num2str(my_realmax)])


# eps = 2.2204e-16; my_eps = 2.2204e-16
# k = 1074
# realmin = 2.2251e-308; my_realmin = 4.9407e-324; 2^-1022 (smallest normalized double) = 2.2251e-308
# k = 1023
# realmax = 1.797693134862316e+308; my_realmax = 1.797693134862316e+308

