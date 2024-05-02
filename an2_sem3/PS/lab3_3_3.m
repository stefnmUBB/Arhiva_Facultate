function retval = lab3_3_3()
  m = 6*6*6*6;
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

  sume_posibile = 4:24;

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
