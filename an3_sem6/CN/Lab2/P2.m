"P2";

syms x;
s = taylor(sin(x), x, 0, 'order', 10)
c = taylor(cos(x), x, 0, 'order', 10)

val_s = subs(s, x, 10*sym(pi))
val_c = subs(c, x, 10*sym(pi))

eval(val_s)
eval(val_c)

disp(["sin(10*pi) =", num2str(eval(val_s)), "; cos(10*pi) = ", num2str(eval(val_c))])


# sin(10*pi) =76403121.4251; cos(10*pi) = 22237894.9081

# Eroarea apare din urmatorul motiv:
# - Functia Taylor devine din ce in ce mai inexacta pe masura ce ne indepartam de x0=0
#   Cel mai mic termen neglijat in (Tn_sin) este 15625000*pi^11/6237 > 10^6
#   care nu ofera o marja accepabila a erorii

# Posibila solutie:
# - Reducerea argumentului x la in intervalul [-pi, pi]. sin(x) = sin(x-2*pi*floor(x/(2*pi))).
# - De acordat atentie erorilor de precizie care apar la diferentele in virgula mobila
