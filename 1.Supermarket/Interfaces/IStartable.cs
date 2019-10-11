using System.Threading;

namespace Supermarket
{
    public interface IStartable
    {
        void Start(CancellationTokenSource tokenSource);
    }
}
