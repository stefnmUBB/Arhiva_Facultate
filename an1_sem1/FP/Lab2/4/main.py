from math import floor, sqrt
from sys import exit

n = int(input("n = "))

# precompute primes

E = [1, 1] + [0]*(n-2)

for i in range(2, floor(sqrt(n))+1):
    if E[i] == 0:
        for d in range(i*i, n, i):
            E[d] = 1

primes = [p for p in range(2, n) if E[p] == 0]

# print(primes)

# now proceed to solve the problem

p1 = p2 = 0

if n % 2 == 1:
    p1 = 2
    p2 = n-2
    if not (p2 in primes):
        print(f"There is no solution for n = {n}")
        exit()
else:
    lastprime = primes[-1]
    for p1 in primes:
        p2 = n - p1
        if p2 in primes:
            break
        if p1 == lastprime:
            print(f"There is no solution for n = {n}")
            exit()

print(f"p1 = {p1}\np2 = {p2}")
