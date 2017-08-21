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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NasgledSys.Controllers
{
    public class MgtStaffController : Controller
    {
        // GET: MgtStaff
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation val = new FormValidation();
        private MgtInstituteInfo manage = new MgtInstituteInfo();
        public Byte[] BufferFromImage(HttpPostedFileBase file, int width, int height)
        {
            Byte[] buffer = null;
            ImageConverter converter = new ImageConverter();
            using (var srcImage = Image.FromStream(file.InputStream, true, true))
            {

                using (var newImage = new Bitmap(width, height))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, width, height));
                    buffer = (byte[])converter.ConvertTo(newImage, typeof(byte[]));

                }
            }


            return buffer;
        }

        public ActionResult Index(Guid id)
        {
           try
            {
                GlobalClass.Company = db.Company.Find(id);
                var staffList = db.StaffList.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey && m.IsDelete == false).OrderBy(m => m.PName);
                return View(staffList.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
            }

        }
        // GET: MgtStaff
        public ActionResult DIndex()
        {
            try
            {
                var staffList = db.StaffList.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey && m.IsDelete == false).OrderBy(m => m.PName);
                return View(staffList.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        // GET: MgtStaff
        public ActionResult Details(Guid? id)
        {
            try
            {
                StaffList staffList = db.StaffList.Find(id);

                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        // GET: MgtStaff
        public ActionResult MyProfile()
        {
            try
            {
                StaffList staffList = db.StaffList.Find(GlobalClass.LoginUser.PersonnelKey);

                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        // GET: MgtStaff
        public ActionResult Create()
        {
           try
            {
                ViewBag.mess = "";
                ViewBag.Usergr = new SelectList(db.Usergroup.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey).OrderBy(m => m.GroupName), "UserGroupKey", "GroupName");

                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaffClass staffList, HttpPostedFileBase file)
        {
           try
            {
                ViewBag.mess = "";
                bool flag = true;
                if (string.IsNullOrEmpty(staffList.PID))
                {

                }
                else
                {
                    var uid = from x in db.StaffList where x.PID == staffList.PID && x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                    if (uid.Count() > 0)
                    {
                        ViewBag.mess = "Duplicate ID";
                        flag = false;
                    }
                    else { }
                }
                if (flag == true)
                {
                    if (file != null)
                    {
                        byte[] data = null;

                        data = BufferFromImage(file, 50, 80);
                        staffList.Pic = data;
                        staffList.PicType = file.ContentType;
                    }
                    staffList = manage.ValidateStaff(staffList, true);
                    StaffList model = new StaffList();
                    model.PersonnelKey = Guid.NewGuid();
                    model.IsUser = true;
                    model.IsDelete = false;
                    model.Pic = staffList.Pic; model.PicType = staffList.PicType;
                    model.PID = staffList.PID;
                    model.PName = staffList.PName;
                    model.Mobile = staffList.Mobile;
                    model.Mail = staffList.Mail;
                    model.Designation = staffList.Designation;
                    model.Department = staffList.Department;
                    model.Username = staffList.Username;
                    model.Password = staffList.Password;
                    model.Usergr = staffList.Usergr;
                    model.CompanyKey = GlobalClass.Company.CompanyKey;
                    db.StaffList.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.CompanyKey });
                }

                ViewBag.Usergr = new SelectList(db.Usergroup.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey).OrderBy(m => m.GroupName), "UserGroupKey", "GroupName", staffList.Usergr);

                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

       
        public ActionResult Edit(Guid? id)
        {
           try
            {
                StaffList staffList = db.StaffList.Find(id);
                ViewBag.mess = "";

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaffList staffList, HttpPostedFileBase file)
        {
            try
            {
                ViewBag.mess = "";
                bool flag = true;
                if (string.IsNullOrEmpty(staffList.PID))
                {

                }
                else
                {
                    var uid = from x in db.StaffList where x.PID == staffList.PID && x.PersonnelKey != staffList.PersonnelKey && x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                    if (uid.Count() > 0)
                    {
                        ViewBag.mess = "Duplicate ID";
                        flag = false;
                    }
                    else { }
                }
                if (flag == true)
                {
                    StaffList model = db.StaffList.Find(staffList.PersonnelKey);
                    if (file != null)
                    {
                        byte[] data = null;

                        data = BufferFromImage(file, 50, 80);
                        model.Pic = data;
                        model.PicType = file.ContentType;
                    }
                    model.PID = staffList.PID;
                    model.PName = staffList.PName;
                    model.Mobile = staffList.Mobile;
                    model.Department = staffList.Department;
                    model.Designation = staffList.Designation;
                    model.Username = staffList.Username;
                    model.Usergr = staffList.Usergr;
                    model.Mail = staffList.Mail;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.CompanyKey });
                }

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {

                StaffList staffList = db.StaffList.Find(id);
                staffList.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=staffList.CompanyKey});
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult DCreate()
        {
            try
            {
                ViewBag.mess = "";
                ViewBag.Usergr = new SelectList(db.Usergroup.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey).OrderBy(m => m.GroupName), "UserGroupKey", "GroupName");

                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DCreate(StaffClass staffList, HttpPostedFileBase file)
        {
            try
            {
                ViewBag.mess = "";
                bool flag = true;
                if (string.IsNullOrEmpty(staffList.PID))
                {

                }
                else
                {
                    var uid = from x in db.StaffList where x.PID == staffList.PID && x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                    if (uid.Count() > 0)
                    {
                        ViewBag.mess = "Duplicate ID";
                        flag = false;
                    }
                    else { }
                }
                if (flag == true)
                {
                    if (file != null)
                    {
                        byte[] data = null;

                        data = BufferFromImage(file, 50, 80);
                        staffList.Pic = data;
                        staffList.PicType = file.ContentType;
                    }
                    staffList = manage.ValidateStaff(staffList, true);
                    StaffList model = new StaffList();
                    model.PersonnelKey = Guid.NewGuid();
                    model.IsUser = true;
                    model.IsDelete = false;
                    model.Pic = staffList.Pic; model.PicType = staffList.PicType;
                    model.PID = staffList.PID;
                    model.PName = staffList.PName;
                    model.Mobile = staffList.Mobile;
                    model.Mail = staffList.Mail;
                    model.Designation = staffList.Designation;
                    model.Department = staffList.Department;
                    model.Username = staffList.Username;
                    model.Password = staffList.Password;
                    model.Usergr = staffList.Usergr;
                    model.CompanyKey = GlobalClass.Company.CompanyKey;
                    db.StaffList.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("DIndex");
                }

                ViewBag.Usergr = new SelectList(db.Usergroup.Where(m => m.CompanyKey == GlobalClass.Company.CompanyKey).OrderBy(m => m.GroupName), "UserGroupKey", "GroupName", staffList.Usergr);

                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult EditMyProfile()
        {
            try
            {
                StaffList staffList = db.StaffList.Find(GlobalClass.LoginUser.PersonnelKey);
                ViewBag.mess = "";

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMyProfile(StaffList staffList, HttpPostedFileBase file)
        {
            try
            {
                ViewBag.mess = "";
                bool flag = true;
                if (string.IsNullOrEmpty(staffList.PID))
                {

                }
                else
                {
                    var uid = from x in db.StaffList where x.PID == staffList.PID && x.PersonnelKey != staffList.PersonnelKey && x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                    if (uid.Count() > 0)
                    {
                        ViewBag.mess = "Duplicate ID";
                        flag = false;
                    }
                    else { }
                }
                if (flag == true)
                {
                    StaffList model = db.StaffList.Find(staffList.PersonnelKey);
                    if (file != null)
                    {
                        byte[] data = null;

                        data = BufferFromImage(file, 50, 80);
                        model.Pic = data;
                        model.PicType = file.ContentType;
                    }
                    model.PID = staffList.PID;
                    model.PName = staffList.PName;
                    model.Mobile = staffList.Mobile;
                    model.Department = staffList.Department;
                    model.Designation = staffList.Designation;
                    model.Username = staffList.Username;
                    model.Password = staffList.Password;
                    model.Mail = staffList.Mail;
                    db.SaveChanges();
                    return RedirectToAction("DIndex");
                }

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }


        public ActionResult DEdit(Guid? id)
        {
            try
            {
                StaffList staffList = db.StaffList.Find(id);
                ViewBag.mess = "";

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DEdit(StaffList staffList, HttpPostedFileBase file)
        {
            try
            {
                ViewBag.mess = "";
                bool flag = true;
                if (string.IsNullOrEmpty(staffList.PID))
                {

                }
                else
                {
                    var uid = from x in db.StaffList where x.PID == staffList.PID && x.PersonnelKey != staffList.PersonnelKey && x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                    if (uid.Count() > 0)
                    {
                        ViewBag.mess = "Duplicate ID";
                        flag = false;
                    }
                    else { }
                }
                if (flag == true)
                {
                    StaffList model = db.StaffList.Find(staffList.PersonnelKey);
                    if (file != null)
                    {
                        byte[] data = null;

                        data = BufferFromImage(file, 50, 80);
                        model.Pic = data;
                        model.PicType = file.ContentType;
                    }
                    model.PID = staffList.PID;
                    model.PName = staffList.PName;
                    model.Mobile = staffList.Mobile;
                    model.Department = staffList.Department;
                    model.Designation = staffList.Designation;
                    model.Username = staffList.Username;
                    model.Usergr = staffList.Usergr;
                    model.Mail = staffList.Mail;
                    db.SaveChanges();
                    return RedirectToAction("DIndex");
                }

                ViewBag.Usergr = new SelectList(db.Usergroup, "UserGroupKey", "GroupName", staffList.Usergr);
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }

        public ActionResult ChangePassword(Guid? id)
        {
            try
            {
                StaffList staffList = db.StaffList.Find(id);
                StaffClass model = new StaffClass();
                model.PName = staffList.PName;
                model.PID = staffList.PID;
                model.PersonnelKey = staffList.PersonnelKey;
                model.Department = staffList.Department;
                model.Designation = staffList.Designation;
                model.Mail = staffList.Mail;
                model.Mobile = staffList.Mobile;
                model.Username = staffList.Username;
                ViewBag.mess = "";

                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(StaffClass staffList, HttpPostedFileBase file)
        {
            try
            {
                ViewBag.mess = "";
               
                if (ModelState.IsValid)
                {
                    StaffList model = db.StaffList.Find(staffList.PersonnelKey);
                  
                    model.Password = staffList.Password;
                 
                    db.SaveChanges();
                    return RedirectToAction("DIndex");
                }

               
                return View(staffList);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "DIndex"));
            }
        }


        public ActionResult DDelete(Guid? id)
        {
            try
            {

                StaffList staffList = db.StaffList.Find(id);
                staffList.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("DIndex");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
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