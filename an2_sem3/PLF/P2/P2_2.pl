%2.
% a) Sa se sorteze o lista cu pastrarea dublurilor. De ex: [4 2 6 2 3 4]
% => [2 2 3 4 4 6]

insert_sorted([],E,[E]):-!.
insert_sorted([H|T],E,[E,H|T]):-E<H,!.
insert_sorted([H|T],E,[H|R]):- E>=H,
    insert_sorted(T,E,R).


srtlsth([],C,C):-!.
srtlsth([H|T],S,C):-
    insert_sorted(C,H,C1),
    srtlsth(T,S,C1).

srtlst(L,R):-srtlsth(L,R,[]).


