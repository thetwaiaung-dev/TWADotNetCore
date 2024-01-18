namespace TWADotNetCore.ShoppingCartMvcApp.Models
{
    public static class ChangeModel
    {
        public static ProductDataModel Change(this ProductViewModel request)
        {
            if (request is null) return new();
            return new ProductDataModel
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                PhotoUrl = request.PhotoUrl,
                Photo = request.Photo,
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
                Photo = model.Photo,
            };
        }
    }
}
