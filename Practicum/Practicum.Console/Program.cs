using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Practicum.Domain.ServiceInterfaces;
using Practicum.Services;

namespace Practicum.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainProcessing();
        }

        private static void MainProcessing()
        {
            string output;
            
            Console.SetWindowSize(125, 50);
            //Take Order Input
            Console.WriteLine();
            Console.Write("Input: ");
            string inputOrder = Console.ReadLine().ToLower();

            //Unity stuff would go somewhere else like Global.asx in a real world app
            var container = new UnityContainer();
            container.RegisterType<IOrderService, OrderService>();
            IOrderService orderService = container.Resolve<OrderService>();

            output = "Output: " + orderService.ProcessOrder(inputOrder);

            Console.WriteLine(output);

            MainProcessing();
        }
    }
}
