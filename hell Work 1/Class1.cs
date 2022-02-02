using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hell_Work_1
{
    class Class1
    {
        public void foo()
        {
          bool isRun = true;
            while (isRun)
            {

                bool prost = true;
                Console.WriteLine("Введите число\n");
                int n = int.Parse(Console.ReadLine());
                for (int i = 2; i <= n / 2; i++)
                {
                    if (n % i == 0)
                    {
                        prost = false;
                        break;
                    }
                }
                if (prost)
                {
                    Console.WriteLine("Число простое");
                }
                else
                {
                    Console.WriteLine("Число не простое");
                }
                Console.WriteLine("Хотите продолжить? Y/N");
                string userAnswer = Console.ReadLine();

                if (userAnswer.ToUpper() == "N")
                {
                    isRun = false;
                }
            }
            
        } 
    }
}
