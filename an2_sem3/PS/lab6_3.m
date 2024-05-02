function lab6_3(n=1000)
  r = rand(1,n);
  r
  t = exprnd(5,1,n) .* (r<=0.4)
    .+ unifrnd(4,6,1,n) .* (r>0.4);

  [mean(t), std(t)]

  mean(t>5)

  count2P = 0;
  countTime=0;
  for i = 1:n
    if t(i)>5
      countTime++;
      if(r(i)>0.4)
        count2P++;
      endif
    endif
  endfor

  count2P / countTime

  s1 = sum(t>5)
  s2 = sum((t>5) & (r>0.4))
  s2 / s1
##  t = zeros(1,n);
##  for i = 1:n
##    if r(i) < 0.4
##      t(i) = exprnd(5)
##    else
##      t(i) = unifrnd(4,6);
##    endif
##  endfor
end
