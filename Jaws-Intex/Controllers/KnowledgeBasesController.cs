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
    public class KnowledgeBasesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: KnowledgeBases
        [Authorize]
        public ActionResult Index()
        {
            return View(db.knowledgeBases.ToList());
        }

        // GET: KnowledgeBases/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.knowledgeBases.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: KnowledgeBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "KnowledgeBaseId,Title,Author,Date,Contents")] KnowledgeBase knowledgeBase)
        {
            if (ModelState.IsValid)
            {
                db.knowledgeBases.Add(knowledgeBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.knowledgeBases.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // POST: KnowledgeBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "KnowledgeBaseId,Title,Author,Date,Contents")] KnowledgeBase knowledgeBase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knowledgeBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.knowledgeBases.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // POST: KnowledgeBases/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KnowledgeBase knowledgeBase = db.knowledgeBases.Find(id);
            db.knowledgeBases.Remove(knowledgeBase);
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
