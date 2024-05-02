;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;
;;;; 13 a) Sa se intercaleze un element pe pozitia a n-a a unei liste liniare.
;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


; intercalare_aux (l : Lista, e: Element, n:Intreg, i:Intreg)
; l = lista in care se face inserarea
; e = elementul care trebuie inserat in lista
; n = pozitia la care inseram elementul e
; i = indice parcurgere curenta

(defun intercalare_aux (l e n i)
	(cond
		( (= i n) (cons e l) )
		(  t      (cons (car l) (intercalare_aux (cdr l) e n (+ 1 i))))
	)
)

; intercalare(l:Lista, e:Element, n:Intreg)
; insereaza un element la pozitia n intr-o lista
; l = lista in care se face inserarea
; e = elementul care trebuie inserat in lista
; n = pozitia la care inseram elementul e
(defun intercalare (l e n) (intercalare_aux l e n 1))


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;
;;;; 13 b) Sa se construiasca o functie care intoarce suma atomilor numerici 
;;;;       dintr-o lista, de la orice nivel.
;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; suma(l:Lista)
; l = lista a carei suma o calculam
(defun suma (l) 
	(cond
		( (numberp l) l)
		( (atom l) 0)
		( t  (+ (suma (car l)) (suma (cdr l))))		 ; (listp l)
	)
)


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;
;;;; 13 c) Sa se scrie o functie care intoarce multimea tuturor sublistelor unei
;;;; liste date. Ex: Ptr. lista ((1 2 3) ((4 5) 6)) => ((1 2 3) (4 5) ((4 5) 6))
;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defun subliste (l)
	(cond 
		( (null l)          nil)
		( (listp (car l))   (cons (car l) (append (subliste (car l)) (subliste (cdr l)))))
		( t                 (subliste (cdr l)))
	)
)


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;
;;;; 13 d)  Sa se scrie o functie care testeaza egalitatea a doua multimi, fara
;;;; sa se faca apel la diferenta a doua multimi
;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; contains(A:Lista, e:Element)
; A = lista in care cautam elementul e
; e = elementul a carui apartenenta o testam in A
(defun contains (A e)
	(cond 
		( (null A)       nil)
		( (equal e (car A)) t)
		(  t             (contains (cdr A) e))
	)
)

; includes(A:Lista, B:Lista)
; A = o multime
; B = multimea pe care dorim sa o verificam daca este inclusa in A 
(defun includes (A B)
	(cond
		( (null B)  t)
		( t        (and (contains A (car B)) (includes A (cdr B)) ) )
	)
)

; seteq(A:Lista, B:Lista)
; A, B = multimi (se verifica A=B aka A<=B && B<=A)
(defun seteq (A B) (and (includes A B) (includes B A)))