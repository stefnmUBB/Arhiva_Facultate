% Lista = Element*


% exista(E:Element, L:lista)
% (i,i)
% E - elementul de verificat daca exista in L
% L - lista de elemente

exista(_, []) :- fail().
exista(H, [H|_]).
exista(E, [H|T]) :- E\= H, exista(E,T).


% cpylst(A:Lista,R:Lista)
% (i,i) (i,o)
% A - lista
% R - copie lista (neordonat)
cpylst([],[]).
cpylst(H, H). % caz separat un singur element?
cpylst([H|T],R) :-
    cpylst(T,R1),
    R is [H|R1].


% emultime(L:Lista)
% (i)
% L - Lista de verificat daca e multime
emultime([]).
emultime([H|T]) :- not(exista(H,T)), emultime(T).

goals_emultime() :-
    emultime([]),
    emultime([1]),
    emultime([1,2,3]),
    not(emultime([1,2,1,4,5])),
    not(emultime([1,1,1])).


% adaugaSf(E:Element, L:Lista, R:Lista)
% (i,i,i), (i,i,o)
% E - elementul de adaugat
% L - lista initiala
% R - lista L cu elementul E la final
adaugaSf(E,[],[E]).
adaugaSf(E,[H|T],R) :- adaugaSf(E,T,R1), R = [H|R1].

% eliminah(E:Element, L:Lista, C:Lista, N:Integer, R:Lista)
% (i,i,i,i,i), (i,i,o,i,o)
% E - elementul de eliminat
% L - lista din care eliminam
% C - variabila colectoare
% N - numarul de elimnari ramase
% R - lista rezultata prin eliminarea a N aparitii ale lui E
eliminah(_,[],C,_,R) :- R=C.

eliminah(_, [H|T], C, 0, R) :-
    adaugaSf(H,C,C1),
    eliminah(_,T,C1,0,R).

eliminah(E, [E|T], C, N, R) :- N>0,
    N1 is N-1,
    eliminah(E,T,C,N1,R).

eliminah(E, [H|T], C, N, R) :- N>0, E \= H,
    adaugaSf(H,C,C1),
    eliminah(E,T,C1,N,R).

% elimina(E:Element, L:Lista, R:Lista)
% (i,i,i), (i,i,o)
% E - elementul de eliminat din lista
% L - lista initiala
% R - lista rezultata prin eliminarea a 3 aparitii ale lui E din L
elimina(E,L,R) :- eliminah(E,L,[],3,R).

goals_elimina() :-
    elimina(3,[],[]),
    elimina(2,[1,3,5],[1,3,5]),
    elimina(2,[1,2,3,4,5],[1,3,4,5]),
    elimina(2,[2,4,2,6],[4,6]),
    elimina(2,[2,4,2,6,2,8],[4,6,8]),
    elimina(2,[2,4,2,6,2,8,2,10],[4,6,8,2,10]),
    elimina(2,[2],[]),
    elimina(2,[2,2],[]),
    elimina(2,[2,2,2],[]),
    elimina(2,[2,2,2,2],[2]).
