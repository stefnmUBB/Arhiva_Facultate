bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 7
    b DB 3
    c DB 2
    d DB 4

; (a-b-b-c)+(a-c-c-d)

; our code starts here
segment code use32 class=code
    start:
        MOV BL, [b] ; BL = b = 3
        ADD BL, BL  ; BL = 3+3 = 6
        
        MOV CL, [c] ; CL = c = 2
        ADD [d], CL ; d = d+CL = 4+2 = 6
        
        ADD CL, CL  ; CL = 2+2 = 4
        
        MOV AL, [a] ; AL = a = 7
        ADD AL, AL  ; AL = 7+7 = 14
        SUB AL, BL  ; AL = AL – 6 = 8
        SUB AL, CL  ; AL = AL – 4 = 4
        SUB AL, [d] ; AL = AL – d = 4-6 = -2 ($FE)
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        
        
        