using System;
using System.Collections.Generic;

namespace Phones
{
    class Program
    {
        static void Main(string[] args)
        {
            Galaxy s8 = new Galaxy("s8", 100, "Sprint", "Boo bop beep");
            Nokia elevenHundred = new Nokia("1100", 80, "Verizon", "Zip zoop zeep");

            s8.DisplayInfo();
            elevenHundred.DisplayInfo();

        }
    }
}
