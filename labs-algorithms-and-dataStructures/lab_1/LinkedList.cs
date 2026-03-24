using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class LinkedList
    {
        private ListNode head; 

        public LinkedList()
        {
            head = null; 
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void AddFirst(string value)
        {
            ListNode newNode = new ListNode(value); 
            newNode.Next = head; 
            head = newNode;      
        }

        public void AddLast(string value)
        {
            ListNode newNode = new ListNode(value);

            if (IsEmpty())
            {
                head = newNode; 
                return;
            }

            ListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode; 
        }


        public string RemoveFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("RemoveFirst: список порожній!");

            string value = head.Data; 
            head = head.Next;       
            return value;
        }

        public bool RemoveByValue(string value)
        {
            if (IsEmpty())
                throw new InvalidOperationException("RemoveByValue: список порожній!");

            if (head.Data == value)
            {
                head = head.Next;
                return true;
            }

            ListNode current = head;
            while (current.Next != null)
            {
                if (current.Next.Data == value)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }

            return false; 
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("  Список порожній.");
                return;
            }

            Console.Write("  Список: ");
            ListNode current = head;
            while (current != null)
            {
                Console.Write($"[{current.Data}]");
                if (current.Next != null) Console.Write(" → ");
                current = current.Next;
            }
            Console.WriteLine(" → null");
        }

        public ListNode GetHead()
        {
            return head;
        }
    }

}
