#include<iostream>
#include<string>
#include<stack>
#include<cctype>
#include<sstream>

using namespace std;

int precedence(char c) {
	if (c == '+' || c == '-') {
		return 1;
	}
	else if (c == '*' || c == '/') {
		return 2;
	}
}

string infixToPostfix(const string& expression) {
	stack<char>operators;
	string output;
	int i = 0;

	while (i < expression.length()) {
		char value = expression[i];
		if (isspace(value)) {
			i++;
			continue;
		}

		if (isdigit(value)) {
			string number = "";
			while (i < expression.length() && isdigit(expression[i])) {
				number += expression[i];
				i++;
			}
			output += number + " ";
		}
		else if (value == '(') {
			operators.push(value);
			i++;
		}
		else if (value == ')') {
			while (!operators.empty() && operators.top() != '(') {
				output += operators.top();
				output += " ";
				operators.pop();
			}
			operators.pop();
			i++;
		}
		else {
			while (!operators.empty() && precedence(operators.top()) >= precedence(value)) {
				output += operators.top();
				output += " ";
				operators.pop();
			}
			operators.push(value);
			i++;
		}
	}
	while (!operators.empty()) {
		output += operators.top();
		output += " ";
		operators.pop();
	}

	return output;
}

int performOperation(char operation, int operand1, int operand2) {
	switch (operation) {
	case '+':
		return operand1 + operand2;
	case '-':
		return operand1 - operand2;
	case '*':
		return operand1 * operand2;
	case '/':
		if (operand2 == 0) {
			throw std::invalid_argument("Division by zero");
		}
		return operand1 / operand2;
	default:
		throw std::invalid_argument("Invalid operation");
	}
}

int evaluatePostfix(const string& expression) {
	stack<int> operands;
	stringstream ss(expression);
	string token;

	while (ss >> token) {
		if (isdigit(token[0])) {
			operands.push(stoi(token));
		}
		else {
			int operand2 = operands.top(); operands.pop();
			int operand1 = operands.top(); operands.pop();
			int result = performOperation(token[0], operand1, operand2);
			operands.push(result);
		}
	}
	return operands.top();
}

int main() {
	string infix_expression;
	cout << "Enter an expression to solve: ";
	getline(cin, infix_expression);
	string postfix_expression = infixToPostfix(infix_expression);
	cout << "Postfix expression: " << postfix_expression << endl;
	try {
		cout << "Result = " << evaluatePostfix(postfix_expression) << endl;
	}
	catch (const invalid_argument& e) {
		cout << "Error: " << e.what() << endl;
	}

	return 0;
}