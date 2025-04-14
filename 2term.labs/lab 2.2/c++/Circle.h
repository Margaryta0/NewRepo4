#pragma once
#include "Shape.h"
class Circle :
    public Shape
{
private:
    double _radius;

public:
    Circle(double radius);
    double getRadius();
    double getArea() const override;
    double getPerimeter() const override;
};
