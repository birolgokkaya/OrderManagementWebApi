namespace OrderManagement.Application.DTO.Products
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}