addf([],E,[E]).
addf([H|T],E,[H|R]):-addf(T,E,R).

invl([],[]):-!.
invl([H|T],R):-invl(T,R1), addf(R1,H,R).

sumh([],[],[],0):-!.
sumh([],[],[1],1):-!.
sumh([],[H|T],R,C):-!,sumh([H|T],[],R,C).
sumh(L,[],L,0):-!.
sumh([H|T],[],R,1):-!,sumh([H|T],[1],R,0).
sumh([HA|TA],[HB|TB],R,C):-
    H is HA+HB+C,
    C1 is H // 10,
    H1 is H mod 10,
    sumh(TA,TB,R1,C1),
    R=[H1|R1].


suma(A,B,R):-
    invl(A,AI),
    invl(B,BI),
    sumh(AI,BI,RI,0),
    invl(RI,R).

sumalist([],[0]).
sumalist([H|T],R):-
    is_list(H), !,
    sumalist(T,R1),
    suma(H,R1,R).
sumalist([H|T],R):-
    not(is_list(H)),
    sumalist(T,R).





