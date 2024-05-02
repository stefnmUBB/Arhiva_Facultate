CREATE OR ALTER PROCEDURE crudCompozitieMoneda
AS BEGIN
	DECLARE @success INT
	
	EXEC crudInsertCompozitieMoneda 'Material1', 'MP1', 60, @success	
	EXEC crudInsertCompozitieMoneda 'Material1', 'MP2', 40, @success	
	EXEC crudInsertCompozitieMoneda 'Material2', 'MP2', 50, @success	
	EXEC crudInsertCompozitieMoneda 'Material2', 'MP3', 30, @success	
	EXEC crudInsertCompozitieMoneda 'Material2', 'MP1', 20, @success	
	
	-- already exists
	EXEC crudInsertCompozitieMoneda 'Material2', 'MP1', 20, @success 	
	-- error
	EXEC crudInsertCompozitieMoneda '$#%#', '$TE$', 20, @success 

	SELECT * FROM crudSelectCompozitieMoneda() WHERE MateriePrima LIKE 'MP%';
		
	EXEC crudUpdateCompozitieMoneda 'Material1', 'MP1', 30, @success
	EXEC crudUpdateCompozitieMoneda 'Material1', 'MP2', 70, @success

	SELECT * FROM crudSelectCompozitieMoneda() WHERE MateriePrima LIKE 'MP%';

	EXEC crudDeleteCompozitieMonedaByDenumire 'Material1', 'MP1'	
	EXEC crudDeleteCompozitieMonedaByDenumire 'Material1', 'MP2'
	EXEC crudDeleteCompozitieMonedaByDenumire 'Material2', 'MP2'
	EXEC crudDeleteCompozitieMonedaByDenumire 'Material2', 'MP3'
	EXEC crudDeleteCompozitieMonedaByDenumire 'Material2', 'MP1'

	SELECT * FROM crudSelectCompozitieMoneda() WHERE MateriePrima LIKE 'MP%';
END
GO

CREATE OR ALTER PROCEDURE crudTipMoneda
AS BEGIN
	DECLARE @success INT

	EXEC crudInsertTipMonede 10, 'Leu', 1993, 'Moneda', 'Fier', @success OUT
	DECLARE @id1 INT; SELECT @id1 = @@IDENTITY
	
	EXEC crudInsertTipMonede 5, 'Ban', 1957, 'Medalie', 'Alama', @success OUT
	DECLARE @id2 INT; SELECT @id2 = @@IDENTITY

	EXEC crudInsertTipMonede 5, 'Dinar', 2005, 'Moneda', 'Aliaj', @success OUT
	DECLARE @id3 INT; SELECT @id3 = @@IDENTITY

	-- errors
	EXEC crudInsertTipMonede 5, '!!!!!!!!!', 2005, 'Moneda', 'Aliaj', @success OUT	
	EXEC crudInsertTipMonede -20, 'Dinar', 2005, 'Moneda', 'Aliaj', @success OUT	
	EXEC crudInsertTipMonede 5, 'Dinar', -15, 'Moneda', 'Aliaj', @success OUT	
	EXEC crudInsertTipMonede 5, 'Dinar', 2005, 'Invalid', 'Aliaj', @success OUT	
	EXEC crudInsertTipMonede 5, 'Dinar', 2005, 'Invalid', '???', @success OUT			

	SELECT * FROM crudSelectTipMoneda()	ORDER BY IdTipMoneda

	EXEC crudUpdateTipMonede @id1, 50, 'Leu', 1993, 'Moneda', 'Alama', @success OUT
	EXEC crudUpdateTipMonede @id3, 5, 'Dinar', 1995, 'Moneda', 'Aliaj', @success OUT

	SELECT * FROM crudSelectTipMoneda()	ORDER BY IdTipMoneda

	EXEC crudInsertExemplarMonede 5, 'Ban', 1957, 'Medalie', 'Alama', 6, @success OUT	

	EXEC crudDeleteTipMonede @id1
	EXEC crudDeleteTipMonede @id2
	EXEC crudDeleteTipMonede @id3
	
	SELECT * FROM crudSelectTipMoneda()	ORDER BY IdTipMoneda
END
GO

CREATE OR ALTER PROCEDURE crudMaterialMoneda
AS BEGIN
	DECLARE @success INT
	EXEC crudInsertMaterialMoneda 'A', @success OUT
	DECLARE @id1 INT; SELECT @id1 = @@IDENTITY

	EXEC crudInsertMaterialMoneda 'B', @success OUT
	DECLARE @id2 INT; SELECT @id2 = @@IDENTITY

	EXEC crudInsertMaterialMoneda 'C', @success OUT
	DECLARE @id3 INT; SELECT @id3 = @@IDENTITY

	EXEC crudInsertMaterialMoneda 'D', @success OUT
	DECLARE @id4 INT; SELECT @id4 = @@IDENTITY

	SELECT * FROM crudSelectMaterialMoneda() ORDER BY IdMaterialMoneda;

	EXEC crudUpdateMaterialMoneda @id2, 'F', @success OUT

	-- error
	EXEC crudUpdateMaterialMoneda @id3, '!!!', @success OUT

	SELECT * FROM crudSelectMaterialMoneda() ORDER BY IdMaterialMoneda;

	EXEC crudInsertCompozitieMoneda 'A', 'TestA', 50, @success OUT
	EXEC crudInsertCompozitieMoneda 'F', 'TestB', 50, @success OUT

	EXEC crudInsertTipMonede 9, 'RON', 2000, 'Moneda', 'F', @success OUT
	EXEC crudInsertTipMonede 9, 'RON', 2001, 'Moneda', 'C', @success OUT
	EXEC crudInsertExemplarMonede 9, 'RON', 2001, 'Moneda', 'C', 10, @success OUT	

	EXEC crudDeleteMaterialMoneda NULL, 'A'
	EXEC crudDeleteMaterialMoneda NULL, 'F'
	EXEC crudDeleteMaterialMoneda NULL, 'C'
	EXEC crudDeleteMaterialMoneda NULL, 'D'
	
	SELECT * FROM crudSelectMaterialMoneda() ORDER BY IdMaterialMoneda;
END
GO

--EXEC crudMaterialMoneda
--EXEC crudCompozitieMoneda
--EXEC crudTipMoneda