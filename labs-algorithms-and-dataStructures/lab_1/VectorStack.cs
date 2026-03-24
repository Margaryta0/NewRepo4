using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class VectorStack
    {
        private double[] data;   
        private int top;         
        private int capacity;    

        public VectorStack(int capacity)
        {
            this.capacity = capacity;
            data = new double[capacity];
            top = -1; 
        }

        public bool IsFull()
        {
            return top == capacity - 1;
        }


        public bool IsEmpty()
        {
            return top == -1;
        }

        public bool Push(double value)
        {
            if (IsFull())
            {
                Console.WriteLine(" Push не вдався: стек повний.");
                return false; 
            }
            top++;          
            data[top] = value; 
            return true;     
        }


        public double Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Pop не вдався: стек порожній!");
            }
            double value = data[top]; 
            top--;                    
            return value;
        }

        public double Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Стек порожній!");
            return data[top];
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("  Стек порожній.");
                return;
            }
            Console.Write("  Стек (вершина → дно): ");
            for (int i = top; i >= 0; i--)
            {
                Console.Write(data[i].ToString("F2")); // 2 знаки після коми
                Console.Write(" | ");
            }
            Console.WriteLine($"  (кількість елементів: {top + 1})");
        }
    }
}
