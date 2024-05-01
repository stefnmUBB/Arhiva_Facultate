#!/bin/bash

dirname=$1

if [ -z "$dirname" ]; then
	echo "No directory is provided"
	exit 1
fi

if [ ! -d "$dirname" ]; then
	echo "The path is not a directory"
	exit 2
fi

STATE=""
while true; do
	S=""
	for P in `find $dirname`; do
		if [ -f $P ]; then
			LS=`ls -l $P | sha1sum`	
			CONTENT=`sha1sum $P`
		elif [ -d $P ]; then
			LS=`ls -l -d $P | sha1sum`
			CONTENT=`ls -l $P | sha1sum`
		fi
		S="$S\nLS $CONTENT"
	done
	if [ -n "$STATE" ] && [ "$S" != "$STATE" ]; then
		echo "Dir state changed"
	fi
	STATE=$S
	sleep 1
done
