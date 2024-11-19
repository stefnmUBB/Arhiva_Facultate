#pragma once

void rgb16_to_rgb32(const unsigned short* src, char* dst, int len)
{
	for(int i=0;i<len;i++)
	{
		int r = src[i]&0x1F;
		int g = (src[i]>>5)&0x1F;
		int b = (src[i]>>10)&0x1F;
		//r*=8, g*=8, b*=8;
		*(dst++) = r*8;///32.0;
		*(dst++) = g*8;///32.0;
		*(dst++) = b*8;///32.0;
		//*(dst++) = 1;
		//dst[i] = (b<<24) | (g<<16) | (r<<8) | (0xFF<<0);
	}		
}