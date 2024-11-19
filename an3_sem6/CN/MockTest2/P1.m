% a

f = @(x) sin(x.*x);
df = @(x) cos(x.*x)*2.*x;
c = chebNodes(10, -2*pi, 2*pi);
x = c;
y = f(x);
dy = df(x);
graph_x = linspace(-2*pi,2*pi);
lagrange_y = LagrangeClassic(x, y, graph_x);
hermite_y = interpolareHermiteWithDerivative(x,y,dy, graph_x);
figure()
clf; hold on;
plot(graph_x, f(graph_x))
scatter(x, y)
plot(graph_x, lagrange_y)
plot(graph_x, hermite_y)
plot([pi/5, pi/5],[-1.5,3.5])
ylim([-1.5,3.5])
legend("f", "nodes", "Lagrange", "Hermite", "x=\pi/5")
hold off;

% b

t = pi/5;
f_t = f(t)
lagrange_t = LagrangeClassic(x,y, t)
hermite_t = interpolareHermiteWithDerivative(x,y,dy, t)

% c
abs_err_lag = max(double(subs(f, graph_x)) - lagrange_y)
abs_err_her = max(double(subs(f, graph_x)) - hermite_y)

function lk = L(nodes,k)
 % polinomul fundamental L_k pentru nodurile nodes_1,...,nodes_n
 % lk = prod_{j,j!=k}(x-xj)/prod_{j,j!=k}(xk-xj)
 m = size(nodes,2);
 roots = cat(2, nodes(1:k-1), nodes(k+1:m));
 p = poly(roots);
 lk = p ./ polyval(p,nodes(k));
end
function u = LagrangeClassic(x,y,t)
 m = size(x,2);
 n = size(t,2);
 pf = zeros('like', x);
 for k=1:m
 pf = pf + (L(x,k) .* y(k));
 end
 u = polyval(pf,t);
end
function c = chebNodes(n,a,b)
 c = (a+b)/2 + (b-a)/2*cos((2*[1:n]-1)*pi/(2*n));
end

function [ H, dH] = interpolareHermiteWithDerivative( x, f, fd, X )
 coefs=dif_div_duble(x,f,fd);

 coefs=coefs(1,:);

 z=repelem(x,2);
 H=X;
 dH=X;
 for k=1:length(X)
 P=1; DP=0;
 H(k)=coefs(1)*P; dH(k)=0;

 for i=2:length(coefs)
 DP=DP*(X(k)-z(i-1))+P;
 P=P*(X(k)-z(i-1));
 H(k)=H(k)+coefs(i)*P;
 dH(k)=dH(k)+coefs(i)*DP;
 end
 end
end
function T=dif_div_duble(x,f,df)
 T=NaN(2*length(x));
 z=repelem(x,2);
 T(:,1)=repelem(f,2);
 T(1:2:end-1,2)=df;
 T(2:2:end-2,2)=diff(f)./diff(x);
 for j=3:length(z)
 T(1:end-j+1,j)=diff(T(1:end-j+2,j-1))./(z(j:end)-z(1:end-j+1))';
 end
end