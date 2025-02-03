#include <iostream>
#include "Rhombus.h"
using namespace std;

void outputData(Rhombus& rhombus) {
    float perimetr = rhombus.getPerimetr();
    float area = rhombus.getArea();
    cout << "Perimetr of rhombus: " << perimetr << endl;
    cout << "Area of rhombus: " << area << endl;
}

int main()
{
    Rhombus rhombus1(6, 3, 4, 2, 1, 5, 7, 8);
    outputData(rhombus1);

    return 0;
}