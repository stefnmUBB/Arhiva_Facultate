;10.
;a) Sa se construiasca o functie care intoarce produsul atomilor numerici
;   dintr-o lista, de la nivelul superficial.
;b) Sa se scrie o functie care, primind o lista, intoarce multimea tuturor
;   perechilor din lista. De exemplu: (a b c d) --> ((a b) (a c) (a d)(b c) 
;   (b d) (c d))
;c) Sa se determine rezultatul unei expresii aritmetice memorate in preordine
;   pe o stiva. Exemple:
;   (+ 1 3) ==> 4 (1 + 3)
;   (+ * 2 4 3) ==> 11 ((2 * 4) + 3)
;   (+ * 2 4 - 5 * 2 2) ==> 9 ((2 * 4) + (5 - (2 * 2))
;d) Definiti o functie care, dintr-o lista de atomi, produce o lista de
;   perechi (atom n), unde atom apare in lista initiala de n ori. De ex:
;   (A B A B A C A) --> ((A 4) (B 2) (C 1)).

(defun a (l)
	(cond
		((null l) 1)
		((numberp (car l)) (* (car l) (a (cdr l))))
		(t (a (cdr l)))
	)
)

(defun perechi (e l)
	(cond 
		((null l) nil)
		(t (cons (list e (car l)) (perechi e (cdr l))))
	)
)

(defun b (l)
	(cond
		((null l) nil)
		(t (append (perechi (car l) (cdr l)) (b (cdr l))))
	)
)

(defun c_op(o a b)
	(cond 
		((equal o '+) (+ a b))
		((equal o '-) (- a b))
		((equal o '*) (* a b))
		((equal o '/) (/ a b))
	)
)

; output: rezultat +  stiva ramasa
(defun c_h (l)
	; (print l)
	(cond
		((null l) (list 0 nil))
		((numberp (car l)) (list (car l) (cdr l)) )
		(t
			; to do : use lambda ...
			(list 
			(c_op			
				(car l)                           ; operator
				(car (c_h (cdr l)))               ; left
				(car (c_h (cadr (c_h (cdr l)))))  ; right
			)
			(cadr (c_h (cadr (c_h (cdr l))))))
		)
	)
)

(defun c(l) (car (c_h l)))


(defun add_to_dict (d e)
	(cond
		( (null d)                  (list (list e 1)) )
		( (equal e (car (car d)))   (cons (list e (+ 1 (cadr (car d)))) (cdr d)) )
		( t                         (cons (car d) (add_to_dict (cdr d) e)) )
	)
)

(defun d_h (l d)
	(cond
		((null l) d)
		(t (d_h (cdr l) (add_to_dict d (car l))))
	)
)

(defun d (l) (d_h l nil))