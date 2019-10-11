using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // create and init init Cashiers & QueueManager
            var cashiers = new List<ICashier>(Config.NumberOfCashiers);
            
            for (int i = 0; i < Config.NumberOfCashiers; i++)
            {
                var cashier = new Cashier(i, Constants.ONE_SEC * Config.OrderProcessingMinTimeInSec, Constants.ONE_SEC * Config.OrderProcessingMaxTimeInSec);
                cashiers.Add(cashier);
            }

            var queueManager = new QueueManager(cashiers);

            // start the Queue Manager
            queueManager.Start(cancellationTokenSource);

            
            // generate new customers
            int peopleJoinQueueRate = Config.PeopleJoinQueueRatePerSecond * Constants.ONE_SEC;

            var addCustomersTask = new Task(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    // add new customers to the queue by defined rate
                    await Task.Delay(peopleJoinQueueRate);

                    queueManager.AddCustomer(new Customer());
                }

            }, token, TaskCreationOptions.LongRunning);

            // start generating customers
            addCustomersTask.Start();


            Console.WriteLine("Type \"quit\" to exit.\r\n");

            // exit application
            var input = string.Empty;
            while (input.ToLower() != "quit")
            {
                input = Console.ReadLine();
            }

            cancellationTokenSource.Cancel();
        }
    }
}
