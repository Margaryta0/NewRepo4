﻿/**
 * Done by:
 * Student Name: Margaryta Smal
 * Student Group: 123
 * Lab 1.7
 */

#include<iostream>

using namespace std;

static int nK = 43;//глобальна статична змінна ініціалізована значенням 43
static int nL;//описана глобальна статична змінна, до присвоєння значення 21 змінній nL присвоєно значення 0.
int main() {
	nL = 21;
	{
		int nA = 0;
	}
	{
		//в даному блоці немає доступу до змінної nA
		int nB = 1;
		{
			int nC = 2;
			nB = 3;
			static int nE = 5;//статична локальна змінна
			int nN;//не статична змінна
			nN = nK * 12;
			nE = nC + nL;

		}
		//в даному блоці немає доступу до змінної nC
	}//nE, статична змінна недоступна поза блоком
	nK += 10;//збільшимо статичну змінну nK на 10
	nL++;//збільшимо статичну змінну nL на 1
	{
		float fltK = 20;//динамічний розподіл пам'яті в стеку, fltK описана та ініціалізована значенням 20
		int nH = 0;//динамічний розподіл пам'яті в стеку, nH описана та ініціалізована значенням 0
		for (int i = 0; i < 5; i++) {
			static int nF = 0;
			nF++;// значення змінної накопичується, тому що вона статична
			int nS = 0;//динамічний розподіл пам'яті в стеку, описана та ініціалізована значенням 0
			nH++;// операція збільшення змінної nH на 1, значення змінної буде накопичуватись, тому що вона створена поза блоком
			nS++;// операція збільшення змінної nL на 1, при кожному повторному входженні в блок ця змінна буде створюватись знову та обнулятися, тому що вона описана в блоці, а коли він закривається то локальна змінна зникає, і при повторенні циклу знову створюється

		}//значення змінної nF накопичується,тому що зв'язок між ім’ям змінної та генерованим вмістом установлюється один раз
		//і лишається незмінним упродовж усього часувиконання програми
	}
	{//Досліджуємо мембранний ефект, він працює так що дія змінних проникає всередину, а назовні не проникає
		char cA = '!';
		{
			char cA = '?';
			int nQ = 1;
			{
				char cA = '@';
				int nQ = 2;// діє тільки всередині цього блоку
				int nM = 6;
			}
			nQ = 5 + nQ;// тут буде значення 6: змінна nQ знову стане 1 і додається 5
		}
	}
	{
		int nK = 32;
		int nC;
		nC = nK * 2;//Значення буде 64, тому що локальна змінна має більший пріоритет, ніж така сама глобальна змінна.
		nC = ::nK * 2;//Значення буде 86, адже використовуємо глобальне розрізнення видимості позначень(імен)
	}
	{
		int* pI;//описуємо вказівну типізвану зінну
		pI = new int;//захоплюємо пам'ять і "купі"
		*pI = 25;//заносимо в купу значення змінної
		delete pI;//звільняємо пам'ять

		int* pW;//виконуємо ті самі дії по відношенню до іншої змінної
		pW = new int;
		*pW = 10;
		pW = pI;//приклад сміття, адже коли ми присвоюємо значення pI змінній pW, то зв'язок з областю пам'яті де знаходиться 10 втрачається
		delete pW;
	}


	return 0;
}