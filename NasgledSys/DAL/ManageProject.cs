using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.DAL
{
    public class ManageProject
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public DataReturn SaveNewProject(ProjectClass obj)
        {
            DataReturn model = new DataReturn();
            try
            {
                Project cust = new Project();
                cust.ProjectKey = Guid.NewGuid();
                cust.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                cust.ProjectName = obj.ProjectName;
                cust.ProjectStatusKey = obj.ProjectStatusKey;
                cust.PrimaryContactKey = obj.PrimaryContactKey;
                cust.PreparedBy = obj.PreparedBy;
                if (obj.ExpectedClosingMonth == null) cust.ExpectedClosingMonth = 1;
                else cust.ExpectedClosingMonth = obj.ExpectedClosingMonth;
                if (obj.EspectedClosingYear == null) cust.EspectedClosingYear = System.DateTime.Now.Year;
                else cust.EspectedClosingYear = obj.EspectedClosingYear;
                if (obj.ProbabilityOfCompletion == null) cust.ProbabilityOfCompletion = 0;
                else cust.ProbabilityOfCompletion = obj.ProbabilityOfCompletion;
                cust.IsAccessableByOthers = obj.IsAccessableByOthers;
                cust.CompanyKey = GlobalClass.CCompany.ClientCompanyKey;
                cust.CreateDate = System.DateTime.Now;

                if (!string.IsNullOrEmpty(obj.OfficePhone)) cust.Phone = obj.OfficePhone;
                else cust.Phone = "(000) 000-0000";
                if (!string.IsNullOrEmpty(obj.Address)) cust.Address = obj.Address;
                else cust.Address = "--";
                if (!string.IsNullOrEmpty(obj.Address2)) cust.Address2 = obj.Address2;
                else cust.Address2 = "--";
                if (!string.IsNullOrEmpty(obj.Zipcode)) cust.Zipcode = obj.Zipcode;
                else cust.Zipcode = "--";
                if (obj.CityKey == null) cust.CityKey = 1;
                else cust.CityKey = obj.CityKey;
                if (obj.StateKey == null) cust.StateKey = 1;
                else cust.StateKey = obj.StateKey;

                if (obj.MarkupPercentage == null) cust.MarkupPercentage = 0;
                else cust.MarkupPercentage = obj.MarkupPercentage;
                if (obj.LaborCost == null) cust.LaborCost = 0;
                else cust.LaborCost = obj.LaborCost;
                if (obj.ShippingCost == null) cust.ShippingCost = 0;
                else cust.ShippingCost = obj.ShippingCost;
                if (obj.MiscCost == null) cust.MiscCost = 0;
                else cust.MiscCost = obj.MiscCost;
                if (obj.TaxIncentives == null) cust.TaxIncentives = 0;
                else cust.TaxIncentives = obj.TaxIncentives;
                if (obj.ProductMargin == null) cust.ProductMargin = 0;
                else cust.ProductMargin = obj.ProductMargin;
                cust.IsDelete = false;

                if (!string.IsNullOrEmpty(obj.GeneralNote)) cust.GeneralNote = obj.GeneralNote;
                else cust.GeneralNote = "--";
                if (!string.IsNullOrEmpty(obj.InternalNotes)) cust.InternalNotes = obj.InternalNotes;
                else cust.InternalNotes = "--";

                db.Project.Add(cust);
                db.SaveChanges();


                model.flag = 1;
                model.mess = "Data has been saved successfully.";
                model.key = cust.ProjectKey;
                GlobalClass.Project = cust;
            }
            catch (Exception ex)
            {
                model.flag = 0;
                model.mess = ex.Message.ToString();
            }
            return model;
        }


        public DataReturn UpdateProject(ProjectClass obj)
        {
            DataReturn model = new DataReturn();
            try
            {
                Project cust =db.Project.Find(obj.ProjectKey);
               
               
                cust.ProjectName = obj.ProjectName;
                cust.ProjectStatusKey = obj.ProjectStatusKey;
                cust.PrimaryContactKey = obj.PrimaryContactKey;
                cust.PreparedBy = obj.PreparedBy;
                if (obj.ExpectedClosingMonth == null) cust.ExpectedClosingMonth = 1;
                else cust.ExpectedClosingMonth = obj.ExpectedClosingMonth;
                if (obj.EspectedClosingYear == null) cust.EspectedClosingYear = System.DateTime.Now.Year;
                else cust.EspectedClosingYear = obj.EspectedClosingYear;
                if (obj.ProbabilityOfCompletion == null) cust.ProbabilityOfCompletion = 0;
                else cust.ProbabilityOfCompletion = obj.ProbabilityOfCompletion;
                cust.IsAccessableByOthers = obj.IsAccessableByOthers;
              
                cust.CreateDate = System.DateTime.Now;

                if (!string.IsNullOrEmpty(obj.OfficePhone)) cust.Phone = obj.OfficePhone;
                else cust.Phone = "(000) 000-0000";
                if (!string.IsNullOrEmpty(obj.Address)) cust.Address = obj.Address;
                else cust.Address = "--";
                if (!string.IsNullOrEmpty(obj.Address2)) cust.Address2 = obj.Address2;
                else cust.Address2 = "--";
                if (!string.IsNullOrEmpty(obj.Zipcode)) cust.Zipcode = obj.Zipcode;
                else cust.Zipcode = "--";
                if (obj.CityKey == null) cust.CityKey = 1;
                else cust.CityKey = obj.CityKey;
                if (obj.StateKey == null) cust.StateKey = 1;
                else cust.StateKey = obj.StateKey;

                if (obj.MarkupPercentage == null) cust.MarkupPercentage = 0;
                else cust.MarkupPercentage = obj.MarkupPercentage;
                if (obj.LaborCost == null) cust.LaborCost = 0;
                else cust.LaborCost = obj.LaborCost;
                if (obj.ShippingCost == null) cust.ShippingCost = 0;
                else cust.ShippingCost = obj.ShippingCost;
                if (obj.MiscCost == null) cust.MiscCost = 0;
                else cust.MiscCost = obj.MiscCost;
                if (obj.TaxIncentives == null) cust.TaxIncentives = 0;
                else cust.TaxIncentives = obj.TaxIncentives;
                if (obj.ProductMargin == null) cust.ProductMargin = 0;
                else cust.ProductMargin = obj.ProductMargin;
               

                if (!string.IsNullOrEmpty(obj.GeneralNote)) cust.GeneralNote = obj.GeneralNote;
                else cust.GeneralNote = "--";
                if (!string.IsNullOrEmpty(obj.InternalNotes)) cust.InternalNotes = obj.InternalNotes;
                else cust.InternalNotes = "--";
                
                db.SaveChanges();


                model.flag = 1;
                model.mess = "Data has been saved successfully.";
                model.key = cust.ProjectKey;
                GlobalClass.Project = cust;
            }
            catch (Exception ex)
            {
                model.flag = 0;
                model.mess = ex.Message.ToString();
            }
            return model;
        }
    }
}