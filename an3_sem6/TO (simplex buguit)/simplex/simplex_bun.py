import re, sys

def fatal(message):
    print(message)
    exit(-1)
    
def argmax(l):
    m = max(l)
    return [i for i in range(len(l)) if l[i]==m][0]
    
def dot(x, y): return sum([x[i]*y[i] for i in range(len(x))])
    
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
        if self.result_sign is not None and self.result_coef is not None: self.result = self.result_sign*self.result_coef
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
    return result
    
###############################################################################
#                                Helpers                                      #
###############################################################################
    
def swap_rows(A, b, i, j):
    A[i], A[j] = A[j], A[i]
    b[i], b[j] = b[j], b[i]
    
def add_rows(A, b, i1, i2, f): # A[i1] += f*A[i2]
    for j in range(len(A[0])):
        A[i1][j]+=f*A[i2][j]
    b[i1]+=f*b[i2]    

def mul_row(A, b, i, f):
    for j in range(len(A[0])):
        A[i][j]*=f
    b[i]*=f
    
# gaussian elimination but make canonical base in the top left corner of the system matrix
def gaussian_elimination_rec(A, b, i, n):    
    if i==n: return
    
    if A[i][i]==0:        
        for k in range(i+1,n):
            if A[k][i]!=0:
                swap_rows(A, b, i,k)
                break
    mul_row(A, b, i, 1/A[i][i])        
        
    for j in range(i+1,n):
        add_rows(A, b, j, i, -A[j][i]) 
        
    for j in range(0, i):
        if A[j][i]!=0:             
            add_rows(A, b, j, i, -A[j][i]/A[i][i])          
    gaussian_elimination_rec(A, b, i+1, n)        
    
