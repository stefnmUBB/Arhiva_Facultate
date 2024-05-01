#!/bin/bash

files=`find $1 -type 'f' | grep -E "\.log$"`

for file in $files; do
	echo "Processing " $file
	cat $file | sort > $file".sorted"
	mv -T $file $file".bak"
	mv -T $file".sorted" $file
	
done

