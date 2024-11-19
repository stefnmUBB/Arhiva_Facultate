"P1";

clear all;

A = [10 7 8 7 ; 7 5 6 5 ; 8 6 10 9 ; 7 5 9 10];
A0 = A;
B = transpose([32 23 33 31]);
B0 = B;
X0 = linsolve(A, B);
x0 = transpose(X0)
% x0 = 1   1   1   1

% (a)

B = transpose([32.1 22.9 33.1 30.9]);
B1 = B;
X1 = linsolve(A, B);
x1 = transpose(X1)
% x1 = 9.2000  -12.6000    4.5000   -1.1000
% Solutia x1 se departeaza mult de x0

err_rel_in = abs(B1-B0) ./ abs(B0)
err_rel_out = abs(X1-X0) ./ abs(X0) % Q? : err relativa cand X0=[0 0 0 0]?
raport_out_in = sum(err_rel_out) / sum(err_rel_in)

%err_rel_in = 3.1250e-03 4.3478e-03 3.0303e-03 3.2258e-03
%err_rel_out = 8.2000 13.6000 3.5000 2.1000
%raport_out_in = 1995.8
% eroarea la iesire este mult mai mare decat eroarea la intrare

% (b)

A = [10 7 8.1 7.2 ; 7.08 5.04 6 5 ; 8 5.98 9.89 9 ; 6.99 5.99 9 9.98];
A1 = A;
B = B0;
X1 = linsolve(A, B);
x1 = transpose(X1)
% x1 = 1.3475   0.2119   1.4859   0.6952

err_rel_in = abs(A1-A0) ./ abs(A0)
err_rel_out = abs(X1-X0) ./ abs(X0)
raport_out_in = sum(err_rel_out) / sum(sum(err_rel_in))

% eroarea la iesire se reduce drastic

% err_rel_in =
%         0         0    0.0125    0.0286
%    0.0114    0.0080         0         0
%         0    0.0033    0.0110         0
%    0.0014    0.1980         0    0.0020
% err_rel_out = 0.3475 0.7881 0.4859 0.3048
% raport_out_in = 6.9728


disp(["Numarul de conditionare Norm2: ", cond(A0)]);
disp(["Numarul de conditionare Norm2: ", cond(A)]);
% "Numarul de conditionare Norm2: "    "2984.0927"



