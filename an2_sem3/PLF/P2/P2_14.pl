%14.
% a) Definiti un predicat care determina predecesorul unui numar
% reprezentat cifra cu cifra intr-o lista. De ex: [1 9 3 6 0 0] =>
% [1 9 3 5 9 9]

predh([],[],0):-!.
predh([H],[H1],0):- H>0,!, H1 is H-1.
predh([0],[9],1):-!.
predh([H|T],R,0):-
    predh(T,R1,C1),
    H1 is H-C1,
    H1>=0,!,
    R=[H1|R1].
predh([H|T],R,1):-
    predh(T,R1,C1),
    H1 is H-C1,
    H1<0,!,
    R=[9|R1].

% predh([1,0,0],R,C) ==> R=[0,9,9], C=0
% va trebui sa eliminam 0-urile de la inceput

elim0([0],[0]):-!.
elim0([0,X|T],[X|T]):-X\=0,!.
elim0([0,0|T],R):-elim0([0|T],R).

pred(X,R):-predh(X,R1,_),elim0(R1,R).
% pred([1,0,0],R) ==> R=[9,9] ^_^
