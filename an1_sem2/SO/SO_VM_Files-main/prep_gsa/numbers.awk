BEGIN {
	n=0
}

/[aeiouAEIOU]+$/ {
	n++
	print $0
}

END {
	print n
}
