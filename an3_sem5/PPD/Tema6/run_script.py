configs = [
    #{"p_r":4, "p_w":4, "dt":[1,2,4], "dx":[1,2], "send":"send20"},
    #{"p_r":2, "p_w":2, "dt":[1,2,4], "dx":[1,2], "send":"send20"},
    #{"p_r":4, "p_w":2, "dt":[1,2,4], "dx":[1,2], "send":"send20"},
    #{"p_r":4, "p_w":8, "dt":[1,2,4], "dx":[1,2], "send":"send20"},
    
    {"p_r":4, "p_w":4, "dt":[1,2,4], "dx":[1,2], "send":"bytes"},
    {"p_r":2, "p_w":2, "dt":[1,2,4], "dx":[1,2], "send":"bytes"},
    {"p_r":4, "p_w":2, "dt":[1,2,4], "dx":[1,2], "send":"bytes"},
    {"p_r":4, "p_w":8, "dt":[1,2,4], "dx":[1,2], "send":"bytes"},
]

clients_count = 5

def get_server_command(cfg):
    return f"Server.exe {cfg['p_w']} {cfg['dt']} {cfg['p_r']}"
    
def get_client_command(client_id, cfg):
    return f"Client.exe {client_id} {cfg['dx']} {cfg['send']}"


def iterate_cfg(cfg):
    keys = cfg.keys()
    it = []
    static_obj = {}
    
    for key in cfg.keys():
        if isinstance(cfg[key], list):
            it.append({"key":key, "crt":0, "length":len(cfg[key])})    
        else:
            static_obj[key] = cfg[key]                
            
    while True:
        item = static_obj.copy()
        for p in it:
            key = p["key"]
            item[key] = cfg[key][p["crt"]]
        yield item
        c = 1
        for i in range(len(it)):
            it[i]["crt"]+=c
            c=0
            if it[i]["crt"]==it[i]["length"]:
                it[i]["crt"]=0
                c=1                       
            if c==0: break
        if c==1: break
    
def iterate_configs():
    for cfg in configs:
        for c in iterate_cfg(cfg):
            yield c


from subprocess import run as prun
from subprocess import Popen, PIPE, STDOUT


def run_process(command):
    print(">> ", command, end=" ")
    return Popen(command, shell=True, text=True, stdout=PIPE,stderr=STDOUT)
    
def read_file_as_string(path):
    with open(path, 'r') as f:
        return f.read()
    
def compare_files(path1, path2):
    return read_file_as_string(path1)==read_file_as_string(path2)    

from time import sleep

def main():
    for cfg in iterate_configs():
        print(cfg)        
        all_times = [0]*(1+clients_count)
        
        for q in range(10):
            tasks=[]
            times=[0]            
            tasks.append(run_process(get_server_command(cfg)))        
            for i in range(1, clients_count):
                print(get_client_command(i, cfg))
                tasks.append(run_process(get_client_command(i, cfg)))
                sleep(0.5)
            for task in tasks:
                print("Wait")
                task.wait()                            
                lines = []
                for line in task.stdout:                 
                    lines.append(str(line.rstrip()))    
                    #print(line)
                    task.stdout.flush() 
                time = float([x for x in lines if x.startswith(">>>>")][0].split(' ')[-1])    
                times.append(time)
            print("Here?")
            if not compare_files("corect_countries.txt", "countries.txt"):
                print("Countries not identical")
                exit(1)
            if not compare_files("corect_participants.txt", "participants.txt"):
                print("Participants not identical")
                exit(1)
            print()
            print(f"{q+1}/10 : ", times)
            all_times = list(map(sum, zip(all_times,times)))
        all_times = list(map(lambda x:x/10, all_times))
        print("Average : ", all_times)

main()