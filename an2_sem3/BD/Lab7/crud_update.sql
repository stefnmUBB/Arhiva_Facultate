CREATE OR ALTER PROCEDURE crudUpdateNotaNumismatica	
	@id INT,
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;	
	IF NOT EXISTS(SELECT * FROM NotaNumismatica WHERE IdNotaNumismatica = @id)
	BEGIN
		PRINT 'Id invalid';
		SET @success = 1;		
		RETURN
	END

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
	
	UPDATE NotaNumismatica SET Valoare = @valoare, Valuta = @valuta WHERE IdNotaNumismatica = @id;	
END
GO

CREATE OR ALTER PROCEDURE crudUpdateMaterialMoneda 
	@id INT,
	@denumire VARCHAR(20),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;		

	IF NOT EXISTS(SELECT * FROM MaterialMoneda WHERE IdMaterialMoneda = @id)
	BEGIN
		PRINT 'Id invalid';
		SET @success = 1;		
		RETURN
	END

	IF dbo.validateOnlyLetters(@denumire)<>0
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

	UPDATE MaterialMoneda  SET Denumire = @denumire 
		WHERE IdMaterialMoneda = @id;
END
GO

CREATE OR ALTER PROCEDURE crudUpdateTipMonede 
	@id INT,
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@an INT,
	@orientare VARCHAR(30) = NULL,
	@material VARCHAR(100) = NULL,
	@success INT OUTPUT
AS BEGIN		
	SET @success = 0

	IF NOT EXISTS(SELECT * FROM TipMonede WHERE IdTipMoneda = @id)
	BEGIN
		PRINT 'Id invalid';
		SET @success = 1;	
		RETURN
	END

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

	UPDATE TipMonede SET
		IdNotaNumismatica = @id_nota_num,
		Orientare = @orientare,
		IdMaterialMoneda = @id_material,
		An = @an
		WHERE IdTipMoneda = @id;
END
GO

CREATE OR ALTER PROCEDURE crudUpdateExemplarMonede
	@id INT,
	@valoare FLOAT,
	@valuta VARCHAR(30),
	@an INT,
	@orientare VARCHAR(30) = NULL,
	@material VARCHAR(100) = NULL,
	@stare_conservare INT,
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;

	IF NOT EXISTS(SELECT * FROM ExemplarMonede WHERE IdExemplarMoneda = @id)
	BEGIN
		PRINT 'Id invalid';
		SET @success = 1;	
		RETURN
	END

	IF dbo.validateExemplarMonedeStareConservare(@stare_conservare)<>0
	BEGIN
		SET @success = 1
		PRINT 'Stare conservare invalida' RETURN
	END	
	EXEC crudInsertTipMonede @valoare, @valuta, @an, @orientare, @material, @success OUTPUT;
	IF @success <> 0 BEGIN PRINT 'Failed' RETURN END

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

	UPDATE ExemplarMonede SET
		IdTipMoneda = @tip_m_id ,
		StareConservare = @stare_conservare
		WHERE IdExemplarMoneda = @id;	
END
GO

CREATE OR ALTER PROCEDURE crudUpdateMateriePrimaMoneda
	@id INT,
	@denumire VARCHAR(100),
	@success INT OUTPUT
AS BEGIN
	SET @success = 0;	
	
	IF NOT EXISTS(SELECT * FROM MateriePrimaMoneda WHERE IdMateriePrima = @id)
	BEGIN
		PRINT 'Id invalid';
		SET @success = 1;	
		RETURN
	END


	IF dbo.validateOnlyLetters(@denumire)<>0
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

	UPDATE MateriePrimaMoneda SET Denumire = @denumire 
		WHERE IdMateriePrima = @id;
END
GO

CREATE OR ALTER PROCEDURE crudUpdateCompozitieMoneda
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

	DECLARE @id_mm INT
	DECLARE @id_mp INT

	SELECT @id_mm = IdMaterialMoneda FROM MaterialMoneda WHERE Denumire = @material;
	SELECT @id_mp = IdMateriePrima FROM MateriePrimaMoneda WHERE Denumire = @materie_prima;

	IF @id_mm = NULL 
	BEGIN
		SET @success = 1
		PRINT 'MaterialMoneda does not exist';
		RETURN
	END

	IF @id_mm = NULL 
	BEGIN
		SET @success = 1
		PRINT 'MateriePrimaMoneda does not exist';
		RETURN
	END


	IF NOT EXISTS(SELECT * FROM CompozitieMoneda 
		WHERE IdMaterialMoneda = @id_mm AND IdMateriePrima = @id_mp)
	BEGIN
		PRINT '(material, materie prima) does not exist'
		SET @success = 1
		RETURN
	END

	UPDATE CompozitieMoneda Set ProcentCompozitie = @procent
	WHERE IdMaterialMoneda = @id_mm
	AND IdMateriePrima = @id_mp;	
END
GO

SELECT * FROM crudSelectCompozitieMoneda()

DECLARE @success INT
EXEC crudUpdateCompozitieMoneda 'HMPLI', 'IDC', 15, @success OUTPUT