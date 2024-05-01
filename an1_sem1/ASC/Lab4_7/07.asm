bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    A DW 1001_1100_0110_0101b
    B DW 0011_0111_1101_0000b
    C RESD 1

;7.Se dau doua cuvinte A si B. Sa se obtina dublucuvantul C:
;bitii 0-4 ai lui C au valoarea 1
;bitii 5-11 ai lui C coincid cu bitii 0-6 ai lui A
;bitii 16-31 ai lui C au valoarea 0000000001100101b = Q = 0065h
;bitii 12-15 ai lui C coincid cu bitii 8-11 ai lui B

segment code use32 class=code
    start:
        mov ECX, 0065001Fh ; ECX = {Q}0..0011111b        
        ;mov CX, 1Fh
        ;rol ECX, 16        
        ;mov CX, 0065h
        ;ror ECX, 16 , if not preprocessing...
        mov  AX, [ A ]      ; AX = A = 1001_1100_0110_0101b (9C65h)
        and  AX, 0_007Fh    ; AX =     000000000_1100101b   (0005h)
        ror  CX,   5        ; CX =     1111_1000_0000_0000b (F800h)    
        ;and CX, 0FFE0h
        or   CX,   AX       ; CX =     1111_1000_0110_0101b (F865h)
        
        ror  CX,   7        ; CX =     1100_1011_1111_0000b (CBF0h)
        ;and  CX, 0_FFF0h
        
        mov  AL, [B+1]      ; AL = HIGH(B) = 0011_0111b (37h)
        and  AL,  0Fh       ; AL &= 1111b  = 0000_0111b (07h)
        or   CL,    AL      ; CL           = 1111_0111b (F7h)
        
        ;rol  CX,   12       ; CX = 0111...                
        ror CX, 4            ; CX = 0111_1100_1011_1111b (7CBFh)
        mov [C], ECX         ; [C] = 00657CBFh      
        
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
