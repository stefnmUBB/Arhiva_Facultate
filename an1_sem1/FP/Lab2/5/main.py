from math import floor, sqrt

n = int(input('n = '))

# get the next odd number
n += 1 if (n % 2 == 0) else 2

# primes up to n

E = [1, 1] + [0] * (n - 2)

for i in range(2, floor(sqrt(n)) + 1):
    if E[i] == 0:
        for d in range(i * i, n, i):
            E[d] = 1

primes = [p for p in range(2, n) if E[p] == 0]

def isPrime(k):
    if k <= primes[-1]:
        return k in primes

    for p in primes:
        if k % p == 0:
            return False
    primes.append(k)
    return True

while True:
    if isPrime(n) and isPrime(n + 2):
        print(f"{n} {n + 2}")
        break
    n += 2