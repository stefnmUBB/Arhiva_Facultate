function P4()    
    checkRandom()    

    n=3
    A = generateSPDmatrix(n);
    b = A*ones(n,1);    
    x = solveCholesky(A,b);

end

function x = solveCholesky(A, b)
    R = Cholesky(A)
    L = R'
    U = R       
    [n,m] = size(A);
    y = [1:n] * 0;
    y(1)=b(1)/L(1,1);    
    for i=2:n                
        j = 1:i-1;        
        y(i) = (b(i) - sum(L(i,j).*y(j)))/L(i,i);
    end        

    x = [1:n]*0;
    x(n) = y(n)/U(n,n);    
    for i=n-1:-1:1        
        j = [i+1:n];
        x(i) = (y(i) - sum(U(i,j).*x(j))) / U(i,i);
    end    
end

function checkRandom()
    A = generateSPDmatrix(3)
    R = Cholesky(A)
    errC = R - chol(A);
    errA = A - R'*R;
    if(errC<1e-5 & errA<1e-5)
        disp("Okay :>")
    else
        disp("Not good :<")
    end
end

function R = Cholesky(A)
    [n,m] = size(A);
    if(n~=m)
        disp("nu e matrice patratica");
        R = 0;
        return;
    end    
    R = A;    
    for k=1:m
        for j=k+1:m
            R(j,j:m) = R(j,j:m) - R(k,j:m)*conj(R(k,j))/R(k,k);            
        end
        R(k,k:m) = R(k,k:m) / sqrt(R(k,k));
    end

    for k=1:m
        for j=1:k-1
            R(k,j)=0;
        end
    end
end



function A = generateSPDmatrix(n)
    % Generate a dense n x n symmetric, positive definite matrix
    
    A = rand(n,n); % generate a random n x n matrix
    
    % construct a symmetric matrix using either
    A = 0.5*(A+A');
    % A = A*A';
    % The first is significantly faster: O(n^2) compared to O(n^3)
    
    % since A(i,j) < 1 by construction and a symmetric diagonally dominant matrix
    %   is symmetric positive definite, which can be ensured by adding nI
    A = A + n*eye(n);
end