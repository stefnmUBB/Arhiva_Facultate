function res = lab5_1(N,x,p)
  p0 = 0;
  U = rand(1,N)
  X = zeros(1,N);

  q=cumsum(p);

  for i = 1:N
    for k = 1:length(p)
      if U(i)<=q(k)
        X(i)=x(k);
        break;
      endif
    endfor
  endfor
  res = X;
end
