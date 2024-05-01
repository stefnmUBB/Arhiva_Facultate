#!/bin/bash

function get_aw(){
	echo $1 | sed -E 's/(.)/\1 /g'|cut -d' ' -f 9
}

for file in `find $1 -type 'f'`; do
	res_ls=`ls -la $file`
	perm=`get_aw $res_ls`
	if [ $perm = 'w' ]; then
		p0=`echo $res_ls | cut -d' ' -f1`
		chmod a-w $file
		p1=`ls -la $file | cut -d' ' -f1`
		echo $p0 $file $p1
	fi
done
