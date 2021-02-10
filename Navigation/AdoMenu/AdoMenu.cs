using System;

namespace Salon.Navigation.AdoMenu
{
    public class AdoMenu
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
                    AdoCustomerMenu.GetCustomerMenu();
                    break;
                case "2":
                    AdoServiceMenu.GetServiceMenu();
                    break;
                case "3":
                    AdoStateMenu.GetStateMenu();
                    break;
                case "4":
                    AdoOrderMenu.GetOrderMenu();
                    break;
                case "5":
                    MainMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    AdoMenu.GetMenu();
                    break;
            }
        }
    }
}
