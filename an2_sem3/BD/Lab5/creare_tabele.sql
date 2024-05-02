-- stocare

CREATE TABLE SpatiuDepozitare (
	IdDepozit INT PRIMARY KEY IDENTITY,
	Cladire   VARCHAR(20),
	Camera    INT,
	Raion     INT,
	Raft      INT
);

-- InregistrareNumismatica

CREATE TABLE NotaNumismatica (
	IdNotaNumismatica INT PRIMARY KEY IDENTITY,
	Valoare            FLOAT       NOT NULL,
	Valuta             VARCHAR(30) NOT NULL,
)

-- Monede

CREATE TABLE MateriePrimaMoneda (  -- fier, cupru, otel,...
	IdMateriePrima INT PRIMARY KEY IDENTITY,
	Denumire       VARCHAR(20)
);

CREATE TABLE MaterialMoneda ( -- inox, ...
	IdMaterialMoneda INT PRIMARY KEY IDENTITY,
	Denumire VARCHAR(20)
);

CREATE TABLE CompozitieMoneda ( -- otel Fe 80%; otel C 20%
	IdMaterialMoneda  INT FOREIGN KEY REFERENCES MaterialMoneda(IdMaterialMoneda),
	IdMateriePrima    INT FOREIGN KEY REFERENCES MateriePrimaMoneda(IdMateriePrima),
	ProcentCompozitie FLOAT CHECK (0<ProcentCompozitie AND ProcentCompozitie<=100)

	CONSTRAINT pk_CompozitieMoneda PRIMARY KEY (IdMaterialMoneda, IdMateriePrima)
);

CREATE TABLE TipMonede (
	IdTipMoneda       INT         PRIMARY KEY IDENTITY,
	IdNotaNumismatica INT FOREIGN KEY REFERENCES NotaNumismatica(IdNotaNumismatica),	
	Orientare         VARCHAR(20) NOT NULL CHECK (Orientare IN ('Moneda', 'Medalie')),		
	IdMaterialMoneda  INT FOREIGN KEY REFERENCES MaterialMoneda(IdMaterialMoneda)
);

CREATE TABLE ExemplarMonede (
	IdExemplarMoneda INT PRIMARY KEY IDENTITY,
	IdTipMoneda      INT FOREIGN KEY REFERENCES TipMonede(IdTipMoneda),
	StareConservare  INT CHECK (1<=StareConservare AND StareConservare<=10),
	IdDepozit        INT FOREIGN KEY REFERENCES SpatiuDepozitare(IdDepozit) 
);

-- Bancnote


CREATE TABLE MaterialBancnota (  -- hartie, plastic...
	IdMaterial INT PRIMARY KEY IDENTITY,
	Denumire       VARCHAR(20)
);

CREATE TABLE TipBancnota (
	IdTipBancnota      INT         PRIMARY KEY IDENTITY,
	IdNotaNumismatica  INT         FOREIGN KEY REFERENCES NotaNumismatica(IdNotaNumismatica),	
	IdMaterialBancnota INT         FOREIGN KEY REFERENCES MaterialBancnota(IdMaterial)
);

CREATE TABLE ExemplarBancnote (
	IdExemplarBancnota  INT PRIMARY KEY IDENTITY,
	IdTipBacncnota      INT FOREIGN KEY REFERENCES TipBancnota(IdTipBancnota),
	StareConservare     INT CHECK (1<=StareConservare AND StareConservare<=10),
	IdDepozit           INT FOREIGN KEY REFERENCES SpatiuDepozitare(IdDepozit) 
);


-- Jocuri Video - Console

CREATE TABLE Companie (
	IdCompanie INT PRIMARY KEY IDENTITY,
	Nume VARCHAR(30)
);

CREATE TABLE TipConsola (
	IdTipConsola      INT PRIMARY KEY IDENTITY,
	IdCompanie        INT FOREIGN KEY REFERENCES Companie(IdCompanie),
	Nume              VARCHAR(30),
	DataLansare       DATETIME,
	Portabilitate     VARCHAR(10) NOT NULL CHECK (Portabilitate in ('TV','Portabil','Hibrid')),
	ProcesorPrincipal VARCHAR(10),
	FrecventaProcesor INT,
	LatimeDeBanda     INT NOT NULL CHECK (LatimeDeBanda in (8,16,32,64))
);

/*CREATE TABLE RegistruBackwardsCompatibility (
	
);*/

CREATE TABLE ExemplarConsole (
	IdExemplarConsola INT PRIMARY KEY IDENTITY,
	IdTipConsola      INT FOREIGN KEY REFERENCES TipConsola(IdTipConsola),
	StareConservare   INT CHECK (1<=StareConservare AND StareConservare<=10),
	IdDepozit         INT FOREIGN KEY REFERENCES SpatiuDepozitare(IdDepozit),
	Culoare           INT, -- RGB
);

-- Jocuri Vide - Titluri

CREATE TABLE Franciza (
	IdFranciza INT PRIMARY KEY IDENTITY,
	Nume VARCHAR(30),
);

CREATE TABLE TitluJocVideo (
	IdTitluJocVideo  INT PRIMARY KEY IDENTITY,
	Nume             VARCHAR(100),
	Platforma        INT NOT NULL FOREIGN KEY REFERENCES TipConsola(IdTipConsola),
	DataLansare      DATETIME,
	CopiiVandute     INT,
	IdFranciza       INT NULL FOREIGN KEY REFERENCES Franciza(IdFranciza),
	IdCompanie       INT FOREIGN KEY REFERENCES Companie(IdCompanie),
	Remake           INT FOREIGN KEY REFERENCES TitluJocVideo(IdTitluJocVideo)
);

CREATE TABLE ExemplarJocVideo (
	IdExemplarJocVideo INT PRIMARY KEY IDENTITY,
	IdTitluJocVideo    INT FOREIGN KEY REFERENCES TitluJocVideo(IdTitluJocVideo),
	StareConservare   INT CHECK (1<=StareConservare AND StareConservare<=10),
	IdDepozit         INT FOREIGN KEY REFERENCES SpatiuDepozitare(IdDepozit),
);

CREATE TABLE Vanzator(
	IdVanzator INT PRIMARY KEY IDENTITY,
	Nume       VARCHAR(30),
	Prenume    VARCHAR(30),
);

CREATE TABLE Achizitie(
	IdAchizitie INT PRIMARY KEY IDENTITY,

	-- doar una din urmatoarele:
	IdMoneda    INT FOREIGN KEY REFERENCES ExemplarMonede(IdExemplarMoneda),
	IdBancnota  INT FOREIGN KEY REFERENCES ExemplarBancnote(IdExemplarBancnota),
	IdConsola   INT FOREIGN KEY REFERENCES ExemplarConsole(IdExemplarConsola),
	IdJocVideo  INT FOREIGN KEY REFERENCES ExemplarJocVideo(IdExemplarJocVideo),

	CONSTRAINT ckUnSingurExemplar CHECK  (
		( CASE WHEN IdMoneda   IS NULL THEN 0 ELSE 1 END
		+ CASE WHEN IdBancnota IS NULL THEN 0 ELSE 1 END
		+ CASE WHEN IdConsola  IS NULL THEN 0 ELSE 1 END
		+ CASE WHEN IdJocVideo IS NULL THEN 0 ELSE 1 END
		) = 1
	),

	DataAchizitiei DATETIME,
	IdVanzator     INT FOREIGN KEY REFERENCES Vanzator(IdVanzator),
	Pret           FLOAT NOT NULL,
);


ALTER TABLE TipMonede ADD An INT;
ALTER TABLE NotaNumismatica ADD Tara VARCHAR(50);