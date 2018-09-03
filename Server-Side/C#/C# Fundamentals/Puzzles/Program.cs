using System;
using System.Collections.Generic;

namespace Puzzles
{
    class Program
    {
        static void Main(string[] args)
        {
            // RandomArray();
            // TossCoin();
            TossMultipleCoins(5);
        }

        // Random Array
        public static int[] RandomArray()
        {
            Random rand = new Random();
            List<int> newList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                newList.Add(rand.Next(5, 26));
            }
            int[] final = newList.ToArray();
            foreach (int i in final)
            {
                Console.WriteLine(i);
            }
            return final;
        }

        // Coin Flip
        public static string TossCoin()
        {
            Console.WriteLine("Tossing Coin...");
            Random rand = new Random();
            int result = rand.Next(0,2);
            string final = "";
            if (result == 0)
            {
                final = "Tails";
            }
            else
            {
                final = "Heads";
            }
            Console.WriteLine(final);
            return final;
        }

        public static double TossMultipleCoins(int num)
        {
            double numHeads = 0;
            double total = 0;
            for (int i = 0; i < num; i++)
            {
                string result = TossCoin();
                if (result == "Heads")
                {
                    numHeads += 1;
                    total += 1;
                }
                else
                {
                    total += 1;
                }
            }
            double final = numHeads/total;
            Console.WriteLine(final);
            return final;
        }

        // Names
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
