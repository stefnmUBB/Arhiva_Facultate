bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data    
    S DD 12345678h, 1A2B3C4Dh, 0FE98DC76h, 03111111h, 19010203h, 3FA0A0A0h
    S_Len EQU ($-S)/4
    D TIMES S_Len RESD 1

; Se da un sir de dublucuvinte. Sa se obtina sirul format din octetii superiori ai
; cuvitelor superioare din elementele sirului de dublucuvinte care sunt divizibili cu 3.
segment code use32 class=code
    start:
        mov ESI, S       ; ESI = S = source
        mov EDI, D       ; ESI = D = destination
        cld              ; DF = 0 (ESI/EDI+)
        mov DL, 3        ; DL = 3
        
        mov ECX, S_Len   ; ECX = S_Len = 3     
        iterate:
            lodsd        ; EAX = S[ESI] = X1_X2_X3_X4h, ESI+=4
            push CX      ; backup counter reg.
            mov CL, 8    ; CL = 8
            rol EAX, CL  ; EAX = X2_X3_X4_X1h
            pop CX       ; restore counter reg.
            mov BL, AL   ; BL = AL = X1h (backup AL)
            mov AH, 0    ; AH = 0 <=> AX = AL                                                           
            div DL       ; AL = AX/3, AH = AX%3          
                        
            or AH, AH    ; AH|=AH, ZF=1 if AH == 0 ; <=> cmp AH, 0
            loopnz iterate
            ;jnz .next    ; jp if(!ZF) <=> if(X1%3!=0) continue;            
            
            ; (X1h % 3 == 0) :
            mov AL, BL   ; AL = BL = X1h (restore AL)
            stosb        ; D[EDI++] = AL
        .next:
            loop iterate ; if (ECX>0) goto iterate
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        