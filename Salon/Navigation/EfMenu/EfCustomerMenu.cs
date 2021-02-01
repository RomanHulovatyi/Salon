using Salon.Services.EfAproach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Navigation.EfMenu
{
    public class EfCustomerMenu
    {
        public static void GetCustomerMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Manage customers");

            Console.WriteLine("1. Get all customers");
            Console.WriteLine("2. Add new customer");
            Console.WriteLine("3. Update customer");
            Console.WriteLine("4. Delete customer");
            Console.WriteLine("5. Back to EF menu");

            Console.WriteLine("Type number of action you want to do:");

            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ManageCustomers.GetList();
                    GetCustomerMenu();
                    break;
                case "2":
                    ManageCustomers.Add();
                    GetCustomerMenu();
                    break;
                case "3":
                    ManageCustomers.Update();
                    GetCustomerMenu();
                    break;
                case "4":
                    ManageCustomers.Delete();
                    GetCustomerMenu();
                    break;
                case "5":
                    EfMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    GetCustomerMenu();
                    break;
            }
        }
    }
}