class SimplexTable:
    def __init__(self, n_crt, primal_base, nonprimal_base, c):
        self.c = c
        self.n_crt = n_crt
        self.primal_base = primal_base
        self.nonprimal_base = nonprimal_base
        self.rows_count = len(nonprimal_base) + 1
        self.cols_count = len(primal_base)+1        
        #print(self.rows_count, self.cols_count)
        #print(primal_base)
        #print(nonprimal_base)
        #print(list(map(lambda i:f"A{primal_base[i]} (c{primal_base[i]}={c[primal_base[i]]})", range(0,self.cols_count-1))))
        
        self.row_headers = [str(self.n_crt)] + list(map(lambda i:f"A{primal_base[i]} (c{primal_base[i]}={c[primal_base[i]]})", range(0,self.cols_count-1)))+[""]
        self.col_headers = list(map(lambda i:f"A{nonprimal_base[i]} (c{nonprimal_base[i]}={c[nonprimal_base[i]]})", range(0,self.rows_count-1)))+["b"]        
        
        self.primal_base_labels = list(map(lambda i:f"A{primal_base[i]}", range(0,self.cols_count-1)))
        self.dual_base_labels = list(map(lambda i:f"A{nonprimal_base[i]}", range(0,self.rows_count-1)))
        
        self.data = []
        for i in range(self.rows_count):
            self.data.append([0]*self.cols_count)        

    def __str__(self):          
        t = []
        for i in range(self.rows_count):
            t.append([self.col_headers[i]] + list(map(str, self.data[i])))
        return print_table(t, self.row_headers, "Simplex table")        
        
    def __getitem__(self, key): 
        if isinstance(key, int): return self.data[key]
        if isinstance(key, tuple): return self.data[key[0]][key[1]]
        raise ValueError(f"Invalid access key: {key}")
        
    def __setitem__(self, key, value):        
        if isinstance(key, tuple): 
            self.data[key[0]][key[1]]=value
            return
        if isinstance(key, int): 
            if not hasattr(value, "__getitem__"):
                raise ValueError(f"Invalid value to assign to row: {value}")
            if len(value)!=self.cols_count:
                raise ValueError(f"Invalid length: {len(value)}, expected {self.cols_count}")
            for i in range(self.cols_count):
                data[key][i] = value[i]
            return
        raise ValueError(f"Invalid access key: {key}")
        
    def populate_from_system(self, A, b, c, base_fix=None):
        if base_fix is None:
            for j in range(self.cols_count-1):
                for i in range(len(self.nonprimal_base)):            
                    self[i,j] = A[j][self.nonprimal_base[i]] * A[j][j] # A[j][j] is 1 or -1
                self[self.rows_count-1,j] = b[j] * A[j][j]
            for i in range(self.rows_count):
                for j in range(self.cols_count-1):
                    self[i, self.cols_count-1] += self[i][j]*c[self.primal_base[j]]                
                if i<self.rows_count-1:
                    self[i, self.cols_count-1] -= c[self.nonprimal_base[i]]
        else:
            k, col = base_fix["k"], base_fix["col"]
            for j in range(self.cols_count-1):
                for i in range(len(self.nonprimal_base)):            
                    self[i,j] = A[j][self.nonprimal_base[i]] + col[j]*A[k][self.nonprimal_base[i]]
                self[self.rows_count-1,j] = b[j] + col[j]*b[k]
            for i in range(self.rows_count):
                for j in range(self.cols_count-1):
                    self[i, self.cols_count-1] += self[i][j]*c[self.primal_base[j]]                
                if i<self.rows_count-1:
                    self[i, self.cols_count-1] -= c[self.nonprimal_base[i]]
            
            
    
    def is_primal_admissible_base(self):
        n = self.rows_count-1
        for j in range(self.cols_count-1):
            if self[n,j]<0: return False
        return True        
    
    def is_dual_admissible_base(self):
        m = self.cols_count-1
        for i in range(self.rows_count-1):
            if self[i,m]>0: return False
        return True        
        
    def gauss_jordan(self, old_table, pr, pc):
        n = self.rows_count
        m = self.cols_count        
        for i in range(n):
            for j in range(m):
                if i==pr or j==pc: continue
                self[i,j] = (old_table[pr,pc]*old_table[i,j] - old_table[pr,j]*old_table[i,pc])/old_table[pr,pc]        
    
    def primal(self):        
        print("Applying primal simplex")
        m = self.cols_count-1
        n = self.rows_count-1        
        for i in range(self.rows_count-1):            
            if self[i,m]>0: 
                has_pos=False
                for j in range(0, self.cols_count-1):
                    if self[i,j]>0: has_pos=True                                        
                if not has_pos:
                    print(f"Failed to find positive numbers on row {i}. No solution")
       
        piv_r, piv_c, piv_value = -1, -1, 0       
        for i in range(self.rows_count-1):
            if self[i,m]>piv_value:
                        piv_r, piv_value = i, self[i,j]               
       
        if piv_value==0: fatal("No solution")        
                    
        
        r = {}        
        for j in range(m):
            if self[piv_r, j]>0:
                r[j] = self[n,j] / self[piv_r, j]
        print(r)        
        piv_c = min(r, key=r.get)
        replace_col_index = piv_c
        replace_row_index = piv_r
        print(replace_col_index)
        print(f"Pivot: {(piv_r, piv_c)}")            
        
        new_primal_base = self.primal_base.copy()
        new_nonprimal_base = self.nonprimal_base.copy()
        
        new_primal_base[replace_col_index], new_nonprimal_base[replace_row_index] = new_nonprimal_base[replace_row_index], new_primal_base[replace_col_index]
        
        print(new_primal_base)
        print(new_nonprimal_base)
                       
        new_table = SimplexTable(self.n_crt+1, new_primal_base, new_nonprimal_base, self.c)                
        new_table[piv_r, piv_c] = 1/piv_value        
        for i in range(0, n+1):
            if i!=piv_r:
                new_table[i, piv_c] = self[i, piv_c] / piv_value        
        for j in range(0, m+1):
            if j!=piv_c:
                new_table[piv_r, j] = -self[piv_r, j] / piv_value        
        new_table.gauss_jordan(self, piv_r, piv_c)        
        return new_table        
        
    def dual(self):        
        print("Applying dual simplex")
        m = self.cols_count-1
        n = self.rows_count-1        
            
        for j in range(self.cols_count-1):
            if self[n,j]<0:
                has_neg=False
                for i in range(self.rows_count-1):
                    if(self[i,j]<0): has_neg=True                   
                if not has_neg:
                    fatal(f"Failed to find negative numbers on column {j}. No solution")
        
        piv_r, piv_c, piv_value = -1, -1, 0        
        for j in range(self.cols_count-1):
            if self[n,j]<0:
                if self[n,j]<piv_value:
                    piv_c, piv_value = j, self[n,j]
        
        if piv_value==0:
            fatal("No solution")        
                
        r = {}        
        for i in range(n):
            if self[i, piv_c]<0:
                r[i] = self[i,m] / self[i, piv_c]
        print(r)        
        piv_r = min(r, key=r.get)     
        replace_row_index = piv_r
        replace_col_index = piv_c
        piv_value = self[piv_r, piv_c]
        
        print(f"Pivot: {(replace_row_index, replace_col_index)}")            
        
        print(replace_col_index)
        
        new_primal_base = self.primal_base.copy()
        new_nonprimal_base = self.nonprimal_base.copy()
        print(replace_row_index, replace_col_index)
        
        new_primal_base[replace_col_index], new_nonprimal_base[replace_row_index] = new_nonprimal_base[replace_row_index], new_primal_base[replace_col_index]
        
        print(new_primal_base)
        print(new_nonprimal_base)
                       
        new_table = SimplexTable(self.n_crt+1, new_primal_base, new_nonprimal_base, self.c)                
        new_table[piv_r, piv_c] = 1/piv_value        
        for i in range(0, n+1):
            if i!=piv_r:
                new_table[i, piv_c] = self[i, piv_c] / piv_value        
        for j in range(0, m+1):
            if j!=piv_c:
                new_table[piv_r, j] = -self[piv_r, j] / piv_value        
        new_table.gauss_jordan(self, piv_r, piv_c)        
        return new_table
        
        
    
