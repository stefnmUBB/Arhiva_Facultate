bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    S DB "aAbB2%xM"
    D RESB ($-S)

; Se da un sir de caractere S. Sa se construiasca sirul D care sa contina toate literele mari din sirul S.
segment code use32 class=code
    start:
        
        xor ESI, ESI            ; ESI = 0
        xor EDI, EDI            ; EDI = 0
        mov ECX, (D-S)          ; ECX = len(S); prepare loop
        jecxz terminate
        .iterate:
            mov AL, [S+ESI]     ; AL = S[ESI]
            inc ESI             ; ESI++
            cmp AL, 'A'         ; compare S[ESI] to ascii('A')
            jb .next            ; if S[ESI] < 'A', skip char
            cmp AL, 'Z'         ; compare S[ESI] to ascii('Z')
            ja .next            ; (else) if S[ESI] > 'Z', also skip char    
            mov [D+EDI], AL     ; if we are here, S[ESI] is letter, push it to D(est)
            inc EDI             ; prepare next destination index
        .next:
            loop .iterate       ; loop until ECX == 0
            mov [D+EDI], BYTE 0 ; end the new string with '\0' character            
        terminate:
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
    