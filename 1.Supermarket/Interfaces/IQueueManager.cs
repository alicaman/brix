using System.Threading;

namespace Supermarket
{
    public interface IQueueManager
    {
        void NotifyWhenReady(ICashier cashier);
    }
}