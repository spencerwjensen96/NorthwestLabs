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
    public class WorkOrdersController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: WorkOrders
        [Authorize]
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.WorkOrderStatus);
            return View(workOrders.ToList());
        }

        // GET: WorkOrders/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            var associatedCompounds = db.Compounds.SqlQuery("SELECT * FROM Compound WHERE OrderId = " + id).ToList<Compound>();
            workOrder.Compounds = associatedCompounds;
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        [Authorize]
        public ActionResult Create(int? ClientId)
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Company");
            ViewBag.StatusId = new SelectList(db.WorkOrderStatuses, "Id", "Status");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Order_Date,Quoted_Price,Amount_Due,StatusId,ClientId,Data_Report_URL,Summary_Report_URL,Discount_Percent,Notes,Confirmation_Sent")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                int newId = workOrder.OrderId;
                return RedirectToAction("Details/" + newId);
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Company", workOrder.ClientId);
            ViewBag.StatusId = new SelectList(db.WorkOrderStatuses, "Id", "Status", workOrder.StatusId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Company", workOrder.ClientId);
            ViewBag.StatusId = new SelectList(db.WorkOrderStatuses, "Id", "Status", workOrder.StatusId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Order_Date,Quoted_Price,Amount_Due,StatusId,ClientId,Data_Report_URL,Summary_Report_URL,Discount_Percent,Notes,Confirmation_Sent")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + workOrder.OrderId);
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Company", workOrder.ClientId);
            ViewBag.StatusId = new SelectList(db.WorkOrderStatuses, "Id", "Status", workOrder.StatusId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            
            var associatedCompounds = db.Compounds.SqlQuery("SELECT * FROM Compound WHERE OrderId = " + id).ToList<Compound>();
            workOrder.Compounds = associatedCompounds;

            foreach (var compound in workOrder.Compounds)
            {
                var associatedSamples = db.Samples.SqlQuery("SELECT * FROM Sample WHERE CompoundId = " + compound.CompoundId).ToList<Sample>();
                compound.Samples = associatedSamples;
                db.CompoundStatuses.SqlQuery("DELETE FROM Compound_Status WHERE CompoundId = " + compound.CompoundId);

            }

            workOrder.Compounds.ToList().ForEach(x => x.Samples.ToList().ForEach(y => db.Samples.Remove(y)));

            workOrder.Compounds.ToList().ForEach(x => db.Compounds.Remove(x));
            db.WorkOrders.Remove(workOrder);
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
