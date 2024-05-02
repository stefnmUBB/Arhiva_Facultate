USE DepozitColectie;
GO

DELETE FROM CompozitieMoneda;
DELETE FROM MateriePrimaMoneda;
DELETE FROM MaterialMoneda;
DELETE FROM TipMonede;
DELETE FROM NotaNumismatica;

INSERT INTO NotaNumismatica (Valoare, Valuta) VALUES
	(  1, 'Leu'),
	(  2, 'Leu'),
	(  3, 'Leu'),
	(  5, 'Leu'),
	( 10, 'Leu'),
	( 50, 'Leu'),
	(100, 'Leu'),
	(200, 'Leu'),
	(500, 'Leu'),
	(1000, 'Leu'),
	(2000, 'Leu'),
	(5000, 'Leu'),
	(10000, 'Leu'),
	(50000, 'Leu'),
	(100000, 'Leu'),
	(500000, 'Leu'),
	(1000000, 'Leu');

SELECT * FROM NotaNumismatica;

INSERT INTO MateriePrimaMoneda (Denumire) VALUES
	('Aluminiu'),
	('Oțel'),
	('Alamă'),
	('Cupru');

INSERT INTO MaterialMoneda (Denumire) VALUES
	('Aluminiu');

SELECT * FROM MateriePrimaMoneda;
SELECT * FROM MaterialMoneda;

INSERT INTO CompozitieMoneda VALUES 
	(1, 1, 100);

SELECT * FROM CompozitieMoneda;


INSERT INTO TipMonede (IdNotaNumismatica, Orientare, An, IdMaterialMoneda) VALUES
	( 12, 'Moneda', 2000, 1),
	( 12, 'Moneda', 2001, 1),
	( 12, 'Moneda', 2002, 1),
	( 12, 'Moneda', 2003, 1),
	( 10, 'Moneda', 2000, 1),
	( 10, 'Moneda', 2001, 1),
	( 10, 'Moneda', 2002, 1),
	( 10, 'Moneda', 2003, 1);


SELECT * FROM TipMonede;


INSERT INTO NotaNumismatica (Valoare, Valuta) VALUES
	(  1, 'Ban'),
	(  2, 'Ban'),	
	(  5, 'Ban'),
	( 10, 'Ban'),
	( 15, 'Ban'),
	( 25, 'Ban'),
	( 50, 'Ban');


SELECT * FROM NotaNumismatica;


INSERT INTO TipMonede (IdNotaNumismatica, Orientare, An) VALUES
	( 20, 'Moneda', 1953),
	( 20, 'Moneda', 1957),
	( 22, 'Moneda', 1960),
	( 22, 'Moneda', 1975),
	( 21, 'Moneda', 1955);
	
SELECT * FROM TipMonede;

UPDATE NotaNumismatica SET Tara='Romania';
SELECT * FROM NotaNumismatica;


INSERT INTO NotaNumismatica (Valoare, Valuta, Tara) VALUES
	(1, 'Rubla', 'Rusia'),
	(2, 'Rubla', 'Rusia'),
	(5, 'Rubla', 'Rusia'),
	(10, 'Rubla', 'Rusia'),
	(50, 'Rubla', 'Rusia'),
	(100, 'Rubla', 'Rusia'),
	(500, 'Rubla', 'Rusia'),
	(1000, 'Rubla', 'Rusia'),
	(5000, 'Rubla', 'Rusia'),
	(1, 'Copeica', 'Rusia'),
	(2, 'Copeica', 'Rusia'),
	(5, 'Copeica', 'Rusia'),
	(10, 'Copeica', 'Rusia'),
	(1, 'Grivna', 'Ucraina'),
	(2, 'Grivna', 'Ucraina'),
	(5, 'Grivna', 'Ucraina'),
	(10, 'Grivna', 'Ucraina'),
	(20, 'Grivna', 'Ucraina'),
	(50, 'Grivna', 'Ucraina'),
	(100, 'Grivna', 'Ucraina'),
	(200, 'Grivna', 'Ucraina'),
	(500, 'Grivna', 'Ucraina'),
	(1, 'Copeica', 'Ucraina'),
	(2, 'Copeica', 'Ucraina'),
	(5, 'Copeica', 'Ucraina'),
	(10, 'Copeica', 'Ucraina'),
	(25, 'Copeica', 'Ucraina'),
	(50, 'Copeica', 'Ucraina');



SELECT * FROM TipMonede;

INSERT INTO ExemplarMonede (IdTipMoneda, StareConservare, IdDepozit) VALUES 
	(1, 10, 1),
	(1, 7, 1),
	(5, 8, 2),
	(7, 5, 4),
	(8, 8, 2),
	(8, 6, 4),
	(11, 4, 2),
	(2, 6, 4),
	(3, 8, 4),
	(11, 7, 3),
	(5, 2, 4),
	(9, 7, 2),
	(3, 9, 3),
	(8, 2, 4),
	(6, 1, 3),
	(7, 6, 4),
	(2, 8, 4),
	(11, 7, 2),
	(10, 3, 1),
	(2, 6, 1),
	(12, 4, 2),
	(11, 2, 1),
	(9, 1, 3),
	(3, 5, 2),
	(10, 1, 1),
	(4, 3, 2),
	(11, 4, 2),
	(6, 1, 4),
	(7, 1, 3),
	(4, 3, 1),
	(4, 6, 4),
	(1, 7, 3),
	(9, 4, 4),
	(12, 9, 4),
	(1, 7, 1),
	(1, 2, 1),
	(11, 1, 3),
	(12, 1, 3),
	(3, 2, 4),
	(10, 6, 3),
	(10, 1, 1),
	(9, 8, 1),
	(3, 2, 2),
	(6, 2, 4),
	(5, 7, 4),
	(2, 7, 4),
	(10, 3, 2),
	(8, 4, 1),
	(6, 4, 3),
	(6, 5, 4),
	(12, 3, 1),
	(4, 3, 2),
	(4, 3, 3),
	(1, 8, 4)