using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] MyArray = {6,-7,8,999,-99};

            // Print();
            // PrintOdd();
            // PrintSum();
            // PrintArray(MyArray);
            // FindMax(MyArray);
            // GetAvg(MyArray);
            // OddNums();
            // NumBig(MyArray, 5);
            // square(MyArray);
            // NoNegative(MyArray);
            // MinMaxAvg(MyArray);
            // shift(MyArray);
            // NumToString(MyArray);

        }
        // print 1-255
        public static void Print()
        {
            for (int i = 1; i <= 255; i++)
            {
                Console.WriteLine(i);
            }
        }

        // print odd
        public static void PrintOdd()
        {
            for (int i =1; i <=255; i+=2)
            {
                Console.WriteLine(i);
            }
        }

        // print sum
        public static void PrintSum()
        {
            int sum = 0;
            for (int i = 0; i <=255; i++)
            {
                sum += i;
                Console.WriteLine("New Number: " + i + " Sum: " + sum);
            }
        }
        // iterate through an array
        public static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        // find max
        public static void FindMax(int[] arr)
        {
            // Console.WriteLine(arr.Max());
            int top = arr[0];
            foreach(int num in arr)
            {
                if (num > top)
                {
                    top = num;
                }
            }
            Console.WriteLine(top);
        }

        // get average
        public static void GetAvg(int[] arr)
        {
            int sum = 0;
            int len = arr.Length;
            foreach(int num in arr)
            {
                sum += num;
            }
            int average = sum/len;
            Console.WriteLine(average);
        }

        // odd nums
        public static int[] OddNums()
        {
            List<int> OnlyOdds = new List<int>();
            for (int i = 1; i < 256; i++)
            {
                if(i % 2 != 0)
                {
                    OnlyOdds.Add(i);
                }
            }
            int[] y = OnlyOdds.ToArray();
            Console.WriteLine(y);
            return y;
        }

        // greater than y
        public static int NumBig(int[] arr, int arr2)
        {
            int num = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] > arr2)
                {
                    num++;
                }
            }
            Console.WriteLine(num);
            return num;
        }

        // square the values
        public static int[] square(int[] arr)
        {
            for(var i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] * arr[i];
            }
            foreach(int num in arr)
            {
                Console.WriteLine(num);
            }
            return arr;
        }

        // eliminate the negative numbers
        public static int[] NoNegative(int[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                if(x[i] < 0)
                {
                    x[i] = 0;
                }
            }
            foreach(int num in x)
            {
                Console.WriteLine(num);
            }
            return x;
        }

        // min, max, avg
        public static void MinMaxAvg(int[] x)
        {
            int min = x[0];
            int max = x[0];
            int sum = x[0];
            int len = x.Length;

            for(int i = 0; i < x.Length; i++)
            {
                if(x[i] < min)
                {
                    min = x[i];
                }
                if(x[i] > max)
                {
                    max = x[i];
                }
                sum += x[i];
            }

            int avg = sum/len;
            Console.WriteLine("Min: " + min);
            Console.WriteLine("Max: " + max);
            Console.WriteLine("Average: " + avg);
        }

        // shifting the values in an array
        public static int[] shift(int[] x)
        {
            List<int> NewList = new List<int>();
            for (int i = 1; i < x.Length; i++)
            {
                NewList.Add(x[i]);
            }
            NewList.Add(0);
            int[] final = NewList.ToArray();
            foreach( int num in final)
            {
                Console.WriteLine(num);
            }

            return final;
        }

        // number to a string
        public static object[] NumToString(int[] x)
        {
            object[] NewArray = x.Cast<object>().ToArray();
            for(int i = 0; i < x.Length; i++)
            {
                if(x[i] < 0)
                {
                    NewArray[i] = "dojo";
                }
            }
            foreach(object num in NewArray)
            {
                Console.WriteLine(num);
            }

            return NewArray;
        }

    }
}
