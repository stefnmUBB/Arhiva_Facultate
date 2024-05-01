#include <stdio.h>

int main()
{
	int S=0, n;
	while(!feof(stdin)) {
		if(scanf("%i", &n)==1)
			S+=n;
	}
	printf("%i\n",S);
	return 0;
}
