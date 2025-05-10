#include "String.h"
#include <cctype>

String::String(string string1) : _string(string1) {};

int String::numberOfDigits()const {
    int count = 0;
    for (int i = 0; i < _string.length(); i++) {
        if (_string[i] >= '0' && _string[i] <= '9') {
            count++;
        }
    }
    return count;
}

string String::getString()const {
    return _string;
}

int String::sumOfDigits()const {
    int sum = 0;
    for (int i = 0; i < _string.length(); i++) {
        if (_string[i] >= '0' && _string[i] <= '9') {
            sum += _string[i];
        }
    }
    return sum;
}

double String::percentOfDigits()const {
    if (_string.empty()) return 0.0;

    int digitCount = 0;

    for (char ch : _string) {
        if (std::isdigit(static_cast<unsigned char>(ch))) {
            digitCount++;
        }
    }

    return (100.0 * digitCount) / _string.length();
}