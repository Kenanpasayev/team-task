using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using WebApplication14.Models;
using WebApplication14.ViewModels.Account;

namespace WebApplication14.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User>userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)return View();
            User newUser= new User();
            {
                newUser.Name = registerVM.Name;
                newUser.Surname = registerVM.Surname;
                newUser.Email = registerVM.Email;

               
            };
            var result=await _userManager.CreateAsync(newUser,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            
            return RedirectToAction("Index", "Home");

           
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM) 
        {
            return View();
        }
    }
}
