using System.Collections.Generic;

namespace Supermarket
{
    public interface ICustomer
    {
        string Id { get; set; }
        string Name { get; set; }
        List<IProduct> Products { get; set; }
    }
}
