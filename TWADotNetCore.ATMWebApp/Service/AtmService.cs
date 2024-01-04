using TWADotNetCore.ATMWebApp.Helper;
using TWADotNetCore.ATMWebApp.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace TWADotNetCore.ATMWebApp.Service;

public class AtmService
{
    private readonly AtmDbContext _context;
    private readonly UserService _userService;
    private readonly HelperService _helperService;

    public AtmService(AtmDbContext context, HelperService helperService)
    {
        _context = context;
        _helperService = helperService;
    }

    public List<AtmCardModel> GetAll()
    {
        return _context.AtmCard.AsNoTracking().ToList();
    }

    public int Save(AtmCardModel atmCard)
    {
        _context.AtmCard.Add(atmCard);
        var result = _context.SaveChanges();

        return result;
    }

    public int SaveUserAndAtm(UserModel user)
    {
        int result = 0;

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.User.Add(user);
            _context.SaveChanges();
            int userId = user.Id;

            if (userId > 0)
            {
                var atmCard = new AtmCardModel
                {
                    CardNo = _helperService.GetAtmCode(),
                    Amount = 0,
                    UserId = userId,
                };

                _context.AtmCard.Add(atmCard);
                result = _context.SaveChanges();
                if (result > 0)
                {
                    transaction.Commit();
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Transaction ::" + ex.Message);
            transaction.Rollback();
        }
        return result;
    }

    public AtmCardModel GetAtmCard(int userId)
    {
        return _context.AtmCard.FirstOrDefault(x => x.UserId == userId);
    }

    public AtmCardModel GetAtmCardByCartNo(string cardNo)
    {
        return _context.AtmCard.AsNoTracking().FirstOrDefault(x => x.CardNo == cardNo);
    }

    public int UdpateAtmCard()
    {
        return _context.SaveChanges();
    }
}