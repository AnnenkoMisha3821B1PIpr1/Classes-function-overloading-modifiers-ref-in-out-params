using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_function_overloading_modifiers_ref_in_out_params
{
    internal class Program
    {
        static void String_to_string_massive_2d(in string str, ref string[,] vs)
        {
            string[] vs_help = str.Split(new char[] { ' ' });
            int[] int_massive = new int[0];
            foreach(var v in vs_help)
            {
                bool flag = int.TryParse(v, out int num);
                if(flag == true)
                {
                    Array.Resize(ref int_massive, int_massive.Length + 1);
                    int_massive[int_massive.Length - 1] = Convert.ToInt32(v);
                }
                else
                {
                    if (v == " " || v == "")
                    {
                        continue;
                    }
                    else if(v != " ")
                    {
                        Array.Resize(ref int_massive, int_massive.Length + 1);
                        int_massive[int_massive.Length - 1] = 0;
                    }
                }
            }
            string[,] mass =  new string[int_massive.Length,1];
            for(int i = 0; i < int_massive.Length; i++)
            {
                mass[i,0] = int_massive[i].ToString();
            }
            vs = mass;
        }

        static void Min(in string[,] str, out int min)
        {
            int min_ = int.MaxValue;
            for(int i = 0; i < str.GetLength(0); i++)
            {
                if (min_ > Convert.ToInt32(str[i, 0]))
                {
                    min_ = Convert.ToInt32(str[i,0]);
                }
            }
            min = min_;
        }

        static void Max(in string[,] str, out int max)
        {
            int max_ = int.MinValue;
            for (int i = 0; i < str.GetLength(0); i++)
            {
                if (max_ < Convert.ToInt32(str[i, 0]))
                {
                    max_ = Convert.ToInt32(str[i, 0]);
                }
            }
            max = max_;
        }

        static void Summa(in string[,] str, out int summa)
        {
            summa = 0;
            foreach (var item in str)
            {
                summa += Convert.ToInt32(item);
            }
        }


        static void Main(string[] args)
        {

            string s = Console.ReadLine();
            string[,] str = new string[0,0];
            String_to_string_massive_2d(s, ref str);
            foreach(var v in str)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
            int summa;
            int max;
            int min;
            Summa(str, out summa);
            Max(str, out max);
            Min(str, out min);
            Console.WriteLine($"Summ: {summa}\nMax: {max}\nMin: {min}");

            Console.ReadKey();
        }
    }
}
