bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    B DB 1100_1010b
    A DW 1001_0011_1011_1000b

; 10. Sa se inlocuiasca bitii 0-3 ai octetului B cu bitii 8-11 ai cuvantului A.
segment code use32 class=code
    start:
        mov   AL , [ B ]   ; AL = B = 1100_1010b (CAh)
        and   AL , 0_F0h   ; AL = B & F0h = 1100_0000b (C0h)
        mov   AH , [A+1]   ; AH = HIGH(A) = 1001_0011b (93h = bits 8-15 of A)     
        and   AH , 0_0Fh   ; AH = AH & 0Fh = 0000_0011b (03h)      
        or    AL ,   AH    ; AL = AL | AH = 1100_0011b (C3h)
        mov [ B ],  AL     ; B = AL = C3h
        
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
