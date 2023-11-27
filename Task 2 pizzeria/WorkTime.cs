using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    public class WorkTime
    {
        public static readonly TimeSpan Working_Day = TimeSpan.FromSeconds(86400); // 1 день = 86 400 секунд
        public static readonly TimeSpan Cooking_Time = TimeSpan.FromMinutes(60);
        public static readonly TimeSpan Delivery_Time = TimeSpan.FromMinutes(60);
        public enum Status_Order { In_The_Queue, In_The_Oven, In_The_Queue_At_The_Storage, On_The_Way, Finished } // доделать 
    }
}
