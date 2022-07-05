using OrderManagement.Domain.Entities.Base;

namespace OrderManagement.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}