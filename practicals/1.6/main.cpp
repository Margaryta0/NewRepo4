/**
 * Done by:
 * Student Name: Smal Margaryta
 * Student Group: 123
 * Practic 1.6
 */

#include<iostream>
#include <vector>
#include<cstdlib>
using namespace std;

void printVector(std::vector<int>& vec) {
	for (int i = 0; i < vec.size(); i++) {
		cout << vec[i] << " ";
	}
	cout << endl;
}

int main() {
	//Завдання 1. Задано ціле значення А. Визначити, яких бітів (0 чи 1) більше в його двійковому поданні.

	int A;
	cout << "Enter the number: " << endl;
	cin >> A;

	int countBits = sizeof(int) * 8;
	int countOnes = 0;

	for (int i = 0; i < countBits; i++) {
		int mask = 1 << i;

		if (A & mask) {
			countOnes++;
		}
	}

	int countZeros = countBits - countOnes;
	if (countOnes > countZeros) {
		cout << "In the binary representation of this number there are more ones: " << countOnes << "," << " " << "than zeros: " << countZeros << endl;

	}
	else if(countZeros > countOnes) {
		cout << "In the binary representation of this number there are more zeros: " << countZeros << "," << " " << "than ones: " << countOnes << endl;

	}
	else {
		cout << "Number of zeros and ones is equal to: " << countOnes << endl;
	}

	//Завдання 2. Задано дві послідовності, які складаються з 0 та 1. Скласти специфікацію для моделювання операцій XOR.

	//Специфікація: 
	//Вхідні дані: 
	//- дві послідоності бітів (А тв В) однакової довжини
	
	// Алгоритм:
    // 1. Пройти по кожному елементу послідовностей A і B.
    // 2. Для кожного елемента обчислити результат операції XOR.
    // 3. Зберегти результат в нову послідовність.

	//Операція XOR: ri = ai XOR bi.

	//Вихідні дані: вектор а, в та XOR.

	int n;
	cout << "Enter the number of sequence elements: ";
	cin >> n;

	std::vector<int>a(n);
	std::vector<int>b(n);

	for (int i = 0; i < n; i++) {
		a[i] = rand() % 2;
		b[i] = rand() % 2;
	}

	cout << "Sequence A: ";
	printVector(a);
	cout << "Sequence B: ";
	printVector(b);

	std::vector<int>XOR(n);

	for (int i = 0; i < n; i++) {
		XOR[i] = a[i] ^ b[i];
	}

	cout << "Operation XOR: ";
	printVector(XOR);

	return 0;
}