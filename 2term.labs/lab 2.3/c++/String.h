#pragma once
#include "IMyInterface.h"
#include <iostream>
#include <string>
using namespace std;

class String :
    public IMyInterface
{
private:
    string _string;

public:
    String(string string1);
    string getString() const;
    virtual int numberOfDigits() const override;
    virtual int sumOfDigits() const override;
    virtual double percentOfDigits() const override;
};