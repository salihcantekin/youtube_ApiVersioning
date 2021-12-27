namespace ApiVersioning.WebApi.Models
{
    public class OrderViewModelv2
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public double DiscountedPrice { get; set; }
    }
}