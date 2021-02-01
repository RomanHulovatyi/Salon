using Salon.Services.EfAproach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Navigation.EfMenu
{
    public class EfOrderMenu
    {
        public static void GetOrderMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Manage orders");

            Console.WriteLine("1. Get all orders");
            Console.WriteLine("2. Add new order");
            Console.WriteLine("3. Update order");
            Console.WriteLine("4. Delete order");
            Console.WriteLine("5. Back to EF menu");

            Console.WriteLine("Type number of action you want to do:");

            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ManageOrders.GetList();
                    GetOrderMenu();
                    break;
                case "2":
                    ManageOrders.Add();
                    GetOrderMenu();
                    break;
                case "3":
                    ManageOrders.Update();
                    GetOrderMenu();
                    break;
                case "4":
                    ManageOrders.Delete();
                    GetOrderMenu();
                    break;
                case "5":
                    EfMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    GetOrderMenu();
                    break;
            }
        }
    }
}
