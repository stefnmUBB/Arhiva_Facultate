USE DepozitColectie;
GO

CREATE TABLE TransactionLogs (
	Id INT PRIMARY KEY IDENTITY,
	OperationType VARCHAR(100),
	TableName VARCHAR(100),
	ExecutionDate DATE
)
GO

CREATE OR ALTER PROCEDURE WriteLog @op VARCHAR(100), @table VARCHAR(100)
AS BEGIN
	INSERT INTO TransactionLogs (OperationType, TableName, ExecutionDate)
	VALUES(@op,@table, GETDATE());
END
GO

CREATE OR ALTER PROCEDURE InsertCompozitieMoneda
	@material VARCHAR(100), @materie_prima VARCHAR(100), 
	@procent INT
AS BEGIN	
	BEGIN TRAN
	BEGIN TRY
		IF dbo.validateCompozitieMonedaProcentInsert(@procent) <> 0		
			RAISERROR('Procent invalid', 14, 1)					

		-- Insert Material
		IF dbo.validateAlphaNum(@material)<>0		
			RAISERROR('Denumire invalida', 14, 1)					
	
		IF NOT EXISTS(SELECT * FROM MaterialMoneda WHERE Denumire = @material)				
		BEGIN
			INSERT INTO MaterialMoneda (Denumire) VALUES (@material)	
			EXEC WriteLog 'INSERT', 'MaterialMoneda'
		END

		-- Insert Materie Prima

		IF dbo.validateAlphaNum(@materie_prima)<>0		
			RAISERROR('Denumire invalida', 14, 1)					
	
		IF NOT EXISTS(SELECT * FROM MateriePrimaMoneda WHERE Denumire = @materie_prima)						
		BEGIN
			INSERT INTO MateriePrimaMoneda (Denumire) VALUES (@materie_prima)	
			EXEC WriteLog 'INSERT', 'MateriePrimaMoneda'
		END

		-- Insert compozitie

		DECLARE @id_mm INT
		DECLARE @id_mp INT

		SELECT @id_mm = IdMaterialMoneda FROM MaterialMoneda WHERE Denumire = @material;
		SELECT @id_mp = IdMateriePrima FROM MateriePrimaMoneda WHERE Denumire = @materie_prima;

		IF EXISTS(SELECT * FROM CompozitieMoneda WHERE IdMaterialMoneda = @id_mm AND IdMateriePrima = @id_mp)
		BEGIN
			RAISERROR('Record already exists, try update', 14, 1);			
		END
	
		INSERT INTO CompozitieMoneda (IdMaterialMoneda, IdMateriePrima, ProcentCompozitie)
			VALUES (@id_mm, @id_mp, @procent);
		EXEC WriteLog 'INSERT', 'CompozitieMoneda'
		COMMIT TRAN
		PRINT 'Success!'
	END TRY BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @err AS VARCHAR(100);
		SELECT @err=ERROR_MESSAGE();
		EXEC WriteLog @err, 'ERROR';
		PRINT 'Error.'		
	END CATCH
END
GO

------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE InsertCompozitieMonedaSaveflag
	@material VARCHAR(100), @materie_prima VARCHAR(100), 
	@procent INT
