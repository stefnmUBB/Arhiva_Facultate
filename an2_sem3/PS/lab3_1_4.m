function retval = lab3_1_4()
  urn = 'rrrrrgggbb';
  sim = 5000;

  PA = 0;
  PAB = 0;
  for i=1:sim
    ext = randsample(urn, 3, replacement = false);
    if(any(ext=='r'))
      PA = PA+1;
      if(all(ext==ext(1)))
        PAB = PAB+1;
      endif
    endif
  endfor

  retval = PAB/PA;
end
