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
    public class AlliedMaterialController : Controller
    {
        // GET: AlliedMaterial
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="Thesis", Value="5"},
                        new SelectListItem{ Text="FYP Reports", Value="7"},
                          new SelectListItem{ Text="Pamphlets", Value="8"},
                          new SelectListItem{ Text="CD/Video Cassettes", Value="9"},
                          new SelectListItem{ Text="Novels", Value="6"},
                    };
            ViewBag.prioritylist = priority;
            List<LR_ArticleCategory> catname = db.ArticleCategory.ToList();
            ViewBag.cat = new SelectList(catname, "id", "catname");
            List<LR_AlliedMaterial> list = db.LR_AlliedMaterial.Where(x => x.activity=="Active").ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult CreateNewBooks(string Role)
        {
            switch (Role)
            {
                //case "1":
                //    return RedirectToAction("AddBooks", new { artId = Role });
                //    ;
                case "5":
                    return RedirectToAction("AddThesis", new { artId = Role });
                case "6":
                    return RedirectToAction("AddNovels", new { artId = Role });
                case "7":
                    return RedirectToAction("AddFYPReports", new { artId = Role });
                case "8":
                    return RedirectToAction("AddPamphlets", new { artId = Role });
                case "9":
                    return RedirectToAction("ADDCds", new { artId = Role });
                default:
                    break;
            }
            return View();
        }
        public ActionResult thesisView(string option, string search)
        {
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null && x.category=="Thesis").ToList());

        }
        public ActionResult fypview(string option, string search)
        {
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null && x.category == "FYP Reports").ToList());

        }
        public ActionResult cdview(string option, string search)
        {
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null && x.category == "CD/Video Cassettes").ToList());

        }
        public ActionResult novelview(string option, string search)
        {
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null && x.category == "Novel").ToList());

        }
        public ActionResult pamphlets(string option, string search)
        {
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null && x.category == "Pamphlets").ToList());

        }
        //Add Thesis

        public ActionResult AddThesis(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddThesis(LR_AlliedMaterial model, FormCollection from)
        {
            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.category = "Thesis";
            var chkauthor = db.LR_AlliedMaterial.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_AlliedMaterial.Where(x => x.title == model.title).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "Already exists.";
                return View();
            }
            else
            {
                db.LR_AlliedMaterial.Add(model);
                db.SaveChanges();
                SaveAlliedLogs(model);
            }

            return RedirectToAction("Index");

        }

        //Add Novels
        

        public ActionResult AddNovels(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddNovels(LR_AlliedMaterial model, FormCollection from)
        {
            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.category = "Novel";
            var chkauthor = db.LR_AlliedMaterial.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_AlliedMaterial.Where(x => x.title == model.title).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "Already exists.";
                return View();
            }
            else
            {
                db.LR_AlliedMaterial.Add(model);
                db.SaveChanges();
                SaveAlliedLogs(model);
            }

            return RedirectToAction("Index");

        }

        //Add FYP Reports

        public ActionResult AddFYPReports(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddFYPReports(LR_AlliedMaterial model, FormCollection from)
        {
            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.category = "FYP Reports";
            var chkauthor = db.LR_AlliedMaterial.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_AlliedMaterial.Where(x => x.title == model.title).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "Already exists.";
                return View();
            }
            else
            {
                db.LR_AlliedMaterial.Add(model);
                db.SaveChanges();
                SaveAlliedLogs(model);
            }

            return RedirectToAction("Index");

        }

        //Add pamphlets
        public ActionResult AddPamphlets(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult AddPamphlets(LR_AlliedMaterial model, FormCollection from)
        {
            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.category = "Pamphlets";
            var chkauthor = db.LR_AlliedMaterial.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_AlliedMaterial.Where(x => x.title == model.title).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "Already exists.";
                return View();
            }
            else
            {
                db.LR_AlliedMaterial.Add(model);
                db.SaveChanges();
                SaveAlliedLogs(model);
            }

            return RedirectToAction("Index");
        }
        //Add CDS
        public ActionResult ADDCds(int? artId)
        {
            ViewBag.artId = artId;
            return View();
        }
        [HttpPost]
        public ActionResult ADDCds(LR_AlliedMaterial model, FormCollection from)
        {
            model.Remaining_Quanity = model.Quantity;
            model.date = DateTime.Now;
            model.activity = "Active";
            model.status = "Available";
            model.category = "CD/Video Cassettes";
            var chkauthor = db.LR_AlliedMaterial.Where(x => x.author_name == model.author_name).ToList();
            var chkname = db.LR_AlliedMaterial.Where(x => x.title == model.title).ToList();
            if (chkauthor.Count > 0 && chkname.Count > 0)

            {
                ViewBag.warning = "Already exists.";
                return View();
            }
            else
            {
                db.LR_AlliedMaterial.Add(model);
                db.SaveChanges();
                SaveAlliedLogs(model);
            }

            return RedirectToAction("Index");
        }
        //Edit Ctaegories
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AlliedMaterial book = db.LR_AlliedMaterial.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_AlliedMaterial book)
        {
            if (ModelState.IsValid)
            {
                book.date = DateTime.Now;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                SaveEditAlliedLogs(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public void SaveEditAlliedLogs(LR_AlliedMaterial model)
        {
            try
            {
                LR_AlliedLogs model2 = new LR_AlliedLogs();
                model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.title;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.category;
                model2.activity = "Edited";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;
                db.LR_AlliedLogs.Add(model2);
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
            LR_AlliedMaterial book = db.LR_AlliedMaterial.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        //// Delete Books
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LR_AlliedMaterial book = db.LR_AlliedMaterial.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}

        //// POST: tblRequisitions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LR_AlliedMaterial book = db.LR_AlliedMaterial.Find(id);
        //    db.LR_AlliedMaterial.Remove(book);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AlliedMaterial book = db.LR_AlliedMaterial.Find(id);
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
                AlliedDeletelogs(book);
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


        private void AlliedDeletelogs(LR_AlliedMaterial model)
        {
            try
            {
                LR_AlliedLogs model2 = new LR_AlliedLogs();
                model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.title;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.category;
                model2.activity = "Deleted";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;

                db.LR_AlliedLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveAlliedLogs(LR_AlliedMaterial model)
        {
            try
            {
                LR_AlliedLogs model2 = new LR_AlliedLogs();
                model2.date = DateTime.Now;
                model2.Catid = model.id;
                model2.title = model.title;
                model2.author = model.author_name;
                model2.cost = model.cost;
                model2.category = model.category;
                model2.activity = "Added";
                model2.quantity = model.Quantity;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.Status = model.status;

                //model2.category = model.Category;

                db.LR_AlliedLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





    }
}