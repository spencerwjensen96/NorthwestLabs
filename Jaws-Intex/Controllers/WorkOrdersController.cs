using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Jaws_Intex.DAL;
using Jaws_Intex.Models;

namespace Jaws_Intex.Controllers
{
    public class WorkOrdersController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        public void SendWorkOrderUpdatedEmail(Client client, WorkOrder workOrder)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            msg.Subject = "Work Order Updated";
            msg.Body = "Your work order #" + workOrder.OrderId + ", has been updated.";
            msg.From = new MailAddress("smtpserverforis403@gmail.com");
            msg.To.Add(client.Email_1);
            if (client.Email_2 != null) { msg.To.Add(client.Email_2); }
            msg.IsBodyHtml = true;
            mailClient.Host = "smtp.gmail.com";
            System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("smtpserverforis403@gmail.com", "cactuscooler");
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = basicauthenticationinfo;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.Send(msg);
        }

        // GET: WorkOrders
        [Authorize]
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.WorkOrderStatus);
            return View(workOrders.ToList());
        }


        [Authorize]
        public ActionResult CreateDataReport(int? id)
        {
            db.Database.ExecuteSqlCommand("UPDATE Work_Order SET Data_Report_URL = 'Report' WHERE OrderId = " + id);
            db.SaveChanges();
            return new RedirectResult("/WorkOrders/Details/" + id);
        }


        [Authorize]
        public ActionResult UploadSummaryReport(int? id)
        {
            db.Database.ExecuteSqlCommand("UPDATE Work_Order SET Summary_Report_URL = 'Report' WHERE OrderId = " + id);
            db.SaveChanges();
            return new RedirectResult("/WorkOrders/Details/" + id);
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

            string query = $@"SELECT   
            C.LT,
            S.SampleId, S.Sequence,
            SUM(T.BasePrice + T.MaterialsCost + T.WageEstimate) AS Cost 
            FROM Work_Order WO 
            INNER JOIN Compound C ON C.OrderId = WO.OrderId 
            INNER JOIN Sample S ON S.CompoundId = C.CompoundId 
            INNER JOIN Sample_Test ST ON S.SampleId = ST.SampleId 
            INNER JOIN Test T ON T.TestId = ST.TestId 
            WHERE WO.OrderId = {workOrder.OrderId}
            GROUP BY S.SampleId, C.LT, S.Sequence";

            var sampleTotalCostList = db.Database.SqlQuery<SampleCost>(query).ToList<SampleCost>();
            ViewBag.SampleCosts = sampleTotalCostList;

            var chargesList = db.Charges.SqlQuery("SELECT * FROM Charge WHERE OrderId = " + workOrder.OrderId).ToList<Charge>();
            ViewBag.Charges = chargesList;

            decimal amountDue = 0;
            foreach(var item in sampleTotalCostList)
            {
                amountDue += item.Cost;
            }
            foreach (var item in chargesList)
            {
                amountDue += item.Cost;
            }
            ViewBag.AmountDue = amountDue;

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
        public ActionResult Create([Bind(Include = "OrderId,Order_Date,Quoted_Price,Amount_Paid,StatusId,ClientId,Data_Report_URL,Summary_Report_URL,Discount_Percent,Notes,Confirmation_Sent")] WorkOrder workOrder)
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
        public ActionResult Edit([Bind(Include = "OrderId,Order_Date,Quoted_Price,Amount_Paid,StatusId,ClientId,Data_Report_URL,Summary_Report_URL,Discount_Percent,Notes,Confirmation_Sent")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                var associatedClient = db.Clients.SqlQuery("SELECT TOP 1 * FROM Client WHERE ClientId = " + workOrder.ClientId).ToList<Client>()[0];
                SendWorkOrderUpdatedEmail(associatedClient, workOrder);
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
