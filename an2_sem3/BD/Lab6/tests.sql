

--SELECT * FROM dbo.GetNumericColumns('dbo.ExemplarConsole')

DECLARE @r VARCHAR(100)
EXEC dbo.GenerateSingleRow 'dbo.TipConsola', @r OUTPUT
PRINT @r
GO

--SELECT * FROM TipConsola


EXEC DeleteFromTable 'dbo.SpatiuDepozitare';
EXEC AddToTable 'dbo.SpatiuDepozitare', 100;
SELECT * FROM dbo.SpatiuDepozitare

--SELECT * FROM GetTextColumns('dbo.TipConsola');

EXEC AddToTable 'dbo.SpatiuDepozitare', 100;
GO

SELECT dbo.ColumnAsList('dbo.TipConsola','Nume') AS X;
GO

