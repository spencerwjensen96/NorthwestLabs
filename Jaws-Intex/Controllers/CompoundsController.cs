﻿using System;
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
    public class CompoundsController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();


        // GET: Compounds
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Compounds.ToList());
        }

        // GET: Compounds/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            //Get associated samples and compound statuses
            var associatedSamples = db.Samples.SqlQuery("SELECT * FROM Sample WHERE CompoundId = " + id).ToList<Sample>();
            var associatedCompoundStatuses = db.CompoundStatuses.SqlQuery("SELECT * FROM Compound_Status INNER JOIN Status ON Status.StatusId = Compound_Status.StatusId WHERE CompoundId = " + id).ToList<CompoundStatus>();
            compound.CompoundStatuses = associatedCompoundStatuses;
            compound.Samples = associatedSamples;
            return View(compound);
        }

        // GET: Compounds/Create
        [Authorize]
        public ActionResult Create(int? OrderId)
        {
            return View();
        }

        // POST: Compounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompoundId,LT,OrderId,Name,Quantity,Date_Arrived,Received_By,Appearance,Reported_Weight,Molecular_Mass,Max_Tolerated_Dose,Due_Date")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                db.Compounds.Add(compound);
                db.SaveChanges();
                int newId = compound.CompoundId;
                return RedirectToAction("Details/" + newId);
            }

            return View(compound);
        }

        // GET: Compounds/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }

        // POST: Compounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompoundId,LT,OrderId,Name,Quantity,Date_Arrived,Received_By,Appearance,Reported_Weight,Molecular_Mass,Max_Tolerated_Dose,Due_Date")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + compound.CompoundId);
            }
            return View(compound);
        }

        // GET: Compounds/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }

        // POST: Compounds/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Deleting will have to go through all associated lists and delete them
        public ActionResult DeleteConfirmed(int id)
        {
            Compound compound = db.Compounds.Find(id);
            var associatedSamples = db.Samples.SqlQuery("SELECT * FROM Sample WHERE CompoundId = " + id).ToList<Sample>();
            associatedSamples.ForEach(sample => db.Database.ExecuteSqlCommand("DELETE FROM Sample_Test WHERE SampleId = " + sample.SampleId));
            compound.Samples = associatedSamples;
            compound.Samples.ToList().ForEach(x => db.Samples.Remove(x));

            db.CompoundStatuses.SqlQuery("DELETE FROM Compound_Status WHERE CompoundId = " + id);
            db.Compounds.Remove(compound);
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
