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
        private ManageProjectArea manage = new ManageProjectArea();
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
            var obj = from asset in db.AreaProduct where asset.AreaKey == id && asset.IsDelete==false select asset;
            if (obj.Count() > 0)
            {
                //foreach (var item in obj)
                //{
                //    ViewSubAreaList m = new ViewSubAreaList();
                //    m.AreaKey = item.AreaKey;
                //    m.SubAreaName = manage.GetAllAreaNamesForSubArea(item.AreaKey);
                //    m.AreaName = " in " + db.Area.FirstOrDefault(d => d.AreaKey == item.ParentAreaKey).AreaName;
                //    m.ProductCount = db.AreaProduct.Where(d => d.AreaKey == item.AreaKey).Count();
                //    list.Add(m);
                //}
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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