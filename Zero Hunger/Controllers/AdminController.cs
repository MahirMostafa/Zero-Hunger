using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DTOS;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class AdminController : Controller
    {

        [Logged]
        public ActionResult Index()
        {
            return View();
        }


        [Logged]
        public ActionResult Management()
        {
            return View();
        }

        [Logged]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [Logged]
        [HttpPost]
        public ActionResult AddAdmin(AdminSignUpDTO Ad)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_NGOEntities2();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AdminSignUpDTO, Admin>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Admin>(Ad);
                db.Admins.Add(Convert(Ad));
                db.SaveChanges();
                TempData["msg"] = "Admin Added!";
                return RedirectToAction("Management");
            }
            else
            {
                TempData["msg"] = "Error Occured!";
                return View();
            }

        }

        [Logged]
        [HttpGet]

        public ActionResult AddEmployee()
        {
            return View();
        }
        [Logged]
        [HttpPost]
        public ActionResult AddEmployee(EmployeeSignUpDTO Em)
        {

            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_NGOEntities2();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeSignUpDTO, Employee>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Employee>(Em);
                db.Employees.Add(Convert(Em));
                db.SaveChanges();
                TempData["msg"] = "Employee Added!";
                return RedirectToAction("Management");
            }
            else
            {
                TempData["msg"] = "Error Occured!";
                return View();
            }

        }

        [Logged]
        public ActionResult ViewAdmin()
        {
            var db = new Zero_Hunger_NGOEntities2();
            var Admins = db.Admins.ToList();
            return View(Admins);
        }

        [Logged]
        public ActionResult ViewEmployee()
        {
            var db = new Zero_Hunger_NGOEntities2();
            var Employees = db.Employees.ToList();
            return View(Employees);
        }

        //[Logged]
        //public ActionResult DeleteEmployee(int id)
        //{
        //    var db = new Zero_Hunger_NGOEntities2();
        //    var employee = db.Employees.Find(id);
        //    if(employee != null)
        //    {
        //        db.Employees.Remove(employee);
        //        db.SaveChanges();
        //        TempData["msg"] = "Employee Deleted!";
        //        return View();
        //    }
        //    else
        //    {
        //        TempData["msg"] = "Error Occured";

        //    return View();
        //    }

        //}


        [Logged]
        [HttpGet]
        public ActionResult AssignEmployee()
        {
            var db = new Zero_Hunger_NGOEntities2();
            var CollectReq = db.CollectRequests.Where(x => x.RequestStatus == "Pending" || x.RequestStatus =="Declined").ToList();

            return View(CollectReq);
        }

        [Logged]
        [HttpGet]
        public ActionResult TrackEmployee()
        {

            var db = new Zero_Hunger_NGOEntities2();
            var CollectReq = db.CollectRequests.Where(x => x.RequestStatus != "Pending").ToList();

            return View(CollectReq);

        }

        [Logged]
        [HttpGet]
        public ActionResult Assign(int id)
        {
            var db = new Zero_Hunger_NGOEntities2();
            var CollectReq =db.CollectRequests.Single(x => x.Id == id);
            var employees = db.Employees.ToList();
            ViewBag.EmployeeId = new SelectList(employees, "Id", "Name");
            return View(CollectReq);
        }

        [Logged]
        [HttpPost]

        public ActionResult Assign(CollectRequest collectRequest)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_NGOEntities2();
                db.Entry(collectRequest).State = EntityState.Modified;
                db.SaveChanges();

                var assignRequest = new AssignRequest
                {
                    CollectRequestId = collectRequest.Id,  
                    AdminId = (int)collectRequest.AdminId,      
                    EmployeeId = (int)collectRequest.EmployeeId 
                };
                db.AssignRequests.Add(assignRequest);
                db.SaveChanges();

                return RedirectToAction("AssignEmployee");
            }
            return View(collectRequest);
        }


        public ActionResult LogOut()
        {
            Session["Id"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "Home");
        }






















        AdminSignUpDTO Convert(Admin a)
        {
            return new AdminSignUpDTO()
            {
                Name = a.Name,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Password = a.Password,
                Address = a.Address,

            };
        }

        Admin Convert(AdminSignUpDTO a)
        {
            return new Admin()
            {
                Name = a.Name,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Password = a.Password,
                Address = a.Address,

            };
        }

        Employee Convert(EmployeeSignUpDTO a)
        {
            return new Employee()
            {
                Name = a.Name,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Password = a.Password,
                Address = a.Address,

            };
        }
    }
}