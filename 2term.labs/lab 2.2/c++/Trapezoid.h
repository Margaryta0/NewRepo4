#pragma once
#include "Shape.h"
#include <string>

using namespace std;

struct Vertex {
    int x, y;
};

class Trapezoid :
    public Shape
{
private:
    Vertex A, B, C, D;

public:
    Trapezoid(Vertex _A, Vertex _B, Vertex _C, Vertex _D);
    Vertex getA() const;
    Vertex getB() const;
    Vertex getC() const;
    Vertex getD() const;
    double getArea() const override;
    double getPerimeter() const override;
};
