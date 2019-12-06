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
    public class ChargesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: Charges
        [Authorize]
        public ActionResult Index()
        {
            var charges = db.Charges.Include(c => c.WorkOrder);
            return View(charges.ToList());
        }

        // GET: Charges/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // GET: Charges/Create
        [Authorize]
        public ActionResult Create(int? OrderId)
        {
            ViewBag.OrderId = new SelectList(db.WorkOrders, "OrderId", "OrderId");
            return View();
        }

        // POST: Charges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChargeId,OrderId,Cost,Notes")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Charges.Add(charge);
                db.SaveChanges();
                return Redirect("/WorkOrders/Details/" + charge.OrderId);
            }

            ViewBag.OrderId = new SelectList(db.WorkOrders, "OrderId", "OrderId", charge.OrderId);
            return View(charge);
        }

        // GET: Charges/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.WorkOrders, "OrderId", "OrderId", charge.OrderId);
            return View(charge);
        }

        // POST: Charges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChargeId,OrderId,Cost,Notes")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(charge).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/WorkOrders/Details/" + charge.OrderId);
            }
            ViewBag.OrderId = new SelectList(db.WorkOrders, "OrderId", "OrderId", charge.OrderId);
            return View(charge);
        }

        // GET: Charges/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // POST: Charges/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Charge charge = db.Charges.Find(id);
            db.Charges.Remove(charge);
            db.SaveChanges();
            return Redirect("/WorkOrders/Details/" + charge.OrderId);
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
