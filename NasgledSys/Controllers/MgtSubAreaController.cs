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
    public class MgtSubAreaController : Controller
    {
        // GET: MgtSubArea
        private NasgledDBEntities db = new NasgledDBEntities();

        private ManageProjectArea manage = new ManageProjectArea();
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area model = db.Area.Find(id);
                    
                   return View(model);
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

        public JsonResult GetSubAreaList(Guid id)
        {
            var list = (from asset in db.Area where asset.ParentAreaKey==id

                        select new
                        {
                            SubAreaName = manage.GetAllAreaNamesForSubArea(asset.AreaKey),
                            AreaName = " in "+ db.Area.FirstOrDefault(m => m.AreaKey==asset.ParentAreaKey).AreaName,
                            ProductCount = db.AreaProduct.Where(m => m.AreaKey == asset.AreaKey).Count()
                        }).OrderBy(m => m.SubAreaName).ToList();

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