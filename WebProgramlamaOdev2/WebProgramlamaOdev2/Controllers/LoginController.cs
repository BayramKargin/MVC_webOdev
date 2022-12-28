using Microsoft.AspNetCore.Mvc;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(Login girisVerileri)
        {
            return View();
        }
        public IActionResult Register(User user)
        { 

            return View();
        }
    }
}
