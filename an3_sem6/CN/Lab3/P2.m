format long;  % pentru output

for n = 10:15
    ca = condVandermonde(linspace(-1, 1, n));    % (a)
    cb = condVandermonde(1 ./ linspace(1,n,n));  % (b)
    disp(["n=", n ,";cond(V(t_a)) = ", ca, "; cond(V(t_b)) = ", double(cb)])
end

function c = condVandermonde(t)
    V = vander(t);
    c = cond(V, "inf");
end

% Concluzie: Matricea de la (a) este mai bine conditionata decat cea de la (b)
%    "n="    "10"    ";cond(V(t_a)) = "    "20561.7054"    "; cond(V(t_b)) = "    "579241656500.1678"
%    "n="    "11"    ";cond(V(t_a)) = "    "63657.4074"    "; cond(V(t_b)) = "    "23823821636359.84"
%    "n="    "12"    ";cond(V(t_a)) = "    "194936.1517"    "; cond(V(t_b)) = "    "1060780168021755" 
%    "n="    "14"    ";cond(V(t_a)) = "    "1780930.0586"    "; cond(V(t_b)) = "    "2.615990468524933e+18"
%    "n="    "15"    ";cond(V(t_a)) = "    "5579758.0049"    "; cond(V(t_b)) = "    "1.436205532479037e+20"
