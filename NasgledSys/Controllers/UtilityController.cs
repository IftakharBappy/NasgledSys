using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using Newtonsoft.Json;

using System.Text;

namespace NasgledSys.Controllers
{
    public class UtilityController : Controller
    {
        // GET: Utility
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult LoadCityData(int SelectID)
        {
            JsonResult result = new JsonResult();
            CityClass obj = new CityClass();
            CityList m = db.CityList.Find(SelectID);
            obj.CityName = m.CityName;
            obj.StateCode = m.StateCode;
            obj.StateName = m.StateList.StateName;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadCoolingEfficiencyTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            CoolingEfficiencyTypeViewModel obj = new CoolingEfficiencyTypeViewModel();
            CoolingEfficientyType m = db.CoolingEfficientyType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;          
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult LoadCoolingSystemTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            CoolingSystemTypeViewModel obj = new CoolingSystemTypeViewModel();
            CoolingSystemType m = db.CoolingSystemType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadIndustryTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            IndustryTypeViewModel obj = new IndustryTypeViewModel();
            IndustryType m = db.IndustryType.Find(SelectID);
            obj.IndustryKey = m.IndustryKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadItemCategoryTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ItemCategoryViewModel obj = new ItemCategoryViewModel();
            ItemCategory m = db.ItemCategory.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult LoadFuelTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            FuelTypeViewModel obj = new FuelTypeViewModel();
            FuelType m = db.FuelType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            obj.Unit = m.Unit;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadHeatingEfficiencyTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            HeatingEfficiencyTypeViewModel obj = new HeatingEfficiencyTypeViewModel();
            HeatingEfficiencyType m = db.HeatingEfficiencyType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadHeatingSystemTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            HeatingSystemTypeViewModel obj = new HeatingSystemTypeViewModel();
            HeatingSystemType m = db.HeatingSystemType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadItemCatelogueData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ItemCatelogueViewModel obj = new ItemCatelogueViewModel();
            ItemCatelogue m = db.ItemCatelogue.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadItemSubcategoryData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ItemSubcategoryViewModel obj = new ItemSubcategoryViewModel();
            ItemSubcategory m = db.ItemSubcategory.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadItemTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ItemTypeViewModel obj = new ItemTypeViewModel();
            ItemType m = db.ItemType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        //JobFunction
        public ActionResult LoadJobFunctionData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            JobFunctionViewModel obj = new JobFunctionViewModel();
            JobFunction m = db.JobFunction.Find(SelectID);
            obj.JobFunctionKey = m.JobFunctionKey;
            obj.FunctionName = m.FunctionName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult LoadIncentiveMaxTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            IncentiveMaxTypeViewModel obj = new IncentiveMaxTypeViewModel();
            IncentiveMaxType m = db.IncentiveMaxType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadLightingSatisfactionFactorData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            LightingSatisfactionFactorViewModel obj = new LightingSatisfactionFactorViewModel();
            LightingSatisfactionFactor m = db.LightingSatisfactionFactor.Find(SelectID);
            obj.PKey = m.PKey;
            obj.FactorName = m.FactorName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadManufacturerData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ManufacturerViewModel obj = new ManufacturerViewModel();
            Manufacturer m = db.Manufacturer.Find(SelectID);
            obj.PKey = m.PKey;
            obj.ManufacturerName = m.ManufacturerName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult LoadIncentiveTypeData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            IncentiveTypeViewModel obj = new IncentiveTypeViewModel();
            IncentiveType m = db.IncentiveType.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult LoadProjectStatusData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            ProjectStatusViewModel obj = new ProjectStatusViewModel();
            ProjectStatus m = db.ProjectStatus.Find(SelectID);
            obj.ProjectStatusKey = m.ProjectStatusKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult LoadAnnualSalesRevenueSetupData(Guid SelectID)
        {
            JsonResult result = new JsonResult();
            AnnualSalesRevenueSetupViewModel obj = new AnnualSalesRevenueSetupViewModel();
            AnnualSalesRevenueSetup m = db.AnnualSalesRevenueSetup.Find(SelectID);
            obj.PKey = m.PKey;
            obj.TypeName = m.TypeName;
            obj.Description = m.Description;
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult CheckUsernameCreate(string user)
        {
            JsonResult result = new JsonResult();
            var temp = from x in db.StaffList where x.Username == user select x;
            if (temp.Count() > 0) result.Data = 99;
            else
                result.Data = 1;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult CheckProfileUsernameCreate(string user)
        {
            JsonResult result = new JsonResult();
            var temp = from x in db.UserProfile where x.Username == user select x;
            if (temp.Count() > 0) result.Data = 99;
            else
                result.Data = 1;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult CheckUsernameEdit(string user,Guid per)
        {
            JsonResult result = new JsonResult();
            var temp = from x in db.StaffList where x.Username == user && x.PersonnelKey!=per select x;
            if (temp.Count() > 0) result.Data = 99;
            else
                result.Data = 1;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetStateName(int StateCode)
        {
            JsonResult result = new JsonResult();

            result.Data = db.StateList.Find(StateCode).StateName;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetCityName(int CityKey)
        {
            JsonResult result = new JsonResult();

            result.Data = db.CityList.Find(CityKey).CityName;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
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