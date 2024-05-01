bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    
    a DB 2048
    b DB 512
    c DB 1024
    d DB 4093
    
    
; [100*(d+3)-10]/d

; our code starts here
segment code use32 class=code
    start:
        MOV BX, 100  ;
        MOV AX, [d]  ; AX = 0FFDh
        ADD AX, 3    ; AX = 1000h
        MUL BX       ; DX:AX = 6:4000h
        PUSH DX ;
        PUSH AX ;
        POP EAX      ; EAX = 00064000h
        SUB EAX,10   ; EAX = 00063FF6h
        
        PUSH EAX     ; >> DX:AX = EAX
        POP AX       ; AX = 3FF6h
        POP DX       ; DX = 0006h
        DIV WORD [d] ; AX = q = 100, DX = r = 290
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        