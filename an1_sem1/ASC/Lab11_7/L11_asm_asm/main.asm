bits 32 ; assembling for the 32 bits architecture

; declare the EntryPoint (a label defining the very first instruction of the program)
global start        

; declare external functions needed by our program
extern exit, printf       
import exit msvcrt.dll  
import printf msvcrt.dll  
                 
                          
extern literal_octal

; our data is declared here (the variables needed by our program)
segment data use32 class=data        
    pattern DB "    chr(%04X) = '%c'", 10, 13, 0 ; \l\n       

; 8. Să se afișeze, pentru fiecare număr de la 32 la 126, valoarea numărului (în baza 8) și caracterul cu acel cod ASCII.
segment code use32 class=code
    start:
        mov ECX, 127-32        ; ECX = count[32..126]
        .iterate:
            pushad             ; backup reg state (opt. push ECX)
            neg ECX            ; ECX = -ECX
            add ECX, 127       ; ECX += 127 := 32,33,...,126

            ; printf(pattern, literal_octal(ECX), ECX);                        
            push ECX           ; param printf '%c', param literal_octal
            call literal_octal ; EAX = literal_octal(ECX)		
            
            push EAX           ; param printf '%X'
                               
            push pattern       ; param printf str
            call [printf]      ; print row
            add ESP, 4*3       ; clear stack
                                    
            
            popad              ; retrieve reg state (opt. pop ECX)                        
            loop .iterate      ; for(ECX=127-32;ECX--;)                             
    
        ; exit(0)
        push    dword 0      ; push the parameter for exit onto the stack
        call    [exit]       ; call exit to terminate the program
