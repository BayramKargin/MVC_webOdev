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
using WebProgramlamaOdev2.Identity;
using WebProgramlamaOdev2.Models;


namespace WebProgramlamaOdev2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager; //kullanıcı oluşturma falan login
        private SignInManager<User> _signInManager;//seession ve cookie
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //CRUD

        [HttpPost]
        public async Task<IActionResult> LoginAsync(RegisterModel gelen, string returnUrl)
        {

            OdevContext c = new OdevContext();
            var user = await _userManager.FindByEmailAsync(gelen.Email);
            var userinfo = c.RegisterModel.FirstOrDefault(x => x.Email == gelen.Email && x.Password == gelen.Password);
            var result = await _signInManager.PasswordSignInAsync(user, gelen.Password, true, false);
            if (result.Succeeded)
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
        public  async Task<IActionResult> Register(RegisterModel model)
        {
			if (!ModelState.IsValid)
				return View(model);
            var user = new User()
            {
                UserName = model.FirstName,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				//password usermanager ile alcaz çünkü hashlenecek şifrelenecek
			}; 
            var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded) //kullanıcı oluşturma başaılı ise
			{
				//token ve email onaylama
				
				//email 
				return RedirectToAction("Login");
			}
			ModelState.AddModelError("", "bilinmeyen bir hata oluştu tekrar deneyiniz.");
			return View();

			//if (ModelState.IsValid)
			//         {
			//             await _userManager.CreateAsync(user,model.Password);
			//             _context.Add(model);
			//             _context.SaveChanges();
			//             return RedirectToAction(nameof(Register));
			//         }
			//         return View();
		}
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("Login");
		}

	}
}
