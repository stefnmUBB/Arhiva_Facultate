#Un client trimite unui server un sir de caractere. Serverul
#va returna clientului numarul de caractere spatiu din sir.

# CLIENT
import socket

def receive_int(s):
    data = s.recv(4)
    return int.from_bytes(data,'little',signed=False)

def send_int(s,x):
    s.send(x.to_bytes(4,'little',signed=False))

def send_str(s,text):
    text = text.encode('utf-8')
    send_int(s, len(text))
    if(len(text)>0):
        s.send(text)
    

TCP_IP = "127.0.0.1"
TCP_PORT = 8888

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

while 1:
    MESSAGE = str(input("Enter a text :"))
    if(MESSAGE==""): break
    send_str(s, MESSAGE)
    print("Sent.")    
    data = receive_int(s)
    print("The number of spaces is ", int(data))    
s.close()


