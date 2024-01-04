using TWADotNetCore.ATMWebApp.Helper;
using TWADotNetCore.ATMWebApp.Models;
using TWADotNetCore.ATMWebApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TWADotNetCore.ATMWebApp.Constatnts;

namespace TWADotNetCore.ATMWebApp.Controllers
{
    public class AtmController : Controller
    {
        private readonly HelperService _helperService;
        private readonly AtmService _atmService;
        private readonly UserService _userService;

        public AtmController(AtmService atmService, HelperService helperService, UserService userService)
        {
            _helperService = helperService;
            _atmService = atmService;
            _userService = userService;
        }

        [Authorize]
        [ActionName("account-detail")]
        public IActionResult AccountDetail(int id)
        {
            #region use session

            //var userId = HttpContext.Session.GetInt32(NameConstant.SessionUserId);
            //if (userId == null)
            //{
            //    return Redirect("/home/login");
            //}

            #endregion

            UserModel user = _userService.GetUser(id);
            if (user == null)
            {
                return Redirect("/home/login");
            }

            AtmCardModel atmCard = _atmService.GetAtmCard(user.Id);
            atmCard.CardNo = DevCode.FormatAtmCard(atmCard.CardNo);
            AtmCardRequestModel model = ChangeModel.Change(user, atmCard);

            return View("AccountDetail", model);
        }

        [Authorize]
        public IActionResult WithDrawl()
        {
            #region use session

            //var userId = HttpContext.Session.GetInt32(NameConstant.SessionUserId);
            //if (userId == null)
            //{
            //    return Redirect("/home/login");
            //}

            #endregion
            return View();
        }

        [HttpPost]
        public IActionResult WithDrawlAtm(int amount)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var atmCard = _atmService.GetAtmCard(userId);
            if (amount > atmCard.Amount)
            {
                TempData["Message"] = "Insufficient Balance.";
                return View("WithDrawl");
            }

            if (atmCard.Amount - amount < 100)
            {
                TempData["Message"] = "At least 100Ks must have in ATM";
                return View("WithDrawl");
            }

            atmCard.Amount = atmCard.Amount - amount;
            var result = _atmService.UdpateAtmCard();

            TempData["Message"] = result > 0 ? "Withdrawl Successful." : "Withdrawl Failed.";
            return View("WithDrawl");
        }

        [Authorize]
        public IActionResult Deposit()
        {
            #region use session

            //var userId = HttpContext.Session.GetInt32(NameConstant.SessionUserId);
            //if (userId == null)
            //{
            //    return Redirect("/home/login");
            //}

            #endregion

            return View();
        }

        [HttpPost]
        public IActionResult DepositAtm(int amount)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var atmCard = _atmService.GetAtmCard(userId);
            if (atmCard == null)
            {
                TempData["Message"] = "Card is Invalid.";
                return View("Deposit");
            }

            atmCard.Amount = atmCard.Amount + amount;
            var result = _atmService.UdpateAtmCard();

            TempData["Message"] = result > 0 ? "Thank you. Deposit successful." : "Deposit Fail. Please try again.";
            return View("Deposit");
        }
    }
}
