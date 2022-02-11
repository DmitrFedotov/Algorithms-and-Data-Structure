using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hell_Work_1
{
    class Class2
    {
        public void Fib() 
        {
            Console.WriteLine("Выберите вывод:");
            Console.WriteLine("1. Вывод без рекурсии");
            Console.WriteLine("2. Вывод рекурсией");
            int fib = Convert.ToInt32(Console.ReadLine());
            if (fib == 1)
            {
                System.Console.WriteLine("Введите число Фибоначи");
                string FindFeb = System.Console.ReadLine();
                int FindFebN = Convert.ToInt32(FindFeb);
                int Searchfib = FibonachiNumber(FindFebN);
                Console.WriteLine($"Число Фибоначи = {Searchfib}");

                int FibonachiNumber(int a)
                {
                    if (a == 0 || a == 1) return a;

                    return FibonachiNumber(a - 1) + FibonachiNumber(a - 2);
                }

            }
            else if (fib == 2)
            {
                for (int i = 1; i < 100; i++)
                {
                    Console.WriteLine(Fib(i));
                }
                static long Fib(int n)
                {
                    if (n < 3)
                        return 1;
                    else
                        return Fib(n - 2) + Fib(n - 1);
                }

            }

            
        }
    }
}
