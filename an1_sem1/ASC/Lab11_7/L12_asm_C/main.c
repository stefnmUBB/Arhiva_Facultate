// 8. Să se afișeze, pentru fiecare număr de la 32 la 126, valoarea numărului (în baza 8) și caracterul cu acel cod ASCII.
#include <stdio.h>

int literal_octal(int n);

int main() 
{
    for(char i=32;i<=126;i++)        
        printf(" %3i. chr(%04X) = '%c'\n", i,literal_octal(i), i);
    return 0;
}