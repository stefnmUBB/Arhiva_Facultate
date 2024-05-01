#!/bin/bash

# 7. Să se scrie un script shell care are ca parametri triplete formate dintr-un nume de fișier, un cuvânt și un număr k. Pentru fiecare astfel de triplet, se vor afișa toate liniile din fișierul care conțin cuvântul dat de exact k ori.

function prlines(){
	cat $1 | while read line; do
		cnt=`echo $line | grep -o -E "$2" | wc -l`


		if [ $cnt = $3 ]; then
			echo $line
		fi
	done
}

while [ $# -ge 3 ]; do
	file=$1
	word=$2
	k=$3
	shift 3
	echo $file $word $k
	prlines $file $word $k
done

