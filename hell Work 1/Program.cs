using System;

namespace hell_Work_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер проекта:");
            Console.WriteLine("1. Проверить является ли число простым или сложным");
            Console.WriteLine("2. решение числа Фибоначчи");
            int numberr = Convert.ToInt32(Console.ReadLine());
          
            if (numberr == 1)
            {
                Class1 linq = new Class1();
                linq.foo();
            }
            else if (numberr == 2)
            {
                Class2 link = new Class2();
                link.doo();
            }
            
            
        }
    }
}
