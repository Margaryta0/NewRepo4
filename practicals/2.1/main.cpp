/**
 * Done by:
 * Student Name: Smal Margaryta
 * Student Group: 123
 * Practic 2.1
 */
#include<iostream>
#include<vector>
#include<ctime>
#include<cstdlib>
#include<limits>

using namespace std;

int main() {
	srand(static_cast<unsigned>(time(0)));

	{//2.Задано множину послідовностей значень A[m, n]A[m, n]A[m, n], де m – номер послідовності, а n – кількість
	 //елементів у m - тій послідовності.Знайти індекси розміщення мінімального значення в множині.
		int m = 10;
		int n = 10;

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
			for (int value : A[i]) {
				cout << value << " ";
			}
			cout << endl;
		}

		int min_value = A[0][0];
		int min_i = 0;
		int min_j = 0;

		for (int i = 0; i < m; i++) {
			for (int j = 0; j < n; j++) {
				if (min_value > A[i][j]) {
					min_value = A[i][j];
					min_i = i;
					min_j = j;
				}
			}
		}
		cout << "the minimum value " << min_value << " is under indexes: " << min_i << " and " << min_j << endl;
	}
	{//3.Задано множину послідовностей значень A[m, n]A[m, n]A[m, n], де m – номер послідовності, а n – кількість
	 //елементів у m - тій послідовності.Знайти максимальне від’ємне значення в множині.
		int m = 10;
		int n = 10;
		vector<vector<int>>A(m);

		for (int i = 0; i < m; i++) {
			A[i].resize(n);
			for (int j = 0; j < n; j++) {
				A[i][j] = rand() % 199 - 99;
			}
		}

		cout << "All sequences: " << endl;
		for (int i = 0; i < m; i++) {
			cout << "Sequences " << i + 1 << ": ";
			for (int value : A[i]) {
				cout << value << " ";
			}
			cout << endl;
		}

		int max_negative = numeric_limits<int>::min();
		for (vector<int>& sequences : A) {
			for (int value : sequences) {
				if (value < 0 && value > max_negative) {
					max_negative = value;
				}
			}
		}
		cout << "The maximum negative value is: " << max_negative << endl;
	}
	{//4.Задано множину послідовностей значень A[m, n]A[m, n]A[m, n], де m – номер послідовності, а n – кількість
	 //елементів у m - тій послідовності.Знайти мінімальне додатне значення в множині.
		int m = 10;
		int n = 10;

		vector<vector<int>>A(m);
		for (int i = 0; i < m; i++) {
			A[i].resize(n);
			for (int j = 0; j < n; j++) {
				A[i][j] = rand() % 199 - 99;
			}
		}

		cout << "All suquences: " << endl;
		for (int i = 0; i < m; i++) {
			cout << "Sequences " << i + 1 << ": ";
			for (int value : A[i]) {
				cout << value << " ";
			}
			cout << endl;
		}

		int min_positive = numeric_limits<int>::max();
		for (vector<int>& sequences : A) {
			for (int value : sequences) {
				if (value > 0 && value < min_positive) {
					min_positive = value;
				}
			}
		}
		cout << "The minimum positive value is: " << min_positive << endl;
	}
	return 0;
}
