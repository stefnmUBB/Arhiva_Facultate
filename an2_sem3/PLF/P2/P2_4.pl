% 4.
% a) Sa se interclaseze fara pastrarea dublurilor doua liste sortate.

hmin(A,B,A):-A<B,!.
hmin(A,B,B):-A>=B.

hmax(A,B,A):-A>B,!.
hmax(A,B,B):-A=<B.


% interclasarea e comutativa
interclasare([],B,R) :- !,interclasare(B,[],R).

% eliminam dublurile din lista ramasa (aia mai lunga)
interclasare([],[],[]):-!.
interclasare([H],[],[H]):-!.
interclasare([H,H|T],[],R):-!, interclasare([H|T],[],R).
interclasare([H,A|T],[],[H|R]):-!, A=\=H, interclasare([A|T],[],R).

% interclasam 2 liste pana una din ele devine []
interclasare(A,B,R):-
    interclasare(A,[],[HA|TA]), % eliminam duplicatele
    interclasare(B,[],[HB|_]), % din fiecare lista
    HA<HB, !,
    interclasare(TA,B,T),
    interclasare([HA|T],[],R).

interclasare(A,B,R):-
    interclasare(A,[],[HA|_]), % eliminam duplicatele
    interclasare(B,[],[HB|TB]), % din fiecare lista
    HA>HB, !,
    interclasare(TB,A,T),
    % in loc de R=[HB|T], mai concatenez o data cu []
    % ca sa elimin duplicatele intre HB si inceputul lui T
    interclasare([HB|T],[],R).

interclasare(A,B,R):-
    interclasare(A,[],[HA|TA]), % eliminam duplicatele
    interclasare(B,[],[HB|TB]), % din fiecare lista
    HA=HB, !,
    interclasare(TA,TB,T),
    interclasare([HA|T],[],R).

