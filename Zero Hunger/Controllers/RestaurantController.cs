using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DTOS;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class RestaurantController : Controller
    {
        [Logged]
        public ActionResult Index()
        {
            return View();
        }

        [Logged]
        [HttpGet]
        public ActionResult CollectRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CollectRequest(CollectRequestDTO obj)
        {
            if (ModelState.IsValid)
            {

                var db = new Zero_Hunger_NGOEntities2();
                var CollectRequest = new CollectRequest()
                {
                    FoodName = obj.FoodName,
                    FoodQuantity = obj.FoodQuantity,
                    FoodType = obj.FoodType,
                    MaxPreserveTime = obj.MaxPreserveTime,
                    RequestStatus = "Pending",
                    RestaurantId = (int)Session["Id"]
                };
                db.CollectRequests.Add(CollectRequest);
                db.SaveChanges();
                TempData["msg"] = "Request Added";
                ModelState.Clear();
            }
            else 
            {
                TempData["msg"] = "Request Failed";
                return View();
            }
            return View();
        }

        [Logged]
        public ActionResult History()
        {
            int Id = (int)Session["Id"];
            var db = new Zero_Hunger_NGOEntities2();
            var history = (from h in db.CollectRequests
                           where h.RestaurantId.Equals(Id) select h);
            var historyList = new List<CollectRequest>(history);

            return View(historyList);
        }

        public ActionResult LogOut()
        {
            Session["Id"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}