bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

; our data is declared here (the variables needed by our program)
segment data use32 class=data
    A DW 00000000101_00100_b
    B DW 110000_00010_10101b
    C DW 1_00011_1001001000b
    D RESB 1
    E RESB 1
    F RESB 1
    
; 32 Se dau cuvintele A, B si C. Sa se obtina octetul D ca suma a numerelor reprezentate de:
; biţii de pe poziţiile 0-4 ai lui A
; biţii de pe poziţiile 5-9 ai lui B
; Octetul E este numarul reprezentat de bitii 10-14 ai lui C. Sa se obtina octetul F ca rezultatul scaderii D-E.
segment code use32 class=code
    start:
        mov  DL, [ A ] ; DL = LOW(A) = 1010_0100b = A4h
        and  DL,  1Fh  ; DL = DL & 0001_1111b = 000_00100b = 04h
        mov  AX, [ B ] ; AX = B = 1100_0000_0101_0101b = C055h
        ror  AX,    5  ; AX = 1010_1110_0000_0010b = AE02h
        and  AL,  1Fh  ; AL = AL & 0001_1111b = 000_00010b = 02h
        add  DL,   AL  ; DL = DL + AL = 04h + 02h = 06h
        mov [D],   DL  ; save DL to var D
        mov  BL, [C+1] ; BL = HIGH(C) = 1000_1110h = 8Eh
        shr  BL,    2  ; BL = BL>>2 = 0010_0011 = 23h
        and  BL,  1Fh  ; BL = BL & 1Fh = 000_00011 = 03h
        mov [E],   BL  ; save BL to var E
        sub  DL,   BL  ; DL = 06h-03h = 03h
        mov [F],   DL  ; save DL to var F
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
