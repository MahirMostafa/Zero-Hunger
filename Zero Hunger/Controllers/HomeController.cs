using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DTOS;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginDto user)
        {
             if(ModelState.IsValid)
            {
                var db = new Zero_Hunger_NGOEntities2();
                var dbRestaurant = db.Restaurants.FirstOrDefault(u => u.Email == user.Email);
                var dbAdmin = db.Admins.FirstOrDefault(u => u.Email == user.Email);
                var dbEmployee = db.Employees.FirstOrDefault(u => u.Email == user.Email);

                if (dbRestaurant != null)
                {
                    if (dbRestaurant.Password == user.Password)
                    {

                        Session["Id"] = dbRestaurant.Id;
                        Session["Name"] = dbRestaurant.Name;
                        Session["Email"] = dbRestaurant.Email;

                        return RedirectToAction("Index", "Restaurant");
                    }
                    else
                    {

                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
                else if(dbAdmin != null)
                {
                    if (dbAdmin.Password == user.Password)
                    {

                        Session["Id"] = dbAdmin.Id;
                        Session["Name"] = dbAdmin.Name;
                        Session["Email"] = dbAdmin.Email;

                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {

                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }

                else if (dbEmployee != null)
                {
                    if (dbEmployee.Password == user.Password)
                    {

                        Session["Id"] = dbEmployee.Id;
                        Session["Name"] = dbEmployee.Name;
                        Session["Email"] = dbEmployee.Email;

                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {

                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "The user with provided email does not exist.");
                }
            }

             return View();
        }

        [HttpGet]
        public ActionResult Signup() 
        { 
            return View();
        }

    
        [HttpPost]
        public ActionResult Signup(RestaurantSignupDTO user)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_NGOEntities2();
                var email = user.Email;
                var exist = db.Restaurants.SingleOrDefault(r => r.Email == email);
                if (exist == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<RestaurantSignupDTO, Restaurant>();
                    });
                    var mapper = new Mapper(config);
                    var data = mapper.Map<Restaurant>(user);
                    db.Restaurants.Add(Convert(user));
                    db.SaveChanges();
                    TempData["msg"] = "Signup Success!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["msg"] = "User Already Exists!";
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        RestaurantSignupDTO Convert(Restaurant u)
        {
            return new RestaurantSignupDTO()
            {
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Password = u.Password,
                Address = u.Address,

            };
        }

        Restaurant Convert(RestaurantSignupDTO u)
        {
            return new Restaurant()
            {
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Password = u.Password,
                Address = u.Address,

            };
        }
    }
}