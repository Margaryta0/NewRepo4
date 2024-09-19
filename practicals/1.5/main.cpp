/**
 * Done by:
 * Student Name: Smal Margaryta
 * Student Group: 123
 * Practic 1.5
 */

#include <iostream>
#include <cstdlib>
#include <ctime>
#include <vector>
#include <climits>

using namespace std;

void initRandomizer() {
    // Seed the random number generator with the current time
    srand(time(0));  // srand(time(NULL)) could also be used
}

int main() {
    // Задано послідовність значень А[n] і деяке значення P. Знайти індекс першого входження Р у послідовність А[n].
    {
        initRandomizer();

		int n;
		cout << "Enter the length of the sequence: ";
		cin >> n;
		/*int p;
		cout << "Enter a number between 0 and 100: ";
		cin >> p;

		std::vector <int> myVector;
		for (int i = 0; i < n; i++) {
			int randomVar = rand() % 100;
			myVector.push_back(randomVar);
		}

		for (int i = 0; i < n; i++) {
			cout << myVector[i] << " ";
		}
		cout << endl;

		bool found = false;
		for (int i = 0; i < n; i++) {
			if (myVector[i] == p) {
				cout << "We found the index of p: " << i << endl;
				found = true;
				break;
			}
		}
		if (!found) {
			cout << "p is not in the array." << endl;
		}*/
		//перше завдання закінчено.
	
		//Завдання 2
		//Задано послідовність значень А[n]. Знайти найменше значення серед додатних елементів послідовності А[n].

		/*std::vector<int>myVector;
		for (int i = 0; i < n; i++) {
			int randomVar = (rand() % 100) - 50;
			myVector.push_back(randomVar);
		}

		for (int i = 0; i < n; i++) {
			cout << myVector[i] << " ";
		}
		cout << endl;

		int leastPositive = INT_MAX; 
		bool foundLeast = false;
		for (int i = 0; i < n; i++) {
			if (myVector[i] > 0 && myVector[i] < leastPositive) {
				leastPositive = myVector[i];
				foundLeast = true;
			}
		}

		if (foundLeast) {
			cout << "Lowest positive value: " << leastPositive << endl;
		}
		else {
			cout << "no positive value" << endl;
		}*/
		// друге завдання закінчено
		
		//Третє завдання
		//Задано послідовність значень А[n]. Знайти найбільше і найменше значення та поміняти їх місцями.

		std::vector<int>myVector;
		for (int i = 0; i < n; i++) {
			int randomVar = rand() % 100;
			myVector.push_back(randomVar);
		}

		for (int i = 0; i < n: i++) {
			cout << myVector[i] << " ";
		}
		cout << endl;

		int min = myVector[0];
		for (int i = 1; i < n; i++) {
			if (myVector[i] < min) {
				min = myVector[i];
			}
		}
		int max = myVector[0];
		for (int i = 0; i < n; i++) {
			if (myVector[i] > max) {
				max = myVector[i];
			}
		}
		int a = min;
		min = max;
		max = a;

		cout << "The minimum value is: " << min << endl;
		cout << "The maxumum value is: " << max << endl;

	}
	return 0;
}


