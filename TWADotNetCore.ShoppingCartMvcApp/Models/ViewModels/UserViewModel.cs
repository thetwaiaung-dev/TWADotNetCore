using System.ComponentModel.DataAnnotations;
using TWADotNetCore.ShoppingCartMvcApp.Constants;

namespace TWADotNetCore.ShoppingCartMvcApp.Models.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="User Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Role is required.")]
        public EnumRole Role { get; set; }
    }
}
