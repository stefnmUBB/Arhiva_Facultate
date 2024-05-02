# AI.Lab4

Să se identifice cel mai scurt drum care pleacă dintr-un nod, vizitează toate nodurile (fiecare nod este vizitat o singură dată) și revine în locația de start folosind un algoritm evolutiv.

## Funcționalități:

- încărcarea unui graf
- crearea unui graf de test
- identificarea drumurilor de lungime cât mai mică între două puncte ale grafului, trecând prin toate nodurile sau identificarea unui ciclu eulerian
- vizualizarea soluțiilor de tip _best-chromosome_ la un moment dat

## Date de intrare

- graful reprezentat prin matricea de adiacență a costurilor
- parametrii problemei (lifespan-ul cromozomilor, probabilitatea de mutație, parametri specifici funcțiilor de fitness)

## Date de ieșire
- numărul de comunități identificate
- o listă de numere [c0 c1 ... cn], unde ci reprezintă comunitatea din care face parte al i-lea nod din graf
- o listă de comunități, în fiecare fiecare comunitate e specificată prin lista nodurilor componente

## Complexitate

- `O(I*P*F)`
- `I` = numărul de iterații
- `P` = numărul mediu de indivizi din populație
- `F` = complexitatea calculului valorilor de fitness (θ(N) crossover + mutatie worst case în implementarea curentă)

De obicei, `P` oscilează în jurul unei valori constante care se reglează automat în funcție de condițiile problemei. Această valoare este un punct de echilibru pentru modelul de creștere a populației (`P << I`). De exemplu, pentru un graf complet cu 10 noduri, `P~150`.
Complexitatea medie a algoritmului se poate scrie `O(I*N)`, unde `N` este numărul de noduri din graf.

