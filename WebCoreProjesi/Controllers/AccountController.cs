using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebCoreProjesi.Models;
using WebCoreProjesi.Models.Entities;

namespace WebCoreProjesi.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly IConfiguration _configuration; 

        public AccountController(DatabaseContext db,IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                string sifre = StringHashed(model.Password);

                User user = _db.Users.FirstOrDefault(x => x.UserName == model.Username && x.Password == sifre);

                if(user!=null)
                {
                    if(!user.Aktivate)
                    {
                        ModelState.AddModelError("", "Kullanıcı aktif değil");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name,user.Name??string.Empty));
                    claims.Add(new Claim("UserName", user.UserName));

                    ClaimsPrincipal principal=new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)); 

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User kullanici=_db.Users.FirstOrDefault(x=>x.UserName==model.Username && x.Email==model.Email);

                if(kullanici!=null)
                {
                    if(kullanici.UserName==model.Username)
                        ModelState.AddModelError("Username", "Bu kullanıcı adı kayıtlı");
                    if (kullanici.Email == model.Email)
                        ModelState.AddModelError("Email", "Bu email kayıtlı");
                    return View(model);
                }

                string sifre = StringHashed(model.Password);

                User user = new User()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Password = sifre,
                    CreateDate = DateTime.Now,
                    Aktivate = true
                };
                _db.Users.Add(user);
                int sonuc = _db.SaveChanges();
                if (sonuc > 0)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı kaydedilemedi");
                }
            }
            return View(model);
        }

        private string StringHashed(string password)
        {
            string salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string sifre = password + salt;
            sifre = sifre.MD5();
            return sifre;
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
