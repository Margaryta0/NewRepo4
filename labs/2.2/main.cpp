/**
 * Done by:
 * Student Name: Margaryta Smal
 * Student Group: 123
 * Lab 2.2
 */

#include<iostream>
#include<string>
using namespace std;

//1-ша компонента: Ім’я – значення перелічувального типу;
//2 - га компонента : Курс – ціле значення;
//3 - тя компонента : Місто – символьне
enum Names { David, Tom, Andrew, Kate, Mary, Olga };
struct Student {
	Names name;
	int yearOfStudy;
	string town;// використовую string, оскільки char можна використовувати лише для запису одного символу, а не цілого слова, у даному випадку міста
};

//1-ша компонента: Ім’я – значення перелічувального типу;
//2 - га компонента : Середній бал – дійсне;
//3 - тя компонента : Спорт – логічне
struct Group {
	Names name;
	float averageScore;
	bool sport;
};

int main() {
	{
		Student myStudents[7];
		myStudents[0] = { David, 4, "Kovel" };
		myStudents[1] = { Tom, 1, "Kyiv" };
		myStudents[2] = { Andrew, 2, "Lviv" };
		myStudents[3] = { Kate, 3, "Kyiv" };
		myStudents[4] = { Mary, 4, "Kyiv" };
		myStudents[5] = { Tom, 4, "Kovel" };
		myStudents[6].name = Olga;
		myStudents[6].yearOfStudy = 2;
		myStudents[6].town = "Lutsk";

		//Процент студентів 4-го курсу, що приїхали з інших міст
		int count = 0;
		for (int i = 0; i < 7; i++) {
			if (myStudents[i].yearOfStudy == 4 && myStudents[i].town != "Kyiv") {
				count++;
			}
		}
		float percent = (float)count * 100 / 7;
	}
	{
		Group myGroup[7];
		myGroup[0] = { Tom, 8.5, true };
		myGroup[1] = { David, 10.3, true };
		myGroup[2] = { Kate, 9.8, false };
		myGroup[3] = { Andrew, 11.0, true };
		myGroup[4] = { Olga, 7.8, false };
		myGroup[5] = { Mary, 10.6, false };
		myGroup[6].name = Andrew;
		myGroup[6].averageScore = 8.9;
		myGroup[6].sport = true;

		//Процент студентів-відмінників, що займаються спортом
		int count = 0;
		for (int i = 0; i < 7; i++) {
			if (myGroup[i].averageScore > 10.0 && myGroup[i].sport == true) {
				count++;
			}
		}
		float percent = (float)count * 100 / 7;
	}
	return 0;
}