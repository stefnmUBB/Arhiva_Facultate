-- view for random elements access
CREATE VIEW vw_rand AS SELECT RAND() AS Value;
GO

-- returns a random int up to @n
CREATE OR ALTER FUNCTION GetRandInt(@n INT) 
RETURNS INT
AS
BEGIN
	DECLARE @result INT;
	SET @result = FLOOR((SELECT Value FROM vw_rand)*@n);	
	RETURN @result;
END
GO

-- get random string
CREATE OR ALTER FUNCTION GetRandText()
RETURNS VARCHAR(9)
AS
BEGIN
	DECLARE @len INT; -- 3..5 caractere
	SELECT @len = dbo.GetRandInt(3);
	SET @len = @len + 3;

	DECLARE @ascii INT;

	DECLARE @result VARCHAR(9) = ''
	DECLARE @k INT = 0;
	WHILE @k < @len 
	BEGIN
		SET @k = @k+1
		SELECT @ascii = dbo.GetRandInt(26);
		SET @ascii = @ascii + 65;
		SET @result = @result + CHAR(@ascii);
	END
	RETURN @result
END
GO

-- get random entry from a table
CREATE OR ALTER PROCEDURE GetRandId 
	@table_name VARCHAR(100),
	@r_id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	-- get rows count
	CREATE TABLE #cnt (Value INT);
	DECLARE @items_count INT;
	DECLARE @query VARCHAR(1000) = 'INSERT INTO #cnt SELECT COUNT(*) FROM ' + @table_name;
	--PRINT @query;
	EXEC sp_sqlexec @query;
	SELECT TOP 1 @items_count = Value FROM #cnt
	DELETE FROM #cnt;

	--PRINT @items_count

	-- get primary key name	
	DECLARE @pk_name VARCHAR(100);
	SELECT @pk_name = dbo.GetPrimaryKey(@table_name);

	--PRINT @pk_name

	-- get a random number from 1..items_count
	DECLARE @pos INT
	SELECT @pos = dbo.GetRandInt(@items_count);
	--PRINT @pos

	-- SELECT * FROM dbo.TipConsola ORDER BY IdTipConsola ASC OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY

	SET @query = 'INSERT INTO #cnt SELECT ' + @pk_name + ' FROM ' + @table_name 
		+ ' ORDER BY ' + @pk_name + ' ASC OFFSET ' + STR(@pos) + ' ROWS FETCH FIRST 1 ROW ONLY'
	--PRINT @query

	EXEC sp_sqlexec @query;
	SELECT TOP 1 @r_id = Value FROM #cnt
	DELETE FROM #cnt;	
	SET NOCOUNT OFF
END

GO

DECLARE @r_id INT;
EXEC GetRandId 'dbo.ExemplarMonede', @r_id OUTPUT;
PRINT @r_id


