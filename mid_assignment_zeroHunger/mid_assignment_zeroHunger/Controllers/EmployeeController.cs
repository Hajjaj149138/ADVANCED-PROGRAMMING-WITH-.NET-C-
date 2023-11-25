using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerV2.Auth;
using ZeroHungerV2.EF;

namespace ZeroHungerV2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Logged]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Logged]
        public ActionResult Assignments()
        {
            var id = Convert.ToInt32(Session["id"]);
            var db = new Assignment_Zero_HungerEntities();
            var data = (from a in db.Assignments
                        where a.EmployeeID == id && a.Status == "Assigned"
                        select a).ToList();
            return View(data);
        }

        public ActionResult Distributed(int id)
        {
            var employeeid = Convert.ToInt32(Session["id"]);
            var db = new Assignment_Zero_HungerEntities();

            var data = db.Assignments.Find(id);
            var collectrequestid = data.RequestID;
            DistributionRecord record = new DistributionRecord
            {
                RequestID = collectrequestid,
                EmployeeID = employeeid,
                DistributionTime = DateTime.Now,

                Status = "Completed"

            };
            db.DistributionRecords.Add(record);
            db.SaveChanges();
            var asdata = db.Assignments.Find(id);
            asdata.Status = "Completed";
            db.SaveChanges();
            var crdata = db.Requests.Find(collectrequestid);
            crdata.Status = "Completed";
            db.SaveChanges();
            var empdata = db.Users.Find(employeeid);
            empdata.Status = "Available";
            db.SaveChanges();

            return RedirectToAction("Assignments");
        }
        [Logged]
        public ActionResult History()
        {
            var id = Convert.ToInt32(Session["id"]);
            var db = new Assignment_Zero_HungerEntities();
            var data = (from dr in db.DistributionRecords
                        where dr.EmployeeID == id
                        select dr).ToList();
            return View(data);
        }

    }
}