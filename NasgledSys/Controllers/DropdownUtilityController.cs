using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Globalization;

namespace NasgledSys.Controllers
{
    public class DropdownUtilityController : Controller
    {
        // GET: DropdownUtility
        private NasgledDBEntities db = new NasgledDBEntities();
        public JsonResult LoadLightingSatisfaction(Guid? LightingSatisfactionKey)
        {
            if (LightingSatisfactionKey == Guid.Empty || LightingSatisfactionKey == null)
            {
                var list = (from a in db.LightingSatisfactionFactor
                            where a.IsDelete == false
                            select new { a.PKey, a.FactorName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.LightingSatisfactionFactor
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.FactorName,
                                Selected = a.PKey == LightingSatisfactionKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadExistingControl(bool? id)
        {
            List<GenericListType> test = new List<GenericListType>();
           
                GenericListType a = new GenericListType();
                a.Text = "No";
                a.Value = false.ToString();
                a.Selected = "";
                test.Add(a);
            GenericListType b = new GenericListType();
            b.Text = "Yes";
            b.Value = true.ToString();
            b.Selected = "";
            test.Add(b);

            if (id == null)
            {
                var list = (from x in test select new { x.Text, x.Value, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string temp = id.ToString();
                var list = (from x in test select new { x.Text, x.Value, Selected = x.Value == temp ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult loadFixture(Guid? FixtureKey)
        {
            if (FixtureKey == Guid.Empty || FixtureKey == null)
            {
                var list = (from a in db.ProfileProduct
                            where a.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                            select new { a.FixtureKey, a.ProductName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.ProfileProduct
                            where a.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                            select new
                            {
                                a.FixtureKey,
                                a.ProductName,
                                Selected = a.FixtureKey == FixtureKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadCategory(Guid? CategoryKey)
        {
            if (CategoryKey == Guid.Empty || CategoryKey == null)
            {
                var list = (from a in db.ItemCategory where a.IsDelete==false
                           
                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.ItemCategory where a.IsDelete==false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == CategoryKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadIncentiveType(Guid? IncentiveTypeKey)
        {
            if (IncentiveTypeKey == Guid.Empty || IncentiveTypeKey == null)
            {
                var list = (from a in db.IncentiveType
                            where a.IsDelete == false

                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.IncentiveType
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == IncentiveTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult LoadIncentiveMaxType(Guid? IncentiveMaxTypeKey)
        {
            if (IncentiveMaxTypeKey == Guid.Empty || IncentiveMaxTypeKey == null)
            {
                var list = (from a in db.IncentiveMaxType
                            where a.IsDelete == false

                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.IncentiveMaxType
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == IncentiveMaxTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadSubcategory(Guid? CategoryKey)
        {
            if (CategoryKey == Guid.Empty || CategoryKey == null)
            {
                var list = (from a in db.ItemSubcategory
                            where a.IsDelete == false

                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.ItemSubcategory
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == CategoryKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadProductType(Guid? CategoryKey)
        {
            if (CategoryKey == Guid.Empty || CategoryKey == null)
            {
                var list = (from a in db.ItemType
                            where a.IsDelete == false

                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.ItemType
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == CategoryKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadCatelogue(Guid? CategoryKey)
        {
            if (CategoryKey == Guid.Empty || CategoryKey == null)
            {
                var list = (from a in db.ItemCatelogue
                            where a.IsDelete == false

                            select new { a.PKey, a.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.ItemCatelogue
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.TypeName,
                                Selected = a.PKey == CategoryKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadManufacturer(Guid? CategoryKey)
        {
            if (CategoryKey == Guid.Empty || CategoryKey == null)
            {
                var list = (from a in db.Manufacturer
                            where a.IsDelete == false

                            select new { a.PKey, a.ManufacturerName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from a in db.Manufacturer
                            where a.IsDelete == false
                            select new
                            {
                                a.PKey,
                                a.ManufacturerName,
                                Selected = a.PKey == CategoryKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult loadArea(Guid? AreaKey)
        {
            if (AreaKey == Guid.Empty || AreaKey == null)
            {
                var list = (from city in db.Area 
                            where city.IsDelete == false && city.ProjectKey==GlobalClass.Project.ProjectKey
                            select new { city.AreaKey, city.AreaName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.Area
                            where city.IsDelete == false && city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.AreaKey,
                                city.AreaName,
                                Selected = city.AreaKey == AreaKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadMonth(int? Month)
        {
            List<GenericListType> test = new List<GenericListType>();
            for (int x = 1; x < 13; x++)
            {
                GenericListType a = new GenericListType();
                a.Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")";
                a.Value = x.ToString();
                a.Selected = "";
                test.Add(a);
            }
            if (Month == null)
            {
                var list = (from city in test select new { city.Text, city.Value, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string temp = Month.ToString();
                var list = (from city in test select new { city.Text, city.Value, Selected = city.Value == temp ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadPrevious(int? Year)
        {
            List<GenericListType> test = new List<GenericListType>();
            for (int x = (DateTime.Today.Year - 50); x < (DateTime.Today.Year + 10); x++)
            {
                GenericListType a = new GenericListType();
                a.Text = x.ToString();
                a.Value = x.ToString();
                a.Selected = "";
                test.Add(a);
            }
            if (Year == null)
            {
                var list = (from city in test select new { city.Text, city.Value, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string temp = Year.ToString();
                var list = (from city in test select new { city.Text, city.Value, Selected = city.Value == temp ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadYear(int? Year)
        {
            List<GenericListType> test = new List<GenericListType>();
            for (int x = DateTime.Today.Year; x < (DateTime.Today.Year+40); x++)
            {
                GenericListType a = new GenericListType();
                a.Text = x.ToString();
                a.Value = x.ToString();
                a.Selected = "";
                test.Add(a);
            }
            if (Year == null)
            {
                var list = (from city in test select new { city.Text, city.Value, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string temp = Year.ToString();
                var list = (from city in test select new { city.Text, city.Value, Selected = city.Value == temp ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadParentArea(Guid? ParentAreaKey)
        {
            if (ParentAreaKey == Guid.Empty || ParentAreaKey == null)
            {
                var list = (from city in db.Area
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new { city.AreaKey, city.AreaName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.Area
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.AreaKey,
                                city.AreaName,
                                Selected = city.AreaKey == ParentAreaKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadOperationSchedule(Guid? OperationScheduleKey)
        {
            if (OperationScheduleKey == Guid.Empty || OperationScheduleKey == null)
            {
                var list = (from city in db.OperatingSchedule where city.ProjectKey==GlobalClass.Project.ProjectKey
                            select new { city.PKey, city.OPName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.OperatingSchedule
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.PKey,
                                city.OPName,
                                Selected = city.PKey == OperationScheduleKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadNewRateSchedule(Guid? NewRateScheduleKey)
        {
            if (NewRateScheduleKey == Guid.Empty || NewRateScheduleKey==null)
            {
                var list = (from city in db.NewRateSchedule
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new { city.PKey, city.NRName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.NewRateSchedule
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.PKey,
                                city.NRName,
                                Selected = city.PKey == NewRateScheduleKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadCoolingSystem(Guid? CoolingSystemKey)
        {
            if (CoolingSystemKey == Guid.Empty ||CoolingSystemKey==null)
            {
                var list = (from city in db.CoolingSystem
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new { city.CoolingSystemKey, city.SystemName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.CoolingSystem
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.CoolingSystemKey,
                                city.SystemName,
                                Selected = city.CoolingSystemKey == CoolingSystemKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadHeatingSystem(Guid? HeatingSystemKey)
        {
            if (HeatingSystemKey == Guid.Empty|| HeatingSystemKey==null)
            {
                var list = (from city in db.HeatingSystem
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new { city.HeatingSystemKey, city.SystemName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.HeatingSystem
                            where city.ProjectKey == GlobalClass.Project.ProjectKey
                            select new
                            {
                                city.HeatingSystemKey,
                                city.SystemName,
                                Selected = city.HeatingSystemKey == HeatingSystemKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadHeatingSystemType(Guid? SystemTypeKey)
        {
            if (SystemTypeKey == Guid.Empty || SystemTypeKey == null)
            {
                var list = (from city in db.HeatingSystemType
                            where city.IsDelete == false
                            select new { city.PKey, city.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.HeatingSystemType
                            where city.IsDelete == false
                            select new
                            {
                                city.PKey,
                                city.TypeName,
                                Selected = city.PKey == SystemTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadCoolingSystemType(Guid? SystemTypeKey)
        {
            if (SystemTypeKey == Guid.Empty || SystemTypeKey==null)
            {
                var list = (from city in db.CoolingSystemType
                            where city.IsDelete == false
                            select new { city.PKey, city.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.CoolingSystemType
                            where city.IsDelete == false
                            select new
                            {
                                city.PKey,
                                city.TypeName,
                                Selected = city.PKey == SystemTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadFuelType(Guid? FuelTypeKey)
        {
            if (FuelTypeKey == Guid.Empty || FuelTypeKey == null)
            {
                var list = (from city in db.FuelType
                            where city.IsDelete == false
                            select new { PKey=city.PKey, TypeName= city.TypeName+"("+city.Unit+")", Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.FuelType
                            where city.IsDelete == false
                            select new
                            {
                                PKey = city.PKey,
                                TypeName = city.TypeName + "(" + city.Unit + ")",
                                Selected = city.PKey == FuelTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadEfficiencyTypeH(Guid? EfficiencyTypeKey)
        {
            if (EfficiencyTypeKey == Guid.Empty || EfficiencyTypeKey == null)
            {
                var list = (from city in db.HeatingEfficiencyType
                            where city.IsDelete == false
                            select new { PKey = city.PKey, TypeName = city.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.HeatingEfficiencyType
                            where city.IsDelete == false
                            select new
                            {
                                PKey = city.PKey,
                                TypeName = city.TypeName,
                                Selected = city.PKey == EfficiencyTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadEfficiencyTypeC(Guid? EfficiencyTypeKey)
        {
            if (EfficiencyTypeKey == Guid.Empty || EfficiencyTypeKey == null)
            {
                var list = (from city in db.CoolingEfficientyType
                            where city.IsDelete == false
                            select new { PKey = city.PKey, TypeName = city.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.CoolingEfficientyType
                            where city.IsDelete == false
                            select new
                            {
                                PKey = city.PKey,
                                TypeName = city.TypeName,
                                Selected = city.PKey == EfficiencyTypeKey ? "selected" : ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadProjectStatus(Guid? ProjectStatusKey)
        {
            if (ProjectStatusKey == Guid.Empty || ProjectStatusKey==null)
            {
                var list = (from city in db.ProjectStatus select new { city.ProjectStatusKey, city.TypeName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from city in db.ProjectStatus select new {
                    city.ProjectStatusKey,
                    city.TypeName,
                    Selected = city.ProjectStatusKey == ProjectStatusKey ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
           
        }
        public JsonResult loadPrimaryContact(Guid? PrimaryContactKey)
        {
            if (PrimaryContactKey == Guid.Empty || PrimaryContactKey==null)
            {
                var list = (from x in db.ClientContact
                            where x.IsActive == true && x.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                            select new
                            {
                                Value = x.ContactKey,
                                Text = x.FirstName + " " + x.LastName + " " + x.Email,
                                Selected = ""
                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from x in db.ClientContact where x.IsActive == true && x.ProfileKey == GlobalClass.ProfileUser.ProfileKey select new {
                    Value=x.ContactKey,
                    Text=x.FirstName+" "+x.LastName + " " + x.Email,
                    Selected = x.ContactKey == PrimaryContactKey ? "selected" : ""
                }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult loadCityDropDown_ToCreate(int? CityKey)
        {
            if (CityKey == -1) {
                var list = (from city in db.CityList select new { city.CityKey, city.CityName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from x in db.CityList select new { x.CityKey, x.CityName, Selected = x.CityKey == CityKey ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadStateDropDown_ToCreate(int? StateKey)
        {
            if (StateKey == -1) {
                var list = (from state in db.StateList select new { state.PKey, state.StateName, Selected = "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from x in db.StateList select new { x.PKey, x.StateName, Selected = x.PKey == StateKey ? "selected" : "" }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetClient(string query)
        {
            var users = (from u in db.ClientContact
                         

                         where ((u.FirstName.ToUpper()+u.LastName.ToUpper()).Contains(query.ToUpper())) && u.ProfileKey==GlobalClass.ProfileUser.ProfileKey
                         orderby u.FirstName
                         select new
                         {
                             text = u.FirstName + " " + u.LastName + " " + " " + u.Email + " ",
                             id = u.ContactKey

                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetCity(int stateId)
        {
            
            List<SelectListItem> districtNames = new List<SelectListItem>();            
                
                List<CityList> city = db.CityList.Where(x => x.StateCode == stateId && x.IsDelete==false).ToList();
                city.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.CityName, Value = x.CityKey.ToString() });
                });
           
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult GetStateName(string query)
        {
            var users = (from u in db.StateList

                         where (u.StateCode.ToUpper().Contains(query.ToUpper()) || u.StateName.ToUpper().Contains(query.ToUpper()))

                         && u.IsDelete == false
                         orderby u.StateName
                         select new
                         {
                             label = u.StateName + "( " + u.StateCode + " )",
                             value = u.PKey

                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCityName(string query, int StateKey)
        {
              var users = (from u in db.CityList

                         where u.CityName.ToUpper().Contains(query.ToUpper())
                         && u.IsDelete == false
                         && u.StateCode == StateKey

                         orderby u.CityName

                         select new
                         {
                             label = u.CityName,
                             value = u.CityKey
                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZipcode(string query, int CityKey)
        {
            var users = (from u in db.ZIPList

                         where u.ZIP.ToUpper().Contains(query.ToUpper()) && u.CityList.IsDelete == false
                         && u.IsDelete == false && u.CityKey == CityKey
                         orderby u.ZIP
                         select new
                         {
                             label = u.ZIP,
                             value = u.ZipKey

                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
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