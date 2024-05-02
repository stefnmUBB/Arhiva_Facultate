% inrange(X:Integer, A:Integer)
% verifica daca |X|<A
% X = numarul de verificat
% A = vecinatate a lui 0
% (i,i)
inrange(X,A) :- X1 is X+A, X2 is X-A, X1>0, X2<0.

% exists(L:Lista, E:Element)
% verifica daca elementul E apartine listei L
% E = element
% L = lista de verificat aparitia elementului E
% (i,i), (i,o)
exists([H|_], H).
exists([_|T], E):- exists(T,E).

% candidat(N:Integer, E:Integer).
% genereaza cate un numar de la 1 la N
% N = limita superioara de generare
% E = elementul generat 1..N
% (i,i), (i,o)
candidat(N,N).
candidat(N,E) :- N>1,
                 N1 is N-1,
                 candidat(N1, E).

% generateh(N:Integer,M:Integer,R:Lista,K:Integer)
% genereaza solutiile partiale ale permutarilor
% cu diferenta in modul dintre elementele consecutive
% mai mare sau egala cu M
% N = lungimea permutarii
% M = diferenta minima dintre elemente consecutive
% R = permutarea rezultata
% K = lungimea permutarii partiale
% (i,i,i,i), (i,i,o,i)
generateh(N,_,[E],1) :- !, candidat(N,E).
generateh(N,M,R,K) :- K>1,
                      K1 is K-1,
                      generateh(N,M,[R1|RT],K1),
                      candidat(N,E),
                      not(exists([R1|RT],E)),
                      D is E-R1,
                      not(inrange(D,M)),
                      R=[E,R1|RT].

% generate(N:Integer, M:Integer, R:Lista)
% genereaza permutarile de lungime N cu diferenta in modul
% dintre elementele consecutive >= M
% N = lungimea permutarii
% M = diferenta minima dintre elemente consecutive
% R = permutarea rezultata
% (i,i,i), (i,i,o)
generate(N,M,R) :- generateh(N,M,R,N).


% numara toate solutiile date de generate() - pentru testare
count_solutions(N,M,C) :-
    findall(R,generate(N,M,R),L),
    length(L, C).

test_generate() :-
    count_solutions(3,1,6),

    generate(4,2,[2,4,1,3]),
    generate(4,2,[3,1,4,2]),

    count_solutions(5,3,0),
    count_solutions(5,4,0),

    count_solutions(5,2,14),
    generate(5,2,[2,4,1,3,5]),
    generate(5,2,[2,4,1,5,3]),
    generate(5,2,[5,3,1,4,2]),
    not(generate(5,2,[1,2,3,4,5])).

