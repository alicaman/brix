using System.Threading;

namespace Supermarket
{
    public interface ICashier
    {
        int Id { get; }
        void Process(ICustomer customer, CancellationTokenSource tokenSource);
        void Subscribe(IQueueManager manager);
        void UnSubscribe(IQueueManager manager);
    }
}
