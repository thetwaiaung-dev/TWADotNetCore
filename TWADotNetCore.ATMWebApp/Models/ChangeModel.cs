namespace TWADotNetCore.ATMWebApp.Models;

public static class ChangeModel
{
    public static AtmCardModel? Change(this AtmCardRequestModel? requestModel)
    {
        if (requestModel == null) return null;
        return new AtmCardModel
        {
            CardNo = requestModel.CardNo,
            Amount = requestModel.Amount
        };
    }

    public static UserModel? ChangeUser(this AtmCardRequestModel? requestModel)
    {
        if (requestModel == null) return null;
        return new UserModel
        {
            Name = requestModel.Name,
            Email = requestModel.Email,
            Password = requestModel.Password,
        };
    }

    public static AtmCardRequestModel Change(UserModel user, AtmCardModel atmCard)
    {
        if (user == null || atmCard == null) return null;
        return new AtmCardRequestModel()
        {
            CardNo = atmCard.CardNo,
            Amount = atmCard.Amount,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }
}