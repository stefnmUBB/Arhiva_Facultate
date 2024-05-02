"""
EXEMPLU FISIER INPUT:
# punctele de control
-1 17
1 2
3 4
5 6
7 -8
# nodurile
0 0 0 0 0.5 1 1 1 1
# pentru fiecare t pe pe urmatoarele linii se calculeaza f(t)
0
0.2
0.5
0.7
0.9
"""

class Point:
    def __init__(self,tuplexy):
        self.x=tuplexy[0]
        self.y=tuplexy[1]

    def __mul__(self, f:float):
        return Point((self.x*f, self.y*f))

    def __rmul__(self, f:float):
        return Point((self.x*f, self.y*f))

    def __add__(self, pt):
        return Point((self.x+pt.x, self.y+pt.y))

    def __str__(self):
        return str((self.x, self.y))


def de_boor(d,t,tx):
    n = 3
    N = 2
    r = 0
    for i in range(len(t)-1):
        if t[i] <= tx < t[i + 1]:
            r = i

    assert(n <= r)
    assert (r < n + N)

    dk = [Point((0,0))]*len(d)
    a = [0]*len(d)

    for i in range(r - n, r + 1): dk[i] = d[i]
    #print(list(map(str,dk)))

    for k in range(1,n+1):
        ndk = [Point((0,0))] * len(d)
        for i in range(r-n+k,r+1):
            a[i] = (tx-t[i])/(t[n+1+i-k]-t[i])
            ndk[i] = (1-a[i])*dk[i-1]+a[i]*dk[i]
        dk = ndk
        #print(list(map(str, dk)))
    #print(list(map(str, dk)))
    return dk[r]

if __name__ == '__main__':
    with open("input.txt") as f:
        lines = [l for l in f.readlines() if not l.startswith("#")]
        d = [Point(tuple(map(int, l.split(" ")[:2]))) for l in lines[:5]]
        t = [float(x) for x in lines[5].split()]
        txs =[float(_) for _ in lines[6:]]
    print("Punctele de control:")
    print(list(map(str,d)))
    print("Nodurile:")
    print(t)
    print("Valorile curbei in punctele date:")
    for tx in txs:
        p = de_boor(d,t,tx)
        print(f"r({tx})={p}")

    def do_plot(d, t):
        try:
            import matplotlib.pyplot as plt

            pts = [de_boor(d, t, tx * 0.01) for tx in range(int(100*min(t)), int(100*max(t)))]
            xs = [p.x for p in pts]
            ys = [p.y for p in pts]
            plt.plot(xs, ys)
            #plt.plot(xs, ys,"ro")

            pts = [de_boor(d, t, tx) for tx in txs]
            xs = [p.x for p in pts]
            ys = [p.y for p in pts]
            plt.scatter(xs,ys)

            for i in range(len(txs)):
                plt.annotate(f"t={txs[i]}", (xs[i],ys[i]))

            plt.show()
        except Exception:
            print("Nu se poate realiza plot")

    do_plot(d,t)


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
