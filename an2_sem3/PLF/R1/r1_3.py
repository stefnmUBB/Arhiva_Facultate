# R1.
#  3a. Să se substituie al i-lea element dintr-o listă.
#  3b. Să se determine diferența a două mulțimi reprezentate sub formă de listă.


# creeaza lista din argumente variabile
# creeaza(1,2,3)     == [1,2,3]
# creeaza(1,2,[3,4]) == [1,2,3,4]
def creeaza(*args):
    lst = []
    for arg in args:
        if isinstance(arg, list):
            lst+=arg
        else:
            lst.append(arg)        
    return lst

# returneaza sublista elementelor cu indicii de la 2 la n
def list_2n(l):
    return l[1:]

# returneaza primul element sin lista
def l1(l):
    return l[0]


print("3a")

# l1..ln = lista
# i      = indicele elementului de substituit
# e      = noua valoare a elementului de pe pos. i

# subst(l1,...,ln,i,e) = \
#
#   creeaza(e,l2,...,ln)          , i=1 si n>1
#   l1 ⊕ subst(l2,...,ln, i-1, e) , i>1 si i<=n
#   l                             , i>n

def subst(l,i,e):
    if i>len(l):
        return l
    if i==1:
        return creeaza(e, list_2n(l))
    return creeaza(l1(l), subst(list_2n(l), i-1, e))


print(subst([],1, 1))
print(subst([1,2,3],5,1))
print(subst([1,2,3,4,5], 1, 10))
print(subst([1,2,3,4,5], 3, 10))



print("3b")    

# a1...an = multimea A
# b1...bm = multimea B

# diff(a1..an, b1..bm) = \
#
#   ∅                         , n=0
#   a1 ⊕ diff(a2..an, b1..bm) , a1 not in {b1..bm}
#   diff(a2..an, b1..bm)      , a1 in {b1..bm}

def diff(a,b):
    if len(a)==0:
        return []
    a1 = l1(a)
    if not a1 in b:
        return creeaza(a1,diff(list_2n(a),b))
    return diff(list_2n(a),b)

print(diff([],[1,2]))
print(diff([4,5],[1,2]))
print(diff([1,2,3,4,5],[1,2,5,7]))





        
