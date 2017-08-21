using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.IO;
using NasgledSys.Validation;

namespace NasgledSys.Controllers
{
    public class DefaultEmailConfigController : Controller
    {
        // GET: DefaultEmailConfig
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()//get all Institute
        {
            try
            {
                DefaultEmail model = db.DefaultEmail.FirstOrDefault();
                if(model==null)return View();
                else return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
            }
        }
        [HttpPost]
        public ActionResult Index(DefaultEmail obj)//get all Institute
        {
            try
            {
              if(obj.ID==null || obj.ID == Guid.Empty)
                {
                    DefaultEmail cust = new DefaultEmail();
                    cust.ID = Guid.NewGuid();
                  
                    if (!string.IsNullOrEmpty(obj.EmailAddress)) cust.EmailAddress = obj.EmailAddress;
                    else cust.EmailAddress = "--";
                    if (!string.IsNullOrEmpty(obj.SenderName)) cust.SenderName = obj.SenderName;
                    else cust.SenderName = "--";
                    cust.SMTPServer = obj.SMTPServer;
                    cust.SMTPPort = obj.SMTPPort;
                    cust.SMTPUsername = obj.SMTPUsername;
                    cust.SMTPPassword = obj.SMTPPassword;
                    cust.IsSMTPssl = obj.IsSMTPssl;

                    if (!string.IsNullOrEmpty(obj.POPServer)) cust.POPServer = obj.POPServer;
                    else cust.POPServer = "--";
                    if (!string.IsNullOrEmpty(obj.IncomingPort)) cust.IncomingPort = obj.IncomingPort;
                    else cust.IncomingPort = "--";
                    if (!string.IsNullOrEmpty(obj.POPUsername)) cust.POPUsername = obj.POPUsername;
                    else cust.POPUsername = "--";
                    if (!string.IsNullOrEmpty(obj.POPpassword)) cust.POPpassword = obj.POPpassword;
                    else cust.POPpassword = "--";
                    if (!string.IsNullOrEmpty(obj.Fullname)) cust.Fullname = obj.Fullname;
                    else cust.Fullname = "--";
                    if (!string.IsNullOrEmpty(obj.Detail)) cust.Detail = obj.Detail;
                    else cust.Detail = "--";
                  
                    db.DefaultEmail.Add(cust);
                    db.SaveChanges();
                }
              else
                {
                    DefaultEmail cust = db.DefaultEmail.Find(obj.ID);

                  
                    if (!string.IsNullOrEmpty(obj.EmailAddress)) cust.EmailAddress = obj.EmailAddress;
                    if (!string.IsNullOrEmpty(obj.SenderName)) cust.SenderName = obj.SenderName;
                    cust.SMTPServer = obj.SMTPServer;
                    cust.SMTPPort = obj.SMTPPort;
                    cust.SMTPUsername = obj.SMTPUsername;
                    cust.SMTPPassword = obj.SMTPPassword;
                    cust.IsSMTPssl = obj.IsSMTPssl;

                    if (!string.IsNullOrEmpty(obj.POPServer)) cust.POPServer = obj.POPServer;
                    if (!string.IsNullOrEmpty(obj.IncomingPort)) cust.IncomingPort = obj.IncomingPort;
                    if (!string.IsNullOrEmpty(obj.POPUsername)) cust.POPUsername = obj.POPUsername;
                    if (!string.IsNullOrEmpty(obj.POPpassword)) cust.POPpassword = obj.POPpassword;
                    if (!string.IsNullOrEmpty(obj.Fullname)) cust.Fullname = obj.Fullname;
                    if (!string.IsNullOrEmpty(obj.Detail)) cust.Detail = obj.Detail;

                   

                    db.SaveChanges();
                }

                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
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