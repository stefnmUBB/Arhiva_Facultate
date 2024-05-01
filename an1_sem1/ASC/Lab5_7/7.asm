bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    S1 DB 1, 3, 5, 7  ; source 1
    S2 DB 2, 6, 9, 4  ; source 2
    S_Len EQU $-S2    ; same for S1 and S2
    
    D RESB 2 * S_Len  ; destiation          

; Se dau doua siruri de octeti S1 si S2 de aceeasi lungime. Sa se obtina sirul D prin intercalarea elementelor celor doua siruri.
segment code use32 class=code       
    start:
    
    ; jmp start_loop ; change method to use
    
    ; start_arr:
        ; cld
        ; mov ESI, S1
        ; mov EBX, S_Len-1
        ; mov EDI, D      
        ; mov ECX, S_Len
        ; .iterate:
            ; movsb            
            ; add ESI, EBX            
            ; movsb
            ; stc
            ; sbb ESI, EBX                                   
        ; loop .iterate        
        ; jmp terminate
        
    ; start_loop:
        xor ESI, ESI            ; ESI = 0
        xor EDI, EDI            ; EDI = 0
        mov ECX, S_Len          ; ECX = len(S1), prepare loop        
        jecxz terminate
        .iterate:
            mov AL, [S1+ESI]    ; AL = S1[ESI]
            mov [D+EDI], AL     ; D[EDI] = AL
            inc EDI             ; EDI++
            mov AL, [S2+ESI]    ; AL = S2[ESI]
            mov [D+EDI], AL     ; D[EDI] = AL
            inc EDI             ; EDI ++
            inc ESI             ; ESI ++
            loop .iterate
    
    terminate:
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        
        