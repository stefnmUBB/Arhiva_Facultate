;1.
;a) Sa se insereze intr-o lista liniara un atom a dat dupa al 2-lea, al 4-lea,
;   al 6-lea,....element.
;b) Definiti o functie care obtine dintr-o lista data lista tuturor atomilor
;   care apar, pe orice nivel, dar in ordine inversa. De exemplu: (((A B) C)
;   (D E)) --> (E D C B A)
;c) Definiti o functie care intoarce cel mai mare divizor comun al numerelor
;   dintr-o lista neliniara.
;d) Sa se scrie o functie care determina numarul de aparitii ale unui atom dat
;   intr-o lista neliniara.

(defun a_h (l e p)
	(cond
		((null l) nil)
		((= 0 (mod p 2)) (cons (car l) (cons e (a_h (cdr l) e (+ 1 p))) )  )
		((= 1 (mod p 2)) (cons (car l) (a_h (cdr l) e (+ 1 p))) )  
	)
)

(defun a (l e) (a_h l e 1))

;(defun inv_lista_h (l c)
;	(cond
;		((null l) c)
;		(t (inv_lista_h (cdr l) (cons (car l) c)))
;	)
;)
;(defun inv_lista (l) (inv_lista_h l '()))

(defun b_h (l c) 
	(cond
		((null l) c)
		((atom (car l)) (b_h (cdr l) (cons (car l) c)))
		(t (b_h (cdr l) (append (b_h (car l) '()) c)))
	)
)

(defun b (l) (b_h l '()))

(defun cmmdc (a b)
	(cond
		((= 0 a) b)
		((= 0 b) a)
		(t (cmmdc b (mod a b)))
	)
)

(defun c (l)
	(cond
		((null l) 0)
		((numberp l) l)
		((atom l) 0)		
		(t (cmmdc (c (car l)) (c (cdr l)))) 
	)
)

(defun d (l e)
	(cond 
		((null l) 0)
		((equal l e) 1)
		((atom l) 0)
		(t (+ (d (car l) e) (d (cdr l) e)))
	)
)