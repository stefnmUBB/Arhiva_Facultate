% cmmdc(a,b,R)
cmmdc(A,0,A):-!.
cmmdc(0,A,A):-!.
cmmdc(1,_,1):-!.
cmmdc(_,1,1):-!.
cmmdc(A,B,R) :- Rst is A mod B,
    cmmdc(B, Rst, R), !.

% cmmmc(A,B,R)
cmmmc(0,_,0):-!.
cmmmc(_,0,0):-!.
cmmmc(A,B,R) :-
    cmmdc(A,B,D),
    R is A*B//D.

% cmmmclst(L:List,R:Integer)
cmmmclst([],0):-!.
cmmmclst([H],H):-!.
cmmmclst([H|T],R) :- cmmmclst(T,RT), cmmmc(H,RT,R).


% addp2(L,R,V,I,P)
addp2([H|T],R,V,I,P):- I<P, !,
    I1 is I+1,
    addp2(T, R2, V, I1, P),
    R = [H|R2].
addp2([H|T],R,V,I,I):-
    I1 is I+1,
    P2 is 2*I,
    addp2(T,R2,V,I1,P2),
    R = [H,V|R2].
addp2([],[],_,_,_).
