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
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("UserName", user.UserName));

                    ClaimsPrincipal principal=new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)); 

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı yada şifre hatalı");
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
                User kullanici1=_db.Users.FirstOrDefault(x=>x.UserName==model.Username);

                User kullanici2 = _db.Users.FirstOrDefault(x => x.Email == model.Email);

                if(kullanici1!=null)
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı kayıtlı");
                }
                if (kullanici2!=null)
                {
                    ModelState.AddModelError("Email", "Bu email kayıtlı");
                    
                }    
                if(kullanici1 != null || kullanici2!=null) 
                {
                    return View(model);
                }


                string sifre = StringHashed(model.Password);

                User user = new User()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Password = sifre,
                    CreateDate = DateTime.Now,
                    Aktivate = true,
                    Role="user"
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

        public User UserFind()
        {
			Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
			return  _db.Users.Find(userid);
		}
        public IActionResult Profile()
		{
	    	ProfilBilgileri();

			return View();
		}

		private void ProfilBilgileri()
		{
			User user = UserFind();
			ViewData["ad"] = user.Name;
			ViewData["soyad"] = user.Surname;
			ViewData["kullanıcı"] = user.UserName;
			ViewData["email"] = user.Email;
			ViewData["şifre"] = user.Password;
		}

		[HttpPost]
        public IActionResult AdKaydet(string ad)
        {
			User user = UserFind();
            user.Name = ad;
            _db.SaveChanges();

			return RedirectToAction("Profile");
        }

		[HttpPost]
		public IActionResult SoyadKaydet(string soyad)
		{
			User user = UserFind();
            user.Surname = soyad;
            _db.SaveChanges();

			return RedirectToAction("Profile");
		}


		[HttpPost]
		public IActionResult UserNameSave(string username)
		{
			User user = UserFind();

			User kullanici1 = _db.Users.FirstOrDefault(x => x.UserName ==username && x.Id!=user.Id);

			if (kullanici1 != null)
			{
				ModelState.AddModelError("Username", "Bu kullanıcı adı kayıtlı");
                ProfilBilgileri();
                return View("Profile");
			}

            user.UserName = username;
            _db.SaveChanges();

			return RedirectToAction("Profile");
		}

		





		public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
