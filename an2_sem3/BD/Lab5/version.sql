CREATE TABLE Versiune (V INT NOT NULL);
INSERT INTO Versiune(V) VALUES (0);

CREATE TABLE VersiuneSteps 
(
	V INT NOT NULL,
	U VARCHAR(50),  -- upgrade
	D VARCHAR(50),  -- downgrade
);

INSERT INTO VersiuneSteps(V,U,D) VALUES
	(0, 'MarireLenCladireSpatiuDepozitare'    , ''),
	(1, 'SetareDefaultDepozitExemplarJocVideo', 'MicsorareLenCladireSpatiuDepozitare'),
	(2, 'CreareTabelWishlistMoneda'           , 'EliminareDefaultDepozitExemplarJocVideo'),
	(3, 'AdaugareDescriereWish'               , 'StergereTabelWishlistMoneda'),
	(4, 'AdaugareFKWish'                      , 'StergereDescriereWish'),
	(5, ''                                    , 'StergereFKWish');

SELECT * FROM VersiuneSteps

SELECT * FROM Versiune
GO

ALTER PROCEDURE ChangeVersion @v INT
AS
BEGIN
	IF @v<0 OR @v>5 
	BEGIN
		RAISERROR('Versiunea nu exista',10,1)	
		RETURN
	END
	DECLARE @crtv INT
	SELECT TOP 1 @crtv=V FROM Versiune
	PRINT 'Versiunea curenta este ' + CAST(@crtv AS VARCHAR);

	IF @v = @crtv
	BEGIN
		PRINT 'Versiunea exista deja.'
		RETURN
	END

	DECLARE @step INT
	IF @v>@crtv 
		SET @step = 1 -- facem upgrade
	ELSE 
		SET @step = -1 -- facem downgrade

	WHILE @crtv <> @v
	BEGIN				
		DECLARE @procname VARCHAR(50)
		IF @step=1 
			SELECT @procname = U FROM VersiuneSteps WHERE V=@crtv
		ELSE
			SELECT @procname = D FROM VersiuneSteps WHERE V=@crtv
		PRINT 'Se executa "' + @procname + '" pentru a iesi din versiunea ' + CAST(@crtv AS VARCHAR)

		DECLARE @query NVARCHAR(200)
		SET @query = 'EXEC '+@procname
		-- PRINT @query
		EXECUTE sp_executesql @query

		SET @crtv = @crtv + @step
		-- incarcam veriunea noua
		DELETE FROM Versiune
		INSERT INTO Versiune(V) VALUES (@crtv);
	END
END
GO

DELETE FROM Versiune
INSERT INTO Versiune(V) VALUES (0);
EXEC ChangeVersion @v=1