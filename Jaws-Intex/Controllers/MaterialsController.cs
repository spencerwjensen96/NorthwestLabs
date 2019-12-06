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
    public class MaterialsController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: Materials
        public ActionResult Index()
        {
            //Get Test Information along with scheduled tests for this week
            string query = $@"
            SELECT T.TestId, T.TestName, CAST(T.QtyInStock AS varchar(50)) AS QtyInStock, CAST(T1.NumTestsScheduled AS varchar(50)) AS NumTestsScheduled
            FROM Test T
            FULL OUTER JOIN (SELECT ST.TestId, SUM(ST.TestId) AS NumTestsScheduled
            FROM Sample_Test ST
            LEFT JOIN Sample S ON S.SampleId = ST.SampleId
            FULL OUTER JOIN Compound C ON C.CompoundId = S.CompoundId
            WHERE C.Due_Date BETWEEN GETDATE() AND GETDATE() + 7
            AND S.EndDate IS NULL
            GROUP BY ST.TestId) AS T1 ON T1.TestId = T.TestId
            ";
            var TestMaterialsList = db.Database.SqlQuery<TestMaterials>(query).ToList<TestMaterials>();
            ViewBag.TestMaterials = TestMaterialsList;

            return View();
        }

        public ActionResult OrderTests(int? id)
        {
            return View(id);
        }
    }
}