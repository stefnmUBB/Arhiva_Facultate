#!/bin/bash
#Să se scrie un script shell care va afișa toate fișierele dintr-un director dat și din subdirectoarele acestuia asupra cărora au drepturi de scriere toate cele trei categorii de utilizatori. Aceste fișiere vor fi apoi redenumite, prin adăugarea sufixul '.all' la numele lor inițial.


dir=$1

files=`find "$dir" -perm -220`


for file in $files; do
	echo $file.all
	mv "$file" "$file.all"
done
