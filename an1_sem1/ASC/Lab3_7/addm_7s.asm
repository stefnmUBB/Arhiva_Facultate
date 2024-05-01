bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions


segment data use32 class=data

a DB 30
b DB  6
c DW -2
e DD 120
x DQ -400
r RESQ 1

;       practically byte
;             v
; solve: (a-2)/(b+c)+a*c+e-x; a,b-byte; c-word; e-doubleword; x-qword (signed)
segment code use32 class=code
    start:        
        mov    AL , [ a ] ;    AL = a = 30              (1Eh)
        sub    AL ,   2   ;    AL = a-2 = 28            (1Ch)
        cbw               ;    AX = a-2 = 28            (001Ch)
        cwd               ; DX:AX = a-2 = 28            (0000:001Ch)
        push   DX
        push   AX         ; backup; pop when needed        
              
        mov    AL , [ b ] ;   AL = b   = 6              (06h)
        cbw               ;   AX = b   = 6              (0006h)
        add    AX , [ c ] ;   AX = b+c = 4              (0004h)
        
        mov    BX ,   AX  ;   BX = b+c = 4              (0004h)
        pop   EAX         ; retrieve EAX = (a-2) = 28  
        idiv   BX         ; EAX/BX = AX(7) r DX(ignore) (0007h)
        mov    BX ,   AX  ; BX = (a-2)/(b+c) = 7        (0007h)
                
        mov    AL , [ a ] ; AL = a = 30                 (1Eh)
        cbw               ; AX = a = 30                 (001Eh)
        imul WORD   [ c ] ; DX:AX = a*c = -60           (FFFF:FFC4h)
        
        push   DX
        push   AX
        pop   ECX         ; ECX = c * a = -60           (FFFFFFC4h)
                
        mov    AX ,   BX  ; AX = (a-2)/(b+c) = 7        (0007h)
        cwd               ; DX:AX = 7                   (0000:0007h)
        push   DX
        push   AX
        pop   EAX         ; EAX = 7                     (00000007h)
        add   EAX ,  ECX  ; EAX = 7 + a*c = -53         (FFFFFFCBh)
        add   EAX , [ e ] ; EAX = -53 + e =  67         (00000043h, c=1)
        cdq               ; EDX:EAX = 67                (0:00000043h)
        mov   ECX , [ x ] ; 
        mov   EBX , [x+4] ; EBX:ECX = x = -400          (FFFFFFFF:FFFFFE70h)
        
        sub   EAX ,  ECX  ;                             (000001D3h, c=1)
        sbb   EDX ,  EBX  ; EDX:EAX = 67 - x = 467      (0:000001D3h)

        mov  [ r ],  EAX
        mov  [r+4],  EDX  ; r = EDX:EAX = 467
  
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
