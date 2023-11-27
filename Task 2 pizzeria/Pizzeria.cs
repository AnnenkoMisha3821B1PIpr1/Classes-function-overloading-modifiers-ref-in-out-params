using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class Pizzeria
    {
        public List<Order> Orders { get; set; }
        public List<Baker> Bakers { get; set; }
        public List<Courier> Couriers { get; set; }
        public int Tonnage_Storage { get; set; }

        Storage Storage;

        List<Order> Orders_Finished;

        public Pizzeria(List<Order> orders, List<Baker> bakers, List<Courier> couriers, int tonnage_storage)
        {
            this.Orders = orders;
            this.Bakers = bakers;
            this.Couriers = couriers;
            Tonnage_Storage = tonnage_storage;
            Storage = new Storage(tonnage_storage, this.Couriers, (int)WorkTime.Working_Day.TotalSeconds);
            Orders_Finished = orders.Where(a => a.Status_Order == WorkTime.Status_Order.Finished).ToList();
            foreach(var order in Orders_Finished)
            {
                Orders_Finished.Remove(order);
            }
        }

        public void Process_Orders()
        {
            while (Orders.Count > 0)
            {
                int Index = 0;
                foreach(var baker in Bakers)
                {
                    if(Index >= Orders.Count)
                    {
                        break;
                    }

                    
                    var order = Orders[Index];
                    Index++;
                    if (order.Status_Order == WorkTime.Status_Order.In_The_Queue && baker.Is_In_Queque_for_Storage == false) // false?
                    {
                        order.Time_In_The_Queue(baker.Time);
                        baker.Cook(order);
                        if(baker.Time >= (int)WorkTime.Working_Day.TotalSeconds)
                        {
                            break;
                        }
                        Storage.Process_Baker(baker);
                    }
                }
                Storage.Process_Orders_In_The_Queue();
                Storage.Process_Baker_In_Queue();
                var Finished = Orders.Where(a => a.Status_Order == WorkTime.Status_Order.Finished).ToList();
                Orders_Finished.AddRange(Finished);

                foreach(var orders_F in Orders_Finished)
                {
                    Orders.Remove(orders_F);
                }
            }
        }

        public void Report()
        {
            var No_Cost_Orders = Orders_Finished.Where(a => a.Get_Time_End_Finished_Order() > a.Waiting_Time_Finish);
            if(No_Cost_Orders.Count() == 0)
            {
                var Order_Finished_Last = Orders_Finished.Max(a => a.Get_Time_End_Finished_Order());
                if(WorkTime.Cooking_Time + WorkTime.Delivery_Time + Order_Finished_Last <= WorkTime.Working_Day)
                {
                    Console.WriteLine("Увеличить число заказов!");
                }
                return;
            }
            
            Dictionary<Baker, int> Baker_Score = new Dictionary<Baker, int>();
            Dictionary<Courier, int> Courier_Score = new Dictionary<Courier,int>();
            int In_The_Queue_At_The_Storage = 0;
            int In_The_Queue = 0;
            foreach(var order in No_Cost_Orders)
            {
                WorkTime.Status_Order status_Order = order.Return_Phase_With_Max_Time();
                if(status_Order == WorkTime.Status_Order.In_The_Oven)
                {
                    if (Baker_Score.ContainsKey(order.Baker))
                    {
                        Baker_Score[order.Baker]++;
                    }
                    else
                    {
                        Baker_Score.Add(order.Baker, 1);
                    }
                }
                else if (status_Order == WorkTime.Status_Order.In_The_Queue)
                {
                    In_The_Queue++;
                }
                else if(status_Order == WorkTime.Status_Order.In_The_Queue_At_The_Storage)
                {
                    In_The_Queue_At_The_Storage++;
                }
                else if(status_Order == WorkTime.Status_Order.On_The_Way)
                {
                    if (Courier_Score.ContainsKey(order.Courier))
                    {
                        Courier_Score[order.Courier]++;
                    }
                    else
                    {
                        Courier_Score.Add(order.Courier, 1);
                    }
                }
            }

            var Slow_Baker = Baker_Score.Where(x => x.Value == Baker_Score.Max(y => y.Value)).ToList();
            var Slow_Courier = Courier_Score.Where(x => x.Value == Courier_Score.Max(y => y.Value)).ToList();
            int kl = Convert.ToInt32(No_Cost_Orders.Count());
            if (In_The_Queue >= No_Cost_Orders.Count() || Slow_Baker.Count() == Bakers.Count())
            {
                Console.WriteLine("Нанять нового Пекаря!");
            }
            if(Slow_Baker.Count() != Bakers.Count())
            {
                foreach(var s in Slow_Baker)
                {
                    Console.WriteLine("Уволить Пекаря: {0}", s.Key);
                }
            }

            if(In_The_Queue_At_The_Storage > No_Cost_Orders.Count() * 0.01 || Slow_Courier.Count() == Couriers.Count())
            {
                Console.WriteLine("Расширить склад или нанять нового Курьера!");
            }
            if (Slow_Courier.Count() != Couriers.Count())
            {
                foreach( var s in Slow_Courier)
                {
                    Console.WriteLine("Уволить Курьера: {0}", s.Key);
                }
            }
        }
    }
}
