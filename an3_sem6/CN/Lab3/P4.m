"P4";

for k=10:30
    E_inv = recinv(1, k);
    E_dir = recdir(k);
    err_inv = abs(E_inv-exp(-1))/exp(-1);
    err_dir = abs(E_dir-exp(-1))/exp(-1);
    fprintf("k=%2d, err_inv=%e, err_dir=%e\n", k, err_inv, err_dir);
end

function E = recdir(n)
   E = 1/exp(1);
   for k=2:n
       E = 1-k*E;
   end
end

function E = recinv(n, k)
    % recurenta inversa
    E = 0;    
    for j=n+k:-1:n+1
        E = (1-E)/j;
    end
end

% k=10, err_inv=5.267586e-09, err_dir=7.719985e-01
% k=11, err_inv=4.073053e-10, err_dir=7.897348e-01
% k=12, err_inv=2.922473e-11, err_dir=8.049000e-01
% k=13, err_inv=1.955900e-12, err_dir=8.180180e-01
% k=14, err_inv=1.228285e-13, err_dir=8.294661e-01
% k=15, err_inv=7.242958e-15, err_dir=8.397268e-01
% k=16, err_inv=4.526849e-16, err_dir=8.460888e-01
% k=17, err_inv=0.000000e+00, err_dir=8.982078e-01
% k=18, err_inv=0.000000e+00, err_dir=1.139775e-01
% k=19, err_inv=0.000000e+00, err_dir=1.511615e+01
% k=20, err_inv=0.000000e+00, err_dir=2.840412e+02
% k=21, err_inv=0.000000e+00, err_dir=5.984147e+03
% k=22, err_inv=0.000000e+00, err_dir=1.316309e+05
% k=23, err_inv=0.000000e+00, err_dir=3.027533e+06
% k=24, err_inv=0.000000e+00, err_dir=7.266077e+07
% k=25, err_inv=0.000000e+00, err_dir=1.816519e+09
% k=26, err_inv=0.000000e+00, err_dir=4.722950e+10
% k=27, err_inv=0.000000e+00, err_dir=1.275197e+12
% k=28, err_inv=0.000000e+00, err_dir=3.570550e+13
% k=29, err_inv=0.000000e+00, err_dir=1.035460e+15
% k=30, err_inv=0.000000e+00, err_dir=3.106379e+16
