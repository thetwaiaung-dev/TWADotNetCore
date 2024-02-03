namespace TWADotNetCore.ShoppingCartMvcApp.Models
{
    public class BaseDataModel
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public long CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long ModifiedUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
