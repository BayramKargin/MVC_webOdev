using Microsoft.AspNetCore.Mvc;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Register(int id)
        {
            if(ModelState.IsValid)
            {
                
            }
            return View();
        }

    }
}
