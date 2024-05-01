bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DW 500
    b DW 200
    c DW 300
    d DW 400
    
; (c+c+c)-b+(d-a)

; our code starts here
segment code use32 class=code
    start:
        MOV AX, [c] ; AX = c = 300
        ADD AX, [c] ; AX = AX + c = 600
        ADD AX, [c] ; AX = AX + c = 900
        
        SUB AX, [b] ; AX = AX – b = 900 – 200 = 700
        ADD AX, [d] ; AX = AX + d = 700 + 400 = 1100
        SUB AX, [a] ; AX = AX – a = 1100 – 500 = 600
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        
        
        
        