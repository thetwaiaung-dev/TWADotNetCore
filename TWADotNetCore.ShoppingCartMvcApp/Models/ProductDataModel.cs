using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWADotNetCore.ShoppingCartMvcApp.Models
{
    [Table("Product")]
    public class ProductDataModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PhotoUrl { get; set; }
        public IFormFile Photo { get; set; }
    }
}
