#!/bin/bash

users=$(cat who.fake | cut -d' ' -f1)

for user in $users; do
	nrp=`grep -c -E "^$user" ps.fake`
	echo $user $nrp
done
