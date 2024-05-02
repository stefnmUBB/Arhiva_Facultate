CREATE OR ALTER VIEW vw_Monede2000 AS 
SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An, COUNT(EX.IdExemplarMoneda) NumarExemplare
	FROM TipMonede TM
	JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
	JOIN ExemplarMonede EX ON EX.IdTipMoneda =TM.IdTipMoneda
	WHERE AN<2000
	GROUP BY TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An;
GO

CREATE OR ALTER VIEW vw_Compozitii AS 
SELECT M.IdMaterialMoneda, MP.Denumire, C.ProcentCompozitie 
	FROM MaterialMoneda M, MateriePrimaMoneda MP, CompozitieMoneda C
	WHERE C.IdMaterialMoneda = M.IdMaterialMoneda 	
	AND C.IdMateriePrima = MP.IdMateriePrima
GO

CREATE OR ALTER VIEW vw_Exemplare AS 
SELECT * FROM ExemplarMonede
GO

SELECT * FROM vw_Exemplare

SELECT * FROM vw_Compozitii
SELECT * FROM vw_Monede2000