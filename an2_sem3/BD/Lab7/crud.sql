USE DepozitColectie;
GO

CREATE OR ALTER PROCEDURE feedbackDeleteAll @table_name VARCHAR(100)
AS BEGIN
	PRINT 'Deleted all from ' + @table_name;
END 
GO

CREATE OR ALTER PROCEDURE feedbackDeleteRows @table_name VARCHAR(100), @rows INT
AS BEGIN
	PRINT 'Deleted ' + CAST(@rows AS VARCHAR(6)) + ' records from '+@table_name;
END 
GO

CREATE OR ALTER PROCEDURE crudDeleteCompozitieMonedaByDenumire 
	@material VARCHAR(100),
	@materie VARCHAR(100)
AS BEGIN
	DECLARE @mm_id INT = NULL
	DECLARE @mp_id INT = NULL
	IF @material IS NOT NULL
		SELECT TOP 1 @mm_id = IdMaterialMoneda FROM MaterialMoneda WHERE Denumire = @material

	IF @materie IS NOT NULL
		SELECT TOP 1 @mp_id = IdMateriePrima FROM MateriePrimaMoneda WHERE Denumire = @materie
	
	EXEC crudDeleteCompozitieMoneda @mm_id, @mp_id
END
GO

CREATE OR ALTER PROCEDURE crudDeleteCompozitieMoneda
	@idMaterialMoneda INT,
	@idMateriePrima INT
AS
BEGIN
	IF @idMaterialMoneda IS NULL
	BEGIN
		IF @idMateriePrima IS NULL
		BEGIN
			DELETE FROM CompozitieMoneda
			EXEC feedbackDeleteAll 'CompozitieMoneda'			
		END
		ELSE
		BEGIN
			DELETE FROM CompozitieMoneda WHERE IdMateriePrima = @idMateriePrima;
			EXEC feedbackDeleteRows 'CompozitieMoneda', @@ROWCOUNT			
		END
		RETURN
	END
	ELSE BEGIN
		IF @idMateriePrima IS NULL
		BEGIN			
			DELETE FROM CompozitieMoneda WHERE IdMaterialMoneda = @idMaterialMoneda;
			EXEC feedbackDeleteRows 'CompozitieMoneda', @@ROWCOUNT			
		END
		ELSE BEGIN
			DELETE FROM CompozitieMoneda WHERE 
				IdMaterialMoneda = @idMaterialMoneda AND
				IdMateriePrima = @idMateriePrima;
			EXEC feedbackDeleteRows 'CompozitieMoneda', @@ROWCOUNT			
		END
	END
END 
GO

CREATE TYPE IdTableType AS TABLE (id INT);
GO

CREATE TYPE IdTableType2 AS TABLE (id1 INT, id2 INT);
GO

CREATE OR ALTER PROCEDURE crudDeleteForeignKeys 	
	@keys_table IdTableType READONLY,
	@ref_table_name VARCHAR(100)
AS BEGIN
	IF NOT EXISTS(SELECT * FROM @keys_table)
		RETURN
	DECLARE crs_id CURSOR LOCAL FOR SELECT id FROM @keys_table;
	OPEN crs_id

	DECLARE @id INT;
	FETCH NEXT FROM crs_id INTO @id;	

	WHILE(@@FETCH_STATUS = 0)
	BEGIN						
		DECLARE @p NVARCHAR(10);
		SET @p = 'NULL';
		IF @id IS NOT NULL SET @p = CAST(@id AS NVARCHAR(10))

		DECLARE @query NVARCHAR(200) = 'EXEC crudDelete'+@ref_table_name+' '
			+ @p;			
		EXEC sp_executesql @query;
		IF @id IS NULL RETURN;	
		FETCH NEXT FROM crs_id INTO @id;				
	END
	CLOSE crs_id;
	DEALLOCATE crs_id
END 
GO

CREATE OR ALTER PROCEDURE crudDeleteForeignKeys2	
	@keys_table IdTableType2 READONLY,
	@ref_table_name VARCHAR(100)
