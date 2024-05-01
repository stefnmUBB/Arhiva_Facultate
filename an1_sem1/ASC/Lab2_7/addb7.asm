bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 4
    b DB 1
    c DB 15
    d DB 3
    
; c-(d+d+d)+(a-b)

; our code starts here
segment code use32 class=code
    start:
        MOV AL, [c] ; AL = c = 15
        
        SUB AL, [d] ; AL = AL-d = 12
        SUB AL, [d] ; AL = AL-d = 9
        SUB AL, [d] ; AL = AL-d = 6
        
        MOV BL, [b] ; BL = b = 1
        SUB [a], BL ; a = a-BL = 4-1 = 3
        ADD AL, [a] ; AL = AL+a = 6+3 = 9
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        
        
        
        