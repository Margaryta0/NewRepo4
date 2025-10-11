using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation;


namespace ConsoleApp
{

    internal class ConsoleMenu
    {
        static void Main(string[] args)
        {
            // Generic
            List<MyString> list = new List<MyString>();

            // Add
            list.Add(new MyString("Megi"));
            list.Add(new MyString("Olena"));
            list.Add(new MyString("Tatti"));
            list.Add(new MyString("Ruvim"));

            Console.WriteLine("List:");
            Display(list);

            // Update 
            list[0] = new MyString("Margo");
            Console.WriteLine("Update:");
            Display(list);

            // Search 
            var found = list.Find(ms => ms.Value.Contains("a"));
            if (found != null)
                Console.WriteLine("Search: " + found.Value + "\n");

            // Delete 
            list.RemoveAt(0);
            Console.WriteLine("Delete:");
            Display(list);



            MyString[] arr = new MyString[4];
            arr[0] = new MyString("Megi");
            arr[1] = new MyString("Olena");
            arr[2] = new MyString("Tatti");
            arr[3] = new MyString("Ruvim");

            Console.WriteLine("Array");
            Display(arr);

            // Update
            arr[0] = new MyString("Margo");
            Console.WriteLine("Update:");
            Display(arr);

            // Search 
            Console.Write("Search: ");
            foreach (MyString s in arr)
            {
                if (s != null && s.Value.Contains("a"))
                {
                    Console.WriteLine(s.Value + "\n");
                    break;
                }
            }

            // Delete 
            arr[2] = null;
            Console.WriteLine("Delete:");
            Display(arr);

            //Non-Generic 
            Console.WriteLine("ArrayList:");
            ArrayList nonGenericList = new ArrayList();

            // Add 
            nonGenericList.Add(new MyString("Olena"));
            nonGenericList.Add(new MyString("Tatti"));
            nonGenericList.Add(new MyString("Ruvim"));
            nonGenericList.Add(new MyString("Megi"));

            Display(nonGenericList);

            // Update 
            nonGenericList[0] = new MyString("Margo");
            Console.WriteLine("Update:");
            Display(nonGenericList);

            // Search
            Console.Write("Search: ");
            MyString foundArrayList = null;
            foreach (object item in nonGenericList)
            {
                if (item is MyString myStr && myStr.Value.Contains("o"))
                {
                    foundArrayList = myStr;
                    break;
                }
            }
            Console.WriteLine(foundArrayList?.Value ?? "Not Found");
            Console.WriteLine();

            // Delete 
            nonGenericList.RemoveAt(2);
            Console.WriteLine("Delete:");
            Display(nonGenericList);

            BinaryTree<MyString> initialAlphaTree = new BinaryTree<MyString>(); 

            initialAlphaTree.Add(new MyString("Olena"));
            initialAlphaTree.Add(new MyString("Tatti"));
            initialAlphaTree.Add(new MyString("Ruvim"));
            initialAlphaTree.Add(new MyString("Megi"));

            Console.WriteLine("Root element: " + new MyString("Olena").Value);
            Console.WriteLine();

            Console.WriteLine("1. Binary Tree Sorted by IComparable (Alphabetical):");


            // IEnumerable
            foreach (var item in initialAlphaTree) 
            {
                Console.WriteLine(item.Value);
            }
            Console.WriteLine();

            Console.WriteLine("2. Binary Tree Sorted by IComparer (By Length):");
            BinaryTree<MyString> lengthTree = new BinaryTree<MyString>(new MyStringLengthComparer());
            lengthTree.Add(new MyString("DDD"));       
            lengthTree.Add(new MyString("AA"));        
            lengthTree.Add(new MyString("EEEEE"));     
            lengthTree.Add(new MyString("B"));        

            Console.WriteLine("Postorder Traversal (Sorted by Length):");
            foreach (var item in lengthTree)
            {
                Console.WriteLine($"'{item.Value}' (Length: {item.Length})");
            }
            Console.WriteLine();
        }

        static void Display(List<MyString> list)
        {
            foreach (MyString s in list)
                Console.WriteLine(s.Value);
            Console.WriteLine();
        }
        static void Display(MyString[] arr)
        {
            foreach (MyString s in arr)
            {
                if (s != null)
                    Console.WriteLine(s.Value);
                else
                    Console.WriteLine("(null)");
            }
            Console.WriteLine();
        }

        static void Display(ArrayList list)
        {
            foreach (object item in list)
            {
                if (item is MyString s)
                    Console.WriteLine(s.Value);
                else
                    Console.WriteLine($"[Not MyString: {item.GetType().Name}]");
            }
            Console.WriteLine();
        }
    }
}