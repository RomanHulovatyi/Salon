using Microsoft.Data.SqlClient;
using Salon.Abstractions.Interfaces;
using SalonAdo;
using SalonDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Salon.Services.AdoAproach
{
    public class ManageServices
    {
        public static void GetList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    Console.WriteLine("List of services:");
                    Console.WriteLine("{0, 5} {1, 50} {2, 10} ", "ID", "Service", "Price");

                    ISalonManager<Service> serviceManager = new ServiceRepository(connection);
                    IEnumerable<Service> listOfServices = serviceManager.GetList();

                    foreach (SalonDAL.Models.Service c in listOfServices)
                    {
                        Console.WriteLine("{0,5} {1,50} {2,10}", c.Id, c.NameOfService, c.Price);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Add()
        {
            try
            {

                Service service = new Service();

                Console.WriteLine("Please enter the following information:");

                Console.Write("Name of service: ");
                service.NameOfService = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(service.NameOfService))
                {
                    Console.Write("Please enter correct name of service:");
                    service.NameOfService = Console.ReadLine();
                }

                Console.Write("Price: ");
                string price = Console.ReadLine();
                decimal decPrice = 0;
                while (!Decimal.TryParse(price, out decPrice))
                {
                    Console.WriteLine("Incorrect value! Please enter a valid price: ");
                    price = Console.ReadLine();
                }
                service.Price = decPrice;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Service> serviceManager = new ServiceRepository(connection);
                    Service addedService = serviceManager.Add(service);
                }

                Console.WriteLine($"Service {service.NameOfService} successfully added!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Update()
        {
            try
            {
                Console.WriteLine("Please select service to update:");
                GetList();

                Console.Write("Enter ID of service you want to update:");
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Service> serviceManager = new ServiceRepository(connection);

                    ServiceRepository checkIds = new ServiceRepository(connection);
                    List<int> listOfIDs = checkIds.GetIds();

                    string idToUpdate = Console.ReadLine();
                    int idOfService;

                    while (!Int32.TryParse(idToUpdate, out idOfService) || !listOfIDs.Contains(idOfService))
                    {
                        Console.WriteLine($"Service with ID {idOfService} dosent found. Try again: ");
                        idToUpdate = Console.ReadLine();
                    }


                    Service selectedService = serviceManager.GetSingle(idOfService);

                    Service serviceToUpdate = new Service();

                    Console.WriteLine("Select what you want to change:");
                    Console.WriteLine($"1. Change name of service");
                    Console.WriteLine($"2. Change price");
                    Console.Write("Enter number of action:");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Write($"Change name of service from {selectedService.NameOfService} to: ");
                            serviceToUpdate.NameOfService = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(serviceToUpdate.NameOfService))
                            {
                                Console.Write("Please enter correct name of service:");
                                serviceToUpdate.NameOfService = Console.ReadLine();
                            }

                            serviceToUpdate.Price = selectedService.Price;
                            break;

                        case "2":
                            Console.WriteLine($"Change price from {selectedService.Price} to: ");
                            serviceToUpdate.NameOfService = selectedService.NameOfService;

                            string price = Console.ReadLine();
                            decimal decPrice = 0;
                            while (!Decimal.TryParse(price, out decPrice))
                            {
                                Console.WriteLine("Incorrect value! Please enter a valid price: ");
                                price = Console.ReadLine();
                            }
                            serviceToUpdate.Price = decPrice;
                            break;

                        default:
                            Console.WriteLine("Wrong number, try again!");
                            Update();
                            break;
                    }

                    Service service = serviceManager.Update(idOfService, serviceToUpdate);

                    Console.WriteLine($"Service {service.NameOfService} updated!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Delete()
        {
            try
            {
                Console.WriteLine("Please select service to delete:");
                GetList();

                Console.Write("Enter ID of service you want to delete:");
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Service> serviceManager = new ServiceRepository(connection);

                    ServiceRepository checkIds = new ServiceRepository(connection);
                    List<int> listOfIDs = checkIds.GetIds();

                    string idToDelete = Console.ReadLine();
                    int idOfService;

                    while (!Int32.TryParse(idToDelete, out idOfService) || !listOfIDs.Contains(idOfService))
                    {
                        Console.WriteLine($"Service with ID {idOfService} dosent found. Try again: ");
                        idToDelete = Console.ReadLine();
                    }

                    serviceManager.Delete(idOfService);

                    Console.WriteLine($"Service with ID {idToDelete} deleted.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
