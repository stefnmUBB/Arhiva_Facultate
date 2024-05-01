#!/bin/bash

#2. Pentru fiecare parametru din linia de comandă:
#- dacă e fișier, se vor afișa numele, numărul de caractere și de linii din el (în această ordine)
#- dacă e director, se vor afișa numele și câte fișiere conține (inclusiv în subdirectoarele sale).
#(comenzi: test, wc, awk, find)

for arg in $@; do
	if [ -f $arg ]; then
		echo "$arg file"
		wc -c $arg | cut -d' ' -f1
		wc -l $arg | cut -d' ' -f1 
	else 
		if [ -d $arg ]; then
			echo "$arg dir"
			find "$arg" -type 'f' | wc -l
		else 
			echo "$arg unknown"
		fi 
	fi
done
