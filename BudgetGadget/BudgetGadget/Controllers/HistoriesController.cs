using BudgetGadget.Entity;
using BudgetGadget.Models;
using BudgetGadget.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BudgetGadget.Controllers
{
    public class HistoriesController : Controller
    {
        
        // GET: Histories
        private BudgetGadgetDBContext db = new BudgetGadgetDBContext();

        public ActionResult MonthHistory(string id, string months)
        {
            if(Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = (int) Session["UserId"];
           
            var Earn = db.Earnings.Where(x => (x.RegistrationId == userid) 
            && (x.Month == months) && (x.Date == id)).ToList();
            var Expen = db.Expenses.Where(x => (x.RegistrationId == userid)
            && (x.Month == months) && (x.Date == id)).ToList();

            var model = new HistoryViewModel
            {
                Earning = Earn,
                Expense = Expen,
                RegistrationId = userid
            };

            string[] mon = { "Jan", "Feb", "Mar","Apr","May","Jun","Jul","Aug", "Sep", "Oct", "Nov", "Dec" };
            int mn=0;
            if(months == "1")
            {
                mn = 0;
            }
            else if (months == "2")
            {
                mn = 1;
            }
            else if (months == "3")
            {
                mn = 2;
            }
            else if (months == "4")
            {
                mn = 3;

            }
            else if (months == "5")
            {
                mn = 4;

            }
            else if (months == "6")
            {
                mn = 5;

            }
            else if (months == "7")
            {
                mn = 6;

            }
            else if (months == "8")
            {
                mn = 7;

            }
            else if (months == "9")
            {
                mn = 8;

            }
            else if (months == "10")
            {
                mn = 9;

            }
            else if (months == "11")
            {
                mn = 10;

            }
            else if (months == "12")
            {
                mn = 11;

            }
            ViewBag.Date = id;
            ViewBag.Month = mon[mn];
            ViewBag.Year = DateTime.Now.Year;
            ViewBag.DateMonth = null;

            var totalearn = db.Earnings.Where(x => (x.RegistrationId == userid)
            && (x.Month == months) && (x.Date == id)).ToList().Sum(s => s.SourceAmount);
            ViewBag.totalearning = totalearn;
            var totex = db.Expenses.Where(x => (x.RegistrationId == userid)
            && (x.Month == months) && (x.Date == id)).ToList().Sum(s => s.ReasonAmount);
            ViewBag.totalexpense = totex;

            return View("Index",model);

        }

        public ActionResult FullMonthHistory(string months)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = (int)Session["UserId"];

            var Earn = db.Earnings.Where(x => (x.RegistrationId == userid)
            && (x.Month == months)).ToList();
            var Expen = db.Expenses.Where(x => (x.RegistrationId == userid)
            && (x.Month == months)).ToList();

            var model = new HistoryViewModel
            {
                Earning = Earn,
                Expense = Expen,
                RegistrationId = userid
            };

            string[] mon = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int mn = 0;
            if (months == "1")
            {
                mn = 0;
            }
            else if (months == "2")
            {
                mn = 1;
            }
            else if (months == "3")
            {
                mn = 2;
            }
            else if (months == "4")
            {
                mn = 3;

            }
            else if (months == "5")
            {
                mn = 4;

            }
            else if (months == "6")
            {
                mn = 5;

            }
            else if (months == "7")
            {
                mn = 6;

            }
            else if (months == "8")
            {
                mn = 7;

            }
            else if (months == "9")
            {
                mn = 8;

            }
            else if (months == "10")
            {
                mn = 9;

            }
            else if (months == "11")
            {
                mn = 10;

            }
            else if (months == "12")
            {
                mn = 11;

            }
            ViewBag.Date = null;
            ViewBag.Month = mon[mn];
            ViewBag.Year = DateTime.Now.Year;
            ViewBag.DateMonth = 1;

            var totalearn = db.Earnings.Where(x => (x.RegistrationId == userid)
            && (x.Month == months)).ToList().Sum(s => s.SourceAmount);
            ViewBag.totalearning = totalearn;
            var totex = db.Expenses.Where(x => (x.RegistrationId == userid)
            && (x.Month == months)).ToList().Sum(s => s.ReasonAmount);
            ViewBag.totalexpense = totex;

            return View("Index", model);

        }


        public ActionResult EditEarning(int? id)
        {
            Earning his = db.Earnings.Find(id);
           
            return View(his);
        }

        [HttpPost]
        public ActionResult EditEarning(Earning earning)
        {
            int userid = (int)Session["UserId"];
            earning.RegistrationId = userid;
            if (ModelState.IsValid)
            {
               
                db.Entry(earning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MonthHistory", "Histories",new { id = earning.Date, months = earning.Month });
            }
            return View(earning);
        }

        public ActionResult DeleteEarning(int id)
        {
            Earning earn = db.Earnings.Find(id);
            db.Earnings.Remove(earn);
            db.SaveChanges();
            return RedirectToAction("MonthHistory", "Histories", new { id = earn.Date, months = earn.Month});
        }



        public ActionResult EditExpense(int? id)
        {
            Expense exp = db.Expenses.Find(id);

            return View(exp);
        }

        [HttpPost]
        public ActionResult EditExpense(Expense expense)
        {
            int userid = (int)Session["UserId"];
            expense.RegistrationId = userid;

            if (ModelState.IsValid)
            {

                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MonthHistory", "Histories", new { id = expense.Date, months = expense.Month });
            }
            return View(expense);
        }

        public ActionResult DeleteExpense(int id)
        {
            Expense exp = db.Expenses.Find(id);
            db.Expenses.Remove(exp);
            db.SaveChanges();
            return RedirectToAction("MonthHistory", "Histories", new { id = exp.Date, months = exp.Month });
        }


    }

}