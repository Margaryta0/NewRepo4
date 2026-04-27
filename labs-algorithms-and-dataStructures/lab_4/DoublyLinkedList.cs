using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    class DoublyLinkedList
    {
        DNode _head;
        DNode _tail;

        public void Add(Student s)
        {
            var node = new DNode(s);
            if (_tail == null) { _head = _tail = node; return; }
            node.Prev  = _tail;
            _tail.Next = node;
            _tail      = node;
        }

        public void CocktailSort()
        {
            if (_head == null || _head == _tail) return;

            DNode leftBound = _head;
            DNode rightBound = _tail;
            bool swapped;

            while (leftBound != rightBound &&
                   leftBound.Prev != rightBound)
            {
                swapped = false;

                // зліва напрво
                for (DNode cur = leftBound; cur != rightBound; cur = cur.Next)
                {
                    if (cur.Data.TaxCode < cur.Next.Data.TaxCode)
                    {
                        SwapData(cur, cur.Next);
                        swapped = true;
                    }
                }
                rightBound = rightBound.Prev;

                if (!swapped) break;
                swapped = false;

                // справа наліво
                for (DNode cur = rightBound; cur != leftBound; cur = cur.Prev)
                {
                    if (cur.Data.TaxCode > cur.Prev.Data.TaxCode)
                    {
                        SwapData(cur, cur.Prev);
                        swapped = true;
                    }
                }
                leftBound = leftBound.Next;

                if (!swapped) break;
            }
        }

        static void SwapData(DNode a, DNode b)
        {
            Student t = a.Data; a.Data = b.Data; b.Data = t;
        }

        public void PrintList()
        {
            TablePrinter.Header();
            for (DNode cur = _head; cur != null; cur = cur.Next)
                Console.WriteLine(cur.Data);
            TablePrinter.Footer();
        }
    }
}
