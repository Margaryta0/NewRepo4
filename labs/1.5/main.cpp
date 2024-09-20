/**
 * Done by:
 * Student Name: Margaryta Smal
 * Student Group: 123
 * Lab 1.5
 */

#include <iostream>
#include <cmath>

using namespace std;

int main() {
    {
        float a = 54.9;
        float b = 82.1;
        int c = 74;
        int d = 74;

        // <УЛО1> (<УЛО2> (A<ОВ1>B) <БЛО> (<УЛО3> (C<ОВ2>D)))
        //    !   (  !    (a <= b)    ||  (empty  (c == d)))
        bool res = !(!(a <= b) || (c == d));
        cout << "res: " << boolalpha << res << endl;
    }

    {
        int a = 49;
        int b = 58;
        float c = 8.8;
        float d = 6.6;

        // <УЛО1> (<УЛО2> (A<ОВ1>B) <БЛО> (<УЛО3> (C<ОВ2>D)))
        //   !    (  !    (a <= b)   ||   (empty  (c == d)))
        bool aRes = !(!(a <= b) || (c == d));
        cout << "aRes: " << boolalpha << aRes << endl;
    }
    {   //Part 2
        // A <БО1> <УО> B <АО1> <СО> C <ОВ> D <АО2> E <БО2> <БазО> F
        //((constA & +  nB) /    *  pnC) > (-593 + (nE <<  sizeof (float)))

        const int constA = 41;
        int nB, nE;
        nB = -13;
        nE = 12;
        int nC;
        int* pnC;
        pnC = &nC;
        *pnC = 20;

        bool bRes = ((constA & +nB) / *pnC) > (-593 + (nE << sizeof(float)));
        cout << "bRes: " << boolalpha << bRes << endl;

    }

    return 0;
}
