#!/bin/bash

function get_user() {
	echo $1
}

function get_proc() {
	echo $8
}


while true; do
	for name in $@; do
		ps -ef | while read line; do
			user=`get_user $line`
			if [ $user = $USER ]; then
				proc=`get_proc $line`			
				if [ $proc = $name ]; then
					echo killing dangerous $name
					#kill $proc
				fi
			fi
		done
	done
	sleep 1
done
