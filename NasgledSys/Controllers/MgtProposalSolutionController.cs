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
    public class MgtProposalSolutionController : Controller
    {
        // GET: MgtProposalSolution
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageProjectArea manage = new ManageProjectArea();
        
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    SolutionMainListClass model = new SolutionMainListClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.AreaList = new List<AreaProductViewClass>();
                    var area = from x in db.Area where x.IsDelete == false && x.ProjectKey == model.ProjectKey select x;
                    if (area.Count() > 0)
                    {
                        foreach(var item in area)
                        {
                            AreaProductViewClass obj = new AreaProductViewClass();
                            obj.AreaKey = item.AreaKey;
                            obj.AreaName= manage.GetAllAreaNamesForSubArea(item.AreaKey);
                            obj.ProductList = new List<ProposalSolutionListClass>();
                            var prod = from x in db.AreaProduct where x.AreaKey == item.AreaKey && x.IsDelete == false select x;
                            if (prod.Count() > 0)
                            {
                                foreach(var pitem in prod)
                                {
                                    ProposalSolutionListClass m = new ProposalSolutionListClass();
                                    m.ProductKey = pitem.ProductKey;
                                    m.ExistingProduct = pitem.ExistingProductName;
                                    m.ExistingCount = pitem.Count;
                                    m.ProposedProduct = pitem.ProductName==null?"":pitem.ProductName;
                                    m.ProposedCount= pitem.ReplaceQty == null ? "" : pitem.ReplaceQty.ToString();
                                    m.OperatingScheduleName =pitem.OperatingScheduleKey==null?"": pitem.OperatingSchedule.OPName;
                                    m.OperatingHours = pitem.OperatingScheduleKey == null ? "" : pitem.OperatingSchedule.AnnualOperationHour.ToString();
                                    m.SolutionLevel= pitem.SolutionLevel == null ? "" : pitem.SolutionLevel.ToString();
                                    m.IsAdd = pitem.ProductName == null ? true : false;
                                    obj.ProductList.Add(m);
                                }
                            }
                            model.AreaList.Add(obj);
                        }
                    }
                    else
                    {
                        AreaProductViewClass o = new AreaProductViewClass();
                        o.ProductList = new List<ProposalSolutionListClass>();
                        model.AreaList.Add(o);
                    }


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

        public ActionResult ChangeOperatingSchedule(Guid id,Guid id2)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id2);
                  
                    AreaProduct obj = db.AreaProduct.Find(id);
                    ChangeOperatingScheduleClass model = new ChangeOperatingScheduleClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.ProductKey = id;
                    model.OperatingScheduleKey = obj.OperatingScheduleKey;

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
        [HttpPost]
        public ActionResult ChangeOperatingSchedule(ChangeOperatingScheduleClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        AreaProduct obj = db.AreaProduct.Find(model.ProductKey);
                        obj.OperatingScheduleKey = model.OperatingScheduleKey;
                        db.SaveChanges();
                        return RedirectToAction("Index", "MgtProposalSolution",new { id=model.ProposalKey});
                    }
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

        public ActionResult Existing(Guid id, Guid id2)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProductSolution model = new ProductSolution();
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id2);

                    AreaProduct obj = db.AreaProduct.Find(id);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.ProductKey = id;
                    model.ExsistingName = obj.ExistingProductName;
                    model.ExistingList = new List<ReuseSolutionClass>();
                    var product = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.ProductName == null select x;
                    if (product.Count() > 0)
                    {
                        foreach (var item in product)
                        {
                            ReuseSolutionClass a = new ReuseSolutionClass();
                            a.ExsistingName = item.ExistingProductName;
                            a.ProposedName = item.ProductName;
                            a.ProductKey = item.ProductKey;
                            model.ExistingList.Add(a);
                        }
                    }
                 
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

        public ActionResult AddProduct(Guid id, Guid id2)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProductSolution model = new ProductSolution();
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id2);

                    AreaProduct obj = db.AreaProduct.Find(id);
                   
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.ProductKey = id;
                    model.ExsistingName = obj.ExistingProductName;
                    model.ExistingList = new List<ReuseSolutionClass>();
                    var product = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.SolutionName != null select x;
                    if (product.Count() > 0)
                    {
                        foreach (var item in product)
                        {
                            ReuseSolutionClass a = new ReuseSolutionClass();
                            a.ExsistingName = item.SolutionName;
                            a.ProposedName = item.ProductName;
                            a.ProductKey = item.ProductKey;
                            model.ExistingList.Add(a);
                        }
                    }
                    model.ItemKey = Guid.Empty;
                    model.CategoryKey = Guid.Empty;
                    model.SubcategoryKey = Guid.Empty;
                    model.ManufacturerKey = Guid.Empty;
                    model.CatelogueKey = Guid.Empty;
                    model.ProduictTypeKey = Guid.Empty;
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

        [HttpPost]
        public ActionResult AddProduct(ProductSolution model,string btnAdd,string btnsave)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (!string.IsNullOrEmpty(btnAdd) && model.ItemKey!=null)
                    {
                        ProfileProduct item = db.ProfileProduct.Find(model.ItemKey);
                        AreaProduct ap = db.AreaProduct.Find(model.ProductKey);
                        ap.ProductName = item.ProductName;
                        ap.ModelNo = item.ModelNo;
                        ap.WattPerProduct = item.Watt;
                        ap.ThermalEfficency = item.ThermalEfficiency;
                        ap.IsOn = 1;
                        ap.SolutionLevel = (from x in db.AreaProduct where x.ProjectKey == ap.ProjectKey && x.IsDelete == false && x.SolutionName != null select x).Count() + 1;
                        db.SaveChanges();
                        return RedirectToAction("Ratio", "MgtProposalSolution",new { id=model.ProductKey,id2=model.ProposalKey});
                    }
                    if (!string.IsNullOrEmpty(btnsave))
                    {
                       
                        return RedirectToAction("Ratio", "MgtProposalSolution", new { id = model.ProductKey, id2 = model.ProposalKey });
                    }
                    model.ExistingList = new List<ReuseSolutionClass>();
                    var product = from x in db.AreaProduct where x.ProjectKey == model.ProjectKey && x.IsDelete == false && x.SolutionName != null select x;
                    if (product.Count() > 0)
                    {
                        foreach (var item in product)
                        {
                            ReuseSolutionClass a = new ReuseSolutionClass();
                            a.ExsistingName = item.SolutionName;
                            a.ProposedName = item.ProductName;
                            a.ProductKey = item.ProductKey;
                            model.ExistingList.Add(a);
                        }
                    }
                    model.ItemKey = Guid.Empty;
                    model.CategoryKey = Guid.Empty;
                    model.SubcategoryKey = Guid.Empty;
                    model.ManufacturerKey = Guid.Empty;
                    model.CatelogueKey = Guid.Empty;
                    model.ProduictTypeKey = Guid.Empty;
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
        public ActionResult ProductDetails(Guid id, Guid id2)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProposalProductDetailClass model = new ProposalProductDetailClass();
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id2);

                    AreaProduct obj = db.AreaProduct.Find(id);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.ProductKey = id;
                    model.ApplyTo = manage.GetAllAreaNamesForSubArea(obj.AreaKey)+"::"+obj.ExistingProductName;
                    model.ProductName = obj.ProductName;
                    model.ModelNo = obj.ModelNo;
                    model.WattPerProduct = obj.WattPerProduct;
                    model.L70 = obj.L70;
                    model.ThermalEfficency = obj.ThermalEfficency;
                    model.CustomNotes = obj.CustomNotes;
                    model.ProductCost = obj.ProductCost;
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
        [HttpPost]

        public ActionResult ProductDetails(ProposalProductDetailClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                   

                    AreaProduct obj = db.AreaProduct.Find(model.ProductKey);
                    obj.SolutionName = model.SolutionName;
                    obj.IncentiveTypeKey = model.IncentiveTypeKey;
                    obj.IncentiveMaxTypeKey = model.IncentiveMaxTypeKey;
                    obj.IncentiveTypeKey = model.IncentiveTypeKey;
                    obj.ProductName = model.ProductName;
                    obj.ModelNo = model.ModelNo;
                    obj.WattPerProduct = model.WattPerProduct;
                    obj.L70 = model.L70;

                    if (model.ThermalEfficency == null) model.ThermalEfficency = 0;
                    if (model.ProductCost == 0) model.ProductCost = 0;
                    if (model.ShipingCost == 0) model.ShipingCost = 0;
                    if (model.MiscCost == 0) model.MiscCost = 0;
                    if (model.MarkupPercentage == 0) model.MarkupPercentage = 0;
                    if (model.IncentiveAmount == 0) model.IncentiveAmount = 0;
                    if (model.IncentiveMaxAmount == 0) model.IncentiveMaxAmount = 0;
                    if (model.InstallationTime == 0) model.InstallationTime = 0;
                    if (string.IsNullOrEmpty(model.CustomNotes)) model.CustomNotes = "n/a";

                    obj.IncentiveAmount = model.IncentiveAmount;
                    obj.IncentiveMaxAmount = model.IncentiveMaxAmount;
                    obj.ThermalEfficency = model.ThermalEfficency;
                    obj.CustomNotes = model.CustomNotes;
                    obj.ProductCost = model.ProductCost;
                    obj.MarkupPercentage = model.MarkupPercentage;
                    obj.InstallationTime = model.InstallationTime;
                    obj.InstallationCost = model.InstallationCost;
                    obj.ShipingCost = model.ShipingCost;
                    obj.MiscCost = model.MiscCost;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Product Added successfully.";
                    return RedirectToAction("Index", "MgtProposalSolution",new { id=model.ProposalKey});
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
        public ActionResult Ratio(Guid id, Guid id2)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProductSolution model = new ProductSolution();
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id2);

                    AreaProduct obj = db.AreaProduct.Find(id);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.ProductKey = id;
                    model.ExsistingName = obj.ExistingProductName;
                    model.newItem = obj.ProductName==null? "Custom Product": obj.ProductName;                   
                    model.forEvery = obj.ForEveryMainQty;
                    model.replaceWith =obj.ForEveryMainQty;
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
        [HttpPost]
        public ActionResult Ratio(ProductSolution model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            
                            AreaProduct ap = db.AreaProduct.Find(model.ProductKey);
                            ap.ForEveryMainQty =model.forEvery;
                            ap.ReplaceQty = model.replaceWith;
                           
                            db.SaveChanges();
                            return RedirectToAction("ProductDetails", "MgtProposalSolution", new { id = model.ProductKey, id2 = model.ProposalKey });
                        }
                        AreaProduct obj = db.AreaProduct.Find(model.ProductKey);
                        model.ExsistingName = obj.ExistingProductName;
                        model.newItem = obj.ProductName == null ? "Custom Product" : obj.ProductName;
                        return View(model);
                    }
                    catch (Exception e)
                    {
                        return View("Error", new HandleErrorInfo(e, "MgtProject", "Created"));
                    }
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

        public ActionResult AddExistingSolution(Guid id, Guid id2,Guid id3)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    

                    AreaProduct tosave = db.AreaProduct.Find(id);
                    AreaProduct old = db.AreaProduct.Find(id2);

                    tosave.ForEveryMainQty = old.ForEveryMainQty;
                    tosave.ReplaceQty = old.ReplaceQty;
                    tosave.SolutionName = old.SolutionName+"1";
                    tosave.IncentiveTypeKey = old.IncentiveTypeKey;
                    tosave.IncentiveAmount = old.IncentiveAmount;
                    tosave.IncentiveMaxTypeKey = old.IncentiveMaxTypeKey;
                    tosave.IncentiveMaxAmount = old.IncentiveMaxAmount;
                    tosave.ProductName = old.ProductName;
                    tosave.ModelNo = old.ModelNo;
                    tosave.WattPerProduct = old.WattPerProduct;
                    tosave.L70 = old.L70;
                    tosave.ThermalEfficency = old.ThermalEfficency;
                    tosave.CustomNotes = old.CustomNotes;
                    tosave.ProductCost = old.ProductCost;
                    tosave.MarkupPercentage = old.MarkupPercentage;
                    tosave.InstallationTime = old.InstallationTime;
                    tosave.InstallationCost = old.InstallationCost;
                    tosave.ShipingCost = old.ShipingCost;
                    tosave.MiscCost = old.MiscCost;
                    tosave.IsOn = old.IsOn;
                    tosave.SolutionLevel = (from x in db.AreaProduct where x.ProjectKey == old.ProjectKey && x.IsDelete == false && x.ProductName != null select x).Count()+1 ;
                   
                    db.SaveChanges();
                   
                    return RedirectToAction("Ratio", "MgtProposalSolution", new { id = tosave.ProductKey, id2 = id3 });
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

        public ActionResult AddItem(Guid id, Guid id2, Guid id3)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {


                    ProfileProduct item = db.ProfileProduct.Find(id);
                    AreaProduct ap = db.AreaProduct.Find(id2);
                    ap.ProductName = item.ProductName;
                    ap.ModelNo = item.ModelNo;
                    ap.WattPerProduct = item.Watt;
                    ap.ThermalEfficency = item.ThermalEfficiency;
                    ap.SolutionLevel = (from x in db.AreaProduct where x.ProjectKey == ap.ProjectKey && x.IsDelete == false && x.ProductName != null select x).Count() + 1;
                    ap.IsOn = 1;
                    db.SaveChanges();


                    return RedirectToAction("Ratio", "MgtProposalSolution", new { id = ap.ProductKey, id2 = id3 });
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