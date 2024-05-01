#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>

#define CRT_SECURE_NO_WARNINS

int get_number(const char* msg);

int is_prime(int n);
void print_first_primes(int n);
void print_primes(int n);
void approx_sqrt(int n, int p);
void print_first_digits_of_fraction(int n, int k, int m);
void print_pascal(int n);

void print_consecutives_sum(int n);

int main()
{
	while (1)
	{
		printf("\n\n");
		printf("  0 - exit\n");
		printf("  1 - show primes\n");
		printf("  2 - show first n primes\n");
		printf("  3 - consecutive numbers sum\n");
		printf("  4 - first N digits of K/M (K<M)\n");
		printf("  6 - Pascal triangle\n");
		printf("  7 - approx sqrt(n)\n");
		int action = get_number(" >>> ");
		if (!action) break;

		if (action == 1)
		{
			int n = get_number("N = ");
			print_primes(n);
		}
		else if (action == 2)
		{
			int n = get_number("N = ");
			print_first_primes(n);
		}
		else if (action == 3)
		{
			int n = get_number("N = ");
			print_consecutives_sum(n);
		}
		else if (action == 4)
		{
			int n = get_number("N = ");
			int k = get_number("K = ");
			int m = get_number("M = ");
			print_first_digits_of_fraction(n, k, m);
		}
		else if (action == 6)
		{
			int n = get_number("N = ");
			print_pascal(n);
		}
		else if (action == 7)
		{
			int n = get_number("N = ");
			int p = get_number("precision = ");
			approx_sqrt(n, p);
		}
	}
	return 0;
}

/// <summary>
/// Reads int from input
/// </summary>
/// <param name="msg">message to display</param>
/// <returns>user input integer</returns>
int get_number(const char* msg)
{
	if (msg != NULL)
	{
		printf(msg);
	}
	int result;
	scanf_s("%i", &result);
	return result;
}

/// <summary>
/// Checks if n is prime
/// </summary>
/// <param name="n">number to check</param>
/// <returns>1 if n is prime, 0 otherwise</returns>
int is_prime(int n)
{
	if (n < 2) return 0;
	for (int d = 2; d * d <= n; d++)
		if (n % d == 0)
			return 0;
	return 1;
}

/// <summary>
/// prints primes up to n (n>0)
/// </summary>
/// <param name="n">upper limit</param>
void print_primes(int n)
{
	if (n <= 0)
	{
		printf("Number must be positive\n");
		return;
	}
	printf("Primes : ");
	for (int i = 2; i < n; i++)
		if (is_prime(i))
		{
			printf("%i ", i);
		}
	printf("\n");
}

/// <summary>
/// prints first n primes
/// </summary>
/// <param name="n">number of primes to print</param>
void print_first_primes(int n)
{
	if (n <= 0)
	{
		printf("N must be a non-zero positive.\n");
		return;
	}
	int k = 1;
	printf("Primes : ");
	for (; n > 0; k++)
	{
		if (is_prime(k))
		{
			printf("%i ", k);
			n--;
		}
	}
}

/// <summary>
/// Prints approximated sqrt(n) with p decimals
/// </summary>
/// <param name="n">sqrt argument (positive)</param>
/// <param name="p">number of decimals, p=0..7 </param>
void approx_sqrt(int n, int p)
{
	if (p < 0 || p>7)
	{
		printf("Precision must be in range 0..7\n");
		return;
	}
	if (n < 0)
	{
		printf("Sqrt argument cannot be negative");
		return;
	}
	printf("%.*f\n", p, sqrt(n));
}


/// <summary>
/// prints first n digits of k/m (k<m)
/// </summary>
/// <param name="n">number of digits</param>
/// <param name="k">nominator</param>
/// <param name="m">denominator ??</param>
void print_first_digits_of_fraction(int n, int k, int m)
{
	if (k >= m)
	{
		printf("Fraction value must be <1\n");
		return;
	}
	if (n <= 0)
	{
		printf("Number of digits must be a non-zero positive.\n");
		return;
	}
	printf("0.");
	for (int i = 0; i < n; i++)
	{
		k *= 10;
		while (k < m)
		{
			k *= 10;
			printf("0");
		}
		printf("%i", k / m);
		k = k % m;
	}
}

/// <summary>
/// prints Pascal triangle of length n
/// </summary>
/// <param name="n">number of lines to print</param>
void print_pascal(int n)
{
	int** mat = (int**)malloc((n + 1) * sizeof(int*));

	for (int i = 0; i <= n; i++)
		mat[i] = (int*)malloc((n + 1) * sizeof(int*));

	mat[0][0] = 1;
	for (int i = 1; i <= n; i++)
	{
		mat[i][0] = 1;
		mat[i][i] = 1;
		for (int j = 1; j < i; j++)
			mat[i][j] = mat[i - 1][j - 1] + mat[i - 1][j];
	}

	for (int i = 0; i <= n; i++)
	{
		for (int j = 0; j <= i; j++)
			printf("%i ", mat[i][j]);
		printf("\n");
		free(mat[i]);
	}

	free(mat);
}

/// <summary>
/// prints consecutive numbers that sum to n starting form k, if exists
/// </summary>
/// <param name="n">total sum</param>
/// <param name="k">first number</param>
void print_consecutives_starting_with(int n, int k)
{
	int s = 0, cnt = 0;
	while (s < n)
	{
		s += k;
		k++;
		cnt++;
	}
	k -= cnt;
	if (s == n)
	{
		for (int i = 0; i < cnt; i++)
			printf("%d ", k + i);
		printf("\n");
	}
}

/// <summary>
/// prints all consecutive numbers that add up to n
/// </summary>
/// <param name="n">target number</param>
void print_consecutives_sum(int n)
{
	for (int i = 1; i <= n / 2; i++)
		print_consecutives_starting_with(n, i);
}