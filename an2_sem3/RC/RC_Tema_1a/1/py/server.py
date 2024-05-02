# Un client trimite unui server un sir de numere.
# Serverul va returna clientului suma numerelor primite.

# SERVER
import socket

TCP_IP = "127.0.0.1"
TCP_PORT = 8888

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)
conn, addr = s.accept()

sum = 0

while 1:        
    print('Connection address:', addr)
    data = conn.recv(4)
    data = int.from_bytes(data,'little',signed=False)
    print("Received ", data)
    if not data: break
    sum+=int(data)        	
    if data==0: break;
data = sum.to_bytes(4,'little',signed=False)
print("Sending sum :", sum)
conn.send(data)
conn.close()
