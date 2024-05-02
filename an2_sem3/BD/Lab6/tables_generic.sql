CREATE OR ALTER FUNCTION GetPrimaryKey (@table_name VARCHAR(100))
RETURNS VARCHAR(100)
AS
BEGIN	
	DECLARE @result VARCHAR(100)
	SELECT TOP 1 @result = name FROM sys.columns WHERE object_id = OBJECT_ID(@table_name) AND is_identity = 1
	RETURN @result
END
GO

-- gets foreign keys list
CREATE OR ALTER FUNCTION GetForeignKeys (@table_name VARCHAR(100))
RETURNS TABLE 
AS RETURN SELECT PC.name AS TableColumn, RT.name AS ReferencedTable, RC.name AS ReferencedColumn FROM 
	sys.foreign_key_columns FK, 
	sys.tables PT, sys.columns PC,
	sys.tables RT, sys.columns RC
	WHERE FK.parent_object_id = OBJECT_ID(@table_name)
	AND PT.object_id = FK.parent_object_id
	AND PC.object_id = FK.parent_object_id AND PC.column_id = FK.parent_column_id
	AND RT.object_id = FK.referenced_object_id
	AND RC.object_id = FK.referenced_object_id AND RC.column_id = FK.referenced_column_id
GO

-- gets INT columns
CREATE OR ALTER FUNCTION GetNumericColumns (@table_name VARCHAR(100))
RETURNS TABLE
AS RETURN SELECT name AS ColumnName FROM sys.columns
	WHERE object_id = OBJECT_ID(@table_name) 
		-- int
		AND system_type_id = 56 
		-- not FK
		AND NOT EXISTS (SELECT * FROM GetForeignKeys(@table_name) FK WHERE FK.TableColumn = name)
		-- not PK		
		AND is_identity = 0
GO

-- gets VARCHAR(...) columns
CREATE OR ALTER FUNCTION GetTextColumns (@table_name VARCHAR(100))
RETURNS TABLE
AS RETURN SELECT name AS ColumnName FROM sys.columns
	WHERE object_id = OBJECT_ID(@table_name) 
	-- varchar
	AND system_type_id = 167
GO


CREATE OR ALTER PROCEDURE ColumnAsList 
	@table_name VARCHAR(100), @column_name VARCHAR(100),
	@quote INT,
	@list VARCHAR(8000) OUTPUT
AS
BEGIN
	SET @list = '';

	DECLARE @k INT;
	SET @k = 0;

	DECLARE @record VARCHAR(8000);

	CREATE TABLE #tmp (tmp_col VARCHAR(8000));	

	DECLARE @query NVARCHAR(4000);
	SET @query = 'INSERT INTO #tmp(tmp_col) SELECT '+@column_name+' FROM '+@table_name;		
	EXEC sp_executesql @query;	
	
	DECLARE col_crs CURSOR FOR SELECT tmp_col FROM #tmp;			
	OPEN col_crs;

	FETCH NEXT FROM col_crs INTO @record;

	WHILE @@FETCH_STATUS = 0  
	BEGIN
		IF @k > 0 BEGIN SET @list = @list + ', '; END;
		SET @k = @k + 1;
		IF @quote <> 0
			SET @list = @list + '''' + @record + '''' ;	
		ELSE 
			SET @list = @list + @record;	
		--PRINT @record;
		FETCH NEXT FROM col_crs INTO @record;
	END	
END
GO

CREATE TYPE SingleColTable AS 
	TABLE (col VARCHAR(8000));
GO

CREATE OR ALTER PROCEDURE ColumnAsListT
	@table SingleColTable READONLY,
	@quote INT,
	@list VARCHAR(8000) OUTPUT
AS
BEGIN
	SET @list = '';

	DECLARE @k INT;
	SET @k = 0;

	DECLARE @record VARCHAR(8000);
	
	DECLARE col_crs CURSOR FOR SELECT col FROM @table;			
	OPEN col_crs;

	FETCH NEXT FROM col_crs INTO @record;

	WHILE @@FETCH_STATUS = 0  
	BEGIN
		IF @k > 0 BEGIN SET @list = @list + ', '; END;
		SET @k = @k + 1;
		IF @quote <> 0
			SET @list = @list + '''' + @record + '''' ;	
		ELSE 
			SET @list = @list + @record;	
		--PRINT @record;
		FETCH NEXT FROM col_crs INTO @record;
	END	
END
GO

DECLARE @tmp VARCHAR(8000)
--EXEC ColumnAsList 'dbo.TipConsola', 'IdTipConsola', @quote = 0, @list=@tmp OUTPUT;


DECLARE @table SingleColTable;
INSERT INTO @table SELECT TableColumn FROM GetForeignKeys('dbo.MateriePrimaMoneda')
EXEC ColumnAsListT @table, @quote = 0, @list=@tmp OUTPUT;
PRINT @tmp
GO

-----------------------------------------------------------------------------------------------------------------

-- generic table delete
CREATE OR ALTER PROCEDURE DeleteFromTable @table_name VARCHAR(100)
AS
BEGIN
	DECLARE @query AS NVARCHAR(4000);
	SET @query = 'DELETE FROM ' + @table_name;
	EXEC sp_executesql @query;
END
GO

-- create row string ready to be plugged into INSERT
CREATE OR ALTER PROCEDURE GenerateSingleRow 
	@table_name VARCHAR(100),
	@vals VARCHAR(1000) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @k INT = 0;
	SET @vals = '';

	-- solve fk
	CREATE TABLE #fktmp (ReferencedTable VARCHAR(100), ReferencedColumn VARCHAR(100));

	DECLARE @query VARCHAR(200) =
		'INSERT INTO #fktmp SELECT ReferencedTable, ReferencedColumn FROM dbo.GetForeignKeys(''' + @table_name + ''')';
	--PRINT @query;
	EXEC sp_sqlexec @query

	DECLARE @rf_table VARCHAR(100);
	DECLARE @rf_col VARCHAR(100);

	DECLARE fk_crs CURSOR FOR SELECT * FROM #fktmp;
	OPEN fk_crs;	
	FETCH NEXT FROM fk_crs INTO @rf_table, @rf_col;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		if @k>0 SET @vals = @vals + ',';
		SET @k = @k+1;		

		-- for each fk choose a valid id from its corresponding table
		DECLARE @id INT
		EXEC dbo.GetRandId @rf_table, @id OUTPUT;		

		SET @vals = CONCAT(@vals, @id);

		FETCH NEXT FROM fk_crs INTO @rf_table, @rf_col;
	END

	CLOSE fk_crs;

	DROP TABLE #fktmp;

	CREATE TABLE #itmp (ColName VARCHAR(100));
	SET @query = 'INSERT INTO #itmp (ColName) SELECT ColumnName FROM GetNumericColumns(''' + @table_name + ''')';
	EXEC sp_sqlexec @query
	

	DECLARE icrs CURSOR FOR SELECT * FROM #itmp;
	OPEN icrs

	FETCH NEXT FROM icrs INTO @rf_col;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		if @k>0 SET @vals = @vals + ',';
		SET @k = @k+1;		
		-- for each int field choose a random value
		DECLARE @v INT
		SELECT @v = dbo.GetRandInt(10);	
		SET @v = @v+1
		SET @vals = CONCAT(@vals, @v);
		FETCH NEXT FROM icrs INTO @rf_col;
	END

	CLOSE icrs

	DELETE FROM #itmp;

	SET @query = 'INSERT INTO #itmp (ColName) SELECT ColumnName FROM GetTextColumns(''' + @table_name + ''')';
	EXEC sp_sqlexec @query
	

	DECLARE tcrs CURSOR FOR SELECT * FROM #itmp;
	OPEN tcrs

	FETCH NEXT FROM tcrs INTO @rf_col;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		if @k>0 SET @vals = @vals + ',';
		SET @k = @k+1;		
		-- for each text field choose a random value
		DECLARE @t VARCHAR(10)
		SELECT @t = dbo.GetRandText();			
		SET @vals = CONCAT(@vals,'''', @t,'''');
		FETCH NEXT FROM tcrs INTO @rf_col;
	END

	CLOSE tcrs	

	SET NOCOUNT OFF
END
GO

CREATE OR ALTER PROCEDURE AddToTable @table_name VARCHAR(100), @count INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @query AS VARCHAR(8000);
	SET @query = 'INSERT INTO ' + @table_name+' ';

	DECLARE @cols_list VARCHAR(8000); SET @cols_list = '';
	DECLARE @col_names VARCHAR(8000);
	DECLARE @table SingleColTable;

	-- get foreign keys columns
	INSERT INTO @table SELECT TableColumn FROM GetForeignKeys(@table_name)
	EXEC ColumnAsListT @table, @quote = 0, @list=@col_names OUTPUT;	
	DELETE FROM @table;
	SET @cols_list = @cols_list + @col_names;	

	-- get int columns
	INSERT INTO @table SELECT ColumnName FROM GetNumericColumns(@table_name)
	EXEC ColumnAsListT @table, @quote = 0, @list=@col_names OUTPUT;	
	DELETE FROM @table;
	IF @col_names <> '' AND @cols_list<>'' SET @cols_list = @cols_list + ', ';
	SET @cols_list = @cols_list + @col_names;	

	-- get varchar columns
	INSERT INTO @table SELECT ColumnName FROM GetTextColumns(@table_name)
	EXEC ColumnAsListT @table, @quote = 0, @list=@col_names OUTPUT;	
	DELETE FROM @table;
	IF @col_names <> '' AND @cols_list<>'' SET @cols_list = @cols_list + ', ';
	SET @cols_list = @cols_list + @col_names;	

	SET @query = @query + '(' + @cols_list + ') VALUES '

	DECLARE @k INT = 0;

	WHILE @k<@count 
	BEGIN
		IF @k>0 SET @query = @query + ',';
		SET @k = @k+1;

		DECLARE @row VARCHAR(100)
		EXEC dbo.GenerateSingleRow @table_name, @row OUTPUT
		SET @query = CONCAT(@query, '(',@row,')');
	END
	
	PRINT @query
	SET NOCOUNT OFF;
	EXEC sp_sqlexec @query; -- ufff
END
GO
