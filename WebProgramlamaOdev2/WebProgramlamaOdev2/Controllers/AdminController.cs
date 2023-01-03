using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProgramlamaOdev2.Identity;
using WebProgramlamaOdev2.Migrations;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class AdminController : Controller
    {

        OdevContext db =new OdevContext();
		private UserManager<User> _userManager; //kullanıcı oluşturma falan login
		private SignInManager<User> _signInManager;//seession ve cookie
        private RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
                return RedirectToAction("RoleList");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.roles2 = _roleManager.Roles.Select(i=> i.Name);
            if (user != null)
            {
                return View(new UserDetail()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = (List<string>)roles
                });      

            }
            return View("UserListeleAdmin");

        }



        [HttpPost]
        public async Task<IActionResult> EditUser(UserDetail model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        List<string>? userRoles = await _userManager.GetRolesAsync(user) as List<string>;

                        selectedRoles = selectedRoles ?? new string[] { };

                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                        return RedirectToAction("UserListeleAdmin");


                    }

                }
                var roles1 = _roleManager.Roles.Select(i => i.Name);
                ViewBag.roles2 = roles1;
                return View(model);

            }
            var roles = _roleManager.Roles.Select(i => i.Name);
            ViewBag.roles2 = roles;
            return View(model);

        }


        [Authorize(Roles ="Admin")]
        public IActionResult UserListeleAdmin()
        {
            var model = db.RegisterModel.ToList();

            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> IndexAsync(string Id)
        {

            //var user = db.RegisterModel.FirstOrDefault(x => x.Id == Id);
            var user = await _userManager.FindByIdAsync(Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Index(User model) 
        {
			var user = new User()
			{
				UserName = model.FirstName,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				//password usermanager ile alcaz çünkü hashlenecek şifrelenecek
			};
            //_userManager.AddToRoleAsync(user, model.Yetki);
            //var userEdit = db.RegisterModel.FirstOrDefault(x => x.Id == model.Id);
            var userEdit = _userManager.FindByIdAsync(model.Id);
            if (userEdit != null)
            {
                //.RegisterModel.Remove(userEdit);

               // db.RegisterModel.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("UserListeleAdmin");
        }

    }
}
