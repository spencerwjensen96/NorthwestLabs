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
                compoundStatus.StatusName = null;
                //db.CompoundStatuses.SqlQuery("INSERT INTO Compound_Status (StatusId, CompoundId, StatusDate) VALUES ('" + compoundStatus.StatusId + "', '" + compoundStatus.CompoundId + "', '" + compoundStatus.StatusDate + "')").S;
                db.CompoundStatuses.Add(compoundStatus);
               var associatedClient = db.Clients.SqlQuery(@"SELECT TOP 1 Client.* FROM Compound CD
                    INNER JOIN Work_Order WO ON WO.OrderId = CD.OrderId
                    INNER JOIN Client ON Client.ClientId = WO.ClientId
                    WHERE CD.CompoundId = " + compoundStatus.CompoundId).ToList<Client>()[0];
                var associatedCompound = db.Compounds.SqlQuery("SELECT TOP 1 * FROM Compound WHERE CompoundId = " + compoundStatus.CompoundId).ToList<Compound>()[0];
                SendCompoundUpdatedEmail(associatedClient, associatedCompound);
                db.SaveChanges();
                return RedirectToAction("Details", new { Controller = "Compounds", Id = compoundStatus.CompoundId });
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", compoundStatus.StatusId);
            return View(compoundStatus);
        }

        public void SendCompoundUpdatedEmail(Client client, Compound compound)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            msg.Subject = "Compound Updated";
            msg.Body = "Your compound " + compound.Name + ", has been updated.";
            msg.From = new MailAddress("smtpserverforis403@gmail.com");
            msg.To.Add(client.Email_1);
            if (client.Email_2 != null)
            {
                msg.To.Add(client.Email_2);
            }
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
