n = 20;

% (a)
% (x-1)...(x-n)=0

csi = linspace(1,n,n);
niu = linspace(0,n-1,n);

p = poly(csi);
a = p 
dp = polyder(p);

% Culproan.pdf pag. 52

disp("(a)")
for i=1:n
    cond_terms = abs(1 ./ (csi(i) .* polyval(dp,csi(i))) .* a(niu + 1) .* (csi(i) .^ niu));
    cond = sum(cond_terms);
    disp(["Conditionarea radacinii x = ", csi(i), " : ", cond])
end

%   "Conditionarea radacini…"    "17"    " : "    "1.076100038070207e+27"

x = linspace(0,n,100);
p_norm = p + normrnd(0, 1e-10, 1, n+1);

p_uni = p + unifrnd(0, 1, 1, n+1);

plot(x, polyval(p, x), x, polyval(p_norm, x), x, polyval(p_uni, x));
ylim([-3e13, 3e13])
legend("p", "p\_norm", "p\_uni")

% (b)

% x^n + 2^-1 * x^(n-1) + ... + 2^-(n-1)*x^1 + 2^-n = 0 | *2^n
% (2x)^n + (2x)^(n-1) + ... + (2x)^1 + 1 = 0
% Not y := 2x
% y^n + y^(n-1) + ... + y + 1 = 0 
% cu solutiile complexe y_k = e^(i*2*k*pi/(n+1)), k=1..n


csi = exp(0+2i.*(niu+1)*pi./(n+1))./2;
%p = real(poly(csi)) % 1.0000 1.0000 .... 1.0000
p = real(poly(csi)); % 2^0 2^-1 ... 2^-n


a = p;
dp = polyder(p);
disp("(b)")
% Culpr20an.pdf pag. 52
for i=1:n    
    cond_terms = abs(1 ./ (csi(i) .* polyval(dp,csi(i))) .* a(niu + 1) .* (csi(i) .^ niu));
    cond = sum(cond_terms);
    disp(["Conditionarea radacinii x = ", csi(i), " : ", cond])
end

% "Conditionarea radacini…"    "0.18267-0.46544i"    " : "    "75007.4785"

figure

x = linspace(0,n,250);

p_norm = p + normrnd(0, 1e-1, 1, n+1); % deviatia mult mai mare fata de 10^10 ca sa se poata observa o diferenta pe grafic
p_uni = p + unifrnd(0, 1, 1, n+1);

plot(x, polyval(p, x), x, polyval(p_norm, x), x, polyval(p_uni, x));
ylim([-50, 10000])
legend("p", "p\_norm", "p\_uni")

