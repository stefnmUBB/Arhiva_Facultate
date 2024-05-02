function ex1(n=2000)
  close all; %inchide figurile din rulari anterioare
  x = normrnd(165, 10, 1, n);

  figure(1)
  hist(x,10);

  figure(2)
  [~, centers] = hist(x,10);
  hist(x,centers, 10 / (max(x)-min(x)));
  hold on;
  t = linspace(min(x), max(x), 2000);
  plot(t, normpdf(t,165,10),'-r','LineWidth',2);

  [mean(x), 165]
  [std(x), 10]
  [mean((x>=160) & (x<=170)),
    normcdf(170,165,10)-normcdf(160,165,10)]



endfunction
