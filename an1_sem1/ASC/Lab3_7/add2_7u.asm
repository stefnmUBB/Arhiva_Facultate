bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

segment data use32 class=data
    a DB 3
    b DW 300
    c DD 100000
    d DQ 1000000

; solve: (c+c+c)-b+(d-a) (unsigned)
segment code use32 class=code
    start:    
        mov  EAX , [ c ] ; EAX = c = 100000       (000186A0h)
        mov  EBX ,  EAX  ; EBX = c = 100000 
        add  EBX ,  EBX  ; EBX = 2*c              (00030D40h)
        add  EAX ,  EBX  ; EAX = c+2*c = 300000   (000493E0h)
        
        xor  EBX ,  EBX  ; EBX = 0
        mov   BX , [ b ] ; (E)BX = b = 300        (012Ch)
        sub  EAX ,  EBX  ; EAX = 3*c - b = 299700 (000492B4h)
        xor  EDX ,  EDX  ; EDX = 0
        
        mov  ECX , [ d ] 
        mov  EBX , [d+4] ; EBX:ECX = d = 1000000  (0:000F4240h)
        
        add  EAX ,  ECX
        adc  EDX ,  EBX  ; EAX:EDX = d + (3*c-b) = 1299700 (0:0013D4F4h) 
        
        xor  EBX ,  EBX  ; EBX = 0
        xor  ECX ,  ECX  ; ECX = 0
        mov   CL , [ a ] ; CL(ECX) = 3
        
        sub  EAX ,  ECX
        sbb  EDX ,  EBX  ; EAX:EDX = d + (3*c-b) - a = 1299697 (0:0013D4F1h)
        
        
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
