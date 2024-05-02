function retval = lab3_3_2()
  m = 1000;
  t = randi(6,4,m);

  sume_posibile = 4:24;
  sume_simulari = sum(t); # suma pe coloane

  frecv_abs = hist(sume_simulari, sume_posibile);
  retval = [sume_posibile;frecv_abs];

  clf; grid on; hold on; grid on;
  xticks(sume_posibile);
  xlim([3 25]);
  yticks(0:0.01:0.14);
  ylim([0 0.14]);
  bar(sume_posibile, frecv_abs/m, 'hist', 'FaceColor', 'b');
  #set(findobj('type','patch'),'facealpha',0.7);xlim([-1 n+1]);

  max_f_abs = max(frecv_abs)
  most_freq_sums =sume_posibile(frecv_abs==max_f_abs)

end
