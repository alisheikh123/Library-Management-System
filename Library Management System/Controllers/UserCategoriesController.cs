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
    public class UserCategoriesController : Controller
    {
        // GET: UserCategories
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<LR_UserCategory> list = db.LR_UserCategory.ToList();
            return View(list);
        }

        //Create Users Categories
        public ActionResult UserCategory()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult UserCategory(LR_UserCategory model, FormCollection from)
        {
           
                model.date = DateTime.Now;
                db.LR_UserCategory.Add(model);
                db.SaveChanges();
               return RedirectToAction("Index");
        }

        // For Assining Roles
        public ActionResult ViewUserRoles()
        {
            List<LR_AssignRolestoUsers> list = db.LR_AssignRolestoUsers.ToList();
            return View(list);
        }

        //Create Users Categories
        public ActionResult AssignRoles()
        {
            List<LR_UserCategory> cat = db.LR_UserCategory.ToList();
            ViewBag.cat = new SelectList(cat, "id", "name","");
            return View();
        }
        [HttpPost]
        public ActionResult AssignRoles(LR_AssignRolestoUsers model, FormCollection from)
        {
            LR_UserCategory model2 = new LR_UserCategory();
            model2.priority = model.priority;
            model.date = DateTime.Now;
            db.LR_AssignRolestoUsers.Add(model);
            db.SaveChanges();
            SaveUserLogs(model);
            return RedirectToAction("ViewUserRoles");
        }
        //Save Logs
        public void SaveUserLogs(LR_AssignRolestoUsers model)
        {
            try
            {
                LR_UserCategoryLogs model2 = new LR_UserCategoryLogs();
                model2.date = DateTime.Now;
                model2.user = model.user;
                model2.priority = model.priority;
                model2.category = model.usercategory_id;
                model2.status = "Role Assigned";

                //model2.category = model.Category;

                db.LR_UserCategoryLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //Actions for user categories

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_UserCategory cat = db.LR_UserCategory.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_UserCategory cat)
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

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_UserCategory cat = db.LR_UserCategory.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LR_UserCategory cat = db.LR_UserCategory.Find(id);
            db.LR_UserCategory.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_UserCategory book = db.LR_UserCategory.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        // Actions for User Roles

        public ActionResult EditRoles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers cat = db.LR_AssignRolestoUsers.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoles(LR_AssignRolestoUsers cat)
        {
            if (ModelState.IsValid)
            {
                cat.date = DateTime.Now;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ViewUserRoles");
            }
            return View(cat);
        }

        public ActionResult DeleteRoles(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers cat = db.LR_AssignRolestoUsers.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }
        [HttpPost, ActionName("DeleteRoles")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedRoles(int id)
        {
            LR_AssignRolestoUsers cat = db.LR_AssignRolestoUsers.Find(id);
            db.LR_AssignRolestoUsers.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("ViewUserRoles");
        }



        public ActionResult DetailsRoles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers book = db.LR_AssignRolestoUsers.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }



    }
}