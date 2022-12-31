using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
        //CRUD
        
        [HttpPost]
        public IActionResult Login(Login gelen)
        {
            OdevContext c = new OdevContext();
            var userinfo = c.RegisterModel.FirstOrDefault(x => x.Email == gelen.Email && x.Password == gelen.Password);
            if (userinfo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Register(RegisterModel model)
        {
            OdevContext _context = new OdevContext();
            
            if(ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Register));
            }
            return View();
        }

    }
}
