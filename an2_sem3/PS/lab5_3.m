function retval = lab5_3 (N)
  # boxmuller
  U = rand(2,N);
  R = sqrt(-2*log(U(1,:)));
  v = 2 * pi * U(2,:);
  X = R .* cos(v)
  Y = R .* sin(v)


  t = linspace(0,2*pi,360)

  clf;
  polar(t,4*ones(1,360),'y');
  hold on;
  plot(X,Y,'r*');

  m=mean(sqrt(X.^2+Y.^2)<0.5)

  rel = 1-exp(-1/8) # frecv rel teoretica


  Z = normrnd(0, 1,2,N);
  plot(Z(1,:), Z(2,:),'c*')
  mz=mean(sqrt(Z(1,:).^2+Z(2,:).^2)<0.5)

  polar(t, 0.5*ones(1,360),'b');

endfunction
