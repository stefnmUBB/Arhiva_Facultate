bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

extern printf
import printf msvcrt.dll
                          
; our data is declared here (the variables needed by our program)
segment data use32 class=data
    fstr DB "%d mod %d = %d", 0
    a DD -104137
    b DW 5

; Se dau doua numere natural a si b (a: dword, b: word, definite in segmentul de date). 
; Sa se calculeze a/b si sa se afiseze restul impartirii in urmatorul format: "<a> mod <b> = <rest>"
; Exemplu: pentru a = 23 si b = 5 se va afisa: "23 mod 5 = 3"
; Valorile vor fi afisate in format decimal (baza 10) cu semn.
segment code use32 class=code
    start:
        mov   AX, [b]   ; AX = b = 5
        cwde            ; EAX = b = 5
        mov  EBX, EAX   ; EBX = b = 5
        
        mov   EAX, [a]   ; EAX = a = -104137  
        cdq              ; EDX:EAX = a = -104137

        push EAX        ; backup a
        idiv EBX        ; EAX = a/b = -20827, EDX = a%b = -2            
        pop  EAX        ; restore a
        
        ; printf("%d mod %d = %d",a,b,{EDX} = a%b)           
        push EDX           ; push arg result = a%b
        push EBX           ; push arg b
        push EAX           ; push arg a       
        push fstr          ; push format str
        call [printf]      ; call printf      
        add ESP, 4*4       ; clean stack (EDX + a + b + fstr) => 4 DWORDS
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        