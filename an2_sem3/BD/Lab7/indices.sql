CREATE OR ALTER VIEW vw_Compozitie AS
	SELECT MM.Denumire AS Material, MP.Denumire AS MateriePrima, C.ProcentCompozitie FROM CompozitieMoneda C
		INNER JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = C.IdMaterialMoneda
		INNER JOIN MateriePrimaMoneda MP ON MP.IdMateriePrima = C.IdMateriePrima		
GO

CREATE OR ALTER VIEW vw_TipMonede AS
	SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.An, TM.Orientare, MM.Denumire AS Material
		FROM TipMonede TM
		INNER JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
		INNER JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = TM.IdMaterialMoneda		
GO

SELECT * FROM vw_TipMonede;
SELECT * FROM vw_Compozitie;

--DROP INDEX N_idx_TipMonedeAn ON TipMonede
--GO
--CREATE NONCLUSTERED INDEX N_idx_TipMonedeAn ON TipMonede (An);
--GO

CREATE NONCLUSTERED INDEX N_idx_MaterialMoneda ON MaterialMoneda(Denumire);
GO

CREATE NONCLUSTERED INDEX N_idx_MateriePrimaMoneda ON MateriePrimaMoneda(Denumire);
GO

CREATE NONCLUSTERED INDEX N_idx_CompozitieMaterial ON CompozitieMoneda(IdMaterialMoneda);
GO
CREATE NONCLUSTERED INDEX N_idx_CompozitieMateriePrima ON CompozitieMoneda(IdMateriePrima);
GO

CREATE NONCLUSTERED INDEX N_idx_CompozitieProcent ON CompozitieMoneda(ProcentCompozitie);
GO

CREATE NONCLUSTERED INDEX N_idx_TipMonedeNota ON TipMonede(IdNotaNumismatica)
GO

--DROP INDEX N_idx_NotaValoare ON NotaNumismatica
--CREATE NONCLUSTERED INDEX N_idx_NotaValoare ON NotaNumismatica(Valoare)
--GO

SELECT * FROM CompozitieMoneda CM
	INNER JOIN MaterialMoneda MM ON CM.IdMaterialMoneda = MM.IdMaterialMoneda
	ORDER BY ProcentCompozitie

SELECT * FROM vw_Compozitie;

SELECT * FROM vw_Compozitie ORDER BY ProcentCompozitie;

SELECT * FROM vw_Compozitie WHERE MateriePrima LIKE 'A%' ORDER BY Material;

SELECT * FROM vw_Compozitie ORDER BY Material;

--SELECT * FROM MaterialMoneda ORDER BY Denumire


SELECT * FROM vw_TipMonede ORDER BY Valoare
SELECT * FROM vw_TipMonede ORDER BY Material
SELECT * FROM vw_TipMonede WHERE Material LIKE 'X%'

SELECT * FROM vw_TipMonede WHERE Material LIKE 'X%'

SELECT * FROM vw_TipMonede ORDER BY An;
