/**
 * Done by:
 * Student Name: Margaryta Smal
 * Student Group: 123
 * Lab 1.5
 */
#include<iostream>

int main() {
	{
		//task 1
		char chA;
		const char CONSTB = '+';
		chA = '4';
		char chC = 'f';

		char chD;
		const char CONSTE = 0x30;
		chD = 0x7;
		char chF = 0x79;
	}
	{
		//task 2
		int nA;
		float fltB;
		unsigned short wC;

		nA = 31765;
		fltB = -7.293e3;
		wC = 26543;

		double dblD;
		int nE;
		char chP;

		dblD = nA;
		nE = fltB;
		chP = wC;

		dblD = (double)nA;
		nE = (int)fltB;
		chP = (char)wC;

		double* pdblD;
		void* pV;
		pV = &nA;
		pdblD = (double*)pV;
		dblD = *pdblD;

		int* pnE;
		pV = &fltB;
		pnE = (int*)pV;
		nE = *pnE;

		char* pchP;
		pV = &wC;
		pchP = (char*)pV;
		chP = *pchP;
	}

	return 0;

}