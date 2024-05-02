from __future__ import annotations
from math import sqrt, cos, sin

class Matrix4:
    def __init__(self, *args):
        self.items = [0]*16
        for i in range(min(16, len(args))):
            self.items[i]=args[i]

    def __getitem__(self, pos):
        i, j = pos
        return self.items[4 * i + j]

    def __setitem__(self, pos, value):
        i, j=pos
        self.items[4 * i + j]=value

    def __str__(self):
        s=""
        for i in range(4):
            s += str(self.items[4*i:4*i+4])+"\n"
        return s

    def _scalar_mul_(self, x:float) -> Matrix4: return Matrix4([x*i for i in self.items])

    def _matrix4_mul_(self, m:Matrix4) -> Matrix4:
        r = Matrix4()
        for i in range(4):
            for j in range(4):
                for k in range(4):
                    r[i,j]+=self[i,k]*m[k,j]
        return r

    def _vec4_mul(self, v:tuple[float]):
        r = [0]*4
        v = (v[0], v[1], v[2], 1)
        for i in range(4):
            for j in range(4):
                r[i] += self[i,j]*v[j]
        return tuple(r)

    def __mul__(self, other):
        if isinstance(other, float) or isinstance(other, int): return self._scalar_mul_(other)
        if isinstance(other, Matrix4): return self._matrix4_mul_(other)
        if isinstance(other, tuple): return self._vec4_mul(other)
        raise Exception("Unsupported operation: invalid multiplication type")

    def __rmul__(self, other):
        if isinstance(other, float) or isinstance(other, int): return self._scalar_mul_(other)
        if isinstance(other, Matrix4): return other._matrix4_mul_(self)
        raise Exception("Unsupported operation: invalid multiplication type")

    def apply(self, pts): return [self * p for p in pts]

def TranslationMatrix(p):
    x, y, z = p
    return Matrix4(1,0,0,x, 0,1,0,y, 0,0,1,z, 0,0,0,1)

def CosSinRotXMatrix(c,s): return Matrix4(1,0,0,0, 0,c,-s,0, 0,s,c,0, 0,0,0,1)
def RotationXMatrix(thetaX): return CosSinRotXMatrix(cos(thetaX), sin(thetaX))

def CosSinRotYMatrix(c,s): return Matrix4(c,0,s,0, 0,1,0,0, -s,0,c,0, 0,0,0,1)
def RotationYMatrix(thetaY): return CosSinRotYMatrix(cos(thetaY), sin(thetaY))

def CosSinRotZMatrix(c,s): return Matrix4(c,-s,0,0, s,c,0,0, 0,0,1,0, 0,0,0,1)
def RotationZMatrix(thetaZ): return CosSinRotZMatrix(cos(thetaZ), sin(thetaZ))

def ReflectYZMatrix(): return Matrix4(-1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1)

def gen_sphere(p, r):
    pts = []

    x,y,z=p
    for t in range(0, 5):
        at = 6.28 * t/5
        for p in range(0, 5):
            ap = 3.14 * p/5
            xx = x+r*cos(at)*cos(ap)
            yy = y + r * cos(at) * sin(ap)
            zz = z + r * sin(at)
            pts.append((xx,yy,zz))

    return pts

"""
out : lista de tupluri (x,y,z)
"""
def read_points():
    r = []
    n = int(input("Introduceti numarul de puncte : "))
    for i in range(n):
        print(f"Punctul {i+1}")
        x = float(input("x = "))
        y = float(input("y = "))
        z = float(input("z = "))
        r.append((x,y,z))
    return r

"""
out: planul (a,b,c,d)
"""
def read_plane():
    print("Introduceti planul:")
    method = input("1 pentru Ecuatia generala, 2 pentru punct & vector director: ")
    if method.startswith("1"):
        print("Precizati parametrii ecuatiei generale a planului ax+by+cz+d=0:")

        while True:
            a = float(input("a = "))
            b = float(input("b = "))
            c = float(input("c = "))
            d = float(input("d = "))

            if a==0 and b==0 and c==0 and d!=0:
                print(f"Equatia planului invalida: {d}=0")

            return (a, b, c, d)
    else:
        print("Precizati coordonatele punctului:")
        px = float(input("px = "))
        py = float(input("py = "))
        pz = float(input("pz = "))
        while True: # ask for input until it is correct
            print("Precizati componentele vectorului normal:")
            a = float(input("a = "))
            b = float(input("b = "))
            c = float(input("c = "))
            norm = sqrt(a**2+b**2+c**2)
            if norm==0:
                print("Vectorul nul nu este acceptat. Va rugam reincercati")
            a /= norm
            b /= norm
            c /= norm
            print(f"Vectorul normalizat este ({a} {b} {c})")
            d = -(a*px+b*py+c*pz)
            print(f"Ecuatia planului este {a}*x + {b}*y + {c}*z + {d} = 0")
            return (a, b, c, d)

"""
    in p: planul (a,b,c,d)
    out: (point, v), point = punctul de intersectie, v = versor Ox, Oy sau Oz
"""
def intersectAxis(p):
    a,b,c,d = p
    if a!=0: return ((-d/a,0,0), (1,0,0))
    if b!=0: return ((0,-d/b,0), (0,1,0))
    if c!=0: return ((0,0,-d/c), (0,0,1))
    if d!=0: raise Exception("Runtime error: plan invalid d=0, d!=0")
    return ((0, 0, 0), (1, 0, 0)) # a=b=c=0, d=0 triat la input

def neg(p):
    x,y,z = p
    return (-x,-y,-z)

