using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Navigation
{
    public class MainMenu
    {
        public static void GetMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Welcome!");
            Console.WriteLine("Please select techonlogy: ");
            Console.WriteLine("1. Use ADO.NET ");
            Console.WriteLine("2. Use EntityFramework ");

            Console.WriteLine("Enter number of action:");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //Menu.GetMenu();
                    break;
                case "2":
                    EfMenu.EfMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    MainMenu.GetMenu();
                    break;
            }
        }
    }
}
