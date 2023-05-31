using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebCoreProjesi.Models;
using WebCoreProjesi.Models.Entities;
using WebCoreProjesi.Models.ViewModel;

namespace WebCoreProjesi.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _db;

        public UserController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<User> liste=_db.Users.ToList();

            List<UserModel> yeniliste= new List<UserModel>();

            //1.yol
            //foreach (User user in liste) 
            //{
            //    yeniliste.Add(new UserModel()
            //    {
            //        Name = user.Name,
            //        Email = user.Email,
            //        Id=user.Id,
            //        Surname=user.Surname
            //    });
            //}

          //  2.yol
          //yeniliste = _db.Users.Select(x => new UserModel() { Id = x.Id, Name = x.Name }).ToList();


            return View();
        }
    }
}
