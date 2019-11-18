using Library_Management_System.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<LR_Category> cat = db.LR_Category.ToList();
            ViewBag.cat = new SelectList(cat, "id", "name");


            return View();
        }
       
        [HttpPost]
        public ActionResult Index(LR_Category model, FormCollection from)
        {
            if (db.LR_Category.Any(p => p.name == model.name))
            {
                ModelState.AddModelError("name", "Category Name already exists.");
            }

            if (ModelState.IsValid)
            {
                

                model.date = DateTime.Now;
                db.LR_Category.Add(model);
                db.SaveChanges();
                SaveCategoryLogs(model);
                return RedirectToAction("catlist");

            }
            else
            {
                List<LR_Category> cat = db.LR_Category.ToList();
                ViewBag.cat = new SelectList(cat, "id", "name", model.parent);
                return View(model);
            }

        }

        //Logs

        public void SaveCategoryLogs(LR_Category model)
        {
            try
            {
                LR_BookCategoryLogs model2 = new LR_BookCategoryLogs();
                model2.date = DateTime.Now;
                model2.cat_id = model.id;
                model2.name = model.name;
                model2.parent = model.parent;
                model2.Activity = "Category Added";
               
                db.LR_BookCategoryLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        public ActionResult catlist()
        {
            List<LR_Category> list = db.LR_Category.ToList();
            return View(list);
        }

        public ActionResult AddCategory()
        {
            List<LR_Category> cat = db.LR_Category.ToList();
            ViewBag.cat = new SelectList(cat, "id", "name");

            List<LR_Books> book = db.LR_Books.ToList();
            ViewBag.book = new SelectList(book, "id", "name");

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(LR_BookCategory model, FormCollection form)
        {
            if(ModelState.IsValid)
            {
                model.book_id = Convert.ToInt32(form["BookId"]);
                string CAT = form["CatId"];
                List<string> CATList = CAT.Split(',').ToList();
                model.date = DateTime.Now;
               
                foreach (string CatId in CATList)
                {
                    int categoryid = Convert.ToInt32(CatId);
                    var chkbook = db.LR_BookCategory.Where(x => x.book_id == model.book_id && x.category_id == categoryid).ToList();
                    if (chkbook.Count <= 0 )

                    {
                        model.category_id = Convert.ToInt32(CatId);
                        db.LR_BookCategory.Add(model);
                        SaveAddCategoryLogs(model);
                        db.SaveChanges();
                    }


                }

            }
            else
            {
                ViewBag.Error = "Something went wrong";
            }
            return RedirectToAction("ViewCat");
        }
        public void SaveAddCategoryLogs(LR_BookCategory model)
        {
            try
            {
                LR_BookAssignCaTLogs model2 = new LR_BookAssignCaTLogs();
                model2.date = DateTime.Now;
                model2.book_id = model.id; 
                model2.category = model.category_id;
                model2.book = model.book_id;
                model2.Activity = "Category Assigned";
                db.LR_BookAssignCaTLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult ViewCat()
        {
            List<LR_BookCategory> list = db.LR_BookCategory.ToList();
            return View(list);
        }





        [HttpPost]
        public JsonResult Index2(string keyword, int courseId)
        {
            List<LR_Books> list = new List<LR_Books>();

            if (Session["userList"] == null)
            {
                UserSession();
            }
            list = (List<LR_Books>)Session["userList"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult UserList()
        {

            if (Session["bookList"] == null)
            {
                UserSession();
            }

            var list = Session["bookList"];
            //var result = list.Select(n => n.UserName).ToArray();

            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            var list = db.LR_Books.Select(x=> new { name = x.name, id = x.id }).ToList();
           
            Session["bookList"] = list;

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Category cat = db.LR_Category.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_Category cat)
        {
            if (ModelState.IsValid)
            {
                cat.date = DateTime.Now;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                EditCategoryLogs(cat);
                return RedirectToAction("Edit");
            }
            return View(cat);
        }
        //Edit Category Logs

        public void EditCategoryLogs(LR_Category model)
        {
            try
            {
                LR_BookCategoryLogs model2 = new LR_BookCategoryLogs();
                model2.date = DateTime.Now;
                model2.cat_id = model.id;
                model2.name = model.name;
                model2.parent = model.parent;
                model2.Activity = "Edited";

                db.LR_BookCategoryLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //
        //public ActionResult Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LR_Category cat = db.LR_Category.Find(id);
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
        //    LR_Category cat = db.LR_Category.Find(id);
        //    db.LR_Category.Remove(cat);
        //    db.SaveChanges();
        //    return RedirectToAction("catlist");
        //}

        //************************************************Delete Category*******************************************//
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Category book = db.LR_Category.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
               
                book.Activity = "Deactive";
                db.Entry(book).State = EntityState.Modified;
                int i = db.SaveChanges();
                CatDeletelogs(book);
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
                return RedirectToAction("catlist");
            }
        }


        private void CatDeletelogs(LR_Category model)
        {
            try
            {
                LR_BookCategoryLogs model2 = new LR_BookCategoryLogs();
                model2.date = DateTime.Now;
                model2.cat_id = model.id;
                model2.name = model.name;
                model2.parent = model.parent;
                model2.Activity = "Deleted";
                db.LR_BookCategoryLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Category book = db.LR_Category.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }





        [HttpPost]
        public JsonResult Index1(string keyword, int courseId)
        {
            List<LR_Category> list = new List<LR_Category>();

            if (Session["userList"] == null)
            {
                UserSession();
            }
            list = (List<LR_Category>)Session["userList"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList1()
        {

            if (Session["CATList"] == null)
            {
                UserSession1();
            }

            var list = Session["CATList"];
            //var result = list.Select(n => n.UserName).ToArray();


            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession1()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            var list = db.LR_Category.Select(x => new { name = x.name, id = x.id }).ToList();

            Session["CATList"] = list;

        }


        public ActionResult EditViewCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_BookCategory cat = db.LR_BookCategory.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditViewCat(LR_BookCategory cat)
        {
            if (ModelState.IsValid)
            {
                cat.date = DateTime.Now;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit");
            }
            return View(cat);
        }

        //public ActionResult DeleteViewCat(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LR_BookCategory cat = db.LR_BookCategory.Find(id);
        //    if (cat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cat);
        //}
        //[HttpPost, ActionName("DeleteViewCat")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmedCat(int id)
        //{
        //    LR_BookCategory cat = db.LR_BookCategory.Find(id);
        //    db.LR_BookCategory.Remove(cat);
        //    db.SaveChanges();
        //    return RedirectToAction("ViewCat");
        //}

        //******************************

        public ActionResult DeleteViewCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_BookCategory book = db.LR_BookCategory.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {

                book.Activity = "Deactive";
                db.Entry(book).State = EntityState.Modified;
                int i = db.SaveChanges();
                DeleteViewlogs(book);
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
                return RedirectToAction("ViewCat");
            }
        }


        private void DeleteViewlogs(LR_BookCategory model)
        {
            try
            {
                LR_BookAssignCaTLogs model2 = new LR_BookAssignCaTLogs();
                model2.date = DateTime.Now;
                model2.book_id = model.id;
                model2.book = model.book_id;
                model2.category = model.category_id;
                model2.Activity = "Deleted";
                db.LR_BookAssignCaTLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult DetailsViewCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_BookCategory book = db.LR_BookCategory.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }




    }
}