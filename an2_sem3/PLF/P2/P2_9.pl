%9.
%a) Dandu-se o lista liniara numerica, sa se stearga toate secventele de
% valori consecutive. Ex: sterg([1, 2, 4, 6, 7, 8, 10], L) va produce
% L=[4,10].

del([],[]).
del([H],[H]):-!.
del([H1,H2],[]):- 1 is H2-H1,!.
del([H1,H2],[H1,H2]):-not(1 is H2-H1),!.
del([H1,H2,H3|T],R):- 1 is H2-H1, 1 is H3-H2, !,
    del([H2,H3|T],R).
del([H1,H2,H3|T],R):-1 is H2-H1, not(1 is H3-H2),!,
    del([H3|T],R).
del([H1,H2|T],[H1|R]):- not(1 is H2-H1),
    del([H2|T],R).

