function retval = lab3_1_1()
  urn = 'rrrrrgggbb';
  sim = 5000;
  PA = 0;
  for i=1:sim
    ext = randsample(urn, 3, replacement = false);
    if(any(ext=='r'))
      PA=PA+1;
    endif
  endfor

  retval = PA/sim;
 endfunction
