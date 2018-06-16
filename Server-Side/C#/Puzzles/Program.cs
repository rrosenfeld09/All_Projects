using System;
using System.Collections.Generic;

namespace puzzles
{
    class Program
    {
        static void Main(string[] args)
        {
            // RandomArray();
            // CoinFlip();
            // TossMultipleCoins(100);
            // Names();
        }

        // random array
        public static int[] RandomArray()
        {
            List<int> FirstList = new List<int>();
            Random RandNum = new Random();
            for(int i = 0; i < 10; i++)
            {
                FirstList.Add(RandNum.Next(5,26));
            }
            foreach (int num in FirstList)
            {
                Console.WriteLine(num);
            }
            int[] final = FirstList.ToArray();
            return final;
        }

        // Coin flip
        public static string CoinFlip()
        {
            Random flip = new Random();
            int result = flip.Next(0,2);
            string final = "";
            if(result == 0)
            {
                Console.WriteLine("Tails!");
                Console.WriteLine(result);
                final = "tails";
            }
            else
            {
                Console.WriteLine("Heads!");
                Console.WriteLine(result);
                final = "heads";            
            }
            return final;
        }

        public static double TossMultipleCoins(int x)
        {
            double tally = 0;
            for(int i = 0; i < x; i++)
            {
               string result = CoinFlip();
               if(result == "heads")
               {
                    Console.WriteLine("=====" + result + "=====");
                    tally++;
               }
               else
               {
                    Console.WriteLine("+++++" + result + "+++++");
               }
            }
            double final = tally/x;
            Console.WriteLine(final);
            return final;
        }

        // names
        public static string[] Names()
        {
            string[] arr = {"Todd", "Tiffany", "Charlie", "Geneva", "Sydney"};
            List<string> BaseList = new List<string>();
            for(int i = 0; i < arr.Length; i++)
            {
                BaseList.Add(arr[i]);
            }

            List<string> NewList = new List<string>();
            int randomIndex = 0;
            Random rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                randomIndex = rand.Next(0, BaseList.Count);
                NewList.Add(BaseList[randomIndex]);
                BaseList.RemoveAt(randomIndex);
            }

            string[] NameArray = NewList.ToArray();
            
            List<string> ReturnList = new List<string>();
            for(int i = 0; i < NewList.Count; i++)
            {
                if(NewList[i].Length > 5)
                {
                    ReturnList.Add(NewList[i]);
                }
            }

            string[] final = ReturnList.ToArray();
            foreach(string name in final)
            {
                Console.WriteLine(name);
            }

            return final;
        }

    }
}