AS BEGIN
	IF NOT EXISTS(SELECT * FROM @keys_table)
		RETURN
	DECLARE crs_id CURSOR LOCAL FOR SELECT id1, id2 FROM @keys_table;
	OPEN crs_id

	DECLARE @id1 INT;
	DECLARE @id2 INT;
	FETCH NEXT FROM crs_id INTO @id1, @id2;	

	WHILE(@@FETCH_STATUS = 0)
	BEGIN												
		DECLARE @query NVARCHAR(200);

		DECLARE @p1 NVARCHAR(10)
		SET @p1 = 'NULL';
		IF @id1 IS NOT NULL SET @p1 = CAST(@id1 AS NVARCHAR(10))

		DECLARE @p2 NVARCHAR(10)
		SET @p2 = 'NULL';
		IF @id2 IS NOT NULL SET @p2 = CAST(@id2 AS NVARCHAR(10))

		SET @query = 'EXEC crudDelete'+@ref_table_name+' '
			+ @p1 + ', ' + @p2;
		PRINT @query
		EXEC sp_executesql @query;
		IF @id1 IS NULL AND @id2 IS NULL
			RETURN;
		FETCH NEXT FROM crs_id INTO @id1, @id2;				
	END
	CLOSE crs_id;
	DEALLOCATE crs_id
END 
GO


SELECT * FROM CompozitieMoneda;
SELECT * FROM MateriePrimaMoneda WHERE IdMateriePrima = 19600;

SELECT * FROM CompozitieMoneda WHERE IdMaterialMoneda = 4821

DECLARE @keys_table IdTableType2;
INSERT INTO @keys_table VALUES (4821, NULL);
EXEC crudDeleteForeignKeys2 @keys_table, 'CompozitieMoneda';

SELECT * FROM CompozitieMoneda;

GO
CREATE OR ALTER PROCEDURE crudDeleteMaterialMoneda 
	@id INT, @denumire VARCHAR(100) = NULL
AS BEGIN
	IF @id IS NULL
	BEGIN
		IF @denumire IS NULL
		BEGIN
			-- delete all occurences of MaterialMoneda in Compozitie			
			DECLARE @all_keys_table IdTableType2;
			INSERT INTO @all_keys_table VALUES (NULL, NULL);				
			EXEC crudDeleteForeignKeys2 @all_keys_table, 'CompozitieMoneda';

			-- also delete from TipMonede
			DECLARE @all_keys_tip_moneda IdTableType;
			INSERT INTO @all_keys_tip_moneda VALUES (NULL);
			EXEC crudDeleteForeignKeys @all_keys_tip_moneda, 'TipMonede';

			DELETE FROM MaterialMoneda;
			EXEC feedbackDeleteAll 'MaterialMoneda'
		END ELSE
		BEGIN						
			-- delete all occurences of MaterialMoneda in Compozitie			
			DECLARE @mm_keys_table IdTableType2;
			INSERT INTO @mm_keys_table
				SELECT IdMaterialMoneda AS id1, NULL AS id2 FROM MaterialMoneda
				WHERE Denumire = @denumire;
			EXEC crudDeleteForeignKeys2 @mm_keys_table, 'CompozitieMoneda';

			-- also delete from TipMonede
			DECLARE @keys_tip_moneda IdTableType;
			INSERT INTO @keys_tip_moneda 
				SELECT id = IdTipMoneda FROM TipMonede TM
					INNER JOIN MaterialMoneda MM ON TM.IdMaterialMoneda = MM.IdMaterialMoneda
					WHERE Denumire = @denumire;			
			EXEC crudDeleteForeignKeys @keys_tip_moneda, 'TipMonede';
												
			DELETE FROM MaterialMoneda WHERE Denumire = @denumire;
			EXEC feedbackDeleteRows 'MaterialMoneda', @@ROWCOUNT
			
		END
	END
	ELSE -- IF @id IS NOT NULL
	BEGIN
		IF @denumire IS NOT NULL 
		BEGIN
			IF NOT EXISTS (SELECT * FROM MaterialMoneda WHERE
				IdMaterialMoneda = @id AND Denumire = @denumire)
			BEGIN
				PRINT 'No such records';
				RETURN
			END
		END				
		-- delete foreign key occurences of MaterialMoneda in Compozitie			
		DECLARE @mm_keys_table2 IdTableType2;
		INSERT INTO @mm_keys_table2 VALUES (@id, NULL);
		EXEC crudDeleteForeignKeys2 @mm_keys_table2, 'CompozitieMoneda';

		-- also delete from TipMonede
		DECLARE @keys_tip_moneda2 IdTableType;
		INSERT INTO @keys_tip_moneda2 
			SELECT IdTipMoneda FROM TipMonede WHERE IdMaterialMoneda = @id;;
		EXEC crudDeleteForeignKeys @keys_tip_moneda2, 'TipMonede';

		DELETE FROM MaterialMoneda WHERE IdMaterialMoneda = @id;				
		EXEC feedbackDeleteRows 'MaterialMoneda', @@ROWCOUNT		
	END
