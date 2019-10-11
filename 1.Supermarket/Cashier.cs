using System;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Cashier : ICashier
    {
        private IQueueManager manager;
        private readonly Random random;
        private readonly int orderProcessingMinTime;
        private readonly int orderProcessingMaxTime;

        public Cashier(int cashierId, int orderProcessingMinTime, int orderProcessingMaxTime)
        {
            Id = cashierId;
            this.orderProcessingMinTime = orderProcessingMinTime;
            this.orderProcessingMaxTime = orderProcessingMaxTime;
            random = new Random();
        }

        public void Process(ICustomer customer, CancellationTokenSource tokenSource)
        {
            var token = tokenSource.Token;

            var task = new Task(() => 
            {
                Console.WriteLine($"Cashier #{Id} started processing.");

                foreach (var item in customer.Products)
                {
                    /* Process order */
                    if (token.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        token.ThrowIfCancellationRequested();
                    }
                }

                // Order processing time of each cashier is an average of 1 to 5 seconds
                int randomSleep = random.Next(orderProcessingMinTime, orderProcessingMaxTime);

                try
                {
                    Task.Delay(randomSleep, token).Wait();
                }
                catch(Exception exp)
                {
                    // TODO: log it
                }
                
                Console.WriteLine($"Cashier #{Id} finished processing. Total time: {TimeSpan.FromMilliseconds(randomSleep)}");

                if (this.manager != null)
                {
                    this.manager.NotifyWhenReady(this);
                }

            }, token);

            task.Start();
        }

        public void Subscribe(IQueueManager manager)
        {
            this.manager = manager;
        }

        public void UnSubscribe(IQueueManager manager)
        {
            this.manager = null;
        }

        public int Id { get; private set; }
    }
}
