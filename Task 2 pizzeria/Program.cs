using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_pizzeria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Courier> couriers = new List<Courier>()
            {
                new Courier(1, 2, 3),
                //new Courier(2, 2, 3),
                //new Courier(3, 2, 3),
                //new Courier(4, 2, 3),

            };

            List<Baker> bakers = new List<Baker>()
            {
                new Baker(1, 3),
                new Baker(2, 3),
                //new Baker(1, 3),
                //new Baker(1, 3),
                //new Baker(2, 3),
                //new Baker(1, 3),
            };

            List<Order> orders = new List<Order>()
            {
                new Order(1, TimeSpan.FromMinutes(120)),
                new Order(2, TimeSpan.FromMinutes(120)),
                new Order(3, TimeSpan.FromMinutes(120)),
                new Order(4, TimeSpan.FromMinutes(120)),
                new Order(5, TimeSpan.FromMinutes(120)),
                //new Order(6, TimeSpan.FromMinutes(120)),
                //new Order(7, TimeSpan.FromMinutes(120)),
                //new Order(8, TimeSpan.FromMinutes(120)),
                //new Order(9, TimeSpan.FromMinutes(120)),
                //new Order(10, TimeSpan.FromMinutes(120)),
                //new Order(11, TimeSpan.FromMinutes(120)),
                //new Order(12, TimeSpan.FromMinutes(120)),
                //new Order(13, TimeSpan.FromMinutes(120)),
                //new Order(14, TimeSpan.FromMinutes(120)),
            };

            Pizzeria pizzeria = new Pizzeria(orders, bakers, couriers, 2);

            pizzeria.Process_Orders();
            pizzeria.Report();
            Console.ReadKey();

        }
    }
}