END 
GO

CREATE OR ALTER PROCEDURE crudDeleteTipMonede @id INT
AS BEGIN
	IF @id IS NULL 
	BEGIN
		DECLARE @keys_table IdTableType;
		INSERT INTO @keys_table VALUES (NULL);
		EXEC crudDeleteForeignKeys @keys_table, 'ExemplarMonede';
		
		DELETE FROM TipMonede;
		EXEC feedbackDeleteRows 'TipMonede', @@ROWCOUNT		
	END
	ELSE -- IF @id IS NOT NULL
	BEGIN		
		DECLARE @keys_table2 IdTableType;
		INSERT INTO @keys_table2
			SELECT id = IdExemplarMoneda FROM ExemplarMonede WHERE IdTipMoneda = @id;	
		SELECT * FROM @keys_table2
		EXEC crudDeleteForeignKeys @keys_table2, 'ExemplarMonede';
		
		DELETE FROM TipMonede WHERE IdTipMoneda = @id;
		EXEC feedbackDeleteRows 'TipMonede', @@ROWCOUNT		
	END	
END
GO

CREATE OR ALTER PROCEDURE crudDeleteExemplarMonede @id INT
AS BEGIN
	IF @id IS NULL 
	BEGIN				
		DELETE FROM ExemplarMonede;
		EXEC feedbackDeleteRows 'ExemplarMonede', @@ROWCOUNT
	END
	ELSE -- IF @id IS NOT NULL
	BEGIN				
		DELETE FROM ExemplarMonede WHERE IdExemplarMoneda = @id;
		EXEC feedbackDeleteRows 'ExemplarMonede', @@ROWCOUNT		
	END
END
GO

CREATE OR ALTER PROCEDURE crudDeleteMateriePrimaMoneda
	@id INT, @denumire VARCHAR(100) = NULL
AS BEGIN
	IF @id IS NULL
	BEGIN
		IF @denumire IS NULL
		BEGIN
			-- delete all occurences of MateriePrimaMoneda in Compozitie			
			DECLARE @all_keys_table IdTableType2;
			INSERT INTO @all_keys_table VALUES (NULL, NULL);				
			EXEC crudDeleteForeignKeys2 @all_keys_table, 'CompozitieMoneda';			

			DELETE FROM MateriePrimaMoneda;
			EXEC feedbackDeleteAll 'MateriePrimaMoneda'
		END ELSE
		BEGIN			
			-- delete all occurences of MateriePrimaMoneda in Compozitie			
			DECLARE @mp_keys_table IdTableType2;
			INSERT INTO @mp_keys_table
				SELECT NULL AS id2, IdMateriePrima AS id2 FROM MateriePrimaMoneda
				WHERE Denumire = @denumire;
			EXEC crudDeleteForeignKeys2 @mp_keys_table, 'CompozitieMoneda';			
												
			DELETE FROM MateriePrimaMoneda WHERE Denumire = @denumire;
			EXEC feedbackDeleteRows 'MateriePrimaMoneda', @@ROWCOUNT
			
		END
	END
	ELSE -- IF @id IS NOT NULL
	BEGIN
		IF @denumire IS NOT NULL 
		BEGIN
			IF NOT EXISTS (SELECT * FROM MaterialMoneda WHERE
				IdMaterialMoneda = @id AND Denumire = @denumire)
			BEGIN
				PRINT 'No such records';
				RETURN
			END
		END				

		-- delete all occurences of MateriePrimaMoneda in Compozitie			
		DECLARE @mp_keys_table2 IdTableType2;
		INSERT INTO @mp_keys_table2 VALUES (NULL, @id);				
		EXEC crudDeleteForeignKeys2 @mp_keys_table2, 'CompozitieMoneda';					

		DELETE FROM MateriePrimaMoneda WHERE IdMateriePrima = @id;				
		EXEC feedbackDeleteRows 'MateriePrimaMoneda', @@ROWCOUNT		
	END
END 
GO


/*EXEC crudDeleteMaterialMoneda 4805;

SELECT * FROM MateriePrimaMoneda WHERE IdMateriePrima = 19563

EXEC crudDeleteMateriePrimaMoneda 19563

SELECT * FROM TipMonede WHERE IdMaterialMoneda = 4805;

SELECT * FROM MaterialMoneda
SELECT * FROM CompozitieMoneda;*/