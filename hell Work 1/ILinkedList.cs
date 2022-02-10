using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static hell_Work_1.ListFull.Node;

namespace hell_Work_1
{
    public class ListFull
    {
        public class Node
        {
            public int Value { get; set; }
            public Node NextNode { get; set; }
            public Node PrevNode { get; set; }

            public Node(int value)
            {
                Value = value;
            }
            public interface ILinkedList
            {
                int GetCount(); // возвращает количество элементов в списке
                void AddNode(int value);  // добавляет новый элемент списка
                void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
                void RemoveNode(int index); // удаляет элемент по порядковому номеру
                void RemoveNode(Node node); // удаляет указанный элемент
                void RemoveFirst(); // удаляет первую ноду в списке
                void RemoveLast(); // удаляет последнюю ноду в списке
                void ClearList(); // очищает список
                Node FindNode(int searchValue); // ищет элемент по его значению
                bool CheckNode(Node node); // проверяет есть ли такой элемент в списке
                Node FindNodeByIndex(int index); // ищет элемент по его индексу
            }


            public class TwoLinkedList : ILinkedList
            {
                private Node startNode;
                private Node endNode;
                private int count = 0;
                
                public void AddNode(int value)
                {
                    if (count != 0)
                    {
                        endNode.NextNode = new Node(value);
                        endNode.NextNode.PrevNode = endNode;
                        endNode = endNode.NextNode;
                    }
                    else
                    {
                        startNode = new Node(value);
                        endNode = startNode;
                    }
                    count++;
                }                
                public void AddNodeAfter(Node node, int value)
                {
                    if (CheckNode(node))
                    {
                        if (node == endNode)
                        {
                            AddNode(value);
                        }
                        else 
                        {
                            Node nextNode = node.NextNode;//Сохраняем следующую ноду для дальнейшего использования
                            node.NextNode = new Node(value);//Создаем новую ноду после найденной
                            node.NextNode.NextNode = nextNode;//У новой ноды устанавливаем ссылку на следующую
                            node.NextNode.PrevNode = node;//И на предыдущую
                            nextNode.PrevNode = node.NextNode;//Обновляем ссылку на предыдущую ноду у ранее сохраненной
                            count++;//Обновляем количество
                        }
                    }
                }                
                public Node FindNode(int searchValue)
                {
                    Node currentNode = startNode;

                    bool isSearchEnd = false;
                    while (!isSearchEnd)
                    {
                        if (currentNode != null)
                            if (currentNode.Value == searchValue)
                                isSearchEnd = true;
                            else
                                currentNode = currentNode.NextNode;
                        else
                            isSearchEnd = true;
                    }

                    return currentNode;
                }

                public int GetCount()
                {
                    return count;
                }

                public void RemoveNode(int index)
                {
                    RemoveNode(FindNodeByIndex(index));
                }

                public void RemoveNode(Node node)
                {
                    if (node != null && CheckNode(node))
                    {
                        if (node == startNode) RemoveFirst();
                        else if (node == endNode) RemoveLast();
                        else
                        {
                            node.PrevNode.NextNode = node.NextNode;
                            node.NextNode.PrevNode = node.PrevNode;
                            count--;
                        }
                    }
                }
                public Node FindNodeByIndex(int index)
                {
                    Node currentNode = null;
                    if (index >= 0 && index < count)
                    {
                        if (index <= count / 2)
                        {
                            currentNode = startNode;
                            int i = 0;
                            while (i < index)
                            {
                                currentNode = currentNode.NextNode;
                                i++;
                            }
                        }
                        else
                        {
                            currentNode = endNode;
                            int i = count - 1;
                            while (i > index)
                            {
                                currentNode = currentNode.PrevNode;
                                i--;
                            }
                        }
                    }
                    return currentNode;
                }                
                public void RemoveFirst()
                {
                    if (count > 1)
                    {
                        startNode.NextNode.PrevNode = null;
                        startNode = startNode.NextNode;
                        count--;
                    }
                    else if (count == 0)
                    {
                        ClearList();
                    }
                }                
                public void RemoveLast()
                {
                    if (count > 1)
                    {
                        endNode.PrevNode.NextNode = null;
                        endNode = endNode.PrevNode;
                        count--;
                    }
                    else if (count == 0)
                    {
                        ClearList();
                    }
                }
                public void ClearList()
                {
                    startNode = null;
                    endNode = null;
                    count = 0;
                }

               
                public bool CheckNode(Node node)
                {
                    bool isNodeInList = false;
                    if (count != 0)
                    {
                        int i = 0;
                        Node currentNode = startNode;
                        while (!isNodeInList && i < count)
                        {
                            isNodeInList = currentNode == node;
                            currentNode = currentNode.NextNode;
                            i++;
                        }
                    }
                    return isNodeInList;
                }
            }


        }
        public void ProgramList()
        {
            ILinkedList list = new TwoLinkedList();


            list.AddNode(12);
            list.AddNode(-5);
            list.AddNode(8);
            list.AddNode(1231);
            list.AddNode(11);
            list.AddNode(-4);
            list.AddNode(88);
            list.AddNode(456);


            Console.WriteLine($"Количество элементов в списке: {list.GetCount()}\n");
            PrintList(list);
            Console.ReadLine();


            Node testNode = list.FindNodeByIndex(4);
            list.AddNodeAfter(testNode, 1000);
            Console.WriteLine("Добавлен элемент со значением 1000, после элемента с индексом 4");
            PrintList(list);
            Console.ReadLine();


            list.RemoveFirst();
            list.RemoveLast();
            Console.WriteLine("Удалены первый и последний элементы списка");
            PrintList(list);
            Console.ReadLine();

            list.RemoveNode(5);
            Console.WriteLine("Удален элемент с индексом 5");
            PrintList(list);
            Console.ReadLine();

            testNode = list.FindNode(88);
            list.RemoveNode(testNode);
            Console.WriteLine("Удален элемент со значением 88");
            PrintList(list);
            Console.ReadLine();

            list.ClearList();
            Console.WriteLine("Список полностью очищен");
            PrintList(list);

            Console.ReadLine();
        }
        private static void PrintList(ILinkedList list)
        {
            Console.WriteLine("Полный список элементов по индексу:");
            for (int i = 0; i < list.GetCount(); i++)
            {
                Console.WriteLine(i + ":" + list.FindNodeByIndex(i).Value);
            }
            Console.WriteLine();
        }

    }


}



