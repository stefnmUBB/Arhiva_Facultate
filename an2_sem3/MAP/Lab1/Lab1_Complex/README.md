Se citesc, ca parametri în linia de comandă, separați prin spațiu, mai multe numere complexe de forma  `a+b*i`
și o operație, sub forma unui operator (+,-,\*,/). 1.Sa se verifice dacă parametrii citiți în linia de comandă, 
separați prin spațiu,reprezintă o expresie aritmetică de forma : 𝑛1 𝑜𝑝 𝑛2 𝑜𝑝... 𝑜𝑝 𝑛𝑘,unde 𝑛1,𝑛2...,𝑛𝑘 sunt numere 
complexe de forma 𝑎+𝑏∗𝑖, iar 𝑜𝑝 este operatorul dat. 

Exemplu:

`2+3*i+5−6*i+−2+i`

```
args[0]=2+3*i, 
args[1]= +, 
args[2]= 5−6*i,  
args[3]= +,  
args[4]=−2+i
```

Dacă parametrii citiți în  linia  de  comandă reprezintă o  expresie  aritmetică de  forma descrisă la punctul 1, 
se cere să se afișeze rezultatul acestei expresii. Exemplu: pentru expresia data se va afisa: `5−2*i`
