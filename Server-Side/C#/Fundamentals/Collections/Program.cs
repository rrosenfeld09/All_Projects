using System;
using System.Collections.Generic;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] num_values = new int[10] {0,1,2,3,4,5,6,7,8,9};
            // for(int i = 0; i < num_values.Length; i++)
            // {
            //     Console.WriteLine(num_values[i]);
            // }

            // string[] names = new string[4] {"Tim", "Martin", "Nikki", "sara"};
            // for(int i = 0; i < names.Length; i++)
            // {
            //     Console.WriteLine(names[i]);
            // }

            // bool[] TrueFalse = new bool[10] {true, false, true, false, true, false, true, false, true, false};
            // for(int i = 0; i < TrueFalse.Length; i++)
            // {
            //     Console.WriteLine(TrueFalse[i]);
            // }

            // List<string> IceCream = new List<string>();
            // IceCream.Add("Vanilla");
            // IceCream.Add("Chocolate");
            // IceCream.Add("Strawberry");
            // IceCream.Add("Coffee");
            // IceCream.Add("Something");
            // // for(int i = 0; i < IceCream.Count; i++)
            // // {
            // //     Console.WriteLine(IceCream[i]);
            // // }
            // // foreach(string flavor in IceCream)
            // // {
            // //     Console.WriteLine(flavor);
            // // }

            // // Console.WriteLine(IceCream[2]);

            // IceCream.RemoveAt(2);
            // foreach(string flavor in IceCream)
            // {
            //     Console.WriteLine(flavor);
            // }


            Dictionary<string,string> UserInfo = new Dictionary<string,string>();
            UserInfo.Add("Tim", null);
            UserInfo.Add("Martin", null);
            UserInfo.Add("Nikki", null);
            UserInfo.Add("Sara", null);

            UserInfo["Tim"] = "Vanilla";
            UserInfo["Martin"] = "Chocolate";
            UserInfo["Nikki"] = "Strawberry";
            UserInfo["Sara"] = "Coffee";

            foreach(var name in UserInfo)
            {
                Console.WriteLine(name.Key + " - " + name.Value);
            } 
        }
    }
}
