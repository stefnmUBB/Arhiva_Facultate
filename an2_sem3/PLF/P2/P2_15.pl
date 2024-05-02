%15.
% a) Sa se determine cea mai lunga secventa de numere pare consecutive
% dintr-o lista (daca sunt mai multe secvente de lungime maxima, una
% dintre ele).

% secvh(A, R, L=lungimea, C, CL, M, ML).
secvh([],M,ML,_,CL,M,ML):- ML>CL,!.
secvh([],C,CL,C,CL,_,ML) :- CL>=ML,!.

% gasim un numar impar, updatam M si ML daca CL>ML si resetam C
secvh([I|T],R,L,C,CL,_,ML):- 1 is I mod 2,
    CL>ML, !,
    secvh(T,R,L,[],0,C,CL).

secvh([I|T],R,L,_,CL,M,ML):- 1 is I mod 2,
    CL=<ML, !,
    secvh(T,R,L,[],0,M,ML).

% gasim un numar impar, il adaugam in C
secvh([P|T],R,L,C,CL,M,ML):-
    0 is P mod 2,
    C1 = [P|C], % se face aici adaugare la sfarsit daca se vrea ordinea din sir
    CL1 is CL+1,
    secvh(T,R,L,C1,CL1,M,ML).
