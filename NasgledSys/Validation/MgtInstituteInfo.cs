using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;

namespace NasgledSys.Validation
{
    public class MgtInstituteInfo
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public CompanyClass FillCompanyInfo(Company model)
        {
            
            CompanyClass obj = new CompanyClass();
            obj.CompanyKey = model.CompanyKey;
            obj.CompanyID = model.CompanyID;
            obj.CompanyName = model.CompanyName;
            obj.CompanyAddress = model.CompanyAddress;
            obj.CompanyPhone = model.CompanyPhone;
            obj.CompanyMobile = model.CompanyMobile;
            obj.CompanyEmail = model.CompanyEmail;
            obj.CompanyWebsite = model.CompanyWebsite;
            obj.CompanyFax = model.CompanyFax;
            obj.ContactPersonName = model.ContactPersonName;
            obj.ContactPersonNo = model.ContactPersonNo;
            obj.Logo = model.Logo;
            obj.LogoType = model.LogoType;
            obj.IsDelete = model.IsDelete;
            obj.StateCode = model.StateCode;
            obj.CityKey = model.CityKey;
            obj.Zipcode = model.Zipcode;
            obj.Title = model.Title;
            obj.ContactEmail = model.ContactEmail;

            return obj;
        }
        public StaffClass ValidateStaff(StaffClass model, bool ICreate)
        {
            if (ICreate == true)
            {
                if (String.IsNullOrEmpty(model.PID))
                    model.PID = UniqueIDCreator.CreateID();

                if (String.IsNullOrEmpty(model.Mail))
                    model.Mail = "n/a";
                if (String.IsNullOrEmpty(model.Mobile))
                    model.Mobile = "n/a";
                if (String.IsNullOrEmpty(model.Department))
                    model.Department = "none";

                if (String.IsNullOrEmpty(model.Designation))
                    model.Designation = "none";
            }
            else
            {
                if (String.IsNullOrEmpty(model.PID))
                    model.PID = UniqueIDCreator.CreateID();

                if (String.IsNullOrEmpty(model.Mail))
                    model.Mail = "n/a";
                if (String.IsNullOrEmpty(model.Mail))
                    model.Mail = "n/a";
                if (String.IsNullOrEmpty(model.Mobile))
                    model.Mobile = "n/a";
                if (String.IsNullOrEmpty(model.Department))
                    model.Department = "none";

                if (String.IsNullOrEmpty(model.Designation))
                    model.Designation = "none";

            }
            return model;
        }
    }
}