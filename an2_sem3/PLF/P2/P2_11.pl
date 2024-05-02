% 11.
% a) Se da o lista de numere intregi. Se cere sa se scrie de 2 ori in
% lista fiecare numar prim.

primh(1,_):-fail(), !.
primh(_,1):-!.
primh(X,D):- X>1, not(0 is X mod D), D1 is D-1, primh(X,D1).

prim(X):-X1 is X-1, primh(X,X1).

dupprim([],[]).
dupprim([H|T],[H,H|R]):-prim(H),!, dupprim(T,R).
dupprim([H|T],[H|R]):-not(prim(H)), dupprim(T,R).
