﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.IO;


namespace NasgledSys.Controllers
{
    public class ShowImageController : Controller
    {
        // GET: ShowImage
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult GetCompanyLogo(Guid id)
        {

            Company obj = db.Company.SingleOrDefault(m => m.CompanyKey == id);

            if (obj.Logo != null)
                return File(obj.Logo, obj.LogoType);
            else
            {
                ImageFile faoimagefile = db.ImageFile.Single(f => f.ImageFileKey == 1);
                return File(faoimagefile.FileContent, faoimagefile.FileType);
            }
        }
        public ActionResult GetUserPic(Guid id)
        {

            StaffList obj = db.StaffList.SingleOrDefault(m => m.PersonnelKey == id);

            if (obj.Pic != null)
                return File(obj.Pic, obj.PicType);
            else
            {
                ImageFile faoimagefile = db.ImageFile.Single(f => f.ImageFileKey == 1);
                return File(faoimagefile.FileContent, faoimagefile.FileType);
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