using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class FinesController : Controller
    {
         ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fines
        public ActionResult Index()
        {
            return View(db.LR_Fine.ToList());
        }

        // GET: Fines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Fine fine = db.LR_Fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // GET: Fines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fineId,fine,dayLimit")] LR_Fine fine)
        {
            if (ModelState.IsValid)
            {
                //var chk = db.Fined.Where(x=>x.dayLimit == fine.dayLimit);
                //if(chk > 14)
                //{

                //}
                db.LR_Fine.Add(fine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fine);
        }

        // GET: Fines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Fine fine = db.LR_Fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // POST: Fines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fineId,fine,dayLimit")] LR_Fine fine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fine);
        }

        // GET: Fines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Fine fine = db.LR_Fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // POST: Fines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LR_Fine fine = db.LR_Fine.Find(id);
            db.LR_Fine.Remove(fine);
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
