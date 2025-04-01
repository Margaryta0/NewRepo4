#include "../staticLibc++/NumericalString.h"
#include "../staticLibc++/String.h"
#include <iostream>
using namespace std;

int main()
{
    String string;
    String string1(string);
    cout << string1.getLength() << endl;
    NumericalString numericalString1(string);

    vector<char>vec = { '1', '2', '6' };
    NumericalString numericalString2(vec);
    numericalString2.deleteSymbol('2');
    cout << "The string: ";
    for (char val : numericalString2.getValue()) {
        cout << val << " ";
    }
    cout << endl;
}
