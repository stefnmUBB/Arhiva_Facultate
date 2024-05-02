CREATE OR ALTER FUNCTION validateNotaNumismaticaValoare (@valoare FLOAT)
	RETURNS INT
AS BEGIN
	IF @valoare<0  RETURN 1;
	RETURN 0;
END
GO

CREATE OR ALTER FUNCTION validateOnlyLetters (@name VARCHAR(100))
	RETURNS INT
AS BEGIN
	IF @name LIKE '%[^a-zA-Z]%' RETURN 1;
	RETURN 0;
END
GO

CREATE OR ALTER FUNCTION validateAlphaNum (@name VARCHAR(100))
	RETURNS INT
AS BEGIN
	IF @name LIKE '%[^a-zA-Z0-9]%' RETURN 1;
	RETURN 0;
END
GO

CREATE OR ALTER FUNCTION validateNotNullVarchar (@name VARCHAR(100))
	RETURNS INT
AS BEGIN
	IF @name IS NULL RETURN 1
	RETURN 0
END
GO

CREATE OR ALTER FUNCTION validateNotaNumismaticaValuta (@valuta VARCHAR(30))
	RETURNS INT
AS BEGIN
	IF dbo.validateNotNullVarchar(@valuta)<>0 OR dbo.validateOnlyLetters(@valuta)<>0
		RETURN 1
	RETURN 0
END
GO

CREATE OR ALTER PROCEDURE crudInsertNotaNumismatica
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;	
	IF dbo.validateNotaNumismaticaValoare(@valoare)<>0
	BEGIN
		PRINT 'Valoare invalida';
		SET @success = 1;		
	END
	IF dbo.validateNotaNumismaticaValuta(@valuta)<>0
	BEGIN
		PRINT 'Valuta invalida';
		SET @success = 1;		
	END

	IF @success <> 0 RETURN

	IF EXISTS(SELECT * FROM NotaNumismatica WHERE Valoare = @valoare AND Valuta = @valuta)
	BEGIN
		PRINT 'Nota exista deja'
		RETURN -- no error tho
	END	
	
	INSERT INTO NotaNumismatica (Valoare, Valuta) VALUES (@valoare, @valuta);	
END
GO

CREATE OR ALTER PROCEDURE crudInsertMaterialMoneda @denumire VARCHAR(20),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;		
	IF dbo.validateAlphaNum(@denumire)<>0
	BEGIN
		PRINT 'Denumire invalida';
		SET @success = 1;		
	END
	IF @success <> 0 RETURN

	IF EXISTS(SELECT * FROM MaterialMoneda WHERE Denumire = @denumire)
	BEGIN
		PRINT 'Materialul exista deja'
		RETURN -- no error tho
	END	

	INSERT INTO MaterialMoneda (Denumire) VALUES (@denumire)
END
GO

CREATE OR ALTER FUNCTION validateTipMonedeOrientare (@orientare VARCHAR(100))
RETURNS INT
AS BEGIN
	IF (@orientare = 'Moneda' OR @orientare = 'Medalie')
		RETURN 0;
	RETURN 1
END
GO

CREATE OR ALTER FUNCTION validateTipMonedeAn (@an INT)
RETURNS INT
AS BEGIN
	IF (@an<0 OR @an>YEAR(GETDATE())) RETURN 1
	RETURN 0;
END
GO

CREATE OR ALTER PROCEDURE crudInsertTipMonede 
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@an INT,
	@orientare VARCHAR(30) = NULL,
	@material VARCHAR(100) = NULL,
	@success INT OUTPUT
AS BEGIN	
	SET @success = 0
	EXEC crudInsertNotaNumismatica @valoare, @valuta, @success OUTPUT;
	IF @success <> 0 BEGIN PRINT 'Failed' RETURN END

	EXEC crudInsertMaterialMoneda @material, @success OUTPUT;
	IF @success <> 0 BEGIN PRINT 'Failed' RETURN END

	IF dbo.validateTipMonedeOrientare(@orientare)<>0 
	BEGIN
		SET @success = 1
		PRINT 'Orientare invalida' RETURN
	END

	IF dbo.validateTipMonedeAn(@an)<>0
	BEGIN
		SET @success = 1
		PRINT 'An invalid' RETURN
	END	

	DECLARE @id_material INT
	SELECT TOP 1 @id_material = IdMaterialMoneda FROM MaterialMoneda
		WHERE Denumire = @material

	IF @id_material IS NULL 
	BEGIN 
		SET @success = 13
		PRINT 'Material null???' RETURN 
	END

	DECLARE @id_nota_num INT
	SELECT TOP 1 @id_nota_num = IdNotaNumismatica FROM NotaNumismatica
		WHERE Valoare = @valoare AND Valuta = @valuta;

	IF @id_nota_num IS NULL 
	BEGIN 
		SET @success = 13
		PRINT 'Nota null???' RETURN 
	END

	IF EXISTS(SELECT * FROM TipMonede WHERE 
		IdNotaNumismatica = @id_nota_num AND
		Orientare = @orientare AND
		An = @an AND
		IdMaterialMoneda = @id_material)
	BEGIN
		SET @success = 1
		PRINT 'Duplicates not allowed'
		RETURN
	END

	INSERT INTO TipMonede (IdNotaNumismatica, Orientare, IdMaterialMoneda, An)
	VALUES (@id_nota_num, @orientare, @id_material, @an);		
