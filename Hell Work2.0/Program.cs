using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Hell_Work2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введи число от 1 до 3 , что бы начать проверку");

            int numberr = Convert.ToInt32(Console.ReadLine());

            if (numberr == 1)
            {
                PointClass linq = new PointClass();
                linq.foo();
            }
            else if (numberr == 2)
            {
                PointStruct link = new PointStruct();
                link.qoo();
            }
            else if (numberr == 3)
            {
                Class1 linq = new Class1();
                
            }
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

        }


    }

}

public class BechmarkClass
{
    public int SumValueType(int value)
    {
        return 9 + value;
    }

    public int SumRefType(object value)
    {
        return 9 + (int)value;
    }

    [Benchmark]
    public void TestSum()
    {
        SumValueType(99);
    }

    [Benchmark]
    public void TestSumBoxing()
    {
        object x = 99;
        SumRefType(x);
    }

}
