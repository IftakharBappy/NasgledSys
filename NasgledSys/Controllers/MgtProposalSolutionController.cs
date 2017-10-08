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
                                    m.OperatingScheduleName = pitem.OperatingSchedule.OPName;
                                    m.OperatingHours = pitem.OperatingSchedule.AnnualOperationHour;
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