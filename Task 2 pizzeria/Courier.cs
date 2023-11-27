using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class Courier
    {
        public int ID_Courier { get; set; }

        public List<Order> Courier_Orders { get; set;}

        public int Power_Time { get; set; }

        public int Capacity_Highway { get; set; } // по правильному обозвать мне надо было рюкзаком

        public int Time { get; set; }

        public Courier(int ID, int power_time, int capacity_highway)
        {
            ID_Courier = ID;
            Power_Time = power_time;
            Capacity_Highway = capacity_highway;
            Courier_Orders = new List<Order>();
            Time = 0;
        }

        public void Take_Order(List<Order> order)
        {
            Courier_Orders.AddRange(order);
        }

        public void Delivery_Of_Orders()
        {
            int Orders_Counts = 0;
            while(Courier_Orders.Count > 0)
            {
                Orders_Counts++;
                var order = Courier_Orders.First();
                order.Courier = this;
                Time += (int)WorkTime.Delivery_Time.TotalSeconds / Power_Time;
                int time_way = (int)WorkTime.Delivery_Time.TotalSeconds / Power_Time * Orders_Counts;
                order.Time_On_The_Way(time_way);
                order.Finished();
                Courier_Orders.RemoveAt(0);
            }
        }

        public int The_Maximum_Possible_Count_Of_Orders_For_Delivery()
        {
            return Capacity_Highway - Courier_Orders.Count;
        }

        public override string ToString()
        {
            return $"Курьер {ID_Courier}".ToString();
        }

        public override int GetHashCode()
        {
            return $"Курьер{ID_Courier}".GetHashCode();
        }
    }
}
