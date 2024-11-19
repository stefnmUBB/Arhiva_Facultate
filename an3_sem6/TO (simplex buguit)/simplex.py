import re, sys

def fatal(message):
    print(message)
    exit(-1)
    
def argmax(l):
    m = max(l)
    return [i for i in range(len(l)) if l[i]==m][0]
    
###############################################################################
#                                 Procesare input                             #
###############################################################################

def split_line(line):       
    tokens = re.split(r'([\+\-\=]|x[0-9]+)|\s|\n', line)
    return [tk for tk in tokens if tk is not None and tk!=""]   
    
class ParseContext:
    def __init__(self):
        self.coeffs = {}
        self.pending_sign = 1
        self.pending_coef = 1   
        self.result_sign = 1
        self.result_coef = 1
        self.result=None
    def set_pending(self, s, n):
        if s is not None: self.pending_sign = s
        if n is not None: self.pending_coef = n    
    def set_result(self, s,n):
        if s is not None: self.result_sign = s
        if n is not None: self.result_coef = n    
        self.result = s*n
    def nop(self): pass       
    def push_xi(self, i): 
        if i<0 or i>100: fatal(f"Invalid x{i}")
        self.coeffs[i] = self.pending_sign*self.pending_coef
        self.pending_sign, self.pending_coef = 1, 0
        
    def seal_coeffs(self):
        c = [0]*max(self.coeffs.keys())
        for (i, v) in self.coeffs.items():
            c[i-1]=v
        self.coeffs=c
        
    def negate_coeffs(self):
        self.coeffs = [-c for c in self.coeffs]
        
    def __str__(self): return str((self.coeffs, self.result))
    
FA_eq = {
    ("q0", "+-") : ("q1", lambda ctx, s: ctx.set_pending(s, 1)), 
    ("q0", "N")  : ("q2", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q0", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q1", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q1", "N")  : ("q2", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q2", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q3", "+-") : ("q4", lambda ctx, s: ctx.set_pending(s, 1)), 
    ("q4", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q4", "N")  : ("q5", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q5", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)),     
    ("q3", "=") : ("q6", lambda ctx, e: ctx.nop()), 
    ("q6", "N") : ("q8", lambda ctx, n: ctx.set_result(1, n)), 
    ("q6", "+-") : ("q7", lambda ctx, s: ctx.set_result(s, None)), 
    ("q7", "N") : ("q8", lambda ctx, n: ctx.set_result(None, n)), 
    "QI" : "q0",
    "QF" : ["q8"]
}

FA_obj = {
    ("q0", "+-") : ("q1", lambda ctx, s: ctx.set_pending(s, 1)), 
    ("q0", "N")  : ("q2", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q0", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q1", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q1", "N")  : ("q2", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q2", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q3", "+-") : ("q4", lambda ctx, s: ctx.set_pending(s, 1)), 
    ("q4", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)), 
    ("q4", "N")  : ("q5", lambda ctx, n: ctx.set_pending(None, n)), 
    ("q5", "xi") : ("q3", lambda ctx, i: ctx.push_xi(i)),        
    "QI" : "q0",
    "QF" : ["q3"]
}

def run_FA(FA, tokens):
    def is_float(s):
        try: float(s)
        except ValueError: return False
        else: return True

    ctx = ParseContext()
    tr, arg = None, None
    q = FA["QI"]            
    for tk in tokens:    
        if tk=='-': tr, arg ="+-", -1
        elif tk=='+': tr, arg ="+-", +1
        elif tk=='=': tr, arg ="=", 0
        elif is_float(tk): tr, arg ="N", float(tk)
        elif tk.startswith('x'): tr, arg = "xi", int(tk[1:])
        if not (q, tr) in FA:
            fatal(f"Unable to parse: {tk}")
        (q, action) = FA[(q, tr)]
        action(ctx, arg)
    if not q in FA["QF"]: fatal("Unexpected end of input")
    ctx.seal_coeffs()
    return ctx
    
