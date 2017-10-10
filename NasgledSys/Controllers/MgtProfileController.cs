using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
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
using DataTables.Mvc;
using System.Linq.Dynamic;


namespace NasgledSys.Controllers
{
    public class MgtProfileController : Controller
    {
        // GET: MgtProfile
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        public ActionResult LoadEmail(Guid id)
        {
            JsonResult result = new JsonResult();
           
            result.Data = "--@gmail.com";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    List<SelectListItem> HireOutsiders = new List<SelectListItem>();
                    HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                    HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });
                    ViewBag.email = "--@gmail.com";
                    ViewBag.mess = "";
                    ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");
                    ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text");
                    ViewBag.AnnualSalesRevenue = new SelectList(db.AnnualSalesRevenueSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");
                    ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");

                    return View();
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
        public ActionResult Index(ProfileClass modelClass)
        {
            if (GlobalClass.MasterSession)
            {
                ViewBag.mess = "";
                List<SelectListItem> HireOutsiders = new List<SelectListItem>();
                HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });
                UserProfile imp = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
               
                if (ModelState.IsValid)
                {

                    try
                    {
                        modelClass = validate.ValidateProfileRegistration(modelClass);
                        UserProfile asset = EM_Profile.ConvertToEntity(modelClass);
                        asset.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                        asset.StateKey = db.CityList.Find(asset.CityKey).StateCode;
                        UserProfile pf = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                        pf.FirstName = asset.FirstName;
                        pf.LastName = asset.LastName;
                        pf.CompanyName = asset.CompanyName;
                        pf.JobTitle = asset.JobTitle;
                        pf.CityKey = asset.CityKey;
                        pf.StateKey = asset.StateKey;
                        pf.PhoneNo = asset.PhoneNo;
                        pf.PrimaryBusinessType = asset.PrimaryBusinessType;
                        pf.HireOutsideAuditor = asset.HireOutsideAuditor;
                        pf.AnnualSalesRevenue = asset.AnnualSalesRevenue;
                        pf.SalesReach = asset.SalesReach;
                        pf.DirectManufacture = asset.DirectManufacture;
                        pf.DirectDistributor = asset.DirectDistributor;

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Profile is Saved";
                        return RedirectToAction("Company");
                    }
                    catch (Exception e)
                    {

                        ViewBag.mess = e.Message.ToString();
                    }
                }
                ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", modelClass.PrimaryBusinessType);
                ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text", modelClass.HireOutsideAuditor);
                ViewBag.AnnualSalesRevenue = new SelectList(db.AnnualSalesRevenueSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", modelClass.AnnualSalesRevenue);
                ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", modelClass.SalesReach);

                return View(modelClass);
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult Company()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    return View();
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
        public ActionResult JoinCompany()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    return View();
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
        public ActionResult GetCompanyList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<ClientCompany> query = db.ClientCompany;
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                ClientCompanyKey = asset.ClientCompanyKey,
                CompanyName = asset.CompanyName,
                Description = asset.Description,
                IndustryType = asset.IndustryType.TypeName,
                OfficePhone = asset.OfficePhone,
                Address=asset.Address+" "+asset.Zipcode+" "+asset.CityList.CityName+" "+asset.StateList.StateName
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<ClientCompany> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<ClientCompany> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CompanyName.ToUpper().Contains(value.ToUpper()));
            }

            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "CompanyName asc" : orderByString);

            return query;

        }

        public ActionResult SendJoiningRequest(Guid id)
        {
            JsonResult result = new JsonResult();
            if (GlobalClass.MasterSession)
            {
                try
                {
                   
                    var asset = from x in db.ClientCompanyProfileRequest where x.ClientCompanyKey== id && x.ProfileKey==id select x;
                    if (asset == null)
                    {
                        ClientCompanyProfileRequest request = new ClientCompanyProfileRequest();
                        request.RequestID = Guid.NewGuid();
                        request.ClientCompanyKey = id;
                        request.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                        request.RequestDate = System.DateTime.Now;
                        request.IsConfirmed = false;
                        request.IsCancelled = false;
                        request.Remarks = "";
                        db.ClientCompanyProfileRequest.Add(request);
                        db.SaveChanges();
                        result.Data = "Request has been sent Successfully.";
                    }
                    else
                    {
                        asset = from x in db.ClientCompanyProfileRequest where x.ClientCompanyKey == id && x.ProfileKey == id && x.IsConfirmed==false && x.IsCancelled == false select x;
                        if (asset == null)
                        {
                            asset = from x in db.ClientCompanyProfileRequest where x.ClientCompanyKey == id && x.ProfileKey == id && x.IsConfirmed == true && x.IsCancelled == false select x;
                            if (asset == null)
                            {
                                result.Data = "Request has been Accepted. Please check your Dashboard.";
                            }
                            else
                            {
                                result.Data = "Sorry Your Response has been Denied.";
                            }
                        }
                        else
                        {
                            result.Data = "You already have a request Pending. Please wait for response.";
                        }
                    }
                    
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
                catch (Exception e)
                {

                    result.Data = e.Message.ToString();
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult UpdateUserProfile()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    List<SelectListItem> HireOutsiders = new List<SelectListItem>();
                    HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                    HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });
                    UserProfile userProfile = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                    ProfileClass model = new ProfileClass();
                    model.ProfileKey = userProfile.ProfileKey;
                    model.FirstName = userProfile.FirstName;
                    model.LastName = userProfile.LastName;
                    model.Email = userProfile.Email;
                    model.CompanyName = userProfile.CompanyName;
                    model.Photo = userProfile.Photo;
                    model.JobTitle = userProfile.JobTitle;
                    model.PhoneNo = userProfile.PhoneNo;
                    model.PrimaryBusinessType = userProfile.PrimaryBusinessType;
                    model.HireOutsideAuditor = userProfile.HireOutsideAuditor;
                    model.AnnualSalesRevenue = userProfile.AnnualSalesRevenue;
                    model.SalesReach = userProfile.SalesReach;
                    model.DirectManufacture = userProfile.DirectManufacture;
                    model.DirectDistributor = userProfile.DirectDistributor;
                    model.CityKey = userProfile.CityKey;
                    model.FileType = userProfile.FileType;
                    model.FileName = userProfile.FileName;
                    ViewBag.RoleKeys = new SelectList(db.UserRole, "RoleKey", "RoleName", userProfile.RoleKey);

                    ViewBag.AnnualSalesRevenues = new SelectList(db.AnnualSalesRevenueSetup, "TypeName", "TypeName", userProfile.AnnualSalesRevenue);
                    ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", userProfile.PrimaryBusinessType);
                    ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text", userProfile.HireOutsideAuditor);
                    ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", userProfile.SalesReach);
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

        [HttpPost]
        public ActionResult UpdateUserProfile(ProfileClass model, HttpPostedFileBase Photo)
        {
            if (GlobalClass.MasterSession)
            {
                List<SelectListItem> HireOutsiders = new List<SelectListItem>();
                HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });
                try
                {
                    if (ModelState.IsValid)
                    {                       
                       
                        UserProfile userProfile = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                        if (userProfile == null)
                        {
                            userProfile = new UserProfile();
                        }
                        userProfile.FirstName = model.FirstName;
                        userProfile.LastName = model.LastName;
                        userProfile.Email = model.Email;
                        userProfile.CompanyName = model.CompanyName;
                        userProfile.RoleKey = model.RoleKey;
                        userProfile.CityKey = model.CityKey;
                        if (string.IsNullOrEmpty(model.AnnualSalesRevenue)) model.AnnualSalesRevenue = "n/a";
                        if (string.IsNullOrEmpty(model.HireOutsideAuditor)) model.HireOutsideAuditor = "No";
                        if (string.IsNullOrEmpty(model.SalesReach)) model.SalesReach = "0";
                        if (string.IsNullOrEmpty(model.DirectManufacture)) model.DirectManufacture = "n/a";
                        if (string.IsNullOrEmpty(model.DirectDistributor)) model.DirectDistributor = "n/a";
                      
                        userProfile.AnnualSalesRevenue = model.AnnualSalesRevenue;
                        userProfile.JobTitle = model.JobTitle;
                        userProfile.PrimaryBusinessType = model.PrimaryBusinessType;
                        userProfile.HireOutsideAuditor = model.HireOutsideAuditor;                      
                        userProfile.SalesReach = model.SalesReach;
                        userProfile.DirectManufacture = model.DirectManufacture;
                        userProfile.DirectDistributor = model.DirectDistributor;
                      
                        if (Photo != null && Photo.ContentLength > 0)
                        {
                            userProfile.FileType = Photo.ContentType;
                            userProfile.FileName = Photo.FileName;
                            byte[] imgBinaryData = new byte[Photo.ContentLength];
                            int readresult = Photo.InputStream.Read(imgBinaryData, 0, Photo.ContentLength);
                            userProfile.Photo = imgBinaryData;
                        }
                      
                        db.SaveChanges();
                        TempData["mess"] = "User Profile is successfully updated.";
                        return RedirectToAction("UpdateUserProfile");
                    }
                    ViewBag.AnnualSalesRevenues = new SelectList(db.AnnualSalesRevenueSetup, "TypeName", "TypeName", model.AnnualSalesRevenue);
                    ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", model.PrimaryBusinessType);
                    ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text", model.HireOutsideAuditor);
                    ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName", model.SalesReach);
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

        public ActionResult ChangeUsernamePassword()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                   
                    UserProfile userProfile = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                    UsernameClass model = new UsernameClass();
                    model.ProfileKey = userProfile.ProfileKey;
                    model.FirstName = userProfile.FirstName;
                    model.LastName = userProfile.LastName;
                    model.Username = userProfile.Username;
                    model.Password = userProfile.Password;
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

        [HttpPost]
        public ActionResult ChangeUsernamePassword(UsernameClass model)
        {
            if (GlobalClass.MasterSession)
            {
              
                try
                {
                    if (ModelState.IsValid)
                    {

                        UserProfile userProfile = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                       
                        userProfile.FirstName = model.FirstName;
                        userProfile.LastName = model.LastName;
                        userProfile.Username = model.Username;
                        userProfile.Password = model.Password;
                     
                        db.SaveChanges();
                        TempData["mess"] = "User Access is successfully updated.";
                        return RedirectToAction("ChangeUsernamePassword");
                    }
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
        public JsonResult GetCityList()
        {
            UserProfile userProfile = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
            var cityList = (from city in db.CityList

                            orderby city.CityName
                            select new
                            {
                                Text = city.CityName + " , " + city.StateList.StateName,
                                Value = city.CityKey.ToString(),
                                Selected = city.CityKey == userProfile.CityKey ? "selected" : ""
                            }
                            ).ToList();
            return Json(cityList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityListFCr()
        {

            var cityList = (from city in db.CityList

                            orderby city.CityName
                            select new
                            {
                                Text = city.CityName + "," + city.StateList.StateName,
                                Value = city.CityKey.ToString()
                            }
                            ).ToList();
            return Json(cityList, JsonRequestBehavior.AllowGet);
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