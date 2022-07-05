namespace OrderManagement.Application.DTO.OrderProducts
{
    public class CreateOrderProductModel
    {
        public int CustomerOrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}