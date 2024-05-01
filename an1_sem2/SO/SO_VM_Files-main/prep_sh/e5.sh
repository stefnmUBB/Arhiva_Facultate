#!/bin/bash

#5. Calculați numărul mediu de linii ale fișierelor de tip text dintr-un director dat ca parametru.
#(comenzi: find, file, wc)

dir=$1

nlines=0
nfiles=0
for file in `find "$dir" -type 'f' | grep -E "\.txt$"`; do
	let "nfiles+=1"
	lns=`wc -l "$file" | cut -d' ' -f1`
	let "nlines+=lns"
done
echo $nfiles
echo $nlines

if [ "$nfiles" == 0 ]; then
	echo 0
else
	echo $nlines
	echo $nfiles
	m=`bc <<< "scale=4; $nlines/$nfiles"`
	#let "m=1.0*nlines/nfiles"
	echo $m
fi

