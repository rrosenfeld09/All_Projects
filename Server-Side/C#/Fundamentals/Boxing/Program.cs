using System;
using System.Collections.Generic;

namespace boxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> newList = new List<object>();
            newList.Add(7);
            newList.Add(28);
            newList.Add(-1);
            newList.Add(true);
            newList.Add("chair");

            foreach(var i in newList)
            {
                Console.WriteLine(i);
            }

            int sum = 0;
            

            foreach(var i in newList)
            {
                if(i is int)
                {
                    int iCasted = (int)i;
                    sum += iCasted;
                }
            }
            Console.WriteLine(sum);

        }


    }
}
