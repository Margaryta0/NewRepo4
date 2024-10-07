#include <iostream>
#include<iomanip>
#include<vector>
using namespace std;
//Задача - Christmas Tree

//Основна вимоги(Закриється одна практична -> 2.3 або 2.4) Напишіть програму, яка отримує від користувача число n,
// що вказує кількість рівнів(шарів) ялинки.Програма повинна вивести ялинку з n рівнів у вигляді трикутників з зірочок(*).
// Кожен рівень ялинки складається з кількох рядків, 
// де кількість зірочок збільшується від 1 до непарного числа, а також відображається стовбур.

int main() {
	int n;
	cout << "Enter the numbers of Christmas tree levels: " << endl;
	cin >> n;

	int ret = n + 2;
	int quan = 5;
	char star = '*';
	cout << "Christmas tree" << endl;
	for (int a = 1; a <= n; a++) {
		for (int i = 1; i <= quan; i += 2) {
			cout << setw(ret);
			for (int j = 0; j < i; j++) {
				cout << star;
			}
			ret -= 1;
			cout << endl;
		};
		quan += 2;
		ret = n + 2;
	}
	cout << setw(n + 2) << '*' << endl << setw(n + 2) << '*' << endl;
}
//У цьому коді я хотіла показати просто виведення ялинки, 
// та у практичній 2.4 я покращила цей код і виконала інші вимоги.