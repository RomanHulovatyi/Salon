using System;

namespace Salon.Navigation.EfMenu
{
    public class EfMenu
    {
        public static void GetMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Please select what to do");
            Console.WriteLine("1. Manage customers");
            Console.WriteLine("2. Manage services");
            Console.WriteLine("3. Manage order statuses");
            Console.WriteLine("4. Manage orders");
            Console.WriteLine("5. Return to main menu");

            Console.WriteLine("Type number of action you want to do:");


            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    EfCustomerMenu.GetCustomerMenu();
                    break;
                case "2":
                    EfServiceMenu.GetServiceMenu();
                    break;
                case "3":
                    EfStateMenu.GetStateMenu();
                    break;
                case "4":
                    EfOrderMenu.GetOrderMenu();
                    break;
                case "5":
                    MainMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    EfMenu.GetMenu();
                    break;
            }
        }
    }
}