AS BEGIN	
	BEGIN TRAN		
	BEGIN TRY
		-- Insert Material
		IF dbo.validateAlphaNum(@material)<>0		
			RAISERROR('Denumire invalida', 14, 1)	
		IF NOT EXISTS(SELECT * FROM MaterialMoneda WHERE Denumire = @material)				
		BEGIN
			INSERT INTO MaterialMoneda (Denumire) VALUES (@material)	
			EXEC WriteLog 'INSERT', 'MaterialMoneda'
		END
		COMMIT TRAN
	END TRY BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @err AS VARCHAR(100);
		SELECT @err=ERROR_MESSAGE();
		EXEC WriteLog @err, 'ERROR';
		PRINT 'Error.'		
		RETURN
	END CATCH

	BEGIN TRAN	
	BEGIN TRY
		-- Insert Materie Prima

		IF dbo.validateAlphaNum(@materie_prima)<>0		
			RAISERROR('Denumire invalida', 14, 1)					
	
		IF NOT EXISTS(SELECT * FROM MateriePrimaMoneda WHERE Denumire = @materie_prima)						
		BEGIN
			INSERT INTO MateriePrimaMoneda (Denumire) VALUES (@materie_prima)	
			EXEC WriteLog 'INSERT', 'MateriePrimaMoneda'
		END
		COMMIT TRAN
	END TRY BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @err2 AS VARCHAR(100);
		SELECT @err2=ERROR_MESSAGE();
		EXEC WriteLog @err2, 'ERROR';
		PRINT 'Error.'		
		RETURN
	END CATCH

	BEGIN TRAN	
	BEGIN TRY
		-- Insert compozitie

		IF dbo.validateCompozitieMonedaProcentInsert(@procent) <> 0		
			RAISERROR('Procent invalid', 14, 1)							

		DECLARE @id_mm INT
		DECLARE @id_mp INT

		SELECT @id_mm = IdMaterialMoneda FROM MaterialMoneda WHERE Denumire = @material;
		SELECT @id_mp = IdMateriePrima FROM MateriePrimaMoneda WHERE Denumire = @materie_prima;

		IF EXISTS(SELECT * FROM CompozitieMoneda WHERE IdMaterialMoneda = @id_mm AND IdMateriePrima = @id_mp)
		BEGIN
			RAISERROR('Record already exists, try update', 14, 1);			
		END
	
		INSERT INTO CompozitieMoneda (IdMaterialMoneda, IdMateriePrima, ProcentCompozitie)
			VALUES (@id_mm, @id_mp, @procent);
		EXEC WriteLog 'INSERT', 'CompozitieMoneda'
		COMMIT TRAN		
	END TRY BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @err3 AS VARCHAR(100);
		SELECT @err3=ERROR_MESSAGE();
		EXEC WriteLog @err3, 'ERROR';
		PRINT 'Error.'		
	END CATCH
END
GO

------------------------------------------------------------------------------------------------

--BEGIN TRAN
--	INSERT INTO MateriePrimaMoneda (Denumire) VALUES ('123')	
--ROLLBACK TRAN

DELETE FROM CompozitieMoneda
DELETE FROM MaterialMoneda
DELETE FROM MateriePrimaMoneda

-- Inserts all
EXEC dbo.InsertCompozitieMoneda 'A', 'B', 10
SELECT * FROM TransactionLogs

-- Referenced already exists
EXEC dbo.InsertCompozitieMoneda 'A', 'C', 10
SELECT * FROM TransactionLogs

-- Denumire wrong
EXEC dbo.InsertCompozitieMoneda '!$####', 'C', 10
SELECT * FROM TransactionLogs

-- Procent wrong
EXEC dbo.InsertCompozitieMoneda 'X', 'C', 50000
SELECT * FROM TransactionLogs

-- Already exists
EXEC dbo.InsertCompozitieMoneda 'A', 'B', 30
SELECT * FROM TransactionLogs

------------------------------------------------------

DELETE FROM CompozitieMoneda
DELETE FROM MaterialMoneda
DELETE FROM MateriePrimaMoneda

-- Inserts all
EXEC dbo.InsertCompozitieMonedaSaveflag 'A', 'B', 10
SELECT * FROM TransactionLogs

-- Referenced already exists
EXEC dbo.InsertCompozitieMonedaSaveflag 'A', 'C', 10
SELECT * FROM TransactionLogs

-- Denumire 1 wrong
EXEC dbo.InsertCompozitieMonedaSaveflag '!$####', 'C', 10
SELECT * FROM TransactionLogs

-- Denumire 2 wrong
EXEC dbo.InsertCompozitieMonedaSaveflag 'C', '!$####', 10
SELECT * FROM TransactionLogs

-- Procent wrong
EXEC dbo.InsertCompozitieMonedaSaveflag 'X', 'D', 50000
SELECT * FROM TransactionLogs

-- Already exists
EXEC dbo.InsertCompozitieMonedaSaveflag 'A', 'B', 30
SELECT * FROM TransactionLogs

------------------------------------------------------