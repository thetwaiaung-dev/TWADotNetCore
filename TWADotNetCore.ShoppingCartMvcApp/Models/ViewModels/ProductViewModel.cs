namespace TWADotNetCore.ShoppingCartMvcApp.Models.ViewModels;
public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public IFormFile Photo { get; set; }
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
    public int SaleCount { get; set; }
    public int TotalCount { get; set; }
}
