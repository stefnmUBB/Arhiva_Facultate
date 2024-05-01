bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 31
    b DB 8
    c DB 4
    d DB 11

; [(a-d)+b]*2/c
; our code starts here
segment code use32 class=code
    start:
        MOV AL, [a]  ; AL = a = 31
        ADD AL, [b]  ; AL = AL + b = 39
        SUB AL, [d]  ; AL = AL – d = 28
        
        MOV BL, 2 ;
        MUL BL       ; AX = 28 * 2 = 56
        DIV BYTE [c] ; AX/=c, AL = q = 14, AH = r = 0
                    
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        
        