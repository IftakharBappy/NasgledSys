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

namespace NasgledSys.Controllers
{
    public class MgtScheduleController : Controller
    {
        // GET: MgtSchedule
        private NasgledDBEntities db = new NasgledDBEntities();
       
        public ActionResult Index()
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
    }
}