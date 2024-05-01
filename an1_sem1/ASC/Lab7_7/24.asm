bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit               ; tell nasm that exit exists even if we won't be defining it
import exit msvcrt.dll    ; exit is a function that ends the calling process. It is defined in msvcrt.dll
                          ; msvcrt.dll contains exit, printf and all the other important C-runtime specific functions

extern fprintf
import fprintf msvcrt.dll

extern fopen
import fopen msvcrt.dll

extern fclose
import fclose msvcrt.dll
                          
; our data is declared here (the variables needed by our program)
segment data use32 class=data
    fmode DB "w", 0
    fname DB "file.txt", 0
    text DB "mD0rC_95_hqp$Rt;423", 0            

; Se dau un nume de fisier si un text (definite in segmentul de date). Textul contine litere mici, litere mari, cifre si caractere speciale. 
; Sa se inlocuiasca toate CIFRELE din textul  dat cu caracterul 'C'. Sa se creeze un fisier cu numele dat si sa se scrie textul obtinut prin inlocuire in fisier.
segment code use32 class=code
    start:
        mov ESI, text   ; ESI = first(text)
        mov EDI, ESI    ; EDI = ESI = first(text)
        cld             ; DF = 0 (frst->lst)      
        parse: 
            lodsb       ; AL = text[i] = *(ESI), ESI++
            or AL, AL   ; AL |= AL; cmp AL, 0; set ZF
            jz fin      ; text[i]=='\0', end of text
            
            cmp AL, '0' ; AL - '0', set flags
            jb .write   ; if (AL<'0'), don't change char
            
            cmp AL, '9' ; AL - '9', set flags
            ja .write   ; if (AL>'9'), don't change char
            
            mov AL, 'C' ; if AL in '0'..'9', AL='C'
            
            .write:
            stosb       ; text[i] = *(EDI) = AL, EDI++, now EDI=ESI
            
            jmp parse   ; while (text[i]!='\0')
        fin:
        
        ; fopen ("file.txt", 'w')
        push fmode     ; push offset fmode "w" - write
        push fname     ; push offset fname "file.txt" 
        call [fopen]   ; call fopen, EAX = fptr
        add ESP, 2*4   ; clean stack             
        
        push EAX       ; backup fptr
        
        ; fprintf(fptr, {modified_}text)
        push text      ; push offset text = mDCrC_CC_hqp$Rt;CCC"
        push EAX       ; push fptr
        call [fprintf] ; call fprintf
        add ESP, 2*4   ; clean stack
                
        ; top of stack = fptr
        call [fclose]
        add ESP, 4
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program

        
        
        