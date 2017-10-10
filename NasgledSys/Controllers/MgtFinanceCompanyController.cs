using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtFinanceCompanyController : Controller
    {
        // GET: MgtFinanceCompany
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {

                var FinCompany = db.FinanceCompany;
                FinanceCompanyViewModel model = new FinanceCompanyViewModel();

                model.FinanceCompanyViewModelList = FinCompany.Select(p => new FinanceCompanyViewModel()
                {
                    FinanceKey = p.FinanceKey,
                    FinancingCompanyName = p.FinancingCompanyName,
                    IntroText = p.IntroText,
                    Hyperlink = p.Hyperlink,
                    Logo = p.Logo,
                    AboutUsLink=p.AboutUsLink
                }).ToList();

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new FinanceCompanyViewModel();
                    return View("Create", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtFinanceCompany", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult Create(FinanceCompanyViewModel obj, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (string.IsNullOrEmpty(obj.Hyperlink)) obj.Hyperlink = "#h";
                        if (string.IsNullOrEmpty(obj.AboutUsLink)) obj.AboutUsLink = "#a";
                        FinanceCompany model = new FinanceCompany();
                        model.FinanceKey = Guid.NewGuid();
                        model.FinancingCompanyName = obj.FinancingCompanyName;
                        model.Hyperlink = obj.Hyperlink;
                        model.AboutUsLink = obj.AboutUsLink;
                        model.IntroText = obj.IntroText;

                        byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                        int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                        model.Logo = imgBinaryData;
                        model.FileName = PostedLogo.FileName;
                        model.LogoType = PostedLogo.ContentType;
                        ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", model.FileName);

                        db.FinanceCompany.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Finance Company is Saved Successfully";
                        return RedirectToAction("Index");
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtFinanceCompany", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult Edit(Guid? FinanceKey)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (FinanceKey == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var FinCompany = db.FinanceCompany.Find(FinanceKey);

                    FinanceCompanyViewModel model = new FinanceCompanyViewModel();
                    model.FinanceKey = FinCompany.FinanceKey;
                    model.FinancingCompanyName = FinCompany.FinancingCompanyName;
                    model.IntroText = FinCompany.IntroText;
                    model.Hyperlink = FinCompany.Hyperlink;
                    model.Logo = FinCompany.Logo;
                    model.AboutUsLink = FinCompany.AboutUsLink;
                    if (FinCompany == null)
                    {
                        return HttpNotFound();
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtFinanceCompany", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult Edit(FinanceCompanyViewModel obj, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        if (string.IsNullOrEmpty(obj.Hyperlink)) obj.Hyperlink = "#h";
                        if (string.IsNullOrEmpty(obj.AboutUsLink)) obj.AboutUsLink = "#a";
                        FinanceCompany model = db.FinanceCompany.Find(obj.FinanceKey);
                        model.FinancingCompanyName = obj.FinancingCompanyName;
                        model.IntroText = obj.IntroText;
                        model.Hyperlink = obj.Hyperlink;
                        model.AboutUsLink = obj.AboutUsLink;
                        if (PostedLogo != null && PostedLogo.ContentLength > 0)
                        {
                            byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                            int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                            model.Logo = imgBinaryData;
                            model.FileName = PostedLogo.FileName;
                            model.LogoType = PostedLogo.ContentType;
                            ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", model.FileName);
                        }
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Finance Company is Updated Successfully";

                        return RedirectToAction("Index", new { id = obj.FinanceKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtFinanceCompany", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }

        }
        public ActionResult Delete(Guid id)
        {
            try
            {
                FinanceCompany model = db.FinanceCompany.Find(id);
                db.FinanceCompany.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtFinanceCompany", "Index"));
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