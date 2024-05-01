#!/bin/bash

# 4. Afișați numele fișierelor dintr-un director dat ca parametru care conțin numere cu mai mult de 5 cifre.

dir=$1

for file in `find $dir -type 'f'`; do
	cnt=`grep -c -E "[1-9][0-9]{5}" "$file"`
	if [ "$cnt" -ne 0 ]; then
		echo "$file"
	fi
done
