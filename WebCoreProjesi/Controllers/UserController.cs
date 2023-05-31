using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using WebCoreProjesi.Models;
using WebCoreProjesi.Models.Entities;
using WebCoreProjesi.Models.ViewModel;

namespace WebCoreProjesi.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public UserController(DatabaseContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;   
        }

        public IActionResult Index()
        {
            List<User> liste=_db.Users.ToList();

            List<UserModel> yeniliste = liste.Select(x => _mapper.Map<UserModel>(x)).ToList();
            return View(yeniliste);


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



        }

        public IActionResult Create() 
        { 
          return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if(ModelState.IsValid)
            {
                bool usernameok = _db.Users.Any(x => x.UserName == model.UserName);
                bool emailok = _db.Users.Any(x => x.Email == model.Email);
                if (usernameok)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı kayıtlı");
                }
                if (emailok)
                {
                    ModelState.AddModelError("Email", "Bu email kayıtlı");
                }
                if(emailok || usernameok)
                {
                    return View(model);
                }


                User user=_mapper.Map<User>(model);
                user.CreateDate = DateTime.Now;
                user.ProfilImageFileName = "noimage.jpg";

                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(Guid Id)
        {
            if(Id==null)
            {
                return NotFound();
            }
            User user=_db.Users.Find(Id);
           
            EditUserModel model=_mapper.Map<EditUserModel>(user);   

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid Id,EditUserModel model)
        {
            if(ModelState.IsValid) 
            {
                bool usernameok = _db.Users.Any(x => x.UserName == model.UserName && x.Id!=model.Id);
                bool emailok = _db.Users.Any(x => x.Email == model.Email && x.Id != model.Id);
                if (usernameok)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı kayıtlı");
                }
                if (emailok)
                {
                    ModelState.AddModelError("Email", "Bu email kayıtlı");
                }
                if (emailok || usernameok)
                {
                    return View(model);
                }

                User user = _db.Users.Find(Id);
                _mapper.Map(model, user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid Id)
        {
            User user = _db.Users.Find(Id);

            if(user!=null)
            { 
                _db.Users.Remove(user);
                _db.SaveChanges();

            }         

            return RedirectToAction("Index");
        }



        }
}
