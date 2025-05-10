#pragma once
class IMyInterface
{
public:
	virtual int numberOfDigits() const = 0;
	virtual int sumOfDigits() const = 0;
	virtual double percentOfDigits() const = 0;
};