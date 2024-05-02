SELECT * FROM TipConsola

SELECT * FROM ExemplarConsole

INSERT INTO ExemplarConsole (IdTipConsola, StareConservare) VALUES
	(1, 4),
	(2, 6),
	(3, 7),
	(4, 9)

INSERT INTO Vanzator (Nume, Prenume) VALUES
	('Vladimirescu', 'Alexandru'),
	('Iftimie', 'Alberta'),
	('Popa', 'Rares'),
	('Stelian', 'Vasile')

SELECT * FROM Vanzator
SELECT * FROM ExemplarConsole

INSERT INTO Achizitie (IdVanzator, IdConsola, Pret) VALUES
	(1, 2, 150),
	(2, 3, 200),
	(2, 5, 75),
	(4, 4, 63)

SELECT * FROM Achizitie