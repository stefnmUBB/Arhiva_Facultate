# Proiect Securitate Software : Client FTP
Colaborare cu: https://github.com/mihai12p/ubb/tree/main/sem5/ss/proiect

## Cerinta

Proiectul consta in implementarea protocolului FTP (Fle Transfer Protocol) intr-o aplicatie client-server, respectand standardul in vigoare: https://datatracker.ietf.org/doc/html/rfc959

Aplicatia va fi implementata in limbajul C/C++. Nu sunt permise folosirea altor librarii care nu sunt standard C/C++ sau care deja implementeaza total sau partial protocolul FTP.

Aplicatia (atat client, cat si server) trebuie implementata astfel incat, cel putin urmatoarele functionalitati sa fie functionale pe partea de client:

- conectare / logare
- deconectare / delogare
- listare fisiere
- urcare fisiere
- descarcare fisiere, inclusiv binary (transfer mode) 
- passive

Pentru ca se cere implementarea standardului, partea de client va trebui sa se conecteze la orice alt server FTP care implementeaza standardul, iar partea de server va trebui sa accepte orice alt client FTP care implementeaza standardul.

Accentul in evaluarea implementarii se va pune pe urmatoarele aspecte:
- validarea parametrilor
- validarea input-ului, sanitizare, etc
- evaluarea rezultatelor apelurilor de functii
- utilizarea api-urilor safe/unsafe
- utilizarea api-urilor bounded unde este cazul
- gestionarea corecta a bufferelor
- aspectul estetic al codului
- NU se evalueaza in mod direct eficienta implementarii.

## Comenzi/Functionalitati

- ```login <0=user:STRING> <1=pass:STRING>```

    Logare username, parola

    **Comenzi FTP executate**
    ```
    USER user
    PASS pass
    ```

- ```help```

    Afisare meniu de ajutor

- ```logout```

    Delogare

    **Comenzi FTP executate**
    ```
    QUIT
    ```

- ```list <0=path:STRING>```

    Listare fisiere dintr-un anumit path

    **Comenzi FTP executate**
    ```
    PASV
    LIST path
    ```

    Se primesc N bytes din Data Transfer Port (N=specificat de raspunsul 150)

- ```list```

    Listare fisiere din directorul curent

    **Comenzi FTP executate**
    ```
    PASV
    LIST
    ```


- ```put <0=path:STRING>```

    Incarcare fisier la pathul specificat

    **Comenzi FTP executate**
    ```
    PASV
    STOR path
    ```

    Pe Data Transfer Port se trimite continutul fisierului si se inchide socketul.

- ```get <0=path:STRING>```

    Descarcare fisier de la pathul specificat

    **Comenzi FTP executate**
    ```
    PASV
    RETR path
    ```

    Se primesc N bytes din Data Transfer Port (N=specificat de raspunsul 150)

- ```binary```

    Seteaza modul de transfer ca binar/image type

    **Comenzi FTP executate**
    ```
    TYPE I
    ```

- ```ascii```

    Seteaza modul de transfer ca ASCII (se gestioneaza transmiterea corecta a caracterelor sfarsit de linie intre sisteme)

    **Comenzi FTP executate**
    ```
    TYPE A
    ```

## Comenzi FTP implementate

- `USER`
- `PASS`
- `QUIT`
- `PASV`
- `LIST`
- `STOR`
- `RETR`
- `TYPE`

## Resurse

- [RFC959](https://datatracker.ietf.org/doc/html/rfc959)
- http://www.tcpipguide.com/free/t_TCPIPFileandMessageTransferApplicationsandProtocol.htm - mai bine explicat protocolul
- Clientul a fost testat impreuna cu serverul FTP Xlight: https://www.xlightftpd.com/
