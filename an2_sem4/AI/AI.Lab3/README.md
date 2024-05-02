# AI.Lab3

Se cere identificarea comunităților existente într-o rețea folosind un algoritm evolutiv.

## Funcționalități:

- încărcarea unui graf neorientat format GML
- vizualizarea rețelei
- identificarea comunităților existente în rețeaua dată prin simulare
- vizualizarea soluțiilor de tip _best-chromosome_ la un moment dat

## Date de intrare

- rețeaua în format GML, încărcată dintr-un fișier la alegere sau din template-urile încorporate în aplicație
- parametrii problemei (lifespan-ul cromozomilor, probabilitatea de mutație, parametri specifici funcțiilor de fitness)

## Date de ieșire
- numărul de comunități identificate
- o listă de numere [c0 c1 ... cn], unde ci reprezintă comunitatea din care face parte al i-lea nod din graf
- o listă de comunități, în fiecare fiecare comunitate e specificată prin lista nodurilor componente

## Complexitate

- `O(I*P*F)`
- `I` = numărul de iterații
- `P` = numărul mediu de indivizi din populație
- `F` = complexitatea calculului valorilor de fitness 
    - θ(E) - modularity, density
    - θ(K*K) - community score


<p align="center"><img src="https://user-images.githubusercontent.com/91871146/226187485-2c3444fa-22f6-4fe3-9a9d-0371f56bff59.png"></p>
