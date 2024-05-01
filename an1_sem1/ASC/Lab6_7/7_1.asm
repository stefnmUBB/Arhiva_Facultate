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
    D_Len RESB 1 ; actual dest length

; Se da un sir de dublucuvinte. Sa se obtina sirul format din octetii superiori ai
; cuvitelor superioare din elementele sirului de dublucuvinte care sunt divizibili cu 3.
segment code use32 class=code
    start:
        mov ESI, S+4*(S_Len-1) ; ESI = lst(source)
        mov EDI, D         ; ESI = D = destination        
        mov DL , 3         ; DL = 3
        
        mov ECX, S_Len     ; ECX = S_Len = 6     
        iterate:
            std            ; DF = 1 (ESI-)
            lodsd          ; EAX = S[ESI] = X1_X2_X3_X4h, ESI-=4
            push CX        ; backup counter
            mov CL, 8      ; CL = 8
            rol EAX, CL    ; EAX = X2_X3_X4_X1h
            pop CX         ; restore counter
            mov BL, AL     ; BL = AL = X1h (backup AL)
            mov AH, 0      ; AH = 0 <=> AX = AL                                  
            div DL         ; AL = AX/3, AH = AX%3            
                       
            or AH, AH      ; AH|=AH, set ZF=1 if AH == 0 
            jnz .next      ; if(!ZF) <=> if(X1h%3!=0) continue;                        
            ; (X1h % 3 == 0) :
            mov AL, BL     ; AL = BL = A1h (restore AL)
            cld            ; DF = 0 (EDI+)
            stosb          ; D[EDI++] = AL
            inc BYTE [D_Len] ; D_Len ++
        .next:
            loop iterate ; if (ECX>0) goto iterate
          
        xor ECX, ECX       ; ECX = 0        
        mov  CL, [D_Len]   ; CL = (ECX =) D_Len = 3             
        
        mov ESI, D         ; ESI = D       
        mov EDI, ESI       ; EDI = ESI = D        
        add ESI, ECX       ; ESI = D + D_Len
        dec ESI            ; ESI = D + D_Len - 1 = lst(D)
        shr ECX, 1         ; ECX = D_Len/2 = 1
        
        reverseD:            
            cld           ; DF = 0 (ESI/EDI+)
            mov AL, [EDI] ; AL = *EDI := D[i], i=S_Len/2-ECX
            movsb         ; [EDI]=[ESI], EDI++, ESI++                       
            dec ESI       ; ESI -= 1, crt. elem.                                   
            mov [ESI], AL ; [ESI] := D[D_Len-1-i] = AL 
            dec ESI       ; ESI -= 1, prev. elem.
            loop reverseD ; if (ECX>0) goto reverseD
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
