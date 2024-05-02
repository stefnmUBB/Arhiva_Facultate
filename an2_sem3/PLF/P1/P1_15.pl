lista_par([]).
lista_par([_|T]) :- not(lista_par(T)).

elmin([],0).
elmin([H],H):-!.
elmin([H|T],M) :- elmin(T,M), M<H, !.
elmin([H|T],H) :- elmin(T,M), H=<M, !.

delfirst([],_,[]).
delfirst([H|T],H,T) :- !.
delfirst([H|T],V,[H|R]):-
    H\=V, !,
    delfirst(T,V,R).

delfirstmin(L,R):-
    elmin(L, M),
    delfirst(L,M,R).
