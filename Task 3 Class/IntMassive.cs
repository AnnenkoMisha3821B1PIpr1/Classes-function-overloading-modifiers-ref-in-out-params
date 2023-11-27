using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_Class
{
    public class IntMassive
    {
        private int Size = 0;
        private int[] array;
        
        public IntMassive(int size)
        {
            Size = size;
            array = new int[Size];
        }

        public void InputData(params int[] vs)
        {
            if(vs.Length == 0)
            {
                Console.WriteLine("Невозможно: массив пуст!");
            }
            else
            {
                for(int i = 0; i < vs.Length; i++)
                {
                    array[i] = vs[i];
                }
            }
        }

        public void InputDataRandom()
        {
            Random random = new Random();
            for(int i = 0; i < Size; i++)
            {
                array[i] = random.Next(1, 101);
            }
        }

        public void Print(int start, int end)
        {
            if (Size == 0)
            {
                Console.WriteLine("Невозможно: массив пуст!");
            }
            if (end > Size)
            {
                Console.WriteLine("Конец вывода диапазона больше, чем длина самого массива");
            }
            else
            {
                for (int i = start; i < end; i++)
                {
                    Console.Write(array[i] + " ");
                }
                Console.WriteLine();
            }  
        }

        public int[] FindValue(int element)
        {
            int Count_element = 0;
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    Count_element++;
                }
            }
            int[] vs = new int[Count_element];
            int vs_posion = 0;
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i] == element)
                {
                    vs[vs_posion] = i;
                    vs_posion++;
                }
            }
            return vs;
        }

        public void DelValue(int element) // проверить!!!
        {
            int[] position_element = FindValue(element);
            int[] orig = new int[Size];
            for(int i = 0; i < Size; i++)
            {
                orig[i] = array[i];
            }
            for (int i = 0; i < position_element.Length; i++)
            {
                for (int j = position_element[i]; j < Size - 1; j++)
                {
                    orig[j] = orig[j + 1];
                }
                    
                for (int j = i + 1; j < position_element.Length; j++)
                {
                    position_element[j]--;
                }
            }
            int[] neworig = new int[Size - position_element.Length];
            for(int i = 0; i < Size - position_element.Length; i++)
            {
                neworig[i] = orig[i];
            }
            array = neworig;
            Size = array.Length;
        }

        public int FindMax()
        {
            int max = 0;
            for(int i = 0; i < Size; i++)
            {
                if(array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }

        public void Sort()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public IntMassive Add(in IntMassive array2)
        {
            if(array2.Size != Size)
            {
                Console.WriteLine("Не удалось сложить, т.к. размеры массивов разные");
                return new IntMassive(0);
            }
            else
            {
                IntMassive result = new IntMassive(Size);
                int[] vs = new int[Size];
                for(int i = 0; i < Size; i++)
                {
                    vs[i] = array[i] + array2.array[i];
                }
                result.InputData(vs);
                return result;
            }
            
            
        }

    }
}
