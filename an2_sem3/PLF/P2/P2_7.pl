%7.
%a) Definiti un predicat care determina produsul unui numar reprezentat
% cifra cu cifra intr-o lista cu o anumita cifra. De ex: [1 9 3 5 9 9] * 2
%=> [3 8 7 1 9 8]

lmulh([],_,[],0):-!.
lmulh(_,0,[0],0):-!.
lmulh([H],B,[R],C):- !, R is (H*B) mod 10, C is (H*B) // 10.
lmulh([H|T],B,R,C):-
    lmulh(T,B,R1,C1),
    P is H*B+C1,
    HR is P mod 10,
    C is P//10,
    R = [HR|R1].

lmul(A,B,R):- lmulh(A,B,R,0).
lmul(A,B,R):- lmulh(A,B,R1,C), C\=0, R=[C|R1].
