using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.Validation
{
    public class FormValidation
    {
        public Company ValidateCompany(Company model)
        {
            if (String.IsNullOrEmpty(model.CompanyID))
                model.CompanyID = model.CompanyName;
            if (String.IsNullOrEmpty(model.CompanyFax))
                model.CompanyFax = "n/a";
            if (String.IsNullOrEmpty(model.CompanyAddress))
                model.CompanyAddress = "n/a";
            if (String.IsNullOrEmpty(model.CompanyPhone))
                model.CompanyPhone = "n/a";
            if (String.IsNullOrEmpty(model.CompanyMobile))
                model.CompanyMobile = "n/a";
            if (String.IsNullOrEmpty(model.CompanyEmail))
                model.CompanyEmail = "n/a";
            if (String.IsNullOrEmpty(model.Title))
                model.Title = "n/a";
            if (String.IsNullOrEmpty(model.ContactEmail))
                model.ContactEmail = "n/a";
            if (String.IsNullOrEmpty(model.CompanyWebsite))
                model.CompanyWebsite = "n/a";
            if (String.IsNullOrEmpty(model.ContactPersonName))
                model.ContactPersonName = "n/a";
            if (String.IsNullOrEmpty(model.ContactPersonNo))
                model.ContactPersonNo = "n/a";
            if (model.Zipcode==null)
                model.Zipcode = 0;
            return model;
        }

        public CompanyClass ValidateCompanyClass(CompanyClass model)
        {
            if (String.IsNullOrEmpty(model.CompanyID))
                model.CompanyID = model.CompanyName;
            if (String.IsNullOrEmpty(model.CompanyFax))
                model.CompanyFax = "n/a";
            if (String.IsNullOrEmpty(model.CompanyAddress))
                model.CompanyAddress = "n/a";
            if (String.IsNullOrEmpty(model.CompanyPhone))
                model.CompanyPhone = "n/a";
            if (String.IsNullOrEmpty(model.CompanyMobile))
                model.CompanyMobile = "n/a";
            if (String.IsNullOrEmpty(model.CompanyEmail))
                model.CompanyEmail = "n/a";
            if (String.IsNullOrEmpty(model.Title))
                model.Title = "n/a";
            if (String.IsNullOrEmpty(model.ContactEmail))
                model.ContactEmail = "n/a";
            if (String.IsNullOrEmpty(model.CompanyWebsite))
                model.CompanyWebsite = "n/a";
            if (String.IsNullOrEmpty(model.ContactPersonName))
                model.ContactPersonName = "n/a";
            if (String.IsNullOrEmpty(model.ContactPersonNo))
                model.ContactPersonNo = "n/a";
            return model;
        }
    }
}