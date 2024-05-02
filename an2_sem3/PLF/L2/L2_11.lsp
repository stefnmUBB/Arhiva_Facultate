; 11. Se da un arbore de tipul (2). Sa se afiseze nivelul (si lista corespunza-
; toare a nodurilor) avand numar maxim de noduri. Nivelul rad. se considera 0.


; lvlcnt(l:Lista, n:Intreg)
; Gaseste lista nodurilor de pe nivelul n din arborele l
; l = arbore sub forma de lista
; n = nivelul arborelui
(defun lvlcnt (l n)
	(cond
		( (null l) nil)
		( (= n 0) (list (car l)))
		( t (append (lvlcnt (cadr l) (- n 1)) (lvlcnt (caddr l) (- n 1)) ))
	)
)

; len(l:Lista)
; Lungimea listei l
; l = lista
(defun len(l)
	(cond
		((null l) 0)
		(t (+ 1 (len (cdr l))))
	)
)

; maxim(r1:Lista,r2:Lista)
; Returneaza lista care are primul element mai mare
; r1 = lista (Numar, *)
; r2 = lista (Numar, *)
(defun maxim (r1 r2) 
	(cond 
		((null r1) r2)
		((null r2) r1)
		((> (car r1) (car r2)) r1)
		(t r2)
	)
)

; levels(l:Lista, n:Lista)
; Returneaza o lista de forma (nr_noduri, lista_noduri)
; reprezentand nodurile de pe nivelul cu nr maxim de noduri,
; precum si numarul acestora
; l = arbore sub forma de lista
; n = nivelul de la care incepem cautarea
(defun levels(l n)
	((lambda(row)
		(cond 
			((null row) (list 0 nil))
			(t (maxim (list (len row) row) (levels l (+ n 1))))
		))
		(lvlcnt l n)
	)
)

; level_max_row(l:lista)
; Returneaza o lista de forma (nr_noduri, lista_noduri)
; reprezentand nodurile de pe nivelul cu nr maxim de noduri,
; precum si numarul acestora
; l = arbore sub forma de lista
(defun level_max_row (l) (levels l 0))