using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_02
{
    class Work
    {
        
        enum Arguments
        {
            Help,
            Seed,
            SeedHelp
        }

        private static readonly Dictionary<Arguments, string> arguments = new Dictionary<Arguments, string>
        {
        { Arguments.Help, "-h"},
        { Arguments.Seed, "-s"},
        { Arguments.SeedHelp, "-s <int> - seed for random number generator"},
        };
        enum Errors
        {
            ItemNotFound,
            RepeatInputError,
        }

        private static readonly Dictionary<Errors, string> errors = new Dictionary<Errors, string>
        {
        { Errors.ItemNotFound, "Элемент не найден."},
        { Errors.RepeatInputError, "Ошибка. Повторите ввод."}
        };

        enum Messages
        {
            ChooseOption,
            EnterNumber,
            PressAnyKey,
            From,
            To,
            NumbersList,
            Amount,
            Contain,
            NotContain,
            WhiteSpaceLine
        }

        private static readonly Dictionary<Messages, string> messages = new Dictionary<Messages, string>
        {
        { Messages.ChooseOption, "Выберите опцию:"},
        { Messages.EnterNumber, "Введите число: "},
        { Messages.PressAnyKey, "Нажмите любую клавишу."},
        { Messages.From, "от"},
        { Messages.To, "до"},
        { Messages.NumbersList, "Содержимое дерева"},
        { Messages.Amount, "всего"},
        { Messages.Contain, "Данное число присутствует в дереве."},
        { Messages.NotContain, "Данного числа нет в дереве."},
        { Messages.WhiteSpaceLine, "        "}
        };

       

        private static readonly string[] mainMenu = new string[]
        {
            "Добавить число в дерево",
            "Добавить случайное число в дерево\n",
            "Удалить число из дерева\n",
            "Проверить наличие числа в дереве\n",
            "Изменить способ отображения дерева\n",
            "Выход"
        };

        private const int VALUE_MIN = 0;

        private const int VALUE_MAX = 99;

        private const int ELEMENTS = 10;

        private static Random rnd;

        public void  Derevo ()
        {  
            int seed = 0;

            if (seed != 0)
                rnd = new Random(seed);
            else
                rnd = new Random();

            string mainMenuMessage = "\n" + messages[Messages.ChooseOption] + "\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";

            BTree tree = new BTree();

            for (int i = 0; i < ELEMENTS; i++)
            {
                AddRandomNumberToTree(tree, rnd);
                Console.Clear();
            }

            bool printMethod = false;

            Print(tree, printMethod);

            bool isExit = false;
            while (!isExit)
            {
                int input = NumberInput(mainMenuMessage, 1, mainMenu.Length);
                switch (input)
                {
                    case 1:
                        Print(tree, printMethod);
                        tree.AddNode(NumberInput(messages[Messages.EnterNumber], VALUE_MIN, VALUE_MAX, false));
                        Print(tree, printMethod);
                        break;
                    case 2:
                        AddRandomNumberToTree(tree, rnd);
                        Print(tree, printMethod);
                        break;
                    case 3:
                        Print(tree, printMethod);
                        tree.RemoveNode(NumberInput(messages[Messages.EnterNumber], VALUE_MIN, VALUE_MAX, false));
                        Print(tree, printMethod);
                        break;
                    case 4:
                        Print(tree, printMethod);
                        bool isContain = tree.Contains(NumberInput(messages[Messages.EnterNumber], VALUE_MIN, VALUE_MAX, false));
                        if (isContain)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(messages[Messages.Contain]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(messages[Messages.NotContain]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        MessageWaitKey(string.Empty);
                        Print(tree, printMethod);
                        break;
                    case 5:
                        printMethod = !printMethod;
                        Print(tree, printMethod);
                        break;
                    case 6:
                        isExit = true;
                        break;
                }

            }
        }

        private static void AddRandomNumberToTree(BTree tree, Random rnd)
        {
            bool isDone = false;
            while (!isDone)
            {
                int newValue = rnd.Next(VALUE_MIN, VALUE_MAX + 1);
                if (!tree.Contains(newValue))
                {
                    tree.AddNode(newValue);
                    isDone = true;
                }

            }
        }

        private static void Print(BTree tree, bool method = false)
        {
            Console.Clear();
            if (method)
            {
                tree.Print();
            }
            else
            {
                BTreePrinter.Print(tree.Root, "[0]", 4, 2, 2);
            }
        }
        private static int NumberInput(string message, int min, int max, bool isOneDigit = true)
        {
            bool isInputCorrect = false;
            int input = 0;
            Console.WriteLine($"{message}({messages[Messages.From]} {min} {messages[Messages.To]} {max})");
            while (!isInputCorrect)
            {
                if (isOneDigit)
                    isInputCorrect = int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                else
                    isInputCorrect = int.TryParse(Console.ReadLine(), out input);

                if (isInputCorrect && (input < min || input > max))
                    isInputCorrect = false;

                if (!isInputCorrect)
                    if (isOneDigit)
                        try
                        {
                            Console.CursorLeft--;
                        }
                        catch
                        {

                        }
                    else
                    {
                        Console.WriteLine(errors[Errors.RepeatInputError]);
                        try
                        {
                            Console.CursorLeft = 0;
                            Console.CursorTop -= 2;
                            Console.Write(messages[Messages.WhiteSpaceLine]);
                            Console.CursorLeft = 0;
                        }
                        catch
                        {

                        }
                    }
            }
            Console.WriteLine();
            return input;
        }



        private static void MessageWaitKey(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(messages[Messages.PressAnyKey]);
            Console.ReadKey();
        }
    }
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
    }

    public static class BTreePrinter
    {
        class NodeInfo
        {
            public Node node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this Node root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { node = next, Text = next.Value.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Print(item.Text, top, item.StartPos);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (item.Left != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (item.Right != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }

    public class Node : IComparable
    {
        private Node left;
        private Node right;
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
        public Node(int value, Node parent, BTree tree)
        {
            Value = value;
            Parent = parent;
            Tree = tree;
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



