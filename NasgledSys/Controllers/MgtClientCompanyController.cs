using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using NLog;
using System.Web.Security;
using System.Web.Mail;
using System.Threading.Tasks;
using NasgledSys.EM;
using NasgledSys.Validation;
using System.Data.Entity;

using NasgledSys.DAL;
using System.ComponentModel.DataAnnotations;
namespace NasgledSys.Controllers
{
    public class MgtClientCompanyController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: MgtClientCompany
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        private ValidateClientCompany val = new ValidateClientCompany();
        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                // logger.Info("MgtClientCompany Create() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);
                try
                {
                    ViewBag.mess = "";
                    ViewBag.IndustryTypeKey = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName");

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
        public ActionResult Create(ClientCompany modelClass)
        {
            if (GlobalClass.MasterSession)
            {


                //logger.Info("MgtClientCompany HttpPost Create() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);
                ViewBag.mess = "";

                if (ModelState.IsValid)
                {

                    try
                    {
                        modelClass = validate.ValidateClient(modelClass);

                        ClientCompany pf = new ClientCompany();
                        pf.ClientCompanyKey = Guid.NewGuid();

                        pf.CompanyName = modelClass.CompanyName;
                        pf.Description = modelClass.Description;
                        pf.IndustryTypeKey = modelClass.IndustryTypeKey;
                        pf.ProfileKey = modelClass.ProfileKey;
                        db.ClientCompany.Add(pf);
                        db.SaveChanges();
                        GlobalClass.CCompany = pf;
                        Session["GlobalMessege"] = "Company is Created";
                        return RedirectToAction("Index", "MgtProject");
                    }
                    catch (Exception e)
                    {

                        ViewBag.mess = e.Message.ToString();
                    }
                }
                ViewBag.IndustryTypeKey = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName");

                return View(modelClass);
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        //# to Index
        public ActionResult IndexClientCompany()
        {
            if (GlobalClass.MasterSession)
            {
                //logger.Info("MgtClientCompany IndexClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);
                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public JsonResult GetClientCompanyListData() {
            var list = (from cc in db.ClientCompany
                        select new
                        {
                            cc.ClientCompanyKey,
                            cc.CompanyName,
                            cc.Address
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //# to create
        public ActionResult CreateClientCompany()
        {
            if (GlobalClass.MasterSession)
            {
                // logger.Info("MgtClientCompany CreateClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);
                ViewBag.IndustryTyepeSelectList = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName");
                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult CreateClientCompany(ClientCompanyViewModel viewModel)
        {
            if (GlobalClass.MasterSession)
            {
                //logger.Info("MgtClientCompany HttpPost CreateClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);

                if (ModelState.IsValid)
                {
                    if (viewModel.BillingContactEMail != "")
                    {
                        EmailAddressAttribute e = new EmailAddressAttribute();
                        if (!e.IsValid(viewModel.BillingContactEMail))
                        {
                            TempData["mess"] = "Email address has an invalid format. Could not save.";
                            return RedirectToAction("CreateClientCompany");
                        }
                    }
                    try
                    {
                        ClientCompany model = val.ConvertVIewModelToEntityMOdelFOrCreate(viewModel);                      
                        db.ClientCompany.Add(model);
                        db.SaveChanges();
                        TempData["mess"] = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        return View("Error", new HandleErrorInfo(e, "Home", "Index"));
                    }
                }
                else
                {
                    TempData["mess"] = "Mandatory fileds(field with *) are empty, Could not save.";
                }
                return RedirectToAction("CreateClientCompany");
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public JsonResult loadCityDropDown_ToCreate()
        {
            var list = (from city in db.CityList select new { city.CityKey, city.CityName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadStateDropDown_ToCreate()
        {
            var list = (from state in db.StateList select new { state.PKey, state.StateName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //# to edit
        public ActionResult EditClientCompany(Guid clientCompanyKey)
        {
            if (GlobalClass.MasterSession)
            {
                // logger.Info("MgtClientCompany EditClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);

                ClientCompany clientCompany = db.ClientCompany.Where(cc => cc.ClientCompanyKey == clientCompanyKey).FirstOrDefault();

                //# convert to viewmodel
                ClientCompanyViewModel clientCompanyViewModel = new ClientCompanyViewModel();
                clientCompanyViewModel.ClientCompanyKey = clientCompany.ClientCompanyKey;
                clientCompanyViewModel.CompanyName = clientCompany.CompanyName;
                clientCompanyViewModel.Description = clientCompany.Description;

                clientCompanyViewModel.NoOfSalesPerson = clientCompany.NoOfSalesPerson;
                clientCompanyViewModel.OfficePhone = clientCompany.OfficePhone;
                clientCompanyViewModel.Address = clientCompany.Address;
                clientCompanyViewModel.Address2 = clientCompany.Address2;
                clientCompanyViewModel.Zipcode = clientCompany.Zipcode;
                clientCompanyViewModel.BillingContactName = clientCompany.BillingContactName;
                clientCompanyViewModel.BillingContactEMail = clientCompany.BillingContactEMail;
                if (clientCompany.IsAddressSameAsOffice == true)
                {
                    clientCompanyViewModel.IsAddressSameAsOffice = true;
                }
                else
                {
                    clientCompanyViewModel.IsAddressSameAsOffice = false;
                }
                clientCompanyViewModel.ProposalIntro = clientCompany.ProposalIntro;
                clientCompanyViewModel.ProposalTeam = clientCompany.ProposalTeam;
                clientCompanyViewModel.ProposalLegal = clientCompany.ProposalLegal;
                clientCompanyViewModel.ProposalDisclaimer = clientCompany.ProposalDisclaimer;
                clientCompanyViewModel.ProposalReference = clientCompany.ProposalReference;
                clientCompanyViewModel.EstimateFooter = clientCompany.EstimateFooter;
                clientCompanyViewModel.MarkupOrMargin = clientCompany.MarkupOrMargin;
                clientCompanyViewModel.MarkupOrMarginPercentage = clientCompany.MarkupOrMarginPercentage;
                ViewBag.IndustryTyepeSelectList = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName", clientCompany.IndustryTypeKey);
                return View(clientCompanyViewModel);
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult EditClientCompany(ClientCompanyViewModel viewModel)
        {
            if (GlobalClass.MasterSession)
            {
                //logger.Info("MgtClientCompany HttpPost EditClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (viewModel.BillingContactEMail != "" || viewModel.BillingContactEMail != "N/A")
                        {
                            EmailAddressAttribute e = new EmailAddressAttribute();
                            if (!e.IsValid(viewModel.BillingContactEMail))
                            {
                                TempData["mess"] = "Email address has an invalid format. Could not update.";
                                return RedirectToAction("EditClientCompany", new { clientCompanyKey = viewModel.ClientCompanyKey });
                            }
                        }

                        ClientCompany model = db.ClientCompany.Where(cc => cc.ClientCompanyKey == viewModel.ClientCompanyKey).FirstOrDefault();
                        model = val.ConvertVIewModelToEntityMOdelFOrEdit(model,viewModel);
                        db.SaveChanges();
                        TempData["mess"] = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        return View("Error", new HandleErrorInfo(e, "Home", "Index"));
                    }
                }
                else
                {
                    TempData["mess"] = "Mandatory fileds(field with *) are empty, Could not update.";
                }
                return RedirectToAction("EditClientCompany", new { clientCompanyKey = viewModel.ClientCompanyKey });
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public JsonResult loadCityDropDown_ToEdit(Guid clientCompanyKey)
        {
            ClientCompany clientCompany = db.ClientCompany.Where(cc => cc.ClientCompanyKey == clientCompanyKey).FirstOrDefault();
            var list = (from city in db.CityList
                        select new
                        {
                            city.CityKey,
                            city.CityName,
                            Selected = city.CityKey == clientCompany.CityKey ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadStateDropDown_ToEdit(Guid clientCompanyKey)
        {
            ClientCompany clientCompany = db.ClientCompany.Where(cc => cc.ClientCompanyKey == clientCompanyKey).FirstOrDefault();
            var list = (from state in db.StateList
                        select new
                        { state.PKey,
                            state.StateName,
                            Selected = state.PKey == clientCompany.StateKey ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //# to view
        public ActionResult ViewClientCompany(Guid clientCompanyKey)
        {
            if (GlobalClass.MasterSession)
            {
                //logger.Info("MgtClientCompany ViewClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);

                ClientCompany clientCompany = db.ClientCompany.Where(cc => cc.ClientCompanyKey == clientCompanyKey).FirstOrDefault();

                //# convert to viewmodel
                ClientCompanyViewModel clientCompanyViewModel = new ClientCompanyViewModel();
                clientCompanyViewModel.ClientCompanyKey = clientCompany.ClientCompanyKey;
                clientCompanyViewModel.CompanyName = clientCompany.CompanyName;
                clientCompanyViewModel.Description = clientCompany.Description;

                clientCompanyViewModel.NoOfSalesPerson = clientCompany.NoOfSalesPerson;
                clientCompanyViewModel.OfficePhone = clientCompany.OfficePhone;
                clientCompanyViewModel.Address = clientCompany.Address;
                clientCompanyViewModel.Address2 = clientCompany.Address2;
                clientCompanyViewModel.Zipcode = clientCompany.Zipcode;
                clientCompanyViewModel.BillingContactName = clientCompany.BillingContactName;
                clientCompanyViewModel.BillingContactEMail = clientCompany.BillingContactEMail;
                if (clientCompany.IsAddressSameAsOffice == true)
                {
                    clientCompanyViewModel.IsAddressSameAsOffice = true;
                }
                else
                {
                    clientCompanyViewModel.IsAddressSameAsOffice = false;
                }
                clientCompanyViewModel.ProposalIntro = clientCompany.ProposalIntro;
                clientCompanyViewModel.ProposalTeam = clientCompany.ProposalTeam;
                clientCompanyViewModel.ProposalLegal = clientCompany.ProposalLegal;
                clientCompanyViewModel.ProposalDisclaimer = clientCompany.ProposalDisclaimer;
                clientCompanyViewModel.ProposalReference = clientCompany.ProposalReference;
                clientCompanyViewModel.EstimateFooter = clientCompany.EstimateFooter;
                clientCompanyViewModel.MarkupOrMargin = clientCompany.MarkupOrMargin;
                clientCompanyViewModel.MarkupOrMarginPercentage = clientCompany.MarkupOrMarginPercentage;
                ViewBag.IndustryTyepeSelectList = new SelectList(db.IndustryType.Where(x => x.IndustryKey == clientCompany.IndustryTypeKey), "IndustryKey", "TypeName", clientCompany.IndustryTypeKey);
                ViewBag.CityListSelectList = new SelectList(db.CityList.Where(x => x.CityKey == clientCompany.CityKey), "CityKey", "CityName", clientCompany.CityKey);
                ViewBag.StateListSelectList = new SelectList(db.StateList.Where(x => x.PKey == clientCompany.StateKey), "PKey", "StateName", clientCompany.CityKey);

                return View(clientCompanyViewModel);
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