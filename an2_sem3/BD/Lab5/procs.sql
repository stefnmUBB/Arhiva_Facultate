-- 1
CREATE PROCEDURE MarireLenCladireSpatiuDepozitare
AS
BEGIN
	ALTER TABLE	SpatiuDepozitare 
		ALTER COLUMN Cladire VARCHAR(30);
	PRINT  'SpatiuDepozitare.Cladire are 30 de caractere'
END
GO

CREATE PROCEDURE MicsorareLenCladireSpatiuDepozitare
AS
BEGIN
	ALTER TABLE	SpatiuDepozitare 
		ALTER COLUMN Cladire VARCHAR(20);
	PRINT  'SpatiuDepozitare.Cladire are 20 de caractere'
END
GO

-- 2
CREATE PROCEDURE SetareDefaultDepozitExemplarJocVideo
AS
BEGIN
	ALTER TABLE ExemplarJocVideo
		ADD CONSTRAINT df_Depozit7 DEFAULT 7 FOR IdDepozit
	PRINT 'Exemplarele Joc Video sunt trimise automat la IdDepozit 7'
END
GO

CREATE PROCEDURE EliminareDefaultDepozitExemplarJocVideo
AS
BEGIN
	ALTER TABLE ExemplarJocVideo
		DROP CONSTRAINT df_Depozit7
	PRINT 'S-a anulat default IdDepozit pentru ExemplarJocVideo'
END
GO

--3
CREATE PROCEDURE CreareTabelWishlistMoneda
AS
BEGIN
	CREATE TABLE WishlistMoneda (
		IdWish   INT PRIMARY KEY IDENTITY,
		IdMoneda INT NOT NULL
	)
	PRINT 'S-a creat wishlist pentru monede'
END
GO

CREATE PROCEDURE StergereTabelWishlistMoneda
AS
BEGIN
	DROP TABLE WishlistMoneda
	PRINT 'S-a sters wishlist pentru monede'
END
GO

-- 4
CREATE PROCEDURE AdaugareDescriereWish
AS
BEGIN
	ALTER TABLE WishlistMoneda ADD Descriere VARCHAR(200);
	PRINT 'S-a adaugat descriere wish'
END
GO

CREATE PROCEDURE StergereDescriereWish
AS
BEGIN
	ALTER TABLE WishlistMoneda DROP COLUMN Descriere;
	PRINT 'S-a sters descriere wish'
END
GO

-- 5

CREATE PROCEDURE AdaugareFKWish
AS
BEGIN
	ALTER TABLE WishlistMoneda 
		ADD CONSTRAINT fk_IdMoneda FOREIGN KEY (IdMoneda) REFERENCES TipMonede
	PRINT 'S-a adaugat fk in Wishlist'
END
GO

CREATE PROCEDURE StergereFKWish
AS
BEGIN
	ALTER TABLE WishlistMoneda 
		DROP CONSTRAINT fk_IdMoneda
	PRINT 'S-a sters fk din Wishlist'
END

