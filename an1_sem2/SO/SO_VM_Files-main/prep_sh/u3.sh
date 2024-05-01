#!/bin/bash

#3. Afișați primele 5 linii și ultimele 5 linii ale tuturor fișierelor de tip text din directorul curent. Dacă un fișier are mai puțin de 10 linii, atunci va fi afişat în întregime.
#(comenzi: head, tail, find, file, wc)

dir=$1

for file in `find "$dir" -type 'f' | grep -E "\.txt$"`; do
	echo "------------ $file ----------"
	lines=`wc -l "$file" | cut -d' ' -f1`
	if [ $lines -lt 10 ]; then
		cat "$file"
	else
		head --lines=5 "$file"
		tail --lines=5 "$file"
	fi
done
