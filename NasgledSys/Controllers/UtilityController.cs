﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using Newtonsoft.Json;

using System.Text;

namespace NasgledSys.Controllers
{
    public class UtilityController : Controller
    {
        // GET: Utility
        private NasgledDBEntities db = new NasgledDBEntities();
        
        public ActionResult CheckUsernameCreate(string user)
        {
            JsonResult result = new JsonResult();
            var temp = from x in db.StaffList where x.Username == user select x;
            if (temp.Count() > 0) result.Data = 99;
            else
                result.Data = 1;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult CheckUsernameEdit(string user,Guid per)
        {
            JsonResult result = new JsonResult();
            var temp = from x in db.StaffList where x.Username == user && x.PersonnelKey!=per select x;
            if (temp.Count() > 0) result.Data = 99;
            else
                result.Data = 1;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetStateName(int StateCode)
        {
            JsonResult result = new JsonResult();

            result.Data = db.StateList.Find(StateCode).StateName;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetCityName(int CityKey)
        {
            JsonResult result = new JsonResult();

            result.Data = db.CityList.Find(CityKey).CityName;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
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