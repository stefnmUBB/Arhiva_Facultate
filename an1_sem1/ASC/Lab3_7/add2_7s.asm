bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

segment data use32 class=data

a DB    -10
b DW    430
c DD 100000    
d DQ    -18
x RESQ 1

; solve: (c+c+c)-b+(d-a)
segment code use32 class=code
    start:
        mov   AL , [ a ] ;     AL = a = -10      (F6h)   
        cbw              ;     AX = a = -10      (FFF6h)
        cwd              ;  DX:AX = a = -10
        push  DX
        push  AX
        pop  EAX         ;     EAX = a = -10     (FFFFFFF6h)
        cdq              ; EDX:EAX = a = -10     (FFFFFFFF:FFFFFFF6h)
        
        mov  EBX ,  EDX 
        mov  ECX ,  EAX  ; EBX:ECX = a = -10     (FFFFFFFF:FFFFFFF6h)
        
        mov  EAX , [ d ] 
        mov  EDX , [d+4] ; EDX:EAX = d = -18     (FFFFFFFF:FFFFFFEEh)
        
        sub  EAX , ECX   ;                       (FFFFFFF8h, c=1)
        sbb  EDX , EBX   ; EDX:EAX = d - a = -8  (FFFFFFFF:FFFFFFF8h)
        
        push EAX
        push EDX         ; backup (d-a)           
        
        mov  ECX , [ c ] ;     ECX = c     = 100000 (000186A0h)
        mov  EBX ,  ECX
        add  EBX ,  EBX  ;     EBX = c+c   = 200000 (00030D40h)                 
        add  ECX ,  EBX  ;     ECX = c+c+c = 300000 (000493E0h)
        
        mov   AX , [ b ] ;      AX =  b    =  430   (01AEh)
        neg   AX         ;      AX = -b    = -430   (FE52h)
        cwd              ;   DX:AX = -b    = -430
        push  DX
        push  AX
        pop  EAX         ;     EAX = -b      = -430 (FFFFFE52h)
        add  EAX ,  ECX  ;     EAX = 3*c - b =  299570 (00049232h)
        cdq              ; EDX:EAX = 3*c - b =  299570 (0:00049232h)
        mov  EBX ,  EDX  
        mov  ECX ,  EAX  ; EBX:ECX = EDX:EAX =  299570 (0:00049232)
        
        pop  EDX
        pop  EAX         ; restore (d-a)
        
        add  EAX ,  ECX  ;                                  (0004922Ah, c=0)
        adc  EDX ,  EBX  ; EAX:EDX = (d-a)+(3*c-b) = 299562 (0:0004922Ah)
        
        mov [ x ],  EAX
        mov [x+4],  EDX
        
        
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        
        
        