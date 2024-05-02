function retval = lab3_2_a(p, n, m)
  clf; grid on; hold on;
  x = binornd(n, p, 1, m);
  N = hist(x, 0:n);
  bar(0:n, N/m, 'hist', 'FaceColor', 'b');
  bar(0:n, binopdf(0:n, n, p), 'FaceColor', 'y');
  legend('probabilitatile estimate', 'probabilitatile teoretice');
  set(findobj('type','patch'),'facealpha',0.7);xlim([-1 n+1]);
 end
