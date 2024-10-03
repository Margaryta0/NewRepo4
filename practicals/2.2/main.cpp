/**
 * Done by:
 * Student Name: Smal Margaryta
 * Student Group: 123
 * Practic 2.2
 */

#include<iostream>
#include<vector>
#include<cstdlib>
#include<ctime>
#include<climits>
#include<iomanip>

using namespace std;

void initRandomizer() {
	srand(time(0));
}
int n = 5;
int m = 3;

void printVector(const std::vector<std::vector<int>>& a) {
	for (const std::vector<int>& vec : a) {
		for (int value : vec) {
			cout << value << " ";

		}
		cout << endl;
	}
}


int main() {
	initRandomizer();
	{//Завдання 1
	//Задано множину послідовностей значень A[m,n]. Замінити в кожній послідовності A[i] мінімальні значення на
	//суму елементів цієї послідовності.
		cout << "Tusk 1" << endl;

		vector<vector<int>>A(m);

		for (int i = 0; i < m; i++) {
			A[i].resize(n);
			for (int j = 0; j < n; j++) {
				A[i][j] = rand() % 100;
			}
		}

		cout << "All sequences: " << endl;
		for (int i = 0; i < m; i++) {
			cout << "Sequences " << i + 1 << ": ";
			for (int j = 0; j < n; j++) {
				cout << A[i][j] << " ";
			}
			cout << endl;
		}

		for (int i = 0; i < m; i++) {
			int sum = 0;
			int minValue = INT_MAX;
			for (int j = 0; j < n; j++) {
				if (A[i][j] < minValue) {
					minValue = A[i][j];
				}
			}

			for (int j = 0; j < n; j++) {
				sum += A[i][j];
			}

			for (int j = 0; j < n; j++) {
				if (A[i][j] == minValue) {
					A[i][j] = sum;
				}
			}
		}
		cout << "Updated sequences: " << endl;
		printVector(A);
	}
	{//3. Задано дві послідовності значень A[n] і B[m]. Замінити в A[n] входження значень із B[m] 
	 //на добуток елементів підпослідовності B[m].
		cout << endl;
		cout << "Tusk 2" << endl;
		vector<int>A = { 3,4,5,6 };
		vector<int>B = { 7,8,6,45,32 };

		cout << "Initial sequences A: ";
		for (int i = 0; i < A.size(); i++) {
			cout << A[i] << " ";
		}
		cout << endl;

		cout << "Initial sequences B: ";
		for (int value : B) {
			cout << value << " ";
		}
		cout << endl;

		int multB = 1;

		for (int i = 0; i < B.size(); i++) {
			multB *= B[i];
		}

		for (int i = 0; i < A.size(); i++) {
			for (int j = 0; j < B.size(); j++) {
				if (A[i] == B[j]) {
					A[i] = multB;
					break;
				}
			}
		}
		cout << "Updated suquences A: ";
		for (int i = 0; i < A.size(); i++) {
			cout << A[i] << " ";
		}
		cout << endl << endl;
		{//4. Задано множину послідовностей значень A[m,m]. Замінити в кожному стовпці множини A[m, m] максимальні
		 //значення на добуток елементів цього стовпця.
			cout << "Tusk 3" << endl;
			int m = 3;
			vector<vector<int>>A = {
				{3,4,5},
				{5,6,7},
				{8,9,0}
			};

			cout << "Initial sequences: " << endl;
			for (int i = 0; i < m; i++) {
				for (int value : A[i]) {
					cout << setw(3) << value << " ";
				}
				cout << endl;
			}

			for (int i = 0; i < m; i++) {
				int maxVal = INT_MIN;
				int mult = 1;
				int maxIndex = -1;
				for (int j = 0; j < m; j++) {
					if (A[j][i] > maxVal) {
						maxVal = A[j][i];
						maxIndex = j;
					}
					mult *= A[j][i];
				}
				A[maxIndex][i] = mult;
			}
			cout << "Updated sequences: " << endl;
			for (int i = 0; i < m; i++) {
				for (int j = 0; j < m; j++) {
					cout << setw(3) << A[i][j] << " ";
				}
				cout << endl;
			}
		}
	}
	return 0;
}