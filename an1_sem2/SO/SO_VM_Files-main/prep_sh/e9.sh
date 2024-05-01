# 9. Să se scrie un script shell care va afișa toate numele fișierelor dintr-un director dat ca parametru și din subdirectoarele sale, al căror nume sunt mai scurte de 8 caractere. Dacă aceste fișiere sunt de tip text, se va afișa și primele 10 linii de text pe care le conțin.


function str_len() {
	echo "$1" | sed -E "s/\/(.*$)/\1/" | grep -o -E "." | wc -l
}

d=$1

for f in `find "$d" -type 'f'`; do
	cnt=`str_len "$f"`
	if [ $cnt -lt 8 ]; then
		echo $f
		type=`file $f | grep -o -E "text" | wc -l`	
		if [ $type -gt 0 ]; then
			head -n 10 $f
		fi
	fi
done
