#include "Text.h"
#include <algorithm>
#include "String.h"

Text::Text(vector<String>& stringss) : strings(stringss) {};

void Text::addString(const String& string1) {
    strings.push_back(string1);
}

void Text::deleteAllText() {
    strings.clear();
}

void Text::deleteString(const String& string1) {
    auto it = std::remove_if(strings.begin(), strings.end(), [&](const String& s) {
        return s.getString() == string1.getString();
        });
    strings.erase(it, strings.end());
}

string Text::TheLargestString() {
    if (strings.empty()) return "";

    auto it = std::max_element(strings.begin(), strings.end(), [](const String& a, const String& b) {
        return a.getString().length() < b.getString().length();
        });

    return it->getString();
}
 
double Text::getPercentOfDigits() {
    int totalDigits = 0;
    int totalChars = 0;

    for (const auto& str : strings) {
        totalDigits += str.numberOfDigits();
        totalChars += str.getString().length();
    }

    if (totalChars == 0) return 0.0;
    return (100.0 * totalDigits) / totalChars;
}

int Text::getNumbersOfSymbols() {
    int total = 0;
    for (const auto& str : strings) {
        total += str.getString().length();
    }
    return total;
}