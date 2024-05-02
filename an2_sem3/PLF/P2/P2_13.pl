% listdiv(N:Integer, R:Lista, D:Integer)
% (i,o,i), (i,i,i)
% creeaza lista divizorilor proprii ai lui N
% N = numarul ai carui divizori ii aflam
% R = lista divizorilor lui N
% D = contor divizor D>=2
listdiv(_, [], 0):-!.
listdiv(1, [], _):-!.
listdiv(0, [], _):-!.
listdiv(N, [], D):- D>=N, !.
listdiv(N, R, D) :-
    N mod D =:= 0,
    D1 is D+1,
    listdiv(N,R1,D1),
    R = [D|R1].
listdiv(N,R,D) :-
    N mod D =\= 0,
    D1 is D+1,
    listdiv(N, R, D1).


% conct(L1,L2,R)
% (i,i,i), (i,i,o), (o,o,i)
% concateneaza 2 liste
% L1 = prima lista
% L2 = a doua lista
% R = lista formata din elementele lui L1, apoi L2
conct([],L2,L2).
conct([H|T],L2,[H|R]) :- conct(T,L2,R).

% addiv(L, R)
% (i,o), (i,i)
% creeaza lista obtinuta prin adaugarea dupa fiecare element
% a divizorilor proprii ai acestuia
% L = lista initiala
% R = lista in care s-au adaugat divizorii
addiv([],[]).
addiv([H|T],R) :-
    addiv(T, RT),
    listdiv(H, RD, 2),
    conct(RD, RT, R1),
    R=[H|R1].

% addivet(L,R)
% (i,o), (i,i)
% in fiecare sublista, dupa fiecare element adauga divizorii
% proprii ai acestuia
% L = lista initiala
% R = lista continand sublistele modificate
addivet([], []).
addivet([H|T],[H|R]):-
    number(H),
    addivet(T,R).
addivet([H|T],[H1|R]) :-
    is_list(H),
    addiv(H,H1),
    addivet(T,R).

testaddiv() :-
    addiv([],[]),
    addiv([1,2,3],[1,2,3]),
    addiv([9,6,2],[9,3,6,2,3,2]),
    addiv([34,16,10],[34,2,17,16,2,4,8,10,2,5]),
    not(addiv([15],[3,5])).

testaddivet():-
    addivet([],[]),
    addivet([1,2,3],[1,2,3]),
    addivet([1,[2,5,7],4,5,[1,4],  3,2,[6,2,1],    4,[7,2,8,1],    2],
            [1,[2,5,7],4,5,[1,4,2],3,2,[6,2,3,2,1],4,[7,2,8,2,4,1],2]).
