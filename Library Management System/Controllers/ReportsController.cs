using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class ReportsController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available Books", Value="Available"},
                        new SelectListItem{ Text="Issued Books", Value="Book Issued"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            List<LR_Books> list = db.LR_Books.ToList();
            return View(list);
        }

        public ActionResult LogsApplyFilter(String Code)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available Books", Value="Available"},
                        new SelectListItem{ Text="Issued Books", Value="Book Issued"}
                    };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            if (Code == "All")
            {
                List<LR_Books> req = db.LR_Books.ToList();
                return PartialView("_BookReorts", req);
            }
            List<LR_Books> item = db.LR_Books.Where(x => x.status == Code).ToList();
            return PartialView("_BookReorts", item);
        }
        //Edit Reports
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Books book = db.LR_Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_Books book)
        {
            if (ModelState.IsValid)
            {
                book.date = DateTime.Now;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditBooks");
            }
            return View(book);
        }
        // Details of Books
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Books book = db.LR_Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        // Delete Books
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Books book = db.LR_Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: tblRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LR_Books book = db.LR_Books.Find(id);
            db.LR_Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DateTest()
        {
            List<LR_Issue> list = db.LR_Issue.ToList();
            return View(list);
        }
    }
}