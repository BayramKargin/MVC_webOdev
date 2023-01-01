using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using WebProgramlamaOdev2.Models;


namespace WebProgramlamaOdev2.Controllers
{
    public class AccountController : Controller
    {
                 //[Authorize(Roles ="Admin")]

        public IActionResult Login()
        {
            return View();
        }
        //CRUD

        [HttpPost]
        public IActionResult Login(Login gelen, string returnUrl)
        {

            OdevContext c = new OdevContext();
            var userinfo = c.RegisterModel.FirstOrDefault(x => x.Email == gelen.Email && x.Password == gelen.Password);
            if (userinfo != null)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    //FormsAuthentication.SetAuthCookie(gelen.Email,false);
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
            else
            {
                ViewBag.Mesaj="Hatalı Şifre";
                return RedirectToAction("Register");
            }
           // return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
