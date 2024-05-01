bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 25
    b DB 3
    c DB 9
    d DB 500

; (10*a-5*b)+(d-5*c)
; our code starts here
segment code use32 class=code
    start:
        MOV DX, [d]   ; DX = d = 500
        
        MOV AL, 10    ; AL = 10
        MUL BYTE [a]  ; AX = AL * a = 250
        ADD DX, AX    ; DX = 750
        
        MOV AL, 5     ; AL = 5
        MUL BYTE [b]  ; AX = AL * b = 15
        SUB DX, AX    ; DX = DX – AX = 735
             
        MOV AL, 5     ; AL = 5
        MUL BYTE [c]  ; AX = AL * c = 45
        SUB DX, AX    ; DX = DX – AX = 690
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        