using System;

namespace Human
{
    class Program
    {
        static void Main(string[] args)
        {
            Human steven = new Human("Steven");
            Human rob = new Human("Rob");

            steven.attack(rob);
            rob.attack(steven);
        }
    }
}
