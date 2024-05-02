
function retval = lab3_1_3()
  urn = 'rrrrrgggbb';
  sim = 5000;

  PA = 0;
  PAB = 0;
  for i=1:sim
    ext = randsample(urn, 3, replacement = false);
    if(any(ext=='r'))
      PA = PA+1;
    endif
    if(all(ext=='r'))
      PAB=PAB+1;
    endif
  endfor

  PA/=sim;
  PAB/=sim;

  retval = PAB/PA;
end
