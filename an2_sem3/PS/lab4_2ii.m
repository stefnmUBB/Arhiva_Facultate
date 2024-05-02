
function lab4_2ii(m=1000,k=10, p=0.9)
  v = []
  for i = 1:m
    v = [v, lab4_2i(k,p)(end)];
  endfor

  h = hist(v, -k:k)

  clf; grid on; hold on;

  bar(-k:k, h, 'FaceColor', 'k') # 'k' = negru

  deplasariPos = -k:k
  hMax = max(h)
  fprintf("Pozitia finala cel mai des intalnita este: %d", deplasariPos(h==hMax))
  % find(h==hMax)-k-1

end
