using BudgetGadget.Entity;
using BudgetGadget.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetGadget.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Registrations
        private BudgetGadgetDBContext db = new BudgetGadgetDBContext();

       
       
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignInCheck([Bind(Include = "Email,Password,ErrorMessage")] Registration registration)
        {
            List<Registration> ra = db.Registrations.ToList();
            foreach (var item in ra)
            {
                if (item.EMail == registration.EMail && item.Password == registration.Password)
                {
                    Session["UserId"] = item.Id;
                    Session["UserName"] = item.Name;
                    Session["UserEmail"] = item.EMail;
                    ViewBag.Time = DateTime.Today.ToString();
                    Session["Error"] = null;
                    return RedirectToAction("UserProfile", "Home");
                }

            }
            

            TempData["Error"] = "Email or Password Invalid";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = (int)Session["UserId"];
            var totalearn = db.Earnings.Where(s => s.RegistrationId == userid).ToList().Sum(s => s.SourceAmount);
            ViewBag.totalearning = totalearn;
            var totex = db.Expenses.Where(s=> s.RegistrationId == userid).ToList().Sum(s => s.ReasonAmount);
            ViewBag.totalexpense = totex;
            ViewBag.currentbalance = totalearn - totex;
            var im = db.Registrations.Where(s => s.Id == userid).FirstOrDefault();
            ViewBag.Image = im.ImageName;

            ViewBag.Date = DateTime.Now.Date.ToShortDateString();
            

            return View();
        }
        [HttpPost]
        public ActionResult Earning(Earning earning)
        {
            //history.Time = DateTime.Today;
            earning.Month = DateTime.Now.Month.ToString();
            earning.Date = DateTime.Now.Date.Day.ToString();

            earning.RegistrationId = (int)Session["UserId"];

            if (ModelState.IsValid)
            {
                db.Earnings.Add(earning);
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }

            return View(earning);
        }

        [HttpPost]
        public ActionResult Expense(Expense expense)
        {
            //history.Time = DateTime.Today;
            expense.Month = DateTime.Now.Month.ToString();
            expense.Date = DateTime.Now.Date.Day.ToString();

            expense.RegistrationId = (int)Session["UserId"];

            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
           
            return View(expense);
        }

        [HttpPost]
        
        public ActionResult Create(Registration registration, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var registered = db.Registrations.Where(s => s.EMail == registration.EMail).FirstOrDefault();
                if (registered == null)
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                }
                else
                {
                    TempData["InvalidRegistration"] = "Somthing Wrong Please Try Again";
                    
                    return RedirectToAction("Index");
                }

                if (registration.Image != null)
                {
                    var extension = Path.GetExtension(Path.GetFileName(registration.Image.FileName));
                    var fileName = "/Content/Image/" + registration.Id.ToString() + registration.Name.ToString() + extension;
                    var filePath = Path.Combine(Server.MapPath(fileName));
                    image.SaveAs(filePath);
                    registration.ImageName = fileName;
                    db.Entry(registration).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["succ"] = "Success";
                    return RedirectToAction("Index");
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}