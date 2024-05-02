function retval = lab3_3_4()
  m = 1000;
  t = randi(6,4,m);

  sume_posibile = 4:24;
  sume_simulari = sum(t); # suma pe coloane

  frecv_abs = hist(sume_simulari, sume_posibile);

  PA = sum(sume_simulari<=20);
  PAB = sum(sume_simulari>=10 & sume_simulari<=20);
  p_sim = PAB/PA

  sume_simulari = [];

  for i1=1:6
    for i2=1:6
      for i3=1:6
        for i4 = 1:6
          sume_simulari = [sume_simulari, i1+i2+i3+i4];
        endfor
      endfor
    endfor
  endfor

  PA_th = sum(sume_simulari<=20);
  PAB_th = sum(sume_simulari>=10 & sume_simulari<=20);

  p_th = PAB_th/PA_th

end
