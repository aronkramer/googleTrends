using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillTracker.Models;

namespace BillTracker.Controllers
{
    public class EmailsSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmailsSettings
        public ActionResult Index()
        {
            return View(db.Emails.ToList());
        }

        // GET: EmailsSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // GET: EmailsSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailsSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmailAddress,GoogleTrends")] Emails emails)
        {
            if (ModelState.IsValid)
            {
                db.Emails.Add(emails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emails);
        }

        // GET: EmailsSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // POST: EmailsSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailAddress,GoogleTrends")] Emails emails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emails);
        }

        // GET: EmailsSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // POST: EmailsSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emails emails = db.Emails.Find(id);
            db.Emails.Remove(emails);
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
