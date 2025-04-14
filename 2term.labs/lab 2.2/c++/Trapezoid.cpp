#include "Trapezoid.h"
#include <iostream>
#include <cmath>

using namespace std;

double distance(Vertex A, Vertex B);

Trapezoid::Trapezoid(Vertex _A, Vertex _B, Vertex _C, Vertex _D) : A(_A), B(_B), C(_C), D(_D) {};

Vertex Trapezoid::getA() const {
    return A;
}

Vertex Trapezoid::getB() const {
    return B;
}

Vertex Trapezoid::getC() const {
    return C;
}

Vertex Trapezoid::getD() const {
    return D;
}

double Trapezoid::getArea() const {
    double l1 = distance(A, D);
    double l2 = distance(B, C);

    double h = fabs((B.y - A.y) * A.x - (B.x - A.x) * A.y + (B.x * A.y - A.x * B.y)) / l1;

    double area = ((l1 + l2) * h) / 2;
    return area;
}

double Trapezoid::getPerimeter() const {
    double l1 = distance(A, B);
    double l2 = distance(B, C);
    double l3 = distance(C, D);
    double l4 = distance(D, A);
    double perimeter = l1 + l2 + l3 + l4;
    return perimeter;
}

double distance(Vertex A, Vertex B) {
    double distance = sqrt(pow(B.x - A.x, 2) + pow(B.y - A.y, 2));
    return distance;
};