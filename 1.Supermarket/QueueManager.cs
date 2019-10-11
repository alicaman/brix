using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    public class QueueManager: IQueueManager, IStartable
    {
        private bool isRunning;
        ManualResetEvent cashiersEvent;
        private ConcurrentQueue<ICashier> cashiers;
        private ConcurrentQueue<ICustomer> customers;

        public QueueManager(IEnumerable<ICashier> cashiersList)
        {
            customers = new ConcurrentQueue<ICustomer>();
            cashiers = new ConcurrentQueue<ICashier>();

            foreach (var cashier in cashiersList)
            {
                cashier.Subscribe(this);
                cashiers.Enqueue(cashier);
            }

            cashiersEvent = new ManualResetEvent(false);
        }

        public void NotifyWhenReady(ICashier cashier)
        {
            cashiers.Enqueue(cashier);
            cashiersEvent.Set();
        }

        public void AddCustomer(ICustomer customer)
        {
            customers.Enqueue(customer);
        }

        public void Start(CancellationTokenSource tokenSource)
        {
            if (isRunning)
            {
                return;
            }

            var token = tokenSource.Token;
            
            var processCustomersTask = new Task(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (token.IsCancellationRequested)
                    {
                        break; // finish the task
                    }

                    if (cashiers.IsEmpty)
                        cashiersEvent.WaitOne(Constants.ONE_MINUTE); // no available cashiers - wait one minute

                    if (token.IsCancellationRequested)
                    {
                        break; // finish the task
                    }

                    // no available cashiers
                    if (cashiers.IsEmpty)
                    {
                        cashiersEvent.Reset();
                    }

                    // process current customer
                    if (!customers.IsEmpty)
                    {
                        ICustomer customer;
                        ICashier cashier;

                        while (!customers.TryDequeue(out customer)) ;

                        while (!cashiers.TryDequeue(out cashier)) ;
                        
                        cashier.Process(customer, tokenSource);
                        cashiersEvent.Reset();
                        
                        Console.WriteLine($"Current # of customers: {customers.Count}");
                    }
                }

            }, token, TaskCreationOptions.LongRunning);

            processCustomersTask.Start();

            isRunning = true;
        }
    }
}
