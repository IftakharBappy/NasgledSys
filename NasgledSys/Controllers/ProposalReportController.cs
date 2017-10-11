using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class ProposalReportController : Controller
    {
        public ActionResult Report(Guid id)
        {
            return View();
        }
    }
}