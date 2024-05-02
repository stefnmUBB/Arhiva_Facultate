# Tema Proiect 4 - Saptamana 9

**Serverul Java** comunica cu **clientii C#** printr-un protocol de serializare text-based propriu.

## Descrierea protocolului de serializare (`Stringifier`)

### Exemplu Request:

- `GetAllSpectacoleRequest{}`
- `ReserveBiletRequest{bilet=BiletDTO{numeCumparator="a";nrLocuri=1;spectacol=SpectacolDTO{artist="artist1";data="2023-03-21 10:28:51";locatie="loc";nrLocuriDisponibile=0;nrLocuriVandute=2;id=1};id=0}}`

### Exemplu Response:

- `FilterSpectacoleResponse{spectacole=SpectacolDTO{artist="artist1";data="2023-03-21 10:28:51";locatie="loc";nrLocuriDisponibile=0;nrLocuriVandute=2;id=1},SpectacolDTO{artist="artist-1";data="2023-03-21 16:34:00";locatie="locatie4";nrLocuriDisponibile=55;nrLocuriVandute=6;id=8}}`

### Explicatii:

Exemple de obiecte trimise prin retea
- literali (`5`, `"my string\n"`)
- `ClassName{}`
- `ClassName{field1=value1;field1=value2;...;field1=valueN}`
- `ClassName{array=item1,item2,...,itemk;field1=value}`

`ClassName` este clasa obiectului care este trimis prin retea. Aceasta clasa trebuie sa fie definita identic atat pe server, cat si pe client.
Pentru Java, clasa trebuie sa aiba un constructor default `ClassName()` si trebuie sa fie detectabila de catre ClassLoader.

`fieldX` este un camp din clasa obiectului trimis prin retea. Acesta poate fi public sau privat si este detectat automat de catre mecanismul de serializare, __indiferent daca exista getter/setter pentru acest camp__. Pentru a uniformiza numele campurilor, numele atributelor din C# sunt traduse astfel incat sa coincida cu numele campurilor din Java:
- Un field de forma `_Name` este redenumit in `name`
- Un field ascuns in spatele unei proprietati implicit definite `PropertyName {get; set;}` este redenumit in `propertyName`
- Altfel se face o simpla conversie intre conventiile uzuale de denumire (C# `MyField` --> Java `myField`)

Valorile campurilor `valueX` pot fi literali, alte obiecte (`Parent{child=Child{id=4}}`) sau liste de obiecte (traduse in cod prin __arrays__).