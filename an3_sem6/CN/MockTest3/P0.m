% Se aduc capetele integralei la intervalul [-1,1] printr-o schimbare de
% variabila si se izoleaza ponderea w(x)=sqrt(1-x^2). Se constata ca 
% expresia lui f(x) din formula de tip gauss este sin((x+3)/2)

format("long");
f = @(x) sin((x+3)/2);
gauss_quad_int = GaussQuadIntPrecision(f, @Gauss_Cebisev2, 10, 1e-10) / 4
real =integral(@(t) sqrt(3*t-t.*t-2).*sin(t),1,2)

function I=GaussQuadIntPrecision(f, nodes_generator, max_iters, tol)
    % cauta un nr de noduri care satisface precizia tol
    I = GaussQuadInt(f, nodes_generator, 1);
    I0 = I;
    for i=2:max_iters
        I = GaussQuadInt(f, nodes_generator, i);
        if(abs(I-I0)<tol)
            fprintf("Precision reached after %i epochs", i);
            return
        end
        I0=I;
    end
    fprintf("Required precision was not reached. Error is %f", abs(I-I0));
end

function I=GaussQuadInt(f, nodes_generator, n)
    [g_nodes, g_coeff] = nodes_generator(n);
    I = vquad(g_nodes, g_coeff, f);
end

function I = vquad(g_nodes, g_coeff, f)
    I = g_coeff * f(g_nodes);
end

% Gauss-Cebîșev de speța a II-a.
function [g_nodes, g_coeff] = Gauss_Cebisev2(n)
    beta = [pi / 2, 1 / 4 * ones(1, n - 1)];
    alpha = zeros(n, 1);
    [g_nodes, g_coeff] = GaussQuad(alpha, beta);
end

function [q_nodes, q_coeff] = GaussQuad(alpha, beta)

    n = length(alpha); 
    rb = sqrt(beta(2 : n));
    J = diag(alpha) + diag(rb, -1) + diag(rb, 1);
    [v, d] = eig(J);
    q_nodes = diag(d);
    q_coeff = beta(1) * v(1, :).^2;

end
