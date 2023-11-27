using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class Storage
    {
        public int Tonnage { get; set; } // Вместимость

        List<Courier> Couriers;

        List<Order>[] Orders_At_The_Moment;

        Queue<Baker> BakersQueue;
        
        public Storage(int tonnage, List<Courier> couriers, int WorkDay_IN_Seconds)
        {
            Tonnage = tonnage;
            Couriers = couriers;
            Orders_At_The_Moment = new List<Order>[WorkDay_IN_Seconds];
            for(int i = 0; i < WorkDay_IN_Seconds; i++)
            {
                Orders_At_The_Moment[i] = new List<Order>();
            }
            BakersQueue = new Queue<Baker>();
        }

        public void Process_Baker(Baker baker)
        {
            if (Orders_At_The_Moment[baker.Time].Count + 1 <= Tonnage)
            {
                for(int i = baker.Time; i < Orders_At_The_Moment.Length; i++)
                {
                    if(Orders_At_The_Moment[i].Count + 1 > Tonnage)
                    {
                        var Baker_To_Move = Orders_At_The_Moment[i][0].Baker;
                        if (Baker_To_Move.Is_In_Queque_for_Storage == false)
                        {
                            BakersQueue.Enqueue(Baker_To_Move);
                            Baker_To_Move.Is_In_Queque_for_Storage = true;
                            Orders_At_The_Moment[i].RemoveAt(0);
                        }
                    }

                    baker.Actual_Order.Time_In_The_Queue_At_The_Storage(0);
                    Orders_At_The_Moment[i].Add(baker.Actual_Order);
                    Orders_At_The_Moment[i] = Orders_At_The_Moment[i].OrderByDescending(a => a.Baker.Time).ToList(); // Сортировка в порядке убыванию
                }
            }
            else
            {
                BakersQueue.Enqueue(baker);
                baker.Is_In_Queque_for_Storage = true;
            }
        }

        public void Process_Orders_In_The_Queue()
        {
            for(int i = 0; i < Orders_At_The_Moment.Length; i++)
            {
                var Free_Courier = Couriers.Where(s => s.Time <= i);
                foreach(var courier in Free_Courier)
                {
                    if(Orders_At_The_Moment[i].Count == 0)
                    {
                        break;
                    }
                    int Take_An_Order = Math.Min(Orders_At_The_Moment[i].Count, courier.The_Maximum_Possible_Count_Of_Orders_For_Delivery());
                    Orders_At_The_Moment[i] = Orders_At_The_Moment[i].OrderBy(a => a.Baker.Time).ToList();
                    var Orders_Took = Orders_At_The_Moment[i].GetRange(0, Take_An_Order);
                    courier.Take_Order(Orders_Took);
                    for(int k = courier.Time; k < Orders_At_The_Moment.Length; k++)
                    {
                        foreach(var c in Orders_Took)
                        {
                            Orders_At_The_Moment[k].Remove(c);
                        }
                    }
                    courier.Delivery_Of_Orders();
                }
            }
        }

        public void Process_Baker_In_Queue()
        {
            while(BakersQueue.Count > 0)
            {
                var First_Baker = BakersQueue.Peek();
                bool Have_Free_Places = false;
                for(int i = First_Baker.Time; i < Orders_At_The_Moment.Length; i++)
                {
                    if(Orders_At_The_Moment[i].Count + 1 <= Tonnage)
                    {
                        First_Baker.Actual_Order.Time_In_The_Queue_At_The_Storage(i - First_Baker.Time);
                        First_Baker.Time = i;
                        for(int j = i; j < Orders_At_The_Moment.Length; j++)
                        {
                            Orders_At_The_Moment[j].Add(First_Baker.Actual_Order);
                        }
                        Have_Free_Places = true;
                        First_Baker.Is_In_Queque_for_Storage = false;
                        BakersQueue.Dequeue();
                        break;
                    }
                }
                if (Have_Free_Places == true)
                {
                    break;
                }

            }
        }





    }
}
