using OrderManagement.Application.DTO.OrderProducts;

namespace OrderManagement.Application.DTO.CustomerOrders
{
    public class CreateCustomerOrderModel
    {
        public int CustomerId { get; set; }
        public List<OrderProductModel> OrderProducts { get; set; }
        public string CustomerAddress { get; set; }
    }
}