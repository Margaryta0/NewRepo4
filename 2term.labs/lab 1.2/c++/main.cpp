#include <iostream>
#include "../staticLib/Rhombus.h"
using namespace std;

void outputData(Rhombus& rhombus) {
    double perimetr = rhombus.getPerimeter();
    double area = rhombus.getArea();
    cout << "Perimetr of rhombus: " << perimetr << endl;
    cout << "Area of rhombus: " << area << endl;
}

int main()
{
    Vertex A = { 9,8 };
    Vertex B = { 7,6 };
    Vertex C = { 5,4 };
    Vertex D = { 3,2 };
    Rhombus rhombus(A, B, C, D);
    cout << "Your A: " << rhombus.getA() << ", Your B: " << rhombus.getB() << ", Your C: " << rhombus.getC() << ", Your D: " << rhombus.getD() << endl;
    outputData(rhombus);
    Rhombus rhombus1;
    Rhombus rhombus2(rhombus1, true);
    Rhombus rhombus3(rhombus1);

    return 0;
}