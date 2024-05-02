function out = lab4_2i(k=100,p=1./2)
  pos = zeros(1,k+1);
  for i = 2:k+1
    pos(i) = pos(i-1)+2*binornd(1,p)-1;
  endfor
end
