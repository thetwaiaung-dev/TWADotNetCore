using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWADotNetCore.ShoppingCartMvcApp.Constants;

namespace TWADotNetCore.ShoppingCartMvcApp.Models.DataModels
{
    [Table("User")]
    public class UserDataModel : BaseDataModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnumRole Role { get; set; }
    }
}
