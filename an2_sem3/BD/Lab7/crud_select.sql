CREATE OR ALTER FUNCTION crudSelectCompozitieMoneda()
RETURNS TABLE
AS RETURN SELECT MM.Denumire AS Material, MP.Denumire AS MateriePrima, C.ProcentCompozitie FROM CompozitieMoneda C
	INNER JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = C.IdMaterialMoneda
	INNER JOIN MateriePrimaMoneda MP ON MP.IdMateriePrima = C.IdMateriePrima
GO

CREATE OR ALTER FUNCTION crudSelectTipMoneda()
RETURNS TABLE
AS RETURN SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.An, TM.Orientare, MM.Denumire FROM TipMonede TM
	INNER JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
	INNER JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = TM.IdMaterialMoneda;
GO

CREATE OR ALTER FUNCTION crudSelectMaterialMoneda()
RETURNS TABLE
AS RETURN SELECT * FROM MaterialMoneda
GO

CREATE OR ALTER FUNCTION crudSelectMateriePrimaMoneda()
RETURNS TABLE
AS RETURN SELECT * FROM MateriePrimaMoneda
GO


CREATE OR ALTER Function crudSelectExemplarMonede()
RETURNS TABLE
AS RETURN SELECT EM.IdExemplarMoneda, TM.*, EM.StareConservare FROM ExemplarMonede EM
	INNER JOIN crudSelectTipMoneda() TM ON TM.IdTipMoneda = EM.IdTipMoneda;
GO

SELECT * FROM crudSelectExemplarMonede()