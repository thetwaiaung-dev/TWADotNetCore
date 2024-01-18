namespace TWADotNetCore.ShoppingCartMvcApp.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
    }
}
