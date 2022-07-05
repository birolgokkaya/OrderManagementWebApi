using OrderManagement.Domain.Entities.Base;

namespace OrderManagement.Domain.Entities
{
    public class CustomerOrder : BaseEntity
    {
        public CustomerOrder()
        {
            OrderProducts = new();
        }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderAddress { get; set; }
    }
}