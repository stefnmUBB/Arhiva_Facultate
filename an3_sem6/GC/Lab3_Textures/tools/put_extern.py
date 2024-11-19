import sys

print(f"put_extern {sys.argv[1]}")

with open(sys.argv[1], 'r') as f:
    t = f.read()
      
t = t.replace("const", "extern const")

with open(sys.argv[1], 'w') as f:
    f.write(t)