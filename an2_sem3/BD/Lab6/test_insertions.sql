CREATE OR ALTER PROCEDURE AddTables AS BEGIN
	INSERT INTO Tables (Name) VALUES
		('dbo.CompozitieMoneda'),      -- 1
		('dbo.MateriePrimaMoneda'),    -- 2
		('dbo.MaterialMoneda'),        -- 3
		('dbo.TipMonede'),             -- 4
		('dbo.ExemplarMonede')         -- 5	
END GO

SELECT * FROM Tables GO

CREATE OR ALTER PROCEDURE AddTests AS BEGIN
	INSERT INTO Tests (Name) VALUES ('Test1')  -- 1
	INSERT INTO Tests (Name) VALUES ('Test2')  -- 2
	INSERT INTO Tests (Name) VALUES ('Test3')  -- 3
END GO

SELECT * FROM Tests GO

CREATE OR ALTER PROCEDURE AddTestTables AS BEGIN
	DELETE FROM TestTables

	INSERT INTO TestTables (TestID, TableID, NoOfRows, Position) VALUES
		(1, 2, 50, 1),  -- Materie prima moneda
		(1, 3, 50, 1),  -- Material moneda
		(1, 1, 20, 2),  -- Compozitie moneda
		(1, 4, 20, 2),  -- Tip moneda
		(1, 5, 100, 3)  -- Exemplar

	INSERT INTO TestTables (TestID, TableID, NoOfRows, Position) VALUES
		(2, 2, 300, 1), -- Materie Prima Moneda		
		(2, 1, 60, 2)  -- Compozitie moneda

	INSERT INTO TestTables (TestID, TableID, NoOfRows, Position) VALUES
		(3, 1, 20, 1) -- Compozitie Moneda

	SELECT * FROM TestTables
	SELECT * FROM TestViews
	
END GO

CREATE OR ALTER PROCEDURE AddViews AS BEGIN
	INSERT INTO Views (Name) VALUES 
		('dbo.vw_Exemplare'),
		('dbo.vw_Compozitii'),
		('dbo.vw_Monede2000')
END GO

-- SELECT * FROM Views

CREATE OR ALTER PROCEDURE AddTestViews AS BEGIN
	INSERT INTO TestViews (TestID, ViewID) VALUES 
		(1, 1),
		(1, 2),

		(2, 3),

		(3, 1),
		(3, 2),
		(3, 3)
END GO

-- SELECT * FROM TestViews