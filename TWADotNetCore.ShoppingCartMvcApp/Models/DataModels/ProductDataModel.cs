using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWADotNetCore.ShoppingCartMvcApp.Models.DataModels
{
    [Table("Product")]
    public class ProductDataModel : BaseDataModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int SaleCount { get; set; }
        public int TotalCount { get; set; }
    }
}
