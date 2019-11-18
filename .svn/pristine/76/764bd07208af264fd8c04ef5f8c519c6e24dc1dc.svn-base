using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Management_System.Models;


namespace Library_Management_System.Controllers
{
    public class AddBooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddBooks
        public ActionResult Index()
        {
            return View(db.AddBook.ToList());
        }

        // GET: AddBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddBook addBook = db.AddBook.Find(id);
            if (addBook == null)
            {
                return HttpNotFound();
            }
            return View(addBook);
        }

        // GET: AddBooks/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AddBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookid,isbn,bookname,authorname,edition,copies,date")] AddBook addBook)
        {//checks

            if (ModelState.IsValid)
            {
                addBook.date = DateTime.Now;
                var chkBook = db.AddBook.Where(x=>x.bookname==addBook.bookname).ToList();
                var chkAuthor = db.AddBook.Where(y => y.authorname == addBook.authorname).ToList();
                var chkEdition = db.AddBook.Where(z => z.edition == addBook.edition).ToList();

                if (chkAuthor.Count > 0 && chkEdition.Count > 0 && chkBook.Count > 0)
                {
                    ViewBag.warning = "This book already exists";
                    return View();
                }
                else { 
                db.AddBook.Add(addBook);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
            }

            return View(addBook);
        }

        // GET: AddBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddBook addBook = db.AddBook.Find(id);
            if (addBook == null)
            {
                return HttpNotFound();
            }
            return View(addBook);
        }

        // POST: AddBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookid,isbn,bookname,authorname,edition,copies,date")] AddBook addBook)
        {
            if (ModelState.IsValid)
            {
                addBook.date = DateTime.Now;
                var chkBook = db.AddBook.Where(x => x.bookname == addBook.bookname).ToList();
                var chkAuthor = db.AddBook.Where(y => y.authorname == addBook.authorname).ToList();
                var chkEdition = db.AddBook.Where(z => z.edition == addBook.edition).ToList();
                if (chkAuthor.Count > 0 && chkEdition.Count > 0 && chkBook.Count > 0)
                {
                    ViewBag.warning = "This book already exists";
                    return View();
                }
                else
                {
                    db.Entry(addBook).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(addBook);
        }

        // GET: AddBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddBook addBook = db.AddBook.Find(id);
            if (addBook == null)
            {
                return HttpNotFound();
            }
            return View(addBook);
        }

        // POST: AddBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddBook addBook = db.AddBook.Find(id);
            db.AddBook.Remove(addBook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      
    }
}
