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
            else if (numberr == 3)
            {
                BenchmarkClass link = new BenchmarkClass();
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            }
            else if (numberr == 4)
            {
                ListFull go = new ListFull();
                go.ProgramList();
            }


        }
    }
}
