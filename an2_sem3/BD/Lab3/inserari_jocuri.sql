INSERT INTO Franciza (Nume) VALUES
('Pokemon'),
('Duel Masters'),
('Nintendogs'),
('Eragon'),
('Mario'),
('Legend of Zelda');


INSERT INTO Companie (Nume) VALUES
('Nintendo'),
('Sega'),
('Sony'),
('Game Freak');


INSERT INTO TipConsola (IdCompanie, Nume, DataLansare, Portabilitate, ProcesorPrincipal, FrecventaProcesor, LatimeDeBanda) VALUES
	(1, 'Game Boy', '1989-04-21', 'Portabil', 'GBZ80', 4190000, 8),
	(1, 'Game Boy Color', '1998-10-21', 'Portabil', 'GBZ80', 8000000, 8),
	(1, 'Game Boy Advance', '2001-06-11', 'Portabil', 'ARMv4', 16777216, 32),
	(1, 'Nintendo DS', '2004-11-21', 'Portabil', 'ARMv5', 67108864, 32);	

INSERT INTO TitluJocVideo (Nume, Platforma, DataLansare, IdFranciza, IdCompanie) VALUES
	('Double Dragon', 1, '1990', NULL, NULL),
	('Alleyway', 1, '1990', NULL, NULL),
	('Double Dragon II', 1, '1991', NULL, NULL),
	('Pokemon Jaune - Special Edition Pikachu', 1, '1999', NULL, NULL),
	('Roi Lion: La formidable aventure de Simba', 2, '2000', NULL, NULL),
	('WCW Mayhem', 2, '2000', NULL, NULL),
	('SpyHunter', 3, '2002', NULL, NULL),
	('Tarzan: L appel de la Jungle', 3, '2002', NULL, NULL),
	('Driver 2 Advance', 3, '2002', NULL, NULL),
	('Medal of Honour: Underground', 3, '2002', NULL, NULL),
	('The Lord of the Rings: Return of the king', 3, '2003', NULL, NULL),
	('Dogz', 3, '2004', NULL, NULL),
	('Nintendogs', 3, '2005', 3, 1),
	('Eragon', 3, '2006', 4, NULL),
	('Dr. Kawashima s Brain Training: How old is your brain?', 4, '2006', NULL, NULL),
	('PES 2008', 4, '2007', NULL, NULL),
	('Inazuma Eleven', 4, '2008', NULL, NULL),
	('Lea Passion Maitresse d Ecole', 4, '2008', NULL, NULL),
	('Guitar Hero: on Tour', 4, '2008', NULL, NULL),
	('Mario & Luigi: Bowser s inside story', 4, '2009', 5, 1),
	('The Legend of Zelda: Spirit traks', 4, '2009', 6, 1),
	('Harry Potter and the Half-Blood Prince', 4, '2009', NULL, NULL),
	('Pokemon: Version Blanche', 4, '2009', 1, NULL),
	('Barbie Dreamhouse Party', 4, '2013', NULL, NULL)
	
UPDATE TitluJocVideo SET 
	IdCompanie = 4 WHERE IdTitluJocVideo IN (4, 23)

Select * FROM Franciza;

