using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class Order
    {
        public int ID_Order { get; set; }
        public Baker Baker { get; set; }
        public Courier Courier { get; set; }        
        public TimeSpan Waiting_Time_Finish { get; set; }
        public WorkTime.Status_Order Status_Order { get; set; }

        Dictionary<WorkTime.Status_Order, int> Time_In_Process;

        public Order(int id, TimeSpan waiting_Time_Finish)
        {
            ID_Order = id;
            Waiting_Time_Finish = waiting_Time_Finish;
            Status_Order = WorkTime.Status_Order.In_The_Queue;
            Time_In_Process = new Dictionary<WorkTime.Status_Order,int>();
        }

        private void Time_Process(WorkTime.Status_Order status_Order, int Time)
        {
            if(Time_In_Process == null)
            {
                Time_In_Process = new Dictionary<WorkTime.Status_Order,int>();
            }
            if (Time_In_Process.ContainsKey(status_Order) == false)
            {
                Time_In_Process.Add(status_Order, Time);
                Console.WriteLine($"[{this.ID_Order}] [{status_Order}] [{TimeSpan.FromSeconds(Time)}]");
            }
            else
            {
                Time_In_Process[status_Order] = Time;
            }
            Status_Order = status_Order;
        }

        public void Time_In_The_Queue(int Time_Queue)
        {
            Time_Process(WorkTime.Status_Order.In_The_Queue, Time_Queue);
        }

        public void Time_In_The_Oven(int Time_Oven)
        {
            Time_Process(WorkTime.Status_Order.In_The_Oven, Time_Oven);
        }

        public void Time_In_The_Queue_At_The_Storage(int Time_Queue_At_The_Storage)
        {
            Time_Process(WorkTime.Status_Order.In_The_Queue_At_The_Storage, Time_Queue_At_The_Storage);
        }

        public void Time_On_The_Way(int Time_Way)
        {
            Time_Process(WorkTime.Status_Order.On_The_Way, Time_Way);
        }

        public void Finished()
        {
            Status_Order = WorkTime.Status_Order.Finished;
            int SumTime = 0;
            SumTime = Time_In_Process.Sum(a => a.Value);
            Time_Process(Status_Order, SumTime);
        }

        public TimeSpan Get_Time_End_Finished_Order()
        {
            return TimeSpan.FromSeconds(Time_In_Process[WorkTime.Status_Order.Finished]);
        }

        public WorkTime.Status_Order Return_Phase_With_Max_Time()
        {
            WorkTime.Status_Order STATUS = WorkTime.Status_Order.Finished;
            int Time_max = int.MinValue;
            foreach(var a in Time_In_Process)
            {

                if (a.Key == WorkTime.Status_Order.Finished)
                {
                    continue;
                }
                if (a.Value > Time_max)
                {
                    Time_max = a.Value;
                    STATUS = a.Key;
                }
            }
            return STATUS;
        }

        public override string ToString()
        {
            return $"Заказ {ID_Order}".ToString();
        }

        public override int GetHashCode()
        {
            return $"Заказ {ID_Order}".GetHashCode();
        }
    }
}
