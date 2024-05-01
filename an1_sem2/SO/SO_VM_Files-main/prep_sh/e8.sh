#Pentru fiecare fișier dat în linia de comandă, să se afișeze linia care apare de cele mai multe ori. Afișarea se va face în ordinea descrescatoare a numărului de apariții.

function most_freq_line(){
# $1 = file

	sort $1 | uniq -c | sort -n -r | sed -n "1,1 p"
}

while [ $# -gt 0 ]; do
	most_freq_line $1
	shift 1
done | sort -n -r | sed -E "s/(\s+[0-9]+ )(.*)/\2/"
