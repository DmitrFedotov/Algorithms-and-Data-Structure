using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hell_Work_1.work4
{
    class Derevo
    {

        public class BTree
        {
            public Node Root
            {
                get;
                internal set;
            }
            public int Count
            {
                get;
                private set;
            }

            public void AddNode(int value)
            {
                if (Root == null)
                    Root = new Node(value, null, this);

                else
                    AddToNode(Root, value);

                Count++;
            }
            private void AddToNode(Node node, int value)
            {
                int check = value.CompareTo(node.Value);
                if (check < 0)
                {
                    if (node.Left == null)
                        node.Left = new Node(value, node, this);
                    else
                        AddToNode(node.Left, value);
                }

                else if (check > 0)
                {
                    if (node.Right == null)
                        node.Right = new Node(value, node, this);
                    else
                        AddToNode(node.Right, value);
                }
                node.Balance();
            }
            public bool Contains(int value)
            {
                return Find(value) != null;
            }
            private Node Find(int value)
            {
                Node current = Root;
                while (current != null)
                {
                    int result = current.CompareTo(value);
                    if (result > 0)
                        current = current.Left;
                    else if (result < 0)
                        current = current.Right;
                    else
                        break;
                }
                return current;
            }
            public int GetCount()
            {
                return Count;
            }
            public bool RemoveNode(int value)
            {
                Node current;
                current = Find(value);
                if (current == null)
                    return false;
                Node treeToBalance = current.Parent;
                Count--;
                if (current.Right == null)
                {
                    if (current.Parent == null)
                    {
                        Root = current.Left;
                        if (Root != null)
                            Root.Parent = null;
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);
                        if (result > 0)
                            current.Parent.Left = current.Left;
                        else if (result < 0)
                            current.Parent.Right = current.Left;
                    }
                }
                else if (current.Right.Left == null)
                {
                    current.Right.Left = current.Left;
                    if (current.Parent == null)
                    {
                        Root = current.Right;
                        if (Root != null)
                            Root.Parent = null;
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);
                        if (result > 0)
                            current.Parent.Left = current.Right;
                        else if (result < 0)
                            current.Parent.Right = current.Right;
                    }
                }
                else
                {
                    Node leftmost = current.Right.Left;
                    while (leftmost.Left != null)
                        leftmost = leftmost.Left;
                    leftmost.Parent.Left = leftmost.Right;
                    leftmost.Left = current.Left;
                    leftmost.Right = current.Right;
                    if (current.Parent == null)
                    {
                        Root = leftmost;
                        if (Root != null)
                            Root.Parent = null;
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);
                        if (result > 0)
                            current.Parent.Left = leftmost;
                        else if (result < 0)
                            current.Parent.Right = leftmost;
                    }
                }
                if (treeToBalance != null)
                    treeToBalance.Balance();
                else
                {
                    if (Root != null)
                        Root.Balance();
                }

                return true;
            }
            public void ClearTree()
            {
                Root = null;
                Count = 0;
            }
            public void Print()
            {
                if (Root != null)
                    Root.PrintNode("", Node.NodePosition.center, true, false);
                else
                    Console.WriteLine("Tree is empty.");
            }
            public class Node : IComparable
            {
                private Node left;
                private Node right;
                private BTree bTree;

                public BTree Tree
                {
                    get;
                    private set;
                }

                public Node Left
                {
                    get
                    {
                        return left;
                    }

                    internal set
                    {
                        left = value;

                        if (left != null)
                        {
                            left.Parent = this;
                        }
                    }
                }

                public Node Right
                {
                    get
                    {
                        return right;
                    }

                    internal set
                    {
                        right = value;

                        if (right != null)
                        {
                            right.Parent = this;
                        }
                    }
                }

                public Node Parent
                {
                    get;
                    internal set;
                }

                public int Value
                {
                    get;
                    private set;
                }

                private object p;

                public Node(int value, Node parent, BTree tree)
                {
                    Value = value;
                    Parent = parent;
                    Tree = tree;
                }

                public Node(int value, object p, BTree bTree)
                {
                    Value = value;
                    this.p = p;
                    this.bTree = bTree;
                }

                public int CompareTo(object other)
                {
                    return Value.CompareTo(other);
                }

                internal void Balance()
                {
                    if (State == TreeState.RightHeavy)
                    {
                        if (Right != null && Right.BalanceFactor < 0)
                        {
                            LeftRightRotation();
                        }

                        else
                        {
                            LeftRotation();
                        }
                    }
                    else if (State == TreeState.LeftHeavy)
                    {
                        if (Left != null && Left.BalanceFactor > 0)
                        {
                            RightLeftRotation();
                        }
                        else
                        {
                            RightRotation();
                        }
                    }
                    if (this.Parent != null) this.Parent.Balance();
                }

                private int MaxChildHeight(Node node)
                {
                    if (node != null)
                    {
                        return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
                    }
                    return 0;
                }

                private int LeftHeight
                {
                    get
                    {
                        return MaxChildHeight(Left);
                    }
                }

                private int RightHeight
                {
                    get
                    {
                        return MaxChildHeight(Right);
                    }
                }

                private TreeState State
                {
                    get
                    {
                        if (LeftHeight - RightHeight > 1)
                        {
                            return TreeState.LeftHeavy;
                        }

                        if (RightHeight - LeftHeight > 1)
                        {
                            return TreeState.RightHeavy;
                        }

                        return TreeState.Balanced;
                    }
                }

                private int BalanceFactor
                {
                    get
                    {
                        return RightHeight - LeftHeight;
                    }
                }

                enum TreeState
                {
                    Balanced,
                    LeftHeavy,
                    RightHeavy,
                }
                private void LeftRotation()
                {
                    Node newRoot = Right;
                    ReplaceRoot(newRoot);
                    Right = newRoot.Left;
                    newRoot.Left = this;
                }

                private void RightRotation()
                {
                    Node newRoot = Left;
                    ReplaceRoot(newRoot);
                    Left = newRoot.Right;
                    newRoot.Right = this;
                }
                private void LeftRightRotation()
                {
                    Right.RightRotation();
                    LeftRotation();
                }
                private void RightLeftRotation()
                {
                    Left.LeftRotation();
                    RightRotation();
                }
                private void ReplaceRoot(Node newRoot)
                {
                    if (this.Parent != null)
                    {
                        if (this.Parent.Left == this)
                        {
                            this.Parent.Left = newRoot;
                        }
                        else if (this.Parent.Right == this)
                        {
                            this.Parent.Right = newRoot;
                        }
                    }
                    else
                    {
                        Tree.Root = newRoot;
                    }

                    newRoot.Parent = this.Parent;
                    this.Parent = newRoot;
                }
                public enum NodePosition
                {
                    left,
                    right,
                    center
                }
                private void PrintValue(string value, NodePosition nodePostion)
                {
                    switch (nodePostion)
                    {
                        case NodePosition.left:
                            PrintLeftValue(value);
                            break;
                        case NodePosition.right:
                            PrintRightValue(value);
                            break;
                        case NodePosition.center:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(value);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                private void PrintLeftValue(string value)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("L:");
                    Console.ForegroundColor = (value == "--") ? ConsoleColor.Red : ConsoleColor.Yellow;
                    Console.WriteLine(value);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                private void PrintRightValue(string value)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("R:");
                    Console.ForegroundColor = (value == "--") ? ConsoleColor.Red : ConsoleColor.Yellow;
                    Console.WriteLine(value);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                public void PrintNode(string indent, NodePosition nodePosition, bool last, bool empty)
                {
                    Console.Write(indent);
                    if (last)
                    {
                        Console.Write("└────");
                        indent += "     ";
                    }
                    else
                    {
                        Console.Write("├────");
                        indent += "│    ";
                    }

                    var stringValue = empty ? "--" : Value.ToString();
                    PrintValue(stringValue, nodePosition);

                    if (!empty && (this.Left != null || this.Right != null))
                    {

                        if (this.Right != null)
                            this.Right.PrintNode(indent, NodePosition.right, false, false);
                        else
                            PrintNode(indent, NodePosition.right, false, true);

                        if (this.Left != null)
                            this.Left.PrintNode(indent, NodePosition.left, true, false);
                        else
                            PrintNode(indent, NodePosition.left, true, true);
                    }
                }
               
            }
        }

        
    }

}

   



