#!/bin/bash

files=`find $1 -type 'f' | grep -E "\.c$"`

rescnt=0

for file in $files; do
	# echo $file
	nlines=`wc -l $file | cut -d' ' -f1`
	# echo $nlines
	if [ $nlines -ge 500  ]; then
		echo $file $nlines
		((rescnt+=1))
	fi
	if [ $rescnt = 2 ]; then 
		break 
	fi
done
