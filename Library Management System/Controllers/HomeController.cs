using Library_Management_System.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult AddBooks(int? artId)
       {
        //    List<LR_ArticleCategory> artIdd = db.ArticleCategory.ToList();
        //    ViewBag.artId = new SelectList(artIdd, "id");
        if(artId==0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.artId = artId;
                return View();
            }
            
        }
        //Add Books
        [HttpPost]
        public ActionResult AddBooks(LR_Books model, FormCollection from)
        {
            if (ModelState.IsValid)
            {
                model.date = DateTime.Now;
                model.activity = "Active";
                model.status = "Available";
                model.article_category = "Book";

                model.Remaining_Quanity = model.Quantity;
                var chkisbn = db.LR_Books.Where(x => x.ISBN == model.ISBN).ToList();

                if (chkisbn.Count > 0)

                {
                    ViewBag.warning = "This book already exists.Enter Unique ISBN";
                    return View();
                }
                else
                {
                    db.LR_Books.Add(model);
                    db.SaveChanges();
                    SaveBookLogs(model);
                }
              
            }
            else
            {
                ViewBag.warning = "An Error Accurs";
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Dasboard()
         {

            ViewBag.issueBookCount = db.LR_Issue.Where(x => x.Status == "Book Issued").Count();
            ViewBag.reserveBookCount = db.LR_Reservations.Where(x => x.status == " Reserved").Count();
            ViewBag.FineBookCount = db.LR_ReturnBook.Where(x => x.fine>0).Count();
            ViewBag.ReturnBooksCount = db.LR_Issue.Where(x => x.Status == "Available").Count();


           

            var booksIssued = db.LR_Issue.Where(x => x.Status == "Book Issued").GroupBy(x => x.title).ToList();
            
            List<chart> li = new List<chart>();
            foreach (var item in booksIssued)
            {
                
                chart obj = new chart();
                obj.label = item.Key;
                ViewBag.label = item.Key;
                obj.y = item.Key.Count();

                li.Add(obj);
            }
            
            ViewBag.DataPoints = JsonConvert.SerializeObject(li, _jsonSetting);




            return View();
        }
        //Add Magazines
        public ActionResult AddMagazines(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddMagazines(LR_Books model, FormCollection from)
        {
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
           
            model.article_category = "Magazine";
            var chkmag = db.LR_Books.Where(x => x.name == model.name).ToList();
          
                db.LR_Books.Add(model);
                db.SaveChanges();
                SaveMagazineLogs(model);
            
            return RedirectToAction("Index");

        }

        private void SaveMagazineLogs(LR_Books model)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = model.id;
                model2.ISBN = model.ISBN;
                model2.name = model.name;
                model2.cost = model.cost;
                model2.Status = "Index";

                //model2.category = model.Category;

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Add AddJournal
        public ActionResult AddJournal(int? artId)
        {
            if(artId==0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.artId = artId;
                return View();
            }
           
        }
        [HttpPost]
        public ActionResult AddJournal(LR_Books model, FormCollection from)
        {

            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
           // model.ISBN = "xxxxx";
            model.article_category = "Journal";
            db.LR_Books.Add(model);
            db.SaveChanges();
            SaveJournalLogs(model);

            return RedirectToAction("Index");

        }

        private void SaveJournalLogs(LR_Books model)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = model.id;
                model2.ISBN = model.ISBN;
                model2.name = model.name;
                model2.cost = model.cost;
                model2.Status = "Journal Added";

                

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Add AddNewsPaper
        public ActionResult AddNewsPaper(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewsPaper(LR_Books model, FormCollection from)
        {
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.article_category = "NewsPaper";
            db.LR_Books.Add(model);
            db.SaveChanges();
            SavenewspprLogs(model);

            return RedirectToAction("AddNewsPaper");

        }

        private void SavenewspprLogs(LR_Books model)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = model.id;
                model2.ISBN = model.ISBN;
                model2.name = model.name;
                model2.cost = model.cost;
                model2.Status = "NewsPaper Added";

                //model2.category = model.Category;

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult CreateNewBooks(string Role)
        {
            switch (Role)
            {
                case "1":
                    return RedirectToAction("AddBooks", new { artId = Role });
                    ;
                case "2":
                    return RedirectToAction("AddJournal", new { artId = Role });
                case "3":
                    return RedirectToAction("AddNewsPaper", new { artId = Role });
                case "4":
                    return RedirectToAction("AddMagazines", new { artId = Role });

                default:
                    break;
            }
            return View();
        }
        //View List of Books
        public ActionResult Index()
        {
            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="Books", Value="1"},
                        new SelectListItem{ Text="Magazines", Value="4"},

                    };
            ViewBag.prioritylist = priority;
            List<LR_ArticleCategory> catname = db.ArticleCategory.ToList();
            ViewBag.cat = new SelectList(catname, "id", "catname");
            
            List<LR_Books> list = db.LR_Books.Where(x => x.activity=="Active").ToList();
            return View(list);
        }
        
        // Edit Books
        public ActionResult EditBooks(int? id)
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
        public ActionResult EditBooks(LR_Books book)
        {
            if (ModelState.IsValid)
            {
                book.date = DateTime.Now;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                BookEditlogs(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        private void BookEditlogs(LR_Books book)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = book.id;
                model2.ISBN = book.ISBN;
                model2.name = book.name;
                model2.cost = book.cost;
                model2.Status = "Book Edited";

                //model2.category = book.Category;

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                else
                {
                    book.activity = "deactivate";
                book.status = "Deactive";
                    db.Entry(book).State = EntityState.Modified;
                    int i = db.SaveChanges();
                      BookDeletelogs(book);
                    if (i > 0)
                    {
                        TempData["Message"] = "success";
                        TempData["MessageInside"] = "Item was deleted successfully!";
                    }
                    else
                    {
                        TempData["Message"] = "error";
                        TempData["MessageInside"] = "Item was not deleted! Please try again";
                    }
                    return RedirectToAction("Index");
                }
            }


        private void BookDeletelogs(LR_Books book)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = book.id;
                model2.ISBN = book.ISBN;
                model2.name = book.name;
                model2.cost = book.cost;
                model2.Status = "Book Deleted";

                //model2.category = book.Category;

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //// POST: tblRequisitions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LR_Books book = db.LR_Books.Find(id);
        //    db.LR_Books.Remove(book);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        //Save Books Logs

        public void SaveBookLogs(LR_Books model)
        {
            try
            {
                LR_BooksLogs model2 = new LR_BooksLogs();
                model2.date = DateTime.Now;
                model2.bookid = model.id;
                model2.ISBN = model.ISBN;
                model2.name = model.name;
                model2.cost = model.cost;
                model2.Status = "Book Added";

                //model2.category = model.Category;

                db.LR_BooksLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
      

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Uniqu(string ShortName, int? id)
        {
            if (id != null || id > 0)
            { return Json(!db.LR_Books.Any(x =>  x.ISBN == ShortName && x.id != id), JsonRequestBehavior.AllowGet); }
            else
            { return Json(!db.LR_Books.Any(x => x.ISBN == ShortName), JsonRequestBehavior.AllowGet); }
        }
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }

    
}