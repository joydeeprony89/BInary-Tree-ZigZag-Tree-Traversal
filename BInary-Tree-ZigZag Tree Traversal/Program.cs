using System;
using System.Collections.Generic;
using System.Linq;

namespace BInary_Tree_ZigZag_Tree_Traversal
{
    class Program
    {
        public class Node
        {
            public int value;
            public Node left, right;
            public Node(int val)
            {
                value = val;
                left = right = null;
            }
        }

        static void Main(string[] args)
        {
            Node root = new Node(1);
            root.left = new Node(2);
            root.right = new Node(3);
            root.left.left = new Node(7);
            root.left.right = new Node(6);
            root.right.left = new Node(5);
            root.right.right = new Node(4);

            var results = ZigZag(root); // 1 3 2 7 6 5 4

            foreach (var res in results) Console.WriteLine(string.Join(" ", res));

            results = ZigZagUsingQueue(root); // 1 3 2 7 6 5 4

            foreach (var res in results) Console.WriteLine(string.Join(" ", res));
        }

        static IList<IList<int>> ZigZag(Node root)
        {
            IList<IList<int>> results = new List<IList<int>>();
            if (root == null) return results;

            Stack<Node> current = new Stack<Node>(), prev = new Stack<Node>();
            current.Push(root);
            bool leftToRight = true;
            List<int> list = new List<int>();
            while (current.Count > 0)
            {
                var node = current.Pop();
                list.Add(node.value);

                if (leftToRight && node != null)
                {
                    Add(prev, node.left);
                    Add(prev, node.right);
                }
                else
                {
                    Add(prev, node.right);
                    Add(prev, node.left);
                }

                if (current.Count == 0)
                {
                    leftToRight = !leftToRight;
                    var temp = current;
                    current = prev;
                    prev = temp;
                    results.Add(list);
                    list = new List<int>();
                }
            }

            return results;
        }

        static void Add(Stack<Node> stack, Node node)
        {
            if (node != null)
            {
                stack.Push(node);
            }
        }

        static IList<IList<int>> ZigZagUsingQueue(Node root)
        {
            IList<IList<int>> results = new List<IList<int>>();
            if (root == null) return results;
            bool rightToLeft = false;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                IList<int> list = new List<int>();
                int count = queue.Count;
                while(count-- > 0)
                {
                    Node node = queue.Dequeue();
                    list.Add(node.value);

                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }

                if (rightToLeft) list.Reverse();
                results.Add(list);
                rightToLeft = !rightToLeft;
            }

            return results;
        }
    }
}
