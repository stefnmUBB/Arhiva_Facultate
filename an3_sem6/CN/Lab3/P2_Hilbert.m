format long;  % pentru output

for n = 10:15
    ca = condHilbert(n);    % (a)    
    disp(["n=", n ,";cond(H_n) = ", ca])
end

function c = condHilbert(n)
    A = zeros(n,n);
    for i=1:n
        for j=1:n
            A(i,j) = 1/(i+j-1);
        end
    end
    
    c = cond(A, 2);
end

% "n="    "10"    ";cond(H_n) = "    "35352333500163.55"
% "n="    "11"    ";cond(H_n) = "    "1229476759495174"
% "n="    "12"    ";cond(H_n) = "    "3.841961052442976e+16"
% "n="    "13"    ";cond(H_n) = "    "7.490657528957363e+17"
% "n="    "14"    ";cond(H_n) = "    "1.567896591412978e+18"
% "n="    "15"    ";cond(H_n) = "    "1.209244576336895e+18"

