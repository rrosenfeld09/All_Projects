using System;

namespace HumanOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Human richard = new Human("Richard");
            Human brad = new Human("Brad");

            richard.Attact(brad);
            // Console.WriteLine(brad.name);
        }
    }
}