def pad_to_same_length(A, obj):
    L = max(len(obj), max([len(_) for _ in A]))
    for a in A: a+=[0]*(L-len(a))
    obj += [0]*(L-len(obj))
    return A, obj
    
###############################################################################
#                             Pretty print                                    #
###############################################################################
    
def print_table(lines: list[list[any]], headers: list[str] = None, title: str=""):
    result=""
    if headers is None:
        headers = []
    lines: list[list[str]] = list(map(lambda x: list(map(str, x)), lines))

    cols_count = len(max(lines,key=len))
    cols_count = max(cols_count, len(headers))

    while len(headers)<cols_count:
        headers.append(" ")

    col_span = list(map(len, headers))
    for i in range(len(lines)):
        for j in range(len(lines[i])):
            col_span[j] = max(col_span[j], len(lines[i][j]))

    r_len = sum(col_span) + len(col_span * 3)-2

    if title is not None and len(title)>0:
        if len(title)>r_len:
            col_span[-1] += len(title)-r_len
            r_len = sum(col_span) + len(col_span * 3)-2

        result+="|"
        result+=title.center(r_len)
        result+="|"+"\n"


    result+="|"
    for i in range(len(headers)):
        result+=headers[i].ljust(col_span[i])+" | "
    for i in range(len(headers), cols_count):
        result+="-".ljust(col_span[i]) + " | "
    result+="\n"


    result+="|"
    result+="-"*r_len
    result+="|"+"\n"


    for line in lines:
        result+="|"
        for i in range(len(line)):
            result+=line[i].ljust(col_span[i])+" | "
        for i in range(len(line), cols_count):
            result+="-".ljust(col_span[i]) + " | "
        result+="\n"
    print(result)
    
###############################################################################
#                             Metode ajutatoare                               #
###############################################################################
    
def dot(x, y): return sum([x[i]*y[i] for i in range(len(x))])

def find_primal_base(A):
    N, M = len(A), len(A[0])
    base = []
    for i in range(N):        
        for j in range(M):            
            if A[i][j]!=1: continue    
            
            ok = True
            for k in range(N):
                if k!=i and A[k][j]!=0:
                    ok=False                    
                    break
            if ok: 
                base.append(j)            
                break
        if len(base)==i:                  
            fatal("Could not find primal base. This algorithm only detects the identity matrix as a primal base")                                
    return base    
    
def build_simplex_table(A, B, obj, primal_base, nonprimal_base):    
    t = []
    N, M = len(A), len(A[0])
    PL, NL = len(primal_base), len(nonprimal_base)
    pc = [obj[i] for i in primal_base]
    nc = [obj[i] for i in nonprimal_base]    
    
    for i in range(NL+2):
        t.append([0]*(PL+2))
    for i in range(PL): t[0][i+1]=f"A{primal_base[i]} (c{primal_base[i]}={pc[i]})"        
    for i in range(NL): t[i+1][0]=f"A{nonprimal_base[i]} (c{nonprimal_base[i]}={nc[i]})"        
    t[0][0]=1
    t[0][1+PL]=""
    t[1+NL][0]="b"
    
    for q in range(NL):
        for i in range(N):
            t[1+q][1+i] = A[i][nonprimal_base[q]]
    for i in range(N):
        t[1+NL][1+i]=B[i]
        
    for i in range(NL):        
        t[1+i][1+PL] = dot(t[1+i][1:1+PL], pc)-nc[i]
    t[1+NL][1+PL] = dot(t[1+NL][1:1+PL], pc)    
    return t
    
def is_primal_base_admisible(table):    
    L = len(table)
    return all([table[i][-1]<=0 for i in range(1,L)])
    
def find_pivol_col(table, pivot_row_id):    
    pivot_row_id+=1
    pivot_row=table[pivot_row_id]
    R, C = len(table), len(table[0])
    
    r=(0,0,1e9, 0)
    
    for i in range(1, R-1):
        if i==pivot_row_id: continue
        for j in range(1, C-1):
            if table[pivot_row_id][j]<=0: continue
            f = table[i][j] / table[pivot_row_id][j]             
            if f<r[2]:
                r = (i-1, j-1, f, 1)
    if r[3]==0:
        fatal("Could not find pivot column")
    return r[1]
    
