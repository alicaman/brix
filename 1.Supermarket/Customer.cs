using System.Collections.Generic;

namespace Supermarket
{
    public class Customer : ICustomer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<IProduct> Products { get; set; } = new List<IProduct>();
    }
}
