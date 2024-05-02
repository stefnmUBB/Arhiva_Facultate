--- 1
--- Selecteaza materialele monedelor si compozitiile lor
--- (mn W)
SELECT M.IdMaterialMoneda, MP.Denumire, C.ProcentCompozitie FROM MaterialMoneda M, MateriePrimaMoneda MP, CompozitieMoneda C
	WHERE C.IdMaterialMoneda = M.IdMaterialMoneda 	
	AND C.IdMateriePrima = MP.IdMateriePrima

--- 2
--- Selecteaza toate tipurile de monede emise inainte de 2000, si numarul lor de exemplare
--- (3 W G)
SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An, COUNT(EX.IdExemplarMoneda) NumarExemplare
	FROM TipMonede TM
	JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
	JOIN ExemplarMonede EX ON EX.IdTipMoneda =TM.IdTipMoneda
	WHERE AN<2000
	GROUP BY TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An;

--- 3
--- starea de conservare medie a monedelor de dinainte de 2000 stocate la Alpha
--- (3 W G)
SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An, AVG(EX.StareConservare) StareConservareMedie 
	FROM TipMonede TM
	JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
	JOIN ExemplarMonede EX ON EX.IdTipMoneda = TM.IdTipMoneda
	JOIN SpatiuDepozitare DP ON DP.IdDepozit = EX.IdDepozit
	WHERE AN<2000 AND DP.Cladire = 'Alpha'
	GROUP BY TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, NN.Tara, TM.An

--- 4
--- numarul de titluri lansate in fiecare an pentru consolele mai vechi de 2006,
--- pentru anii in care am cel putin 2 titluri in colectie, si compania care le-a lansat,
--- atunci cand sunt mai putin de 10 titluri lansate
--- (3 W G H)
SELECT COUNT(V.Nume) Titluri, YEAR(V.DataLansare) An, 
		CASe WHEN CP.Nume IS NULL THEN 'Altele' ELSE CP.Nume END NumeCompanie
		 FROM TitluJocVideo V
	LEFT JOIN Companie CP ON V.IdCompanie = CP.IdCompanie
	JOIN TipConsola C ON C.IdTipConsola = V.Platforma	
	WHERE YEAR(C.DataLansare)<2006
	GROUP BY YEAR(V.DataLansare) , CP.Nume
	HAVING COUNT(V.Nume)<10
	ORDER BY YEAR(V.DataLansare), CP.Nume DESC

--- 5
--- exemplare de jocuri lansate dupa 1995 + depozitul in care se afla, ordonate dupa stadiul de conservare
--- (3 W G)
SELECT EJ.IdExemplarJocVideo, TJ.Nume, TC.Nume, EJ.StareConservare, YEAR(TJ.DataLansare) AnLansare, SD.Cladire, SD.Camera, SD.Raion, SD.Raft FROM ExemplarJocVideo EJ 
	INNER JOIN TitluJocVideo TJ ON EJ.IdTitluJocVideo = TJ.IdTitluJocVideo
	INNER JOIN SpatiuDepozitare SD ON EJ.IdDepozit = SD.IdDepozit
	INNER JOIN TipConsola TC ON TJ.Platforma = TC.IdTipConsola
	WHERE YEAR(TJ.DataLansare)>=1995
	ORDER BY EJ.StareConservare DESC, TJ.Nume

--- 6
--- monedele din aluminiu pur stocate la Alpha
--- (3 W) 
SELECT EM.IdExemplarMoneda, Valoare, NN.Valuta, TM.An, EM.StareConservare FROM ExemplarMonede EM
	INNER JOIN TipMonede TM ON EM.IdTipMoneda = TM.IdTipMoneda
	INNER JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica
	INNER JOIN SpatiuDepozitare SD ON EM.IdDepozit = SD.IdDepozit
	FULL JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = TM.IdMaterialMoneda
	FULL JOIN CompozitieMoneda CM ON CM.IdMaterialMoneda = MM.IdMaterialMoneda		
	WHERE MM.Denumire='Aluminiu' AND CM.ProcentCompozitie=100

--- 7
--- anul mediu de lansare a titlurilor pentru fiecare plaforma, daca acesta este >=2000
--- (3 G H)
SELECT AVG(YEAR(V.DataLansare)) AnMediu, T.Nume, C.Nume FROM TitluJocVideo V 
	JOIN TipConsola T ON T.IdTipConsola = V.Platforma
	JOIN Companie C ON T.IdCompanie = C.IdCompanie
	GROUP BY T.Nume, C.Nume 
	HAVING AVG(YEAR(V.DataLansare))>=2000
	ORDER BY AVG(YEAR(V.DataLansare))

--- 8
--- numarul exemplarelor din fiecare tip de moneda
--- (3 G)
SELECT Valoare, NN.Valuta, TM.An, COUNT(EM.IdExemplarMoneda) Cantitate FROM ExemplarMonede EM
	INNER JOIN TipMonede TM ON EM.IdTipMoneda = TM.IdTipMoneda
	INNER JOIN NotaNumismatica NN ON NN.IdNotaNumismatica = TM.IdNotaNumismatica	
	FULL JOIN MaterialMoneda MM ON MM.IdMaterialMoneda = TM.IdMaterialMoneda
	FULL JOIN CompozitieMoneda CM ON CM.IdMaterialMoneda = MM.IdMaterialMoneda			
	GROUP BY Valoare, NN.Valuta, TM.An

--- 9
--- note numismatice pentru fiecare tara
--- (D)
SELECT DISTINCT Valuta, Tara FROM NotaNumismatica ORDER BY Tara;

--- 10
--- numele vanzatorilor de la care am cumparat un tip de consola cel putin o data
--- (mn D)
SELECT DISTINCT V.Nume, V.Prenume, TC.Nume FROM Achizitie A 
	INNER JOIN Vanzator V ON V.IdVanzator = A.IdVanzator
	INNER JOIN ExemplarConsole C ON C.IdExemplarConsola  = A.IdConsola
	JOIN TipConsola TC ON TC.IdTipConsola = C.IdTipConsola


--- numarul companiilor de la care am cel putin 2 titluri de jocuri
SELECT COUNT(V.Nume), C.Nume FROM TitluJocVideo V 
	JOIN Companie c ON V.IdCompanie = C.IdCompanie
	GROUP BY C.Nume HAVING COUNT(V.Nume)>2;