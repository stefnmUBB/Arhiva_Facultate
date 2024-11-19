function P1()
    A = [1 2 1; 2 5 3; 1 3 3];
    %A = zeros(3)
    b = [4; 10 ; 7];
    
    x = gauss_elim(A,b)
end

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


