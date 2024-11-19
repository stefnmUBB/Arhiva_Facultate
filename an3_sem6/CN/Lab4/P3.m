function P3()
    % https://stackoverflow.com/questions/5099368/avoid-generating-a-singular-matrix-in-matlab
    % ?????
    "A randomly generated matrix will be full rank (and hence invertible, if square) with probability 1:"

    n = 30;
    A = randomNonSingular(n)
    mvp = min(svd(A))

    x0 = ones(n,1);
    b = A*x0

    % solve Gauss
    %x = gauss_elim(A,b)

    % solve
    x = solveLUP(A,b)
end

function A = randomNonSingular(n)
    A = eye(n)
    for i = 1:n*n
        op = randi([1,3]);
        j = randi([1,n]);
        k = randi([1,n]);
        if op==1
            A([j,k],:) = A([k,j],:);
        else if op==2
            A(:,[j,k]) = A(:,[k,j]);
        else
            A(k,:) = A(k,:) + A(j,:);
        end
        end
    end
end


% the same from P1 & P2

function x = gauss_elim(A, b)
    x = [];
    A = [A, b];
    n = size(A, 1);
    % eliminare
    for i=1:n-1
        A
        max_on_row = max(A(linspace(i,n,n-i+1), i));
        p = find(abs(A(linspace(i,n, n-i+1),i)) == max_on_row, 1)+i-1;
        if isempty(p)
            fprintf("nu exista solutie unica (1)");            
            return;
        end
        
        if p ~= i 
            A([i p],:) = A([p i],:); % swap rows i and p
        end

        for j=i+1:n
            m = A(j,i) / A(i,i);
            A(j,:) = A(j,:) - m * A(i,:);
        end             
    end
    if A(n,n)==0
        fprintf("nu exista solutie unica (2)")
    end
    % substitutie inversa    

    A
    x = zeros(1,n);

    x(n) = A(n,n+1)/A(n,n);    

    for i=n-1:-1:1
        j = linspace(i+1, n, n-i);
        x(i) = (A(i,n+1)-sum(A(i,j).*x(j)))/A(i,i);        
    end    
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
