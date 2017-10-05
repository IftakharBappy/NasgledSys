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
    public class MgtProposalCustomizeController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        public ActionResult Customize(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal proposal = db.Proposal.Find(id);
                    ProposalToCustomizeViewModel viewModel = new ProposalToCustomizeViewModel();
                    viewModel.ProposalKey = proposal.ProposalKey;
                    viewModel.CompanyKey = company.ClientCompanyKey;
                    viewModel.CompanyName = company.CompanyName;
                    viewModel.ProposalName = proposal.ProposalName;
                    viewModel.ProjectKey = proposal.ProjectKey;
                    viewModel.PreparedByText = proposal.PreparedByText;
                    viewModel.IntroductionText = proposal.IntroductionText;
                    viewModel.Team = proposal.Team;
                    viewModel.Legal = proposal.Legal;
                    viewModel.Disclaimer = proposal.Disclaimer;
                    viewModel.Reference = proposal.Reference;
                    viewModel.Billing = proposal.Billing;
                    viewModel.Summary = proposal.Summary;
                    viewModel.Pic = proposal.Pic;
                    viewModel.Picname = proposal.Picname;
                    viewModel.PicType = proposal.PicType;

                    return View(viewModel);
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
        public ActionResult Customize([Bind(Exclude = "Pic")] ProposalToCustomizeViewModel viewModel, HttpPostedFileBase Pic)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Proposal proposal = db.Proposal.Find(viewModel.ProposalKey);
                    proposal.ProposalName = viewModel.ProposalName;
                    proposal.ProjectKey= viewModel.ProjectKey;
                    proposal.PreparedByText= viewModel.PreparedByText;
                    proposal.IntroductionText= viewModel.IntroductionText;
                    proposal.Team= viewModel.Team;
                    proposal.Legal = viewModel.Legal;
                    proposal.Disclaimer = viewModel.Disclaimer;
                    proposal.Reference = viewModel.Reference;
                    proposal.Billing= viewModel.Billing;
                    proposal.Summary = viewModel.Summary;
                    if (Pic != null)
                    {
                        byte[] imgBinaryData = new byte[Pic.ContentLength];
                        int readresult = Pic.InputStream.Read(imgBinaryData, 0, Pic.ContentLength);
                        proposal.Pic = imgBinaryData;
                        proposal.Picname = Pic.FileName;
                        proposal.PicType = Pic.ContentType;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Customize", new { id = viewModel.ProposalKey });
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
    }
}