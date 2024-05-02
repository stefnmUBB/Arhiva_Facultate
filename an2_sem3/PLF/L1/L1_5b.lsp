;5 b) Definiti o functie care substituie un element E prin elementele unei liste
;L1 la toate nivelurile unei liste date L.

; _subst(e l1 x)
; e = elementul de inlocuit
; l = lista elementelor cu care se inlocuieste e
; x = lista (neliniara) in care inlocuim

(defun _subst (e l1 x)
	(cond
		((and (atom x) (equal x e)) l1)
		((and (atom x) (not (equal x e))) x)
		((and (atom (car x)) (equal (car x) e))
			(append 
				(_subst e l1 (car x))
				(_subst e l1 (cdr x))
			)
		)
		(t
			(cons
				(_subst e l1 (car x))
				(_subst e l1 (cdr x))
			)
		)
	)
)

(defun test ()
	(and
		(equal (_subst 2 '(3 4) '(1 (5 (2 7 (2))))) '(1 (5 (3 4 7 (3 4)))) )
		(equal (_subst 2 '(3 4) '(1 2 3 4 2) ) '(1 3 4 3 4 3 4) )
		(equal (_subst 2 '(3 4) '(1 (2 (3 4) 2)) ) '(1 (3 4 (3 4) 3 4)) )
		(equal (_subst 2 '(3 4) '(1 6 (5 (4 2)) 2 (2)) ) '(1 6 (5 (4 3 4)) 3 4 (3 4)) )
		
		
		(equal (_subst 2 '(3 4) '(2)) '(3 4) )
		(equal (_subst 2 '(3 4) '(1 2 3 2)) '(1 3 4 3 3 4) )
		(equal (_subst 2 '(3 4) '(1 2 (3 2))) '(1 3 4 (3 3 4)) )
		(equal (_subst 2 '(3 4) '(1 5 (3 5))) '(1 5 (3 5)) )
		(equal (_subst 2 '(2 3) '(1 2)) '(1 2 3) )
		(equal (_subst 2 '(2 3) '()) '() )
		;(null 2)
	)
)