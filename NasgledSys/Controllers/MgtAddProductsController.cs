using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Web.Security;
using System.Web.Mail;
using System.Threading.Tasks;
using NasgledSys.EM;
using NasgledSys.Validation;
using System.Globalization;
using NasgledSys.DAL;
using DataTables.Mvc;
using System.Linq.Dynamic;
namespace NasgledSys.Controllers
{
    public class MgtAddProductsController : Controller
    {
        // GET: MgtAddProducts
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area model = db.Area.Find(id);
                    ViewBag.heading = manage.GetAllAreaNamesForSubArea(id);
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "MgtProject", "Created"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public JsonResult GetProductList(Guid id)
        {
            List<ViewSubAreaList> list = new List<ViewSubAreaList>();
            var obj = from asset in db.AreaProduct where asset.AreaKey == id && asset.IsDelete == false select new {
                asset.ExistingProductName,
                asset.Count,
                asset.Description,
                asset.ProductKey
            };
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = "";                  
                    Area model = db.Area.Find(id);
                    AddProductClass obj = new AddProductClass();
                    obj.AreaKey = model.AreaKey;
                    obj.FixtureKey = Guid.Empty;
                    obj.AreaObj = model;
                    return View(obj);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult Create(AddProductClass model,string Save,string Add,string AddDetails)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.Count == null) model.Count = 1;
                        if (string.IsNullOrEmpty(model.Description))
                        {
                            model.Description ="--";
                        }                   
                       
                        AreaProduct obj = new AreaProduct();
                        obj.ProductKey = Guid.NewGuid();
                        obj.FixtureKey = model.FixtureKey;
                        obj.AreaKey = model.AreaKey;
                        obj.Count = model.Count;
                        obj.Description = model.Description;
                        obj.ExistingProductName = manage.GetAllFixtureNamesForExistingKey(model.FixtureKey);
                      
                        obj.IsDelete = false;
                        db.AreaProduct.Add(obj);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Product was successfully Created.";
                       if(!string.IsNullOrEmpty(Save))
                        return RedirectToAction("Index", "MgtAddProducts", new { id = obj.AreaKey });
                        else if (!string.IsNullOrEmpty(Add))
                            return RedirectToAction("Create", "MgtAddProducts", new { id = obj.AreaKey });
                        else if (!string.IsNullOrEmpty(AddDetails))
                            return RedirectToAction("Edit", "MgtAddProducts", new { id = obj.ProductKey });
                        else 
                            return RedirectToAction("Index", "MgtAddProducts", new { id = obj.AreaKey });
                    }
                    return View(model);
                }
                catch (Exception e)
                {
                    Session["GlobalMessege"] = e.Message.ToString();
                    return View(model);
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
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