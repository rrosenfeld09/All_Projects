using System;
using System.Collections.Generic;

namespace Boxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> Boxing = new List<object>();
            Boxing.Add(7);
            Boxing.Add(28);
            Boxing.Add(-1);
            Boxing.Add(true);
            Boxing.Add("chair");
            
            // for(int i = 0; i < Boxing.Count; i++)
            // {
            //     Console.WriteLine(Boxing[i]);
            // }

            int sum = 0;
            for(int i = 0; i < Boxing.Count; i++)
            {
                if(Boxing[i] is int)
                {
                    int New = (int) Boxing[i];
                    sum += New;
                }
            }
            Console.WriteLine(sum);
        }
    }
}