def recompute_table(table, pivot_i, pivot_j, primal_base, nonprimal_base):
    old_table = [t[:] for t in table]
    R, C = len(table), len(table[0])
    pivot_i+=1
    pivot_j+=1
    piv = table[pivot_i][pivot_j]
    table[pivot_i][pivot_j] = 1/piv
    for i in range(1, R):
        if i==pivot_i: continue
        table[i][pivot_j] /= piv
    for j in range(1, C):
        if j==pivot_j: continue
        table[pivot_i][j] /= -piv        
        
    for i in range(1,R):
        if i==pivot_i: continue
        for j in range(1,C):
            if j==pivot_j: continue
            table[i][j] = (old_table[i][j]*piv - old_table[i][pivot_j]*old_table[pivot_i][j])/piv
            
    nonprimal_base[pivot_i-1], primal_base[pivot_j-1] = primal_base[pivot_j-1], nonprimal_base[pivot_i-1]
    
    table[0][pivot_j], table[pivot_i][0] = table[pivot_i][0], table[0][pivot_j]        
    table[0][0]=2     
    
    return table, primal_base, nonprimal_base

def simplex(A, B, obj):
    primal_base = find_primal_base(A)
    nonprimal_base = list(set(range(len(A[0]))).difference(set(primal_base)))        
    table = build_simplex_table(A, B, obj, primal_base, nonprimal_base)    
    print_table(table, None, "Simplex table")
    if(is_primal_base_admisible(table)):
        print("BDA")
        x0 = [0]*len(A[0])
        for i in range(len(primal_base)): x0[primal_base[i]]=B[i]
        print(f"x0={x0}")
        print(f"Optimal value = {dot(x0, obj)}")        
        return 
    
    print("not BDA")
    print("Checking for at least one positive number on each row... ", end="")    
    R, C = len(table), len(table[0])
    res = all(any([table[i][j]>0 for j in range(1,C-1)]) for i in range(1, R-1))
    print(res)
    if not res: return    
    pivot_row_index = argmax([t[-1] for t in table[1:]])
    print(f"Pivot row = {pivot_row_index}")
    pivot_col_index = find_pivol_col(table, pivot_row_index)
    print(f"Pivot col = {pivot_col_index}")    
    pivot_elem = table[1+pivot_row_index][1+pivot_col_index]
    print(f"Pivot element = {pivot_elem}")    
    
    table, primal_base, nonprimal_base = recompute_table(table, pivot_row_index, pivot_col_index, primal_base, nonprimal_base)
    print_table(table)
    
    if(is_primal_base_admisible(table)):
        print("BDA")
        x0 = [0]*len(A[0])
        for i in range(len(primal_base)): x0[primal_base[i]]=table[-1][1+i]
        print(f"x0={x0}")
        print(f"Optimal value = {dot(x0, obj)}")        
        return             
    print("not BDA")
    print("should loop?")
    
    
if __name__=='__main__':
    if(len(sys.argv)>=2):
        with open(sys.argv[1], 'r') as f:
            lines = f.readlines()                    
            
    A = []
    B = []
    obj = None
            
    for line in lines:    
        tokens = split_line(line)
        if len(tokens)==0: 
            continue        
        if tokens[0]=="min" or tokens[0]=="max":
            ctx = run_FA(FA_obj, tokens[1:])
            if tokens[0]=="max": ctx.negate_coeffs()        
            if obj is not None: fatal("Objective already defined")
            obj = ctx.coeffs            
        else:
            ctx = run_FA(FA_eq, tokens)
            A.append(ctx.coeffs)
            B.append(ctx.result)
    A, obj = pad_to_same_length(A, obj)
    inp = [A[i]+[B[i]] for i in range(len(A))]    
    print_table(inp, ["A" if i==0 else "b" if i==len(A[0]) else "" for i in range(len(A[0])+1)], "Input")
    print(f"c={obj}")        
    simplex(A, B, obj)