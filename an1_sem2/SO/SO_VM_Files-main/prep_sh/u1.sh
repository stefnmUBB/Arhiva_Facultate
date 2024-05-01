#!/bin/bash

N=$1

for i in `seq 1 $N`; do
	echo $i
	let 'j=i+5'
	echo $j
	sed -n "$i,$j p" passwd.fake > file_$i.txt
done
