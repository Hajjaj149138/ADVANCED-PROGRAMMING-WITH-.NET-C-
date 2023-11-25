using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerV2.Auth;
using ZeroHungerV2.EF;

namespace ZeroHungerV2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Logged]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Logged]
        public ActionResult ShowAllRequest()
        {
            var db = new Assignment_Zero_HungerEntities();
            ViewBag.Employees = (from us in db.Users
                                 where us.Role == "employee"
                                 select us).ToList();
            var data = (from cr in db.Requests
                        where cr.Status == "Requested"
                        select cr).ToList();
            return View(data);
        }
        [Logged]
        [HttpGet]
        public ActionResult AssignEmployee(int id)
        {
            var db = new Assignment_Zero_HungerEntities();
            ViewBag.Employees = (from us in db.Users
                                 where us.Role == "employee" && us.Status == "Available"
                                 select us).ToList();
            var data = db.Requests.Find(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult AssignEmployee(Assignment a)
        {
            var db = new Assignment_Zero_HungerEntities();                                                  

            db.Assignments.Add(a);
            db.SaveChanges();
            var data = db.Requests.Find(a.RequestID);
            data.Status = "Accepted";
            db.SaveChanges();
            var emp = db.Users.Find(a.EmployeeID);
            emp.Status = "Assigned";
            db.SaveChanges();
            return RedirectToAction("ShowAllAssignments");
        }
        [Logged]
        public ActionResult ShowAllAssignments()
        {
            var db = new Assignment_Zero_HungerEntities();
            var data = (from a in db.Assignments
                        where a.Status == "Assigned"
                        select a).ToList();

            return View(data);
        }
        [Logged]
        public ActionResult DistibutionRecord()
        {
            var db = new Assignment_Zero_HungerEntities();
            var data = db.DistributionRecords.ToList();

            return View(data);
        }
        [Logged]
        public ActionResult RecordDetails(int id)
        {
            var db = new Assignment_Zero_HungerEntities();
            var data = db.DistributionRecords.Find(id);
            return View(data);
        }

        [Logged]

        public ActionResult ShowAllRestaurants()
        {
            var db = new Assignment_Zero_HungerEntities();
            var data = db.Restaurants.ToList();

            return View(data);
        }

        public ActionResult ShowAllEmployees()
        {
            var db = new Assignment_Zero_HungerEntities();
            var data = (from us in db.Users
                        where us.Role == "employee"
                        select us).ToList();
            return View(data);
        }


    }
}