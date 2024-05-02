#Un client trimite unui server un sir de caractere. Serverul
#va returna clientului numarul de caractere spatiu din sir.

# SERVER
import socket

def receive_int(s):
    data = s.recv(4)
    return int.from_bytes(data,'little',signed=False)

def send_int(s,x):
    s.send(x.to_bytes(4,'little',signed=False))

def receive_str(s):
    length = receive_int(s)
    data = s.recv(length)
    return data.decode("utf-8")


TCP_IP = "127.0.0.1"
TCP_PORT = 8888

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)
conn, addr = s.accept()

while 1:        
    print('Connection address:', addr)
    data = receive_str(conn)    
    if not data: break
    print("Received ", data)    
    send_int(conn, data.count(" "))
        

conn.close()
