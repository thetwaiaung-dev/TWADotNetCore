using TWADotNetCore.ATMWebApp.Helper;
using TWADotNetCore.ATMWebApp.Models;
using TWADotNetCore.ATMWebApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TWADotNetCore.ATMWebApp.Constatnts;

namespace TWADotNetCore.ATMWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HelperService _helperService;
        private readonly AtmService _atmService;
        private readonly UserService _userService;
        private readonly AtmDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,
                                HelperService helperService,
                                AtmService atmService,
                                UserService userService,
                                AtmDbContext dbContext)
        {
            _logger = logger;
            _helperService = helperService;
            _atmService = atmService;
            _userService = userService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAtmCard(string cardNo)
        {
            MessageModel model = new MessageModel();

            var validAtm = DevCode.IsValidAtmCode(cardNo);
            if (!validAtm)
            {
                model.IsSuccess = false;
                model.Message = "Card No is invalid.";
                return Json(model);
            }

            AtmCardModel atmCard = _atmService.GetAtmCardByCartNo(cardNo);
            if (atmCard == null)
            {
                model.IsSuccess = false;
                model.Message = "Card No is Wrong.";
                return Json(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.UserId = atmCard.UserId;
            TempData["SuccessAtmCard"] = true;
            return Json(model);
        }

        public IActionResult LoginUser(int id)
        {
            if (TempData["SuccessAtmCard"] == null)
            {
                TempData["Message"] = "Please Login again.";
                return Redirect("/home/login");
            }

            UserModel user = _userService.GetUser(id);
            if (user == null)
            {
                TempData["Message"] = "Please Login again.";
                return Redirect("/home/login");
            }

            AtmCardRequestModel model = new AtmCardRequestModel()
            {
                Name = user.Name,
                UserId = user.Id,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(int userId, string pin)
        {
            MessageModel model = new MessageModel();

            UserModel user = _userService.GetUser(userId);
            if (user.Password != pin)
            {
                model.IsSuccess = false;
                model.Message = "Pin No is invalid";
                return Json(model);
            }

            #region using session

            //HttpContext.Session.SetString(NameConstant.SessionUserName, user.Name);
            //HttpContext.Session.SetInt32(NameConstant.SessionUserId, user.Id);

            #endregion

            #region using cookie base authentication

            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id)),
                 new Claim(ClaimTypes.Name,user.Name),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            #endregion

            model.IsSuccess = true;
            model.Message = "Login Successful.";
            model.UserId = userId;
            return Json(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "home");
        }

        public IActionResult Register()
        {
            return View(new AtmCardRequestModel());
        }

        [HttpPost]
        public IActionResult Register(AtmCardRequestModel request)
        {
            UserModel? user = request.ChangeUser();
            int result = _atmService.SaveUserAndAtm(user);
            if (result > 0)
            {
                TempData["SuccessAtmCard"] = true;
                return Redirect($"LoginUser/{user.Id}");
            }

            return View("Register", request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}