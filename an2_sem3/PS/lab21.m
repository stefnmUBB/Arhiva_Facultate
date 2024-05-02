function retval = lab21(nct=512)
  ncf = 0;

  for k= 1:nct
    ev = randi(365, 1, 23);

    ok = 0;
    if(length(unique(ev)) < length(ev))
      ncf++;
    endif
  endfor

  retval = ncf/nct;
endfunction
