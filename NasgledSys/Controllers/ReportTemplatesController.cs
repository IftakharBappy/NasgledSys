using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;

namespace NasgledSys.Controllers
{
    public class ReportTemplatesController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        // GET: ReportTemplates
        public ActionResult Index()
        {
            return View(db.ReportTemplate.OrderBy(m=>m.TLevel).ToList());
        }

       

        // GET: ReportTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateKey,FactorName,TLevel")] ReportTemplate reportTemplate)
        {
            if (ModelState.IsValid)
            {
                reportTemplate.TemplateKey = Guid.NewGuid();
                db.ReportTemplate.Add(reportTemplate);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(reportTemplate);
        }

        // GET: ReportTemplates/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportTemplate reportTemplate = db.ReportTemplate.Find(id);
            if (reportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(reportTemplate);
        }

        // POST: ReportTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateKey,FactorName,TLevel")] ReportTemplate reportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportTemplate);
        }

        // GET: ReportTemplates/Delete/5
        public ActionResult Delete(Guid? id)
        {
            ReportTemplate reportTemplate = db.ReportTemplate.Find(id);
            db.ReportTemplate.Remove(reportTemplate);
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
