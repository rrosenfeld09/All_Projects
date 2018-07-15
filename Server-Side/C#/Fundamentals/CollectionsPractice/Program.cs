using System;
using System.Collections.Generic;

namespace CollectionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {

            // three basic arrays
            int[] newArray = new int[] {0,1,2,3,4,5,6,7,8,9};
            string[] secondArray = new string[] {"Tim", "Martin", "Nikki", "Sara"};
            bool[] thirdArray = new bool[] {true, false,true, false,true, false,true, false,true, false};
            // foreach (bool i in thirdArray)
            // {
            //     Console.WriteLine(i);
            // }

            // list of flavors
            List<string> flavors = new List<string> {"vanilla", "chocolate", "strawberry", "mint", "chocolate chip"};
            // foreach (string i in flavors)
            // {
            //     Console.WriteLine(i);
            // }
            // Console.WriteLine(flavors[2]);
            // Console.WriteLine(flavors.Count);
            // flavors.RemoveAt(2);
            // Console.WriteLine(flavors.Count);
            

            // user info dictionary
            Dictionary<string, string> firstDict = new Dictionary<string, string>();
            firstDict.Add("Tim", null);
            firstDict.Add("Martin", null);
            firstDict.Add("Nikki", null);
            firstDict.Add("Sara", null);
            firstDict["Tim"] = "vanilla";
            firstDict["Martin"] = "chocolate";
            firstDict["Nikki"] = "strawberry";
            firstDict["Sara"] = "minit";

            foreach (var person in firstDict)
            {
                Console.WriteLine("Name of user: {0}", person.Key);
                Console.WriteLine("Their favorite ice cream: {0}", person.Value);
            }
        }
    }
}
