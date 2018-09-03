using System;

namespace Phones
{
    class Program
    {
        static void Main(string[] args)
        {
            Galaxy s8 = new Galaxy("s8", 100, "Sprint", "ding ding ding");
            Nokia elevenHundred = new Nokia("1100", 80, "Verizon", "boop bot beep");

            s8.DisplayInfo();
            Console.WriteLine(s8.Ring());
            Console.WriteLine(s8.Unlock());

            elevenHundred.DisplayInfo();
            Console.WriteLine(elevenHundred.Ring());
            Console.WriteLine(elevenHundred.Unlock());
        }
    }
}
