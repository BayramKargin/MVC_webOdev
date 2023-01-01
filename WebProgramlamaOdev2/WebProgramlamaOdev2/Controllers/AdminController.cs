using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgramlamaOdev2.Migrations;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class AdminController : Controller
    {

        OdevContext db =new OdevContext();
        [Authorize]
        public IActionResult UserListeleAdmin()
        {
            var model = db.RegisterModel.ToList();
            return View(model);
        }
        public IActionResult Index(int Id)
        {
            var user = db.RegisterModel.FirstOrDefault(x => x.Id == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Index(RegisterModel model) 
        {
            var userEdit = db.RegisterModel.FirstOrDefault(x => x.Id == model.Id);
            if (userEdit != null)
            {
                db.RegisterModel.Remove(userEdit);
                db.RegisterModel.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("UserListeleAdmin");
        }

    }
}
