bits 32
segment code use32 public code
global literal_octal

literal_octal:       
	; EAX = int(str(dec_to_oct(byte([ESP + 4]))))
	; ex. [ESP+4]=9 => EAX = 11
	
	; b7b6b5b4b3b2b1b0 => oooob7b6_oob5b4b3_oob2b1b0, oo:=0b
	; ex. 162 = A2h = 1010_0010b => 0010_0100_0010b = 242h (<=> 242 oct)
	; printf("%X",literal_octal(val));
	
    xor EAX, EAX       ; EAX = 0
	mov AL, [ESP+4]    ; AL = byte([ESP+4])
    
    mov CL, 3          ; solve nibble 0
    call .insert_bit_0 ; EAX = b7b6_b5b4b3_oob2b1b0
    
    mov CL, 7          ; solve nibble 1           
    ;call .insert_bit_0 ; EAX = b7b6_oob5b4b3_oob2b1b0      
    ;ret             
    
    ; fallthrough call .insert_bit_0
    .insert_bit_0:
        ; CL < 31
        ; LEFT = bits EAX[31..CL+1]
        ; RIGHT = bits EAX[CL..0]
        ; EAX = {LEFT,RIGHT} := ((EAX xor RIGHT) << 1) | RIGHT        
        mov EBX, 1   ; EBX = 1
        shl EBX, CL  ; EBX = 100...0 (cnt(0)=CL)
        dec EBX      ; EBX =  11...1 (RIGHT mask)        
        and EBX, EAX ; EBX = RIGHT        
        xor EAX, EBX ; EAX = {LEFT,00..0}       
        add EAX, EAX ; EAX <<= 1, avoid CL        
        or  EAX, EBX ; EAX = LEFT_0_RIGHT        		
        ret