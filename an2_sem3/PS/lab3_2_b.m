function retval = lab3_2_b()
  sim = 5000;
  v = binornd(5, 2/6, 1, sim);

  h = hist(v, 0:5);
  prob_sim = h(3) / sim # sum(v==2) / sim
  prob_ter = binopdf(2, 5, 1/3)

  clf; grid on; hold on;

  bar(0:5, h/5000, 'FaceColor', 'k') # 'k' = negru
  bar(0:5, binopdf(0:5, 5, 1/3), 'Facecolor', 'y')
  legend('probabilitatile estimate', 'probabilitatile teoretice');

  set(findobj('type','patch'),'facealpha',0.7);xlim([-1 6]);
end
