using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtCoolingEfficiencyTypeController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtCoolingEfficiencyType getAll = new MgtCoolingEfficiencyType();
        // GET: MgtCoolingEfficiencyType
        public ActionResult Index()
        {
            try
            {
                CoolingEfficiencyTypeClass obj = new CoolingEfficiencyTypeClass();
                obj.CoolingEfficiencyTypeList = new List<CoolingEfficiencyTypeClass>();
                obj.CoolingEfficiencyTypeList = getAll.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            return View();
        }
        public JsonResult Add(CoolingEfficiencyTypeClass CoolingEfficientyType)
        {
            return Json(getAll.Add(CoolingEfficientyType), JsonRequestBehavior.AllowGet);
        }
    }
}