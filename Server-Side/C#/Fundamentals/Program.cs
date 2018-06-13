using System;

namespace Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {

            // for(int i = 1; i <=100; i++)
            // {
            //     if (i % 3 == 0 && i % 5 == 0)
            //     {
            //         Console.WriteLine("FizzBuzz");
            //     }
            //     else if (i % 3 == 0)
            //     {
            //         Console.WriteLine("Fizz");
            //     }
            //     else if( i % 5 == 0)
            //     {
            //         Console.WriteLine("Buzz");
            //     }
            // }
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rand.Next();
                if (x % 5 == 0 && x % 3 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (x % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (x % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
            }
        }
    }
}
