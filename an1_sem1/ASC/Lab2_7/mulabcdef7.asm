bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 252
    b DB 253
    c DB 254
    d DB 255
    
; (a+b)*(c+d)

; our code starts here
segment code use32 class=code
    start:
        MOV AH, 0   ; AH = BH = CH = 0
        MOV BH, AH ;
        MOV CH, AH ;
        
        MOV AL, [a] ; AL = a = 252        
        MOV BL, [b] ; BL = b = 253
        ADD AX, BX  ; AX = a + b = 505
        
        MOV BL, [c] ; BL = c = 254
        MOV CL, [d] ; CL = d = 255
        ADD BX, CX  ; BX = c + d = 509
        
        MUL BX      ; DX:AX = 3:EC15h = 257045=505*509
        PUSH DX ;
        PUSH AX ;
        POP EAX     ; EAX = DX:AX = 257045
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        