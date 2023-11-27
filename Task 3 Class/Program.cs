using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IntMassive massive1 = new IntMassive(9);
            massive1.InputData(5, 2, 4, 2, 1, 2, 10, 9, 20);
            Console.Write("Первый целочисленный массив: ");
            massive1.Print(0, 8);
            int[] vs = massive1.FindValue(2);
            Console.Write("\nИндексы числа 2 в массиве: ");
            for(int i = 0; i < vs.Length; i++)
            {
                Console.Write(vs[i] + " ");
            }
            Console.WriteLine();
            massive1.DelValue(2);
            Console.Write("\nУдаляем число 2: ");
            massive1.Print(0, 6);
            Console.Write("\nСортируем первый массив: ");
            massive1.Sort();
            massive1.Print(0, 6);

            IntMassive massive2 = new IntMassive(5);
            massive2.InputDataRandom();
            Console.Write("\nВторой целочисленный массив: ");
            massive2.Print(0, 5);
            Console.Write("\nСкладываем первый и второй целочисленный массив: ");
            massive1.Add(massive2);

            IntMassive massive3 = new IntMassive(6);
            massive3.InputDataRandom();
            Console.Write("\nТретий целочисленный массив: ");
            massive3.Print(0, 6); 
            IntMassive result = massive1.Add(massive3);
            Console.Write("\nСкладываем первый и третий целочисленный массив: ");
            result.Print(0,6);
            Console.ReadKey();
        }
    }
}
