﻿using Salon.Abstractions.Interfaces;
using SalonDAL.Models;
using SalonEf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salon.Services.EfAproach
{
    public class ManageOrders
    {
        public static void GetList()
        {
            try
            {
                using (SalonContext salonContext = new SalonContext())
                {
                    Console.WriteLine("List of orders:");
                    Console.WriteLine("{0,4} {1,20} {2,50} {3,7} {4,20} {5,15}", "ID", "Customer", "Service", "Price", "Date", "Status");

                    OrderRepository orderManager = new OrderRepository(salonContext);
                    IEnumerable<OrderTable> listOfOrders = orderManager.GetView();

                    foreach (SalonDAL.Models.OrderTable c in listOfOrders)
                    {
                        Console.WriteLine("{0,4} {1,20} {2,50} {3,7} {4,20} {5,15}", c.Id, c.Customer, c.Service, c.Price, c.Date, c.Status); ;
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

                Order order = new Order();

                Console.WriteLine("Please enter the following information:");

                using (SalonContext salonContext = new SalonContext())
                {
                    ServiceRepository serviceRepository = new ServiceRepository(salonContext);
                    List<int> listOfServiceIds = serviceRepository.GetIds();

                    Console.WriteLine("Select service");
                    ManageServices.GetList();
                    Console.Write("Enter service ID: ");
                    string serviceId = Console.ReadLine();
                    int selectedServiceId;
                    
                    while(!int.TryParse(serviceId, out selectedServiceId) || !listOfServiceIds.Contains(selectedServiceId))
                    {
                        Console.WriteLine("Incorrect value! Please enter a valid ID: ");
                        serviceId = Console.ReadLine();
                    }
                    order.ServiceId = selectedServiceId;



                    CustomerRepository customerRepository = new CustomerRepository(salonContext);
                    List<int> listOfCustomerIds = customerRepository.GetIds();

                    Console.WriteLine("Select customer");
                    ManageCustomers.GetList();
                    Console.Write("Enter customer ID: ");
                    string customerId = Console.ReadLine();
                    int selectedCustomerId;

                    while (!int.TryParse(customerId, out selectedCustomerId) || !listOfCustomerIds.Contains(selectedCustomerId))
                    {
                        Console.WriteLine("Incorrect value! Please enter a valid ID: ");
                        customerId = Console.ReadLine();
                    }
                    order.CustomerId = selectedCustomerId;



                    Console.WriteLine("Type date and time in format 'YYYY-MM-DD HH:MM:SS' :");
                    string datetime = Console.ReadLine();

                    DateTime checkTime = new DateTime();

                    while (!DateTime.TryParse(datetime, out checkTime))
                    {
                        Console.WriteLine("Incorrect value! Please enter a valid format 'YYYY-MM-DD HH:MM:SS': ");
                        datetime = Console.ReadLine();
                    }
                    order.DateOfProcedure = checkTime;

                    order.StatusId = 6;

                    ISalonManager<Order> orderManager = new OrderRepository(salonContext);
                    Order addedOrder = orderManager.Add(order);
                }


                Console.WriteLine($"Added new order on {order.DateOfProcedure}");

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
                Console.WriteLine("Please select order to update:");
                GetList();

                Console.Write("Enter ID of order you want to update:");
                using (SalonContext salonContext = new SalonContext())
                {
                    OrderRepository orderRepository = new OrderRepository(salonContext);
                    List<int> listOfIDs = orderRepository.GetIds();


                    string idToUpdate = Console.ReadLine();
                    int OrderId;

                    while (!Int32.TryParse(idToUpdate, out OrderId) || !listOfIDs.Contains(OrderId))
                    {
                        Console.WriteLine($"Order with ID {OrderId} dosent found. Try again: ");
                        idToUpdate = Console.ReadLine();
                    }

                    ISalonManager<Order> orderManager = new OrderRepository(salonContext);

                    Order selectedOrder = orderManager.GetSingle(OrderId);

                    Order orderToUpdate = new Order();

                    Console.WriteLine("Select what you want to change:");
                    Console.WriteLine($"1. Change service");
                    Console.WriteLine($"2. Change customer");
                    Console.WriteLine($"3. Change date and time");
                    Console.WriteLine($"4. Set status");
                    Console.Write("Enter number of action:");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Select new service");
                            ManageServices.GetList();

                            ServiceRepository serviceRepository = new ServiceRepository(salonContext);
                            List<int> listOfServiceIDs = serviceRepository.GetIds();

                            Console.Write("Change service ID to:");
                            string serviceIdToUpdate = Console.ReadLine();
                            int intServiceIdToUpdate;
                            while (!Int32.TryParse(serviceIdToUpdate, out intServiceIdToUpdate) 
                                || !listOfServiceIDs.Contains(intServiceIdToUpdate))
                            {
                                Console.Write("Incorrect value! Please enter a valid service ID: ");
                                serviceIdToUpdate = Console.ReadLine();
                            }

                            orderToUpdate.ServiceId = intServiceIdToUpdate;
                            orderToUpdate.CustomerId = selectedOrder.CustomerId;
                            orderToUpdate.DateOfProcedure = selectedOrder.DateOfProcedure;
                            orderToUpdate.StatusId = selectedOrder.StatusId;
                            break;

                        case "2":
                            Console.WriteLine("Select new customer");
                            ManageCustomers.GetList();

                            CustomerRepository customerRepository = new CustomerRepository(salonContext);
                            List<int> listOfCustomerIDs = customerRepository.GetIds();


                            Console.Write("Change customer ID to:");
                            string customerIdToUpdate = Console.ReadLine();
                            int intCustomerIdToUpdate;
                            while (!Int32.TryParse(customerIdToUpdate, out intCustomerIdToUpdate)
                                || !listOfCustomerIDs.Contains(intCustomerIdToUpdate))
                            {
                                Console.Write("Incorrect value! Please enter a valid customer ID: ");
                                customerIdToUpdate = Console.ReadLine();
                            }

                            orderToUpdate.ServiceId = selectedOrder.ServiceId;
                            orderToUpdate.CustomerId = intCustomerIdToUpdate;
                            orderToUpdate.DateOfProcedure = selectedOrder.DateOfProcedure;
                            orderToUpdate.StatusId = selectedOrder.StatusId;
                            break;

                        case "3":
                            Console.WriteLine("Enter new time in format 'YYYY-MM-DD HH:MM:SS': ");

                            string newTime = Console.ReadLine();
                            DateTime checkNewTime = new DateTime();
                            while (!DateTime.TryParse(newTime, out checkNewTime))
                            {
                                Console.WriteLine("Incorrect value! Please enter a valid format 'YYYY-MM-DD HH:MM:SS': ");
                                newTime = Console.ReadLine();
                            }

                            orderToUpdate.ServiceId = selectedOrder.ServiceId;
                            orderToUpdate.CustomerId = selectedOrder.CustomerId;
                            orderToUpdate.DateOfProcedure = checkNewTime;
                            orderToUpdate.StatusId = selectedOrder.StatusId;
                            break;

                        case "4":
                            Console.WriteLine("Select order status");
                            ManageStates.GetList();

                            StateRepository stateRepository = new StateRepository(salonContext);
                            List<int> listOfStateIDs = stateRepository.GetIds();

                            Console.WriteLine($"Change status from {selectedOrder.StatusId} to: ");

                            string stateIdToUpdate = Console.ReadLine();
                            int intStateIdToUpdate;
                            while (!Int32.TryParse(stateIdToUpdate, out intStateIdToUpdate)
                                || !listOfStateIDs.Contains(intStateIdToUpdate))
                            {
                                Console.Write("Incorrect value! Please enter a valid service ID: ");
                                stateIdToUpdate = Console.ReadLine();
                            }


                            orderToUpdate.ServiceId = selectedOrder.ServiceId;
                            orderToUpdate.CustomerId = selectedOrder.CustomerId;
                            orderToUpdate.DateOfProcedure = selectedOrder.DateOfProcedure;
                            orderToUpdate.StatusId = intStateIdToUpdate;
                            break;

                        default:
                            Console.WriteLine("Wrong number, try again!");
                            Update();
                            break;
                    }

                    Order order = orderManager.Update(OrderId, orderToUpdate);

                    Console.WriteLine($"Order with ID {order.Id} updated!");
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
                Console.WriteLine("Please select order to delete:");
                GetList();

                Console.Write("Enter ID of order you want to delete:");
                using (SalonContext salonContext = new SalonContext())
                {
                    OrderRepository orderRepository = new OrderRepository(salonContext);
                    List<int> listOfIDs = orderRepository.GetIds();

                    string idToDelete = Console.ReadLine();
                    int idOfOrder;

                    while (!Int32.TryParse(idToDelete, out idOfOrder) || !listOfIDs.Contains(idOfOrder))
                    {
                        Console.WriteLine($"Order with ID {idOfOrder} dosent found. Try again: ");
                        idToDelete = Console.ReadLine();
                    }

                    ISalonManager<Order> orderManager = new OrderRepository(salonContext);
                    orderManager.Delete(idOfOrder);

                    Console.WriteLine($"Order with ID {idToDelete} deleted.");
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
