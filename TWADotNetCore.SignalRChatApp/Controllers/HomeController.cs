using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using TWADotNetCore.SignalRChatApp.Hubs;
using TWADotNetCore.SignalRChatApp.Models;

namespace TWADotNetCore.SignalRChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ChatHub> _chatHub;
        private static int count = 0;

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> chatHub)
        {
            _logger = logger;
            _chatHub = chatHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Order()
        {
            count++;
            await _chatHub.Clients.All.SendAsync("ClientOrderReceiveMessage", count);
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}