using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementApp.Models;
using TaskManagementApp.Models.DataContext;
using TaskManagementApp.Models.Entities;

namespace TaskManagementApp.Controllers
{

    /* Hesap Kontrolünü sağlayan Controller sınıfı(Unauthrozied Controller):
     * Fields:
     *          Database: UserAccount tablosunu çeken field
     * 
     * Methods:
     *                    Index():                                 Ana sayfaya yönlendiren eylem
     *                    Registration():                          Kayıt sayfasını gösteren eylem
     *          [HttpPost]Registration(Registration registration): Kayıt işlemini gerçekleştiren eylem
     *                    Login():                                 Giriş sayfasını gösteren eylem
     *          [HttpPost]Login(Login login):                      Giriş işlemini gerçekleştiren eylem
     *                    LogOut():                                Çıkış işlemini gerçekleştiren eylem
    */
    public class AccountController(ApplicationDbContext applicationDbContext) : Controller
    {
        private readonly ApplicationDbContext context = applicationDbContext;

        public IActionResult Index()
        {
            return View("Index", "Home");
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserAccountDto userDto)
        {
            if (ModelState.IsValid)
            {
                var account = new UserAccount()
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    UserName = userDto.UserName,
                    Password = userDto.Password
                };

                try
                {
                    context.UserAccounts.Add(account);
                    context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.Name} başarılı şekilde kaydedildi. Lütfen giriş yapın.";
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Lütfen farklı bir email ya da username girin.");
                    return View(userDto);
                }

                return View();
            }
            return View(userDto);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = context.UserAccounts.Where(x => x.UserName == login.UserNameOrEmail || x.Email == login.UserNameOrEmail && x.Password == login.Password).FirstOrDefault();
                if ( user != null )
                {
                    // Success login-Creating Cookie
                    var claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, user.Name),
                        new ("Name", user.Name),
                        new (ClaimTypes.Role, "User")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Tasks");

                } else
                {
                    ModelState.AddModelError("", "UserName/Email ya da şifre doğru değil.");
                }
            }
            return View(login);
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
