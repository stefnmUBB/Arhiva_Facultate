#(3) Generați toate permmutările de dimensiune n (1..n), cu propritatea că pentru orice i 2<=i<=n
# exista un j, 1<=j<=i astfel încât |v(i)-v(j)|=1.
import time

from solvers.iterative_solver import IterativeSolver
from solvers.recursive_solver import RecursiveSolver

if __name__ == '__main__':
    start = time.time()
    is_iterative = input("iterative? (Y/N) > ") == "Y"
    print("Solving iterative" if is_iterative else "Solving recursive")
    solver =  IterativeSolver() if is_iterative else RecursiveSolver()
    #solver = RecursiveSolver()
    #solver = IterativeSolver()
    solver.input("n", int(input("n = ")))
    try:
        for solution in solver.solve():
            print(str(solution))
    except Exception as e:
        print(e)
    print(f"Time elapsed: {time.time()-start}")



# Recursive n=20: 151.13378477096558 s
# Iterative n=20: 152.96036577224731 s

# Spatiul de cautare = S_n (permutations of length n)
# Solutia candidat = [x1..xk], k<=n, xi=1..n foreach i=1..k, xi!=xj foreach i!=j
# consistent = check if partial sol. [x1..xk] satisfies the condition:
#              foreach i in [1..n] exists j in [1..i-1] such that |x_j-x_i|=1
# solution = check if a partial sol [x1..xk] has k=n

# Spatiul de cautare = [1..n]^n
# Solutia candidat = [x1..xk], k<=n, xi=1..n foreach i=1..k
# consistent = check if partial sol. [x1..xk] satisfies the conditions:
#            (1) foreach i=1..k, xi!=xj foreach i!=j // assure permutation
#            (2) foreach i in [2..k] exists j in [1..i-1] such that |x_j-x_i|=1 // problem requirement
# solution = check if a consistent partial sol [x1..xk] has k=n