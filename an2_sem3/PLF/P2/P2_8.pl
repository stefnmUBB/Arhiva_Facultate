%8.
% a) Definiti un predicat care determina succesorul unui numar
% reprezentat cifra cu cifra intr-o lista. De ex: [1 9 3 5 9 9] =>
% [1 9 3 6 0 0]

succh([H],[H1],0):-H<9,!,H1 is H+1.
succh([9],[0],1):-!.
succh([H|T],R,C):-
    succh(T,R1,C1), !,
    H1 is (H+C1) mod 10,
    C is (H+C1)//10,
    R=[H1|R1].

succs(A,B):-succh(A,B,0),!.
succs(A,[1|B]):-succh(A,B,1).
