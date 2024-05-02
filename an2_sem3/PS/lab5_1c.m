function lab5_1c()
  N = 1000;
  x = [1,2,3,4];
  p = [0.46, 0.4, 0.1, 0.04];

  x1 = lab5_1(N,x,p);
  x2 = randsample(x, N, true, p);

  clf; grid on; hold on; grid on;

  h1 = hist(x1, 1:4);
  h2 = hist(x2, 1:4);

  bar(1:4, h1, 'FaceColor', 'k') # 'k' = negru
  bar(1:4, h2, 'Facecolor', 'y')

  f1 = h1/N
  f2 = h2/N

end