class Simplex:
    def __init__(self, A, b, c):
        self.A, self.b, self.c = A, b, c                
        self.n, self.m = len(self.A), len(self.A[0])      
        self.c0 = self.c.copy()
                  
    def gaussian_elimination(self): 
        gaussian_elimination_rec(self.A, self.b, 0, min(self.n,self.m))        
            
    def fix_primal_base(self):
        if all(map(lambda t:t>=0, self.b)):
            return                
        print("Could not choose primal base. Restructuring the problem")
        if(all(map(lambda t:t<0, self.b))):
            print("Ensuring primal base failed")
            print("At least one right hand side result must be >=0!")
            return
        k = [t for t in range(len(self.b)) if self.b[t]==max(self.b)][0]
        col = [0]*len(self.b)
        for i in range(len(self.b)):
            if i==k or self.b[i]>0: continue
            col[i] = -self.b[i]/self.b[k]        
        print(k, col)        
        self.base_fix = {"k":k, "col":col}        
        
        #self.signfix = list(map(lambda t:1 if t>=0 else -1, self.b))        
        self.print_new_vars()
         
        for i in range(len(self.b)):       
            self.A[i][k]-=col[i]             
        self.c[k] -= dot(self.c, col)
        #self.c = [self.c[i]-self.c[k]*col[i] for i in range(len(self.c))]                
        self.print_input("Input (x->z)")    
        
        
        
    def print_new_vars(self):        
        k = self.base_fix["k"]
        c = self.base_fix["col"]
        c += [0]*(len(self.A[0])-len(self.A))
        def disp(i):             
            if i==k or c[i]==0: return f"    z{i} = x{i}"                   
            return f"    z{i} = x{i} {'-' if c[i]<0 else '+'} {abs(c[i])}*x{k}"        
        print(f"New vars:")  
        print('\n'.join(map(disp, range(len(self.A[0])))))
        print()
    
                
    def validate_admisible_primal_base(self):
        if self.n>self.m: fatal("Overdetermined system.")        
        for i in range(self.n):
            if self.A[i][i]==0:
                fatal("Failed to find primal base")
        if self.n == self.m:
            print(f"Unique solution x={b}")            
            print(f"Cost {dot(c,b)}")                        
            exit(0)           
     
    def solve(self):
        self.print_input()    
        # build the canonical base on the first column
        self.gaussian_elimination()        
        self.print_input("Input (Gauss Elim.)")    
        self.base_fix = None
        self.fix_primal_base()
        self.validate_admisible_primal_base()
        table = SimplexTable(1, list(range(0, self.n)), list(range(self.n, self.m)), self.c)                        
        table.populate_from_system(self.A, self.b, self.c, self.base_fix)
        print(table)               
        for i in range(3):      
            pab = table.is_primal_admissible_base()        
            dab = table.is_dual_admissible_base()
            
            print(f"{table.primal_base_labels} {'' if pab else 'not'} BPA")        
            print(f"{table.primal_base_labels} {'' if dab else 'not'} BDA")                      
            if pab and dab:              
                print(f"{table.primal_base_labels} B Opt")
                print(f"Solution found!")
                
                x = [0]*len(self.A[0])                
                for i in range(len(table.primal_base)):
                    x[table.primal_base[i]] = table[table.rows_count-1][i]
                 
                if self.base_fix is not None:
                    k, col = self.base_fix["k"], self.base_fix["col"]
                    print(f"z={x}")
                    x0 = x.copy()
                    for i in range(len(self.A)):
                        x0[i] = x[i]-col[i]*x[k]
                    print(f"Cost = {dot(x,self.c)}")
                    x=x0
                print(f"x={x}")
                print(f"Cost = {dot(x,self.c0)}")                
                
                return            
            if dab:  
                table=table.dual()
                print(table)                
            elif pab:                
                table = table.primal()
                print(table)
            else:
                fatal("no solution?")
        
        
    
    def print_input(self, caption="Input"):
        inp = [self.A[i]+[self.b[i]] for i in range(len(A))] 
        print(print_table(inp, ["A" if i==0 else "b" if i==len(self.A[0]) else "" for i in range(len(self.A[0])+1)], caption))
        print(f"c={self.c}")      
        

    
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
    s = Simplex(A, B, obj)
    s.solve()   
    
    #simplex(A, B, obj)