using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class Baker
    {
        public int ID_Baker { get; set; } // ID Повара

        public int Time { get; set; }

        public int Power_Time { get; set; }

        public Order Actual_Order { get; set; } // Текущий заказ

        public bool Is_In_Queque_for_Storage { get; set; } // Очередь на склад

        public Baker(int ID, int power_time)
        {
            ID_Baker = ID;
            Power_Time = power_time;
            Time = 0;
        }

        public void Cook(Order order)
        {
            Actual_Order = order;
            order.Baker = this;
            Time += (int)WorkTime.Cooking_Time.TotalSeconds / Power_Time;
            int time_cook = (int)WorkTime.Cooking_Time.TotalSeconds / Power_Time;
            order.Time_In_The_Oven(time_cook);
        }

        public override string ToString()
        {
            return $"Пекарь {ID_Baker}".ToString();
        }

        public override int GetHashCode()
        {
            return $"Пекарь {ID_Baker}".GetHashCode();
        }
    }
}
