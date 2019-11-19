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
    public class LR_AssignRolestoUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LR_AssignRolestoUsers
        public ActionResult Index()
        {
            var lR_AssignRolestoUsers = db.LR_AssignRolestoUsers.Include(l => l.category);
            return View(lR_AssignRolestoUsers.ToList());
        }

        // GET: LR_AssignRolestoUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers lR_AssignRolestoUsers = db.LR_AssignRolestoUsers.Find(id);
            if (lR_AssignRolestoUsers == null)
            {
                return HttpNotFound();
            }
            return View(lR_AssignRolestoUsers);
        }

        // GET: LR_AssignRolestoUsers/Create
        public ActionResult Create()
        {
            ViewBag.usercategory_id = new SelectList(db.LR_UserCategory, "id", "name");
            return View();
        }

        // POST: LR_AssignRolestoUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user,StudentName,usercategory_id,priority,date")] LR_AssignRolestoUsers lR_AssignRolestoUsers)
        {
            if (ModelState.IsValid)
            {
                db.LR_AssignRolestoUsers.Add(lR_AssignRolestoUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usercategory_id = new SelectList(db.LR_UserCategory, "id", "name", lR_AssignRolestoUsers.usercategory_id);
            return View(lR_AssignRolestoUsers);
        }

        // GET: LR_AssignRolestoUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers lR_AssignRolestoUsers = db.LR_AssignRolestoUsers.Find(id);
            if (lR_AssignRolestoUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.usercategory_id = new SelectList(db.LR_UserCategory, "id", "name", lR_AssignRolestoUsers.usercategory_id);
            return View(lR_AssignRolestoUsers);
        }

        // POST: LR_AssignRolestoUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user,StudentName,usercategory_id,priority,date")] LR_AssignRolestoUsers lR_AssignRolestoUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lR_AssignRolestoUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usercategory_id = new SelectList(db.LR_UserCategory, "id", "name", lR_AssignRolestoUsers.usercategory_id);
            return View(lR_AssignRolestoUsers);
        }

        // GET: LR_AssignRolestoUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_AssignRolestoUsers lR_AssignRolestoUsers = db.LR_AssignRolestoUsers.Find(id);
            if (lR_AssignRolestoUsers == null)
            {
                return HttpNotFound();
            }
            return View(lR_AssignRolestoUsers);
        }

        // POST: LR_AssignRolestoUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LR_AssignRolestoUsers lR_AssignRolestoUsers = db.LR_AssignRolestoUsers.Find(id);
            db.LR_AssignRolestoUsers.Remove(lR_AssignRolestoUsers);
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
