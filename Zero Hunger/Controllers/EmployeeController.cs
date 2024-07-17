using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.EF;
using Zero_Hunger.Auth;

namespace Zero_Hunger.Controllers
{
    public class EmployeeController : Controller
    {


        [Logged]
        public ActionResult Index()
        {
            return View();
        }

        [Logged]
        public ActionResult CollectRequest()

        {
            int Id = (int)Session["Id"];
            var db = new Zero_Hunger_NGOEntities2();
            var Collect = (from c in db.AssignRequests
                           where c.EmployeeId.Equals(Id)
                           select c);
            var CollectList = new List<AssignRequest>(Collect);

            return View(CollectList);
        }

     

        [Logged]
        public ActionResult AcceptReq(int id)
        {
            var db = new Zero_Hunger_NGOEntities2();
            var collectRequest = db.CollectRequests.Find(id);
            if (collectRequest != null)
            {
                if(collectRequest.RequestStatus != "Collecting" && collectRequest.RequestStatus != "Collected")
                {
                    collectRequest.RequestStatus = "Collecting";
                    db.SaveChanges();
                    return RedirectToAction("CollectRequest");
                }
                else if (collectRequest.RequestStatus =="Declined")
                {
                    TempData["msg"] = "Request Declined!";
                    return RedirectToAction("CollectRequest");
                }
                else if(collectRequest.RequestStatus =="Collected")
                {

                    TempData["msg"] = "Can't Accept a collected Request!";
                    return RedirectToAction("CollectRequest");
                }
                else if (collectRequest.RequestStatus =="Collecting")
                {

                    TempData["msg"] = "Already Accepted!";
                    return RedirectToAction("CollectRequest");
                }
            }

            TempData["msg"] = "Data Not Found!";
            return RedirectToAction("CollectRequest");
        }

        [Logged]
        public ActionResult CollectedReq(int id)
        {
            var db = new Zero_Hunger_NGOEntities2();

            var collectRequest = db.CollectRequests.Find(id);
            if (collectRequest != null)
            {
                if (collectRequest.RequestStatus !="Collected" && collectRequest.RequestStatus != "Declined"
                    && collectRequest.RequestStatus != "Assigned")
                {
                    collectRequest.RequestStatus = "Collected";

                    db.SaveChanges();
                    return RedirectToAction("CollectRequest");
                }
                else if (collectRequest.RequestStatus == "Assigned")
                {
                    TempData["msg"] = "Collect It First!";
                    return RedirectToAction("CollectRequest");
                }
                else if (collectRequest.RequestStatus == "Collected")
                {
                    TempData["msg"] = "Already Collected!";
                    return RedirectToAction("CollectRequest");
                }
                else if( collectRequest.RequestStatus =="Declined")
                {
                    TempData["msg"] = "Can't Collect a Declined Request!";
                    return RedirectToAction("CollectRequest");
                }
            }

            TempData["msg"] = "Data Not Found!";
            return RedirectToAction("CollectRequest");
        }

        [Logged]
        public ActionResult Declined(int id) 
        {
            var db = new Zero_Hunger_NGOEntities2();

            var collectRequest = db.CollectRequests.Find(id);
            if (collectRequest != null)
            {
                if(collectRequest.RequestStatus != "Declined")
                {
                    if (collectRequest.RequestStatus != "Collecting" && collectRequest.RequestStatus != "Collected")
                    {
                        collectRequest.RequestStatus = "Declined";

                        db.SaveChanges();
                        return RedirectToAction("CollectRequest");
                    }

                    TempData["msg"] = "Can't Decline Now!";
                    return RedirectToAction("CollectRequest");
                }
                TempData["msg"] = "Already Declined!";
                return RedirectToAction("CollectRequest");
            }

            TempData["msg"] = "Data Not Found!";
            return RedirectToAction("CollectRequest");

        }


        [Logged]
        [HttpGet]
        public ActionResult CollectionHistory()
        {
            int Id = (int)Session["Id"];
            var db = new Zero_Hunger_NGOEntities2();
            var CollectReq = db.CollectRequests.Where(x => x.EmployeeId == Id && x.RequestStatus == "Collected").ToList();

            return View(CollectReq);

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
