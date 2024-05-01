bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

segment data use32 class=data
    
    a DB        -1
    b DW     -1400
    c DD      5000
    d DQ    640000
    x RESQ       1

; solving: c - (d+d+d) + a - b [signed]
segment code use32 class=code
    start:
        mov  ECX ,       [ c ] ;   ECX = c              =    5000 (00001388h)
                
        mov   AX ,       [ b ] ;    AX = b              =   -1400 (FA88h) 
        ;cwd                    ; DX:AX = b (signed)     =   -1400 
        ;push DX
        ;push AX        
        ;pop EAX                ;   EAX = b (signed)     =   -1400 (FFFFFA88h)        
        cwde
        sub  ECX ,        EAX  ;   ECX = c - b          =    6400 (00001900h, c=1)
                
        mov   AL ,       [ a ] ;    AL = a              =      -1 (FFh)
        cbw                    ;    AX = a (signed)     =      -1 (FFFFh)
        cwd                    ; DX:AX = a (signed)     =      -1 
        push DX
        push AX
        pop EAX                ;   EAX = a (signed)     =      -1 (FFFFFFFFh)
        add  ECX ,        EAX  ; ECX = c - b + a        =    6399 (000018FFh)
        
        mov  EAX ,       [ d ] ; 
        mov  EDX ,       [d+4] ; EDX:EAX = d            =  640000 (0:0009C400h)
        
        ;REPT(2)
        add  EAX , DWORD [ d ] ;                                  (00138800h, c=0)
        adc  EDX , DWORD [d+4] ; EDX:EAX = d+d          = 1280000 (0:00138800h)        
        ;ENDM
        ; REPT(2) ??
        add  EAX , DWORD [ d ] ;                                  (001D4C00h)
        adc  EDX , DWORD [d+4] ; EDX:EAX = 2*d+d        = 1920000 (0:001D4C00h, c=0)
                
        ; neg EDX:EAX        
        xor  EAX , 0xFFFFFFFF  ; EDX:EAX = -EDX:EAX /
        xor  EDX , 0xFFFFFFFF  ; -x = (~x) + 1
        add  EAX , 1   
        adc  EDX , 0           ; EDX:EAX = -(3*d)       =-1920000 (FFFFFFFF:FFE2B400h)   

        add  EAX ,  ECX        ;                                  (FFE2CCFFh, c=0)
        adc  EDX ,   0         ; EDX:EDX = ECX - 3*d    =-1913601 (FFFFFFFF:FFE2CCFFh)
        
        mov [ x ], EAX
        mov [x+4], EDX         ; store result
               
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
 
 
 
 
 