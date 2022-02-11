using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Diagnosers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hell_Work_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер проекта:");
            Console.WriteLine("1. Проверить является ли число простым или сложным");
            Console.WriteLine("2. решение числа Фибоначчи");
            Console.WriteLine("3. Benchmark Test");
            Console.WriteLine("4. Работа со списками");
            Console.WriteLine("5. Дерево поиска с операциями вставки");
            int numberr = Convert.ToInt32(Console.ReadLine());
          
            if (numberr == 1)
            {
                Class1 linq = new Class1();
                linq.SimpleComplexNumber();
            }
            else if (numberr == 2)
            {
                Class2 link = new Class2();
                link.Fib();
            }
            else if (numberr == 3)
            {
                BenchmarkClassWork2 link = new BenchmarkClassWork2();
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            }
            else if (numberr == 4)
            {
                ListFull go = new ListFull();
                go.ProgramList();
            }
            else if (numberr == 5)
            {
                Console.WriteLine("Нажмите 1");
                Work4 link = new Work4();
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            }


        }
    }
}
