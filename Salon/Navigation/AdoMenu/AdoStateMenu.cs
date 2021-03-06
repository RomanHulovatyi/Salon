﻿using Salon.Services.AdoAproach;
using System;

namespace Salon.Navigation.AdoMenu
{
    public class AdoStateMenu
    {
        public static void GetStateMenu()
        {
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(" ");

            Console.WriteLine("Manage order statuses");

            Console.WriteLine("1. Get all order statuses");
            Console.WriteLine("2. Add new order status");
            Console.WriteLine("3. Update order status");
            Console.WriteLine("4. Delete order status");
            Console.WriteLine("5. Back to ADO menu");

            Console.WriteLine("Type number of action you want to do:");

            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ManageStates.GetList();
                    GetStateMenu();
                    break;
                case "2":
                    ManageStates.Add();
                    GetStateMenu();
                    break;
                case "3":
                    ManageStates.Update();
                    GetStateMenu();
                    break;
                case "4":
                    ManageStates.Delete();
                    GetStateMenu();
                    break;
                case "5":
                    AdoMenu.GetMenu();
                    break;
                default:
                    Console.WriteLine("Wrong number, try again!");
                    GetStateMenu();
                    break;
            }
        }
    }
}
