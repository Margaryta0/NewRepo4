using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3
{
    class BinaryTree
    {
        private TreeNode _root;

        public BinaryTree() { _root = null; }

        public void Add(Student student)
        {
            _root = Insert(_root, student);
        }

        private TreeNode Insert(TreeNode node, Student student)
        {
            if (node == null)
                return new TreeNode(student);

            if (student.StudentID < node.Data.StudentID)
                node.Left  = Insert(node.Left, student);
            else if (student.StudentID > node.Data.StudentID)
                node.Right = Insert(node.Right, student);
            else
                Console.WriteLine($"[!] Студент з ID {student.StudentID} вже існує в дереві.");

            return node;
        }

        // Паралельний обхід (BFS — по рівнях) 
        public void PrintParallel()
        {
            PrintTableHeader();

            if (_root == null)
            {
                Console.WriteLine("Дерево порожнє.");
                return;
            }

            var queue = new Queue<TreeNode>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Console.WriteLine(node.Data);

                if (node.Left  != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }

            PrintTableFooter();
        }

        public List<Student> Search(int course, string language)
        {
            var result = new List<Student>();
            SearchRecursive(_root, course, language, result);
            return result;
        }

        private void SearchRecursive(TreeNode node, int course,
                                     string language, List<Student> result)
        {
            if (node == null) return;

            SearchRecursive(node.Left, course, language, result);

            if (node.Data.Course == course &&
                node.Data.Language.Equals(language,
                    StringComparison.OrdinalIgnoreCase))
                result.Add(node.Data);

            SearchRecursive(node.Right, course, language, result);
        }

        public void Delete(int course, string language)
        {
            _root = DeleteMatching(_root, course, language);
        }

        private TreeNode DeleteMatching(TreeNode node, int course, string language)
        {
            if (node == null) return null;

            node.Left  = DeleteMatching(node.Left, course, language);
            node.Right = DeleteMatching(node.Right, course, language);

            bool matches = node.Data.Course == course &&
                           node.Data.Language.Equals(language,
                               StringComparison.OrdinalIgnoreCase);

            if (!matches) return node;

            // Випадок 1: немає дочірніх вузлів
            if (node.Left == null && node.Right == null)
                return null;

            // Випадок 2: лише правий дочірній
            if (node.Left == null)
                return node.Right;

            // Випадок 2: лише лівий дочірній
            if (node.Right == null)
                return node.Left;

            // Випадок 3: два дочірніх — замінюємо мінімальним з правого підд.
            TreeNode minNode = FindMin(node.Right);
            node.Data       = minNode.Data;
            node.Right      = RemoveMin(node.Right);
            return node;
        }

        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        private TreeNode RemoveMin(TreeNode node)
        {
            if (node.Left == null) return node.Right;
            node.Left = RemoveMin(node.Left);
            return node;
        }
        private static void PrintTableHeader()
        {
            Console.WriteLine(new string('─', 72));
            Console.WriteLine($"{"ID",8} | {"Прізвище",-12} | {"Ім'я",-10} | " +
                              $"{"Курс",5} | {"Група",-6} | Мова");
            Console.WriteLine(new string('─', 72));
        }

        private static void PrintTableFooter()
            => Console.WriteLine(new string('─', 72));
    }
}
