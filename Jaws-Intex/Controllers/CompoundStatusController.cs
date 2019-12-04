using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jaws_Intex.DAL;
using Jaws_Intex.Models;

namespace Jaws_Intex.Controllers
{
    public class CompoundStatusController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: CompoundStatus
        public ActionResult Index()
        {
            var compoundStatuses = db.CompoundStatuses.Include(c => c.Status);
            return View(compoundStatuses.ToList());
        }

        // GET: CompoundStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompoundStatus compoundStatus = db.CompoundStatuses.Find(id);
            if (compoundStatus == null)
            {
                return HttpNotFound();
            }
            return View(compoundStatus);
        }

        // GET: CompoundStatus/Create
        public ActionResult Create(int? CompoundId)
        {
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName");
            return View();
        }

        // POST: CompoundStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompoundId,StatusId,StatusDate")] CompoundStatus compoundStatus)
        {
            if (ModelState.IsValid)
            {
                db.CompoundStatuses.Add(compoundStatus);
                db.SaveChanges();
                return RedirectToAction("Details", new { Controller = "Compounds", Id = compoundStatus.CompoundId });
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", compoundStatus.StatusId);
            return View(compoundStatus);
        }

        // GET: CompoundStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompoundStatus compoundStatus = db.CompoundStatuses.Find(id);
            if (compoundStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", compoundStatus.StatusId);
            return View(compoundStatus);
        }

        // POST: CompoundStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompoundId,StatusId,StatusDate")] CompoundStatus compoundStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compoundStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", compoundStatus.StatusId);
            return View(compoundStatus);
        }

        // GET: CompoundStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: CompoundStatus/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompoundStatus compoundStatus = db.CompoundStatuses.Find(id);
            db.CompoundStatuses.Remove(compoundStatus);
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