if __name__ == '__main__':
    plane = read_plane() #(2,1,3,-10) #read_plane()
    a,b,c,d = plane

    transforms = []
    inv_transforms = []

    print("Calculam intersectia cu una dintre axe")
    i_point, i_axis = intersectAxis(plane)
    print(f"Planul intersecteaza axa data de versorul {i_axis} in punctul {i_point}")

    print("Determinam matricea de translatie care face planul sa treaca prin origine:")

    if d!=0:
        translate_o = TranslationMatrix(neg(i_point))
        print(translate_o)
        transforms.append(translate_o)
        inv_transforms.append(TranslationMatrix(i_point))
    else:
        print("Planul este deja in origine")

    print("Determinam matricea de rotatie fata de o axa care face ca normala \n"
          "la plan sa fie paralela cu un plan de coordonate:")
    print(">> Rotatie in jurul axei Ox pentru a face normala || cu planul xOy aka perp. pe axa Oz")

    if c!=0:
        norm_x = sqrt(b**2+c**2)
        cos_tx = b/norm_x
        sin_tx = -c/norm_x
        rot_x = CosSinRotXMatrix(cos_tx, sin_tx)
        print(rot_x)
        transforms.append(rot_x)
        inv_transforms.append(CosSinRotXMatrix(cos_tx, -sin_tx))
    else:
        rot_x = RotationXMatrix(0)
        print("Normala este deja paralela cu axa Oz")

    print("Determinam matricea de rotatie fata de alta axa care face ca planul \n"
          "sa coincida cu un plan de coordonate")
    print(">> Rotatie in jurul axei Oz pentru a face planul || cu planul yOz")

    new_a, new_b,_,_ = rot_x.apply([(a,b,c)])[0]

    if new_b != 0:
        norm_z = sqrt(new_a**2+new_b**2)
        cos_tz = new_a/norm_z
        sin_tz = -new_b/norm_z
        rot_z = CosSinRotZMatrix(cos_tz, sin_tz)
        print(rot_z)
        transforms.append(rot_z)
        inv_transforms.append(CosSinRotZMatrix(cos_tz, -sin_tz))
    else:
        print("Planul este deja paralel cu axa Oz")


    print("Planul se transforma in planul yOz")
    print("Matricea de reflexie fata de YZ:")

    refl_yz = ReflectYZMatrix()
    print(refl_yz)
    transforms.append(refl_yz)

    print("Matricile inverse transformarilor planului (in ordinea inversa mentionarii lor)")

    for m in inv_transforms[::-1]:
        print(str(m)+"\n")

    transforms = (transforms+ inv_transforms[::-1])[::-1]

    full_t = Matrix4(1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1)
    for t in transforms:
        full_t *= t

    print("Matricea transformarii compuse")
    print(full_t)

    print()
    pts = read_points() #gen_sphere((0, 0, 0), 1)

    pts_t = full_t.apply(pts)

    print("Matricea coordonatelor omogene")
    for j in range(3):
        for i in range(len(pts_t)):
            print(f"{pts_t[i][j]:.2f}".rjust(6,' '), end=" ")
        print("")
    for i in range(len(pts_t)):
        print(f"{1}".rjust(6, ' '), end=" ")
    print("\n")

    do_plot = input("Doriti plotarea coordonatelor? (0=nu, 1=da) : ")=="1"

    if not do_plot: exit(0)

    import matplotlib.pyplot as plt
    import numpy as np


    def plot_axes(ax):
        x, y, z = np.zeros((3, 1))
        u, v, w = np.array([5, 0, 0])
        ax.quiver(x, y, z, u, v, w, arrow_length_ratio=0.1, color='r')
        u, v, w = np.array([0, 5, 0])
        ax.quiver(x, y, z, u, v, w, arrow_length_ratio=0.1, color='g')
        u, v, w = np.array([0, 0, 5])
        ax.quiver(x, y, z, u, v, w, arrow_length_ratio=0.1, color='b')


    def plot_points(ax, pts, m="o", c="r"):
        for p in pts:
            ax.scatter(p[0], p[1], p[2], marker=m, color=c)


    def plot_surface(ax, pts):
        x = np.array([p[0] for p in pts])
        y = np.array([p[1] for p in pts])
        z = np.array([p[2] for p in pts])
        ax.scatter(x, y, z)


    def plot_plane(fig, p):
        a, b, c, d = p
        if c != 0:
            x = np.linspace(-5, 5, 10)
            y = np.linspace(-5, 5, 10)
            X, Y = np.meshgrid(x, y)
            Z = (-d - a * X - b * Y) / c
        elif b != 0:
            x = np.linspace(-5, 5, 10)
            z = np.linspace(-5, 5, 10)
            X, Z = np.meshgrid(x, z)
            Y = (-d - a * X) / b
        elif a != 0:
            y = np.linspace(-5, 5, 10)
            z = np.linspace(-5, 5, 10)
            Y, Z = np.meshgrid(y, z)
            X = (-d - 0 * Y - 0 * Z) / a
        else:
            y = np.linspace(-5, 5, 10)
            z = np.linspace(-5, 5, 10)
            Y, Z = np.meshgrid(y, z)
            X = (0 - 0 * Y - 0 * Z)

        ax.plot_surface(X, Y, Z)


    fig = plt.figure()
    ax = fig.add_subplot(projection='3d')
    plot_plane(ax, plane)

    plot_points(ax, pts, c="r")
    plot_points(ax, pts_t, c="b")

    plot_axes(ax)
    plt.show()
