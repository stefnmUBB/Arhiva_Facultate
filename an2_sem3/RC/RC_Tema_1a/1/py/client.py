# Un client trimite unui server un sir de numere.
# Serverul va returna clientului suma numerelor primite.

# CLIENT
import socket

TCP_IP = "127.0.0.1"
TCP_PORT = 8888

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

while 1:
    MESSAGE = int(input("Send a number "))
    s.send(MESSAGE.to_bytes(4, 'little', signed=False))    
    print("Sent.")
    if(MESSAGE==0): break;

data = s.recv(4)
data = int.from_bytes(data,'little',signed=False)
s.close()

print("The sum of sent numbers is ", int(data))
