#/bin/bash

d=$1

if [ -z "$d" ]; then
	echo "Argument must not be null"
	exit 1
fi

if [ ! -d "$d" ]; then
	echo "Argument must be a dir"
fi 

nr_lines=0
nr_files=0
for f in `find "$d" -type 'f' | grep -E "\.txt$"`; do
	nr_l=`wc -l "$f" | cut -d' ' -f1`
	let "nr_lines+=nr_l"
	let "nr_files+=1"
done

avg=0
if [ $nr_files != 0 ]; then # sa nu impartim la 0
	let "avg=nr_lines/nr_files"
fi

nr_comm=0
for f in `find "$d" -type 'f' | grep -E "\.c$"`; do
	nr_c=`grep -E "^\/\/" $f | wc -l | cut -d' ' -f1`
	let "nr_comm+=nr_c"
done

echo "$avg $nr_comm" > resultate.txt
