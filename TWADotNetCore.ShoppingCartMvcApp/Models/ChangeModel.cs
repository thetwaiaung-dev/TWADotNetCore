using TWADotNetCore.ShoppingCartMvcApp.Models.DataModels;
using TWADotNetCore.ShoppingCartMvcApp.Models.ViewModels;

namespace TWADotNetCore.ShoppingCartMvcApp.Models
{
    public static class ChangeModel
    {
        #region Product
        public static ProductDataModel Change(this ProductViewModel request)
        {
            if (request is null) return new();
            return new ProductDataModel
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                PhotoUrl = request.PhotoUrl,
                Description = request.Description,
                TotalCount = request.TotalCount,
                SaleCount = request.SaleCount,
            };
        }

        public static ProductViewModel Change(this ProductDataModel model)
        {
            if (model is null) return new();
            return new ProductViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                PhotoUrl = model.PhotoUrl,
                Description = model.Description,
                TotalCount = model.TotalCount,
                SaleCount = model.SaleCount
            };
        }
        #endregion

        #region User
        public static UserDataModel Change(this UserViewModel request)
        {
            if (request is null) return new();
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role,
            };
        }

        public static UserViewModel Change(this UserDataModel model)
        {
            if (model is null) return new();
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
            };
        }
        #endregion
    }
}
