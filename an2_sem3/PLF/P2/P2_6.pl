%6.
%a) Intr-o lista L sa se inlocuiasca toate aparitiile unui element E cu
%elementele unei alte liste, L1. De ex: inloc([1,2,1,3,1,4],1,[10,11],X)
%va produce X=[10,11,2,10,11,3,10,11,4].

lstconcat([],A,A):-!.
lstconcat([H|T],A,[H|R]):-lstconcat(T,A,R).


inloc([],_,_,[]):-!.
inloc([E|T],E,L,R):-!,
    inloc(T,E,L,R1),
    lstconcat(L,R1,R).

inloc([H|T],E,L,[H|R]):-
    inloc(T,E,L,R).




