#!/bin/bash

result="12121"
while read line; do
	result+="$line@scs.ubbcluj.ro,"
done < $1

echo $result | sed -E "s/(.*)(\,)$/\1/"
