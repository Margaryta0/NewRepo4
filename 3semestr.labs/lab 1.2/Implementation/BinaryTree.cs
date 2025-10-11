using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class BinaryTree<T> : IEnumerable<T>, IEnumerable where T : class
    {
        private Node<T> Root { get; set; }
        private readonly IComparer<T> _comparer;

        public BinaryTree(IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                {
                    _comparer = Comparer<T>.Default;
                }
                else
                {
                    throw new ArgumentException("Type T must implement IComparable<T> or an IComparer<T> must be provided.");
                }
            }
            else
            {
                _comparer = comparer;
            }
        }

        public void Add(T data)
        {
            Root = AddRecursive(Root, data);
        }

        private Node<T> AddRecursive(Node<T> current, T data)
        {
            if (current == null)
            {
                return new Node<T>(data);
            }

            int comparisonResult = _comparer.Compare(data, current.Data);

            if (comparisonResult < 0)
            {
                current.Left = AddRecursive(current.Left, data);
            }
            else if (comparisonResult > 0)
            {
                current.Right = AddRecursive(current.Right, data);
            }
            return current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PostorderTraversal(Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private IEnumerable<T> PostorderTraversal(Node<T> node)
        {
            if (node == null)
                yield break;

            foreach (T item in PostorderTraversal(node.Left))
            {
                yield return item;
            }

            foreach (T item in PostorderTraversal(node.Right))
            {
                yield return item;
            }

            yield return node.Data;
        }

    }
}