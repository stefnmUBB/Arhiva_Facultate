function retval = lab22(N, i)
  clf; hold on; axis square; % axis off;
  rectangle('Position', [0 0 1 1])

  R = 0.5;

  f=0;

  for k = 1:N
    x = rand;
    y = rand;

    if(i==1)
      if((x-R)^2 + (y-R)^2 < R*R)
        plot(x, y, 'dr')
        f++;
      endif
    endif
    if(i==2)
      xx = abs(x-R);
      yy = abs(y-R);
      d0 = pdist([0,0; xx,yy]);
      d1 = pdist([xx,yy; R,R]);
      if(d0<d1)
        plot(x,y,'dr');
        f++;
      endif
    endif
    if(i==3)
      d1 = pdist([R,0;x,y]);
      d2 = pdist([R,1;x,y]);
      d3 = pdist([0,R;x,y]);
      d4 = pdist([1,R;x,y]);
      d = [d1, d2, d3, d4];
      l = 0;
      for q = 1:4
        if (d(q)<R)
          l++;
        endif
      endfor
      if(l==2)
        f++;
        plot(x, y, 'dr');
      endif
    endif
    retval = f/N;

  endfor

endfunction
