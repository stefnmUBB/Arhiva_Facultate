SELECT * FROM Facultati

CREATE INDEX ix_facultati_an_asc_denumire_asc ON Facultati (AnInfiintare ASC, Denumire ASC);

SELECT AnInfiintare, Denumire FROM Facultati WHERE AnInfiintare>1960 ORDER BY AnInfiintare

