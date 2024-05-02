% 5.
% a) Sa se determine pozitiile elementului maxim dintr-o lista liniara.
% De ex: poz([10,14,12,13,14], L) va produce L = [2,5].

elmax([],0):-!.
elmax([H],H):-!.
elmax([H|T],H):- elmax(T,MT), H>=MT,!.
elmax([H|T],MT):-elmax(T,MT), H=<MT.


pozh([],[],_,_).
pozh([H|T],R,I,X):-
    H=X,
    I1 is I+1,
    pozh(T,R1,I1,X),
    R=[I|R1].       % ii incarcam pozitia
pozh([H|T],R,I,X):-
    H\=X,
    I1 is I+1,
    pozh(T,R,I1,X).

poz(L,R):-
    elmax(L,M),
    pozh(L,R,1,M).
