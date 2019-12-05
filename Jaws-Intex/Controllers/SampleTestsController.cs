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
    public class SampleTestsController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: SampleTests
        public ActionResult Index(int? SampleId)
        {
            var sampleTests = db.SampleTests.Include(s => s.Sample).Include(s => s.Test);
            return View(sampleTests.ToList());
        }

        // GET: SampleTests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleTest sampleTest = db.SampleTests.Find(id);
            if (sampleTest == null)
            {
                return HttpNotFound();
            }
            return View(sampleTest);
        }

        // GET: SampleTests/Create
        public ActionResult Create(int? SampleId)
        {
            ViewBag.SampleId = new SelectList(db.Samples, "SampleId", "SampleId");
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName");
            return View();
        }

        // POST: SampleTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SampleId,TestId,Notes,Active")] SampleTest sampleTest)
        {
            if (ModelState.IsValid)
            {
                db.SampleTests.Add(sampleTest);
                db.SaveChanges();
                return Redirect("/Samples/Details/" + sampleTest.SampleId);
            }

            ViewBag.SampleId = new SelectList(db.Samples, "SampleId", "SampleId", sampleTest.SampleId);
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName", sampleTest.TestId);
            return View(sampleTest);
        }

        // GET: SampleTests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleTest sampleTest = db.SampleTests.Find(id);
            if (sampleTest == null)
            {
                return HttpNotFound();
            }
            ViewBag.SampleId = new SelectList(db.Samples, "SampleId", "SampleId", sampleTest.SampleId);
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName", sampleTest.TestId);
            return View(sampleTest);
        }

        // POST: SampleTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SampleId,TestId,Notes,Active")] SampleTest sampleTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sampleTest).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Samples/Details/" + sampleTest.SampleId);
            }
            ViewBag.SampleId = new SelectList(db.Samples, "SampleId", "SampleId", sampleTest.SampleId);
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName", sampleTest.TestId);
            return View(sampleTest);
        }

        // GET: SampleTests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleTest sampleTest = db.SampleTests.Find(id);
            if (sampleTest == null)
            {
                return HttpNotFound();
            }
            return View(sampleTest);
        }

        // POST: SampleTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SampleTest sampleTest = db.SampleTests.Find(id);
            db.SampleTests.Remove(sampleTest);
            db.SaveChanges();
            return Redirect("/Samples/Details/" + sampleTest.SampleId);
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