END
GO

CREATE OR ALTER FUNCTION validateExemplarMonedeStareConservare (@stare INT)
RETURNS INT
AS BEGIN
	IF @stare<1 OR @stare>10 RETURN 1
	RETURN 0
END
GO

CREATE OR ALTER PROCEDURE crudInsertExemplarMonede 
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@an INT,
	@orientare VARCHAR(30) = NULL,
	@material VARCHAR(100) = NULL,
	@stare_conservare INT,
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;
	IF dbo.validateExemplarMonedeStareConservare(@stare_conservare)<>0
	BEGIN
		SET @success = 1
		PRINT 'Stare conservare invalida' RETURN
	END	

	IF NOT EXISTS(SELECT * FROM TipMonede TM
		INNER JOIN NotaNumismatica NN ON TM.IdNotaNumismatica = NN.IdNotaNumismatica
		INNER JOIN MaterialMoneda MM ON TM.IdMaterialMoneda = MM.IdMaterialMoneda
		WHERE NN.Valoare = @valoare AND NN.Valuta = @valuta
		AND TM.An = @an AND TM.Orientare = @orientare AND MM.Denumire = @material
		)
	BEGIN
		EXEC crudInsertTipMonede @valoare, @valuta, @an, @orientare, @material, @success OUTPUT;
		IF @success <> 0 BEGIN PRINT 'Failed' RETURN END
	END

	DECLARE @tip_m_id INT
	SELECT TOP 1 @tip_m_id = TM.IdTipMoneda FROM TipMonede TM
		INNER JOIN NotaNumismatica N ON TM.IdNotaNumismatica = N.IdNotaNumismatica
		INNER JOIN MaterialMoneda M ON TM.IdMaterialMoneda = M.IdMaterialMoneda
		WHERE N.Valoare = @valoare
		AND N.Valuta = @valuta
		AND TM.An = @an
		AND TM.Orientare = @orientare
		AND M.Denumire = @material;
	
	IF @tip_m_id IS NULL 
	BEGIN 
		SET @success = 13
		PRINT 'TipMoneda null???' RETURN 
	END

	INSERT INTO ExemplarMonede (IdTipMoneda, StareConservare) VALUES
		(@tip_m_id, @stare_conservare)
END
GO

CREATE OR ALTER PROCEDURE crudInsertMateriePrimaMoneda
	@denumire VARCHAR(100),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;		
	IF dbo.validateAlphaNum(@denumire)<>0

	BEGIN
		PRINT 'Denumire invalida';
		SET @success = 1;		
	END
	IF @success <> 0 RETURN

	IF EXISTS(SELECT * FROM MateriePrimaMoneda WHERE Denumire = @denumire)
	BEGIN
		PRINT 'Materialul exista deja'
		RETURN -- no error tho
	END	

	INSERT INTO MateriePrimaMoneda (Denumire) VALUES (@denumire)	
END
GO

CREATE OR ALTER FUNCTION validateCompozitieMonedaProcentInsert (@procent INT)
RETURNS INT
AS BEGIN
	IF @procent <= 0 OR @procent>100 
		RETURN 1	
	RETURN 0
END
GO

CREATE OR ALTER PROCEDURE crudInsertCompozitieMoneda
	@material VARCHAR(100), @materie_prima VARCHAR(100), 
	@procent INT,
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;
	IF dbo.validateCompozitieMonedaProcentInsert(@procent) <> 0
	BEGIN
		SET @success = 1;
		PRINT 'Procent invalid'
		RETURN
	END

	EXEC crudInsertMaterialMoneda @material, @success OUTPUT
	IF @success<>0 BEGIN PRINT 'Failed' RETURN END

	EXEC crudInsertMateriePrimaMoneda @materie_prima, @success OUTPUT
	IF @success<>0 BEGIN PRINT 'Failed' RETURN END

	DECLARE @id_mm INT
	DECLARE @id_mp INT

	SELECT @id_mm = IdMaterialMoneda FROM MaterialMoneda WHERE Denumire = @material;
	SELECT @id_mp = IdMateriePrima FROM MateriePrimaMoneda WHERE Denumire = @materie_prima;

	IF EXISTS(SELECT * FROM CompozitieMoneda 
		WHERE IdMaterialMoneda = @id_mm AND IdMateriePrima = @id_mp)
	BEGIN
		PRINT 'Record already exists, try update'
		SET @success = 1
		RETURN
	END

	INSERT INTO CompozitieMoneda (IdMaterialMoneda, IdMateriePrima, ProcentCompozitie)
	VALUES (@id_mm, @id_mp, @procent);
END
GO

/*
DECLARE @success INT
EXEC crudInsertCompozitieMoneda 'Cupru', 'Cupru', 1, @success OUT

SELECT * FROM MateriePrimaMoneda

EXEC crudInsertExemplarMonede 5, 'Ban', 2005, 'Moneda', 'Cupru', 6, @success OUTPUT

EXEC crudInsertTipMonede 5, 'Ban', 2005, 'Moneda', 'Cupru', @success OUTPUT

SELECT * FROM TipMonede

DECLARE @success INT
EXEC crudInsertNotaNumismatica 6, 'Leu', @success OUTPUT*/