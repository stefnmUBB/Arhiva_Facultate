function retval = lab3_1_2()
  urn = 'rrrrrgggbb';
  sim = 5000;
  PAB = 0;
  for i=1:sim
    ext = randsample(urn, 3, replacement = false);
    if(any(ext=='r') && all(ext==ext(1)))
      PAB=PAB+1;
    endif
  endfor

  retval = PAB/sim;
 endfunction
