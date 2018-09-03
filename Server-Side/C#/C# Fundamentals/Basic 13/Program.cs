using System;
using System.Collections.Generic;
using System.Linq;



namespace Basic_13
{
    class Program
    {
        static void Main(string[] args)
        {
            // print1();
            // printOdd();
            // printSum();
            // iterate();
            // findMax();
            // getAvg();
            // arrOdd();
            int[] test = new int[] {1,-2,3,4,-5};
            // int z = 3;
            // greaterY(test, z);
            // square(test);
            // noNegatives(test);
            // minMaxAvg(test);
            // shift(test);
            numToStr(test);
            

        }
        public static void print1()
        {
            for (int i =1; i <=255; i++)
            {
                Console.WriteLine(i);
            }
        }
        public static void printOdd()
        {
            for (int i =1; i <= 255; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
        // Print Sum
        public static void printSum()
        {
            int sum = 0;
            for (int i = 1; i <= 255; i++)
            {
                sum += i;
                Console.WriteLine("New number: {0}, Sum: {1}", i, sum);
            }
        }
        // Iterating through an Array
        public static void iterate()
        {
            int[] myArr = new int[] {1,2,3,4,5};
            foreach (int i in myArr)
            {
                Console.WriteLine(i);
            }

        }

        // Find Max
        public static void findMax()
        {
            int[] myArr = new int[] {1,2,300,4,5};
            int max = myArr[0];
            for (int i =1; i < myArr.Length; i++)
            {
                if(myArr[i] > max)
                {
                    max = myArr[i];
                }
            }
            Console.WriteLine(max);
        }

        // get average
        public static void getAvg()
        {
            int[] newArr = new int[] {1,2,3,4,5};
            int denom = newArr.Length;
            int sum = newArr[0];
            for (int i = 1; i < newArr.Length; i++)
            {
                sum += newArr[i];
            }
            double final = (sum/denom);
            Console.WriteLine(final);
        }

        // Array with Odd Numbers
        public static void arrOdd()
        {
            List<int> y = new List<int>();
            for (int i =1; i <=255; i++)
            {
                if (i % 2 != 0)
                {
                    y.Add(i);
                }
            }

            int[] final = y.ToArray();
        }

        // Greater than Y
        public static void greaterY(int[] arr, int z)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                if (i > z)
                {
                    sum += 1;
                }
            }
            Console.WriteLine(sum);
        }

        // Square the Values
        public static void square(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i]*arr[i];
            }
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }
        }

        // Eliminate Negative Numbers
        public static void noNegatives(int[] arr)
        {
            for (int i =0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    arr[i] = 0;
                }
            }

            foreach(int i in arr)
            {
                Console.WriteLine(i);
            }
        }

        // Min, Max, and Average
        public static void minMaxAvg(int[] arr)
        {
            int min = arr[0];
            int max = arr[0];
            int sum = arr[0];
            int len = arr.Length;
            foreach (int i in arr)
            {
                if (i < min)
                {
                    min = i;
                }
                else if (i > max)
                {
                    max = i;
                }
                sum += i;
            }
            float avg = (sum/len);
            Console.WriteLine(min);
            Console.WriteLine(max);
            Console.WriteLine(avg);

        }

        // Shifting the values in an array
        public static void shift(int[] arr)
        {
            List<int> newList = arr.ToList();
            newList.Add(0);
            newList.RemoveAt(0);
            int[] final = newList.ToArray();

            foreach (int i in final)
            {
                Console.WriteLine(i);
            }
        }

        // Number to String
        public static object[] numToStr(int[] args)
        {
            List<object> newList = new List<object>();
            foreach (int i in args)
            {
                if( i < 0)
                {
                    newList.Add("Dojo");
                }
                else
                {
                    newList.Add(i);
                }
            }
            object[] final = newList.ToArray();

            foreach (var thing in final)
            {
                Console.WriteLine(thing);
            }
            return final;
        }
        

    }
}
