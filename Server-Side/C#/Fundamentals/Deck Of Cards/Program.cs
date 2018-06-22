using System;

namespace Deck_Of_Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Player x = new Player();
            x.draw();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(x.hand[i].stringVal);
            }
            
        }
    }
}
