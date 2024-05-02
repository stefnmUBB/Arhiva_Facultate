%10.
% a) Se da o lista de numere intregi. Se cere sa se adauge in lista dupa
% 1-ul element, al 3-lea element, al 7-lea elemen, al 15-lea element ...
% o valoare data e.

adaugh([],_,[],_,_).
adaugh([H|T],E,R,P,P):-
    P2 is 2*P+1,
    I1 is P+1, !,
    adaugh(T,E,R1,I1,P2),
    R=[H,E|R1].
adaugh([H|T],E,R,I,P):-
    I1 is I+1,
    adaugh(T,E,R1,I1,P),
    R=[H|R1].

adaug(L,E,R) :- adaugh(L,E,R,1,1).
