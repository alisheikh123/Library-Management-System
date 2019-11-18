using System;
using Library_Management_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Library_Management_System.Controllers
{
    public class JournalNewsController : Controller
    {
        // GET: JournalNews
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="Journals", Value="2"},
                        new SelectListItem{ Text="NewsPaper", Value="3"},
                          new SelectListItem{ Text="Magazine", Value="4"},

                    };
            ViewBag.prioritylist = priority;
            List<LR_ArticleCategory> catname = db.ArticleCategory.ToList();
            ViewBag.cat = new SelectList(catname, "id", "catname");
            List<LR_JournalsNews> list = db.LR_JournalsNews.Where(x =>x.activity=="Active").ToList();
            return View(list);
            
        }
        //Add AddJournal
        public ActionResult AddJournal(int? artId)
        {
            if (artId == 0)
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
        public ActionResult AddJournal(LR_JournalsNews model, FormCollection from)
        {
            
            if (ModelState.IsValid)
            {
                model.Remaining_Quanity= model.Quantity ;
                model.date = DateTime.Now;
                model.activity = "Active";
                model.status = "Available";
                // model.ISBN = "xxxxx";
                model.article_category = "Journal";
                var chkauthor = db.LR_JournalsNews.Where(x => x.author_name == model.author_name).ToList();
                var chkname = db.LR_JournalsNews.Where(x => x.name == model.name).ToList();
                if (chkauthor.Count > 0 && chkname.Count>0)

                {
                    ViewBag.warning = "This book already exists.";
                    return View();
                }
                else
                {
                    db.LR_JournalsNews.Add(model);
                    db.SaveChanges();
                    SaveJournalLogs(model);
                }
            }

                return RedirectToAction("Index");
           
        }
        public void SaveJournalLogs(LR_JournalsNews model)
        {
            try
            {
                LR_JournalNewsLogs model2 = new LR_JournalNewsLogs();
                model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.name;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.article_category;
                model2.activity = "Added";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;

                //model2.category = model.Category;

                db.LR_JournalNewsLogs.Add(model2);
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
        public ActionResult AddNewsPaper(LR_JournalsNews model, FormCollection from)
        {

            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.article_category = "NewsPaper";
            var chkauthor = db.LR_JournalsNews.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_JournalsNews.Where(x => x.name == model.name).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "This book already exists.";
                return View();
            }
            else
            {
                db.LR_JournalsNews.Add(model);
                db.SaveChanges();
                SaveJournalLogs(model);
            }

            return RedirectToAction("Index");

        }

       
        [HttpPost]
        public ActionResult CreateNewBooks(string Role)
        {
            switch (Role)
            {
                //case "1":
                //    return RedirectToAction("AddBooks", new { artId = Role });
                //    ;
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

        //Add Magazines
        public ActionResult AddMagazines(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddMagazines(LR_JournalsNews model, FormCollection from)
        {
            if (ModelState.IsValid)
            {

                model.Remaining_Quanity = model.Quantity;
                model.date = DateTime.Now;
                model.activity = "Active";
                model.status = "Available";

                model.article_category = "Magazine";
                var chkmag = db.LR_Books.Where(x => x.name == model.name).ToList();
                var chkauthor = db.LR_JournalsNews.Where(x => x.author_name == model.author_name).ToList();
                var chkname = db.LR_JournalsNews.Where(x => x.name == model.name).ToList();
                if (chkauthor.Count > 0 && chkname.Count > 0)

                {
                    ViewBag.warning = "This book already exists.";
                    return View();
                }
                else
                {
                    db.LR_JournalsNews.Add(model);
                    db.SaveChanges();
                    SaveJournalLogs(model);

                }
            }
            else
            {
                return RedirectToAction("AddMagazines");
            }
            return RedirectToAction("Index");

        }

        //private void SaveMagazineLogs(LR_Books model)
        //{
        //    try
        //    {
        //        LR_BooksLogs model2 = new LR_BooksLogs();
        //        model2.date = DateTime.Now;
        //        model2.bookid = model.id;
        //        model2.ISBN = model.ISBN;
        //        model2.name = model.name;
        //        model2.cost = model.cost;
        //        model2.Status = "Index";

        //        //model2.category = model.Category;

        //        db.LR_BooksLogs.Add(model2);
        //        int i = db.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_JournalsNews cat = db.LR_JournalsNews.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_JournalsNews cat)
        {
            if (ModelState.IsValid)
            {

                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                SaveEditJournalLogs(cat);
                return RedirectToAction("Edit");
            }
            return View(cat);
        }

        public void SaveEditJournalLogs(LR_JournalsNews model)
        {
            try
            {
                LR_JournalNewsLogs model2 = new LR_JournalNewsLogs();
                model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.name;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.article_category;
                model2.activity = "Edited";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;
                db.LR_JournalNewsLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public ActionResult Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LR_JournalsNews cat = db.LR_JournalsNews.Find(id);
        //    if (cat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cat);
        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LR_JournalsNews cat = db.LR_JournalsNews.Find(id);
        //    db.LR_JournalsNews.Remove(cat);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_JournalsNews book = db.LR_JournalsNews.Find(id);
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
                JournalDeletelogs(book);
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


        private void JournalDeletelogs(LR_JournalsNews model)
        {
            try
            {
                LR_JournalNewsLogs model2 = new LR_JournalNewsLogs();
                 model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.name;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.article_category;
                model2.activity = "Deleted";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;

                db.LR_JournalNewsLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_JournalsNews book = db.LR_JournalsNews.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


    }
}