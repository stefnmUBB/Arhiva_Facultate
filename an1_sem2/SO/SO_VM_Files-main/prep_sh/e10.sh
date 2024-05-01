#!/bin/bash

# 10. Să se scrie un script shell care pentru fiecare fișier cu drepturile 755 dintr-un director dat ca parametru (si subdirectoarele sale) va schimba drepturile de acces în 744. Înainte de modificarea drepturilor de acces, scriptul va cere confirmare din partea utilizatorului (pentru fiecare fișier în parte).

d=$1

for f in `find "$d" -type 'f' -perm 700`; do	
	echo "Change $f ? (y\n)"
	read answer
	echo $answer
	if [ $answer = 'y' ]; then
		chmod 755 "$f"
	fi
done
