import sys, os

print(f"shadmk {sys.argv[1]}")

with open(sys.argv[1], 'r') as f:
    t = f.read()
file_name = os.path.basename(sys.argv[1]).replace(".","_")
print(file_name)

t = t.splitlines()
t = ['"'+x.replace('"','\\"')+'\\n"' for x in t if x!=""]
code = []
code.append("#pragma once")
code.append(f"static const char* {file_name} = ");
for line in t:
    code.append(f"\t{line}");
code.append(";");   

code = "\n".join(code)

#print(code)
with open(os.path.join(sys.argv[2],file_name+".h"), 'w') as f:
    f.write(code)    