
; prod(x : Lista)
; Calculeaza produsul numerelor dintr-o lista
; de la orice nivel
; x = Lista eterogena
(defun prod (x)
	(cond 
		((numberp x) x)
		((atom x) 1)
		(t (apply #'* (mapcar #'prod x)))
	)
)