# ai-lab02-stefnmUBB

## Cerință

Se cere identificarea comunităților existente într-o rețea folosind un algoritm Greedy.

## Funcționalități:

- încărcarea unui graf neorientat format GML
- vizualizarea rețelei
- identificarea comunităților existente în rețeaua dată
- posibilitatea de a vizualiza multiple soluții furnizate de arborele de stare (dendograma)
- opțiunea de a identifica, la alegerea utilizatorului, subcomunități în comunitățile existente

## Date de intrare

- rețeaua în format GML, încărcată dintr-un fișier la alegere sau din template-urile încorporate în aplicație <br>
  **SAU** o rețea generată aleator în funcție de numărul de noduri, numărul de comunități așteptate și probabilitățile
  de a genera muchii în interiorul fiecărei comunități, respectiv între comunitățile în sine

## Date de ieșire

- numărul de comunități identificate
- o listă de numere `[c0 c1 ... cn]`, unde `ci` reprezintă comunitatea din care face parte al `i`-lea nod din graf
- o listă de comunități, în fiecare fiecare comunitate e specificată prin lista nodurilor componente

## Complexitate (Core algorithm : Girvan-Newman)

- Timp: O(|V|*(|E|+|V|))
- Memorie: Θ(|E|+|V|²)

<p align="center">
    <img src="https://user-images.githubusercontent.com/91871146/224540631-f6a6a0e7-8101-4272-b1b6-322ec969a04f.png"></img>
</p>

<p align="center">
    <img src="https://user-images.githubusercontent.com/91871146/224540899-76708088-2caa-480e-a5a1-3144de4c21b1.png"></img>
</p>
