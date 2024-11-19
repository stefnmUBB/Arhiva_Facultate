function P2()
    A = [
         2 0 2 0.6;
         3 3 4 -2;
         5 5 4 2;
         -1 -2 3.4 -1
        ];
    b = A * [1;1;1;1];
    [L,U,p] = LUP(A);    
    P = eye(4); P = P(p,:);
    sum(sum(P*A == L*U))==16
    x = solveLUP(A, b)
end

function x = solveLUP(A, b)
    [L,U,p] = LUP(A);
    [n,m] = size(A);
    y = [1:n] * 0;
    for i=1:n        
        y(i)=b(p(i));
        if i==1 
            continue; 
        end
        j = 1:i-1;        
        y(i) = y(i) - sum(L(i,j).*y(j));
    end

    x = [1:n]*0;
    x(n) = y(n)/U(n,n);    
    for i=n-1:-1:1        
        j = [i+1:n];
        x(i) = (y(i) - sum(U(i,j).*x(j))) / U(i,i);
    end    
end

function [L,U,p] = LUP(A)    
    [n,m] = size(A);
    if n ~= m
        disp("Nu e matrice patratica")
        L=0; U=0; P=0;
        return
    end    
    p = 1:n;
    for k = 1:n-1
        % pivotare        
        i = k-1 + find(abs(A(linspace(k,n,n-k+1),k))==max(abs(A(linspace(k,n,n-k+1),k))),1) ;
        disp(["swap lines",i,k])        
        A([k,i],:) = A([i,k],:);
        A
        p([k,i]) = p([i,k]);        
        lin = [k+1:n];
        lin
        disp("schur")
        % complementul Schur
        A(lin,k)
        A(k,k)
        A(lin,k) = A(lin,k) / A(k,k);
        A
        A(lin,lin) = A(lin,lin) - A(lin,k)*A(k,lin);
        A
    end            
    U = zeros(n);
    L = eye(n);

    for i=1:n
        for j=1:n
            if i<=j
                U(i,j) = A(i,j);
            else
                L(i,j) = A(i,j);
            end
        end
    end

    P = eye(n);
    P = P(p,:);
    P
end










