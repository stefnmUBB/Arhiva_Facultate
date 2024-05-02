CREATE OR ALTER PROCEDURE ExecuteIndividualTestTable @tid INT 
AS
BEGIN		
	SET NOCOUNT ON
	DECLARE @test_name VARCHAR(100)
	SELECT @test_name = Name FROM Tests WHERE TestId = @tid;
	PRINT 'Executing test : ' + @test_name;	

	INSERT INTO TestRuns VALUES (@test_name, NULL, NULL)
	DECLARE @testrun_id INT
	SELECT @testrun_id = @@IDENTITY;  -- save it to complete later with relevant data

	DECLARE @ds DATETIME --start timetest
	DECLARE @di DATETIME --intermediate timetest
	DECLARE @de DATETIME --end timetest

	DECLARE @table VARCHAR(100)
	DECLARE @norows INT	

	-- choose highest-priority table id as TestRunTables representative table
	DECLARE @table_id INT
	SELECT TOP 1 @table_id = TableID FROM TestTables 
		WHERE TestID = @tid
		AND Position IN (SELECT MAX(Position) FROM TestTables WHERE TestID = @tid)
	

	SET @ds = GETDATE()		

	-- removals
	DECLARE crs CURSOR FOR
		SELECT Name, NoOfRows FROM TestTables TT INNER JOIN Tables T On TT.TableID = T.TableID  WHERE TT.TestID = @tid
		ORDER BY Position DESC
	OPEN crs
	
	FETCH NEXT FROM crs INTO @table, @norows;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT 'DELETE FROM ' + @table;
		
		EXEC DeleteFromTable @table			

		FETCH NEXT FROM crs INTO @table, @norows;
	END

	CLOSE crs
	DEALLOCATE crs

	-- additions

	DECLARE crs2 CURSOR FOR 
		SELECT Name, NoOfRows FROM TestTables TT INNER JOIN Tables T On TT.TableID = T.TableID  WHERE TT.TestID = @tid
		ORDER BY Position ASC
	OPEN crs2
	
	FETCH NEXT FROM crs2 INTO @table, @norows;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT 'INSERT INTO ' + @table;
		
		EXEC AddToTable @table, @norows		

		FETCH NEXT FROM crs2 INTO @table, @norows;
	END

	CLOSE crs2
	DEALLOCATE crs2

	SET @di = GETDATE()

	PRINT 'Executing Views'
	DECLARE @vw_name VARCHAR(100)	

	-- views
	DECLARE crs3 CURSOR FOR 
		SELECT Name FROM TestViews TV INNER JOIN Views V On TV.ViewID = V.ViewID  WHERE TV.TestID = @tid		
	OPEN crs3

	FETCH NEXT FROM crs3 INTO @vw_name;
	DECLARE @view_id INT

	DECLARE @vds DATETIME
	DECLARE @vde DATETIME

	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT @vw_name

		DECLARE @q VARCHAR(200) = 'SELECT * FROM '
		SET @q = @q + @vw_name;

		SET @vds = GETDATE()
		EXEC sp_sqlexec @q
		SET @vde = GETDATE()

		SELECT TOP 1 @view_id = ViewID FROM Views WHERE Name = @vw_name;
		INSERT INTO TestRunViews VALUES (@testrun_id, @view_id, @vds, @vde)
	
		FETCH NEXT FROM crs3 INTO @vw_name;	
	END

	CLOSE crs3
	DEALLOCATE crs3

	SET @de = GETDATE()

	INSERT INTO TestRunTables VALUES (@testrun_id, @table_id, @ds, @di)

	UPDATE TestRuns SET StartAt = @ds, EndAt = @de WHERE TestRunID = @testrun_id; 

	CREATE TABLE #results (Description VARCHAR(50), Value VARCHAR(50));
	INSERT INTO #results VALUES ('Tests executed', NULL)
	INSERT INTO #results VALUES ('Delete/Insert', CONVERT(VARCHAR(10), DATEDIFF(ms, @ds,@di)) + 'ms')
	INSERT INTO #results VALUES ('Views', CONVERT(VARCHAR(10), DATEDIFF(ms, @di,@de)) + 'ms')
	INSERT INTO #results VALUES ('Total', CONVERT(VARCHAR(10), DATEDIFF(ms, @ds,@de)) + 'ms')
	SELECT * FROM #results

	SET NOCOUNT OFF
END
GO

EXEC ExecuteIndividualTestTable 2

--DELETE FROM TestRunTables
--DELETE FROM TestRunViews

SELECT * FROM TestRunTables
SELECT * FROM TestRunViews

GO

CREATE OR ALTER PROCEDURE ExecuteAllTests AS BEGIN
	DECLARE acrs CURSOR FOR SELECT TestID FROM Tests
	OPEN acrs

	DECLARE @test_id INT

	FETCH NEXT FROM acrs INTO @test_id

	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT @test_id
		EXEC ExecuteIndividualTestTable @test_id
		FETCH NEXT FROM acrs INTO @test_id
	END

	CLOSE acrs
	DEALLOCATE acrs
END 
GO

DELETE FROM TestRunTables
DELETE FROM TestRunViews
DELETE FROM TestRuns

EXEC ExecuteAllTests

--EXEC ExecuteIndividualTestTable 1
--EXEC ExecuteIndividualTestTable 2
--EXEC ExecuteIndividualTestTable 3

SELECT * FROM TestTables
SELECT * FROM TestViews
SELECT * FROM Tests

SELECT * FROM TestRuns ORDER BY TestRunID DESC
SELECT TOP 10 * FROM TestRunTables ORDER BY TestRunID DESC
SELECT TOP 10 * FROM TestRunViews ORDER BY TestRunID DESC

SELECT * FROM TestTables

SELECT * FROM TestViews
