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
    public class SamplesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: Samples
        [Authorize]
        public ActionResult Index()
        {
            var samples = db.Samples.Include(s => s.Compound).Include(s => s.SampleTests);
            return View(samples.ToList());
        }

        // GET: Samples/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Sample sample = db.Samples.Find(id);
            //Get Cost for samples - Don't need all the joins but it works
            string query = $@"SELECT 
            S.SampleId,
            SUM(T.BasePrice + T.MaterialsCost + T.WageEstimate) AS Cost
            FROM Work_Order WO
            INNER JOIN Compound C ON C.OrderId = WO.OrderId
            INNER JOIN Sample S ON S.CompoundId = C.CompoundId
            INNER JOIN Sample_Test ST ON S.SampleId = ST.SampleId
            INNER JOIN Test T ON T.TestId = ST.TestId
            WHERE S.SampleId = {sample.SampleId}
            GROUP BY S.SampleId";

            string sampleTotalCost;
            var sampleTotalCostList = db.Database.SqlQuery<SampleCost>(query).ToList<SampleCost>();
                if (sampleTotalCostList.Count > 0)
            {
                sampleTotalCost = sampleTotalCostList[0].Cost.ToString();
            } else
            {
                sampleTotalCost = "No associated tests found";
            }
            ViewBag.TotalCost = sampleTotalCost;
            //Get associated tests with this sample
            var associatedSampleTests = db.SampleTests.SqlQuery("SELECT * FROM Sample_Test WHERE SampleId = " + id).ToList<SampleTest>();
            sample.SampleTests = associatedSampleTests;
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // GET: Samples/Create
        [Authorize]
        public ActionResult Create(int? CompoundId)
        {
            ViewBag.CompoundId = new SelectList(db.Compounds, "CompoundId", "Name");
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SampleId,CompoundId,Sequence,TestId,Concentration,Absorption,Quantity,TotalCost,StartDate,EndDate,Sample_File_URL")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Samples.Add(sample);
                db.SaveChanges();
                int newId = sample.SampleId;
                return RedirectToAction("Details/" + newId);
            }

            ViewBag.CompoundId = new SelectList(db.Compounds, "CompoundId", "Name", sample.CompoundId);
            return View(sample);
        }

        // GET: Samples/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompoundId = new SelectList(db.Compounds, "CompoundId", "Name", sample.CompoundId);
            return View(sample);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SampleId,CompoundId,Sequence,TestId,Concentration,Absorption,Quantity,TotalCost,StartDate,EndDate,Sample_File_URL")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + sample.SampleId);
            }
            ViewBag.CompoundId = new SelectList(db.Compounds, "CompoundId", "Name", sample.CompoundId);
            return View(sample);
        }

        // GET: Samples/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // POST: Samples/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sample sample = db.Samples.Find(id);
            db.Database.ExecuteSqlCommand("DELETE FROM Sample_Test WHERE SampleId = " + id);
            db.Samples.Remove(sample);
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
