from subprocess import run as prun
import time as stime

def read_file_as_string(path):
    with open(path, 'r') as f:
        return f.read()


def run_single(p, mode, pw, pr):    
    cmd = f"Tema4 {mode} {pw} {pr}"    
    data = prun(cmd, capture_output=True, shell=True, text=True)
    if data.stderr != "" :
        print(data.stderr)
        exit(-1)
    #print(cmd)
    #print(data.stdout)
 
    ok = read_file_as_string("result.txt")==read_file_as_string(f"result_corect.txt")
    if not ok:
        print("Matrices are not identical")
        exit(-1)       
    time = float([x for x in data.stdout.splitlines() if x.startswith(">>")][0].split(' ')[-1])
    stime.sleep(1)
    return time    

def run_10(p,  mode, pw, pr):    
    def runi(i):        
        #print(f"Test run {i}..........", end="")
        t = run_single(p, mode, pw, pr)
        print(f"{t}ms".rjust(12), end="", flush=True)
        return t

    times = list(map(runi, range(10)))    
    print(f" | {sum(times)/10}ms")

    return {"times":times, "average":sum(times)/10}
    

configs = [            
    {"mode":"seq","pr":1, "pw":1},    
    
    {"mode":"pll","pr":1, "pw":4},            
    {"mode":"pll","pr":1, "pw":6},    
    {"mode":"pll","pr":1, "pw":8},    
    {"mode":"pll","pr":1, "pw":16},    
    
    {"mode":"pll","pr":2, "pw":4},    
    {"mode":"pll","pr":2, "pw":6},    
    {"mode":"pll","pr":2, "pw":8},    
    {"mode":"pll","pr":2, "pw":16},    
    
    
]

def run_config(config):
    print(f"Running {config}")
    
    #print("Sequencial run")    
    #times0 = run_10(0, config["N"], config["M"])
    timesp = run_10(0, config["mode"], config["pw"], config["pr"])
    
    #for p in config["p"]:
        #print(f"Threads: {p}")        
        
    
def run():
    for c in configs:
        run_config(c)

run()