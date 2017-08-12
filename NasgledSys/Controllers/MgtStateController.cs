using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
namespace NasgledSys.Controllers
{
    public class MgtStateController : Controller
    {
        // GET: MgtState
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllState()
        {
            using (NasgledDBEntities db=new NasgledDBEntities()) {
                var state = db.StateList.Where(m=>m.IsDelete==false).OrderBy(m => m.StateName).ToList();
                return Json(new { data = state }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Save(long id)
        {
            using (NasgledDBEntities db=new NasgledDBEntities())
            {
                var v = db.StateList.Where(m => m.PKey == id).FirstOrDefault();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Save(StateList model)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (NasgledDBEntities db = new NasgledDBEntities())
                {
                    if (model.PKey == null)
                    {
                        db.StateList.Add(model);
                      
                    }
                    else
                    {
                        StateList obj = db.StateList.Find(model.PKey);
                        obj.StateCode = model.StateCode;
                        obj.StateName = model.StateName;
                        obj.IsDelete = model.IsDelete;
                        
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
            
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            bool status = false;

            using (NasgledDBEntities db = new NasgledDBEntities())
            {
                var v = db.StateList.Where(m => m.PKey == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
              
            }
           
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteStateList(long id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (NasgledDBEntities db = new NasgledDBEntities())
                {
                   
                        StateList obj = db.StateList.Find(id);                       
                        obj.IsDelete =false;

                   
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };

        }

    }
}