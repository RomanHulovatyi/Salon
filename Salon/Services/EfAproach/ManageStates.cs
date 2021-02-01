using SalonDAL.Models;
using SalonDAL.Models.Interfaces;
using SalonEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Services.EfAproach
{
    public class ManageStates
    {
        public static void GetList()
        {
            try
            {
                using (SalonContext salonContext = new SalonContext())
                {
                    Console.WriteLine("List of states:");
                    Console.WriteLine("{0, 5} {1, 20} ", "ID", "Order status");

                    ISalonManager<State> stateManager = new StateManager(salonContext);
                    IEnumerable<State> listOfStates = stateManager.GetList();

                    foreach (SalonDAL.Models.State c in listOfStates)
                    {
                        Console.WriteLine("{0,5} {1,20}", c.Id, c.OrderStatus);
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

                State state = new State();

                Console.WriteLine("Please enter the following information:");

                Console.Write("Order status: ");
                state.OrderStatus = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(state.OrderStatus))
                {
                    Console.Write("Please enter correct name of status:");
                    state.OrderStatus = Console.ReadLine();
                }

                using (SalonContext salonContext = new SalonContext())
                {
                    ISalonManager<State> stateManager = new StateManager(salonContext);
                    State addedState = stateManager.Add(state);
                }

                Console.WriteLine($"Status {state.OrderStatus} successfully added!");

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
                Console.WriteLine("Please select state to update:");
                GetList();

                Console.Write("Enter ID of state you want to update:");
                using (SalonContext salonContext = new SalonContext())
                {
                    var listOfServices = salonContext.Services.ToList();
                    List<int> listOfIDs = new List<int>();
                    foreach (SalonDAL.Models.Service c in listOfServices)
                    {
                        listOfIDs.Add(c.Id);
                    }

                    string idToUpdate = Console.ReadLine();
                    int idOfState;

                    while (!Int32.TryParse(idToUpdate, out idOfState) || !listOfIDs.Contains(idOfState))
                    {
                        Console.WriteLine($"State with ID {idOfState} dosent found. Try again: ");
                        idToUpdate = Console.ReadLine();
                    }

                    ISalonManager<State> stateManager = new StateManager(salonContext);

                    State selectedState = stateManager.GetSingle(idOfState);

                    State stateToUpdate = new State();

                    Console.WriteLine("Enter the new name of order status:");
                    stateToUpdate.OrderStatus = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(stateToUpdate.OrderStatus))
                    {
                        Console.Write("Please enter correct order status:");
                        stateToUpdate.OrderStatus = Console.ReadLine();
                    }


                    State state = stateManager.Update(idOfState, stateToUpdate);

                    Console.WriteLine($"Order status {state.OrderStatus} updated!");
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
                Console.WriteLine("Please select order status to delete:");
                GetList();

                Console.Write("Enter ID of order status you want to delete:");
                using (SalonContext salonContext = new SalonContext())
                {
                    var listOfStates = salonContext.States.ToList();
                    List<int> listOfIDs = new List<int>();
                    foreach (SalonDAL.Models.State c in listOfStates)
                    {
                        listOfIDs.Add(c.Id);
                    }

                    string idToDelete = Console.ReadLine();
                    int idOfState;

                    while (!Int32.TryParse(idToDelete, out idOfState) || !listOfIDs.Contains(idOfState))
                    {
                        Console.WriteLine($"Order status with ID {idOfState} dosent found. Try again: ");
                        idToDelete = Console.ReadLine();
                    }

                    ISalonManager<State> stateManager = new StateManager(salonContext);
                    stateManager.Delete(idOfState);

                    Console.WriteLine($"Order status with ID {idToDelete} deleted.");
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
