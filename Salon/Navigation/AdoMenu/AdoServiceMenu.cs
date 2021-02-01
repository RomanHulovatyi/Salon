using Salon.Services.EfAproach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Navigation.AdoMenu
{
    public class AdoServiceMenu
    {
        public static void GetServiceMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Manage services");

            Console.WriteLine("1. Get all services");
            Console.WriteLine("2. Add new service");
            Console.WriteLine("3. Update service");
            Console.WriteLine("4. Delete service");
            Console.WriteLine("5. Back to ADO menu");

            Console.WriteLine("Type number of action you want to do:");

            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ManageServices.GetList();
                    GetServiceMenu();
                    break;
                case "2":
                    ManageServices.Add();
                    GetServiceMenu();
                    break;
                case "3":
                    ManageServices.Update();
                    GetServiceMenu();
                    break;
                case "4":
                    ManageServices.Delete();
                    GetServiceMenu();
                    break;
                case "5":
                    AdoMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    GetServiceMenu();
                    break;
            }
        }
    }
}
