using OrderManagement.Domain.Entities.Base;

namespace OrderManagement.Domain.Entities
{
    public class OrderProduct : BaseEntity
    {
        public int CustomerOrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}