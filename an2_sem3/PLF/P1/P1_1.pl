% exists(L:Lista, E:Element)
% verifica daca elementul E apartine listei L
% E = element
% L = lista de verificat aparitia elementului E
% (i,i), (i,o)
exists([H|_], H):-!.
exists([_|T], E):- exists(T,E).

% diff(A,B,R)

diff([H|T], B, R) :-
    not(exists(B, H)),
    diff(T, B, R1),
    R = [H|R1], !.
diff([H|T], B, R) :-
    exists(B, H),
    diff(T,B,R), !.
diff([], _, []).


% add1(L, R)
add1([H|T],[H,1|R]):- H mod 2 =:= 0, add1(T,R), !.
add1([H|T],[H|R])  :- H mod 2 =\= 0, add1(T,R).
add1([],[]).


