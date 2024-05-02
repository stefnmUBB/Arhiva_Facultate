function retval = lab3_3_1()
  m = 1000;
  t = randi(6,4,m);

  sume_posibile = 4:24;
  sume_simulari = sum(t); # suma pe coloane

  frecv_abs = hist(sume_simulari, sume_posibile);
  retval = [sume_posibile;frecv_abs];

end
