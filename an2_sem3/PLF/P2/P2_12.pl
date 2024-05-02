% 12.
% a) Sa se inlocuiasca toate aparitiile unui element dintr-o lista cu un
% alt element.

repl([],_,_,[]):-!.
repl([E|T],E,N,[N|R]):-repl(T,E,N,R),!.
repl([H|T],E,N,[H|R]):-H\=E,repl(T,E,N,R).
