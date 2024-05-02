from subprocess import run as prun
import time as stime

def read_file_as_string(path):
    with open(path, 'r') as f:
        return f.read()

def run_single(p, N,M):
    data = prun(f"java Main.java {p} {N} {M}", capture_output=True, shell=True, text=True)
    if data.stderr != "" :
        print(data.stderr)
        exit(-1)
    ok = read_file_as_string("test_result.out")==read_file_as_string(f"tests\\test_{N}x{M}_{3}x{3}.out")
    if not ok:
        print("Matrices are not identical")
        exit(-1)
    time = int([x for x in data.stdout.splitlines() if x.startswith(">>")][0].split(' ')[-1])
    stime.sleep(2)
    return time    

def run_10(p, N,M):    
    def runi(i):        
        #print(f"Test run {i}..........", end="")
        t = run_single(p, N, M)
        print(f"{t}ms".rjust(12), end="", flush=True)
        return t

    times = list(map(runi, range(10)))    
    print(f" | {sum(times)/10}ms")

    return {"times":times, "average":sum(times)/10}
    

configs = [    
    {"N":10,"M":10, "p":[4]},
    {"N":1000,"M":1000,"p":[2,4,8,16]},
    {"N":10000,"M":10000,"p":[2,4,8,16]},    
]

def run_config(config):
    print(f"Running {config}")
    
    print("Sequencial run")    
    times0 = run_10(0, config["N"], config["M"])
    
    for p in config["p"]:
        print(f"Threads: {p}")        
        timesp = run_10(0, config["N"], config["M"])                    
    
def run():
    for c in configs:
        run_config(c)

run()
