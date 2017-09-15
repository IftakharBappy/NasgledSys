using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public class EM_Project
    {
         
        public static ProjectClass ConvertToModel(Project ent)
        {
            ProjectClass bo = new ProjectClass();
            NasgledDBEntities db = new NasgledDBEntities();
            bo.ProjectKey = ent.ProjectKey;
            bo.ProjectName = ent.ProjectName;
            bo.ProjectStatusKey = ent.ProjectStatusKey;
            bo.PrimaryContactKey = ent.PrimaryContactKey;
            bo.PreparedBy = ent.PreparedBy;
            UserProfile pf = db.UserProfile.Find(ent.PreparedBy);
            bo.PreparedByDetail = pf.FirstName + " " + pf.LastName + " " + pf.Email;
            bo.ExpectedClosingMonth = ent.ExpectedClosingMonth;
            bo.EspectedClosingYear = ent.EspectedClosingYear;
            bo.ProbabilityOfCompletion = ent.ProbabilityOfCompletion;
            bo.IsAccessableByOthers = (bool)ent.IsAccessableByOthers;

            bo.OfficePhone = ent.Phone;
            bo.Address = ent.Address;
            bo.Address2 = ent.Address2;
            bo.StateKey = ent.StateKey;
            bo.CityKey = ent.CityKey;
            bo.MarkupPercentage = ent.MarkupPercentage;
            bo.LaborCost = ent.LaborCost;
            bo.ShippingCost = ent.ShippingCost;
            bo.MiscCost = ent.MiscCost;
            bo.TaxIncentives = ent.TaxIncentives;
            bo.ProductMargin = ent.ProductMargin;
            bo.GeneralNote = ent.GeneralNote;
            bo.InternalNotes = ent.InternalNotes;
            bo.ProfileKey = ent.ProfileKey;
            bo.IsDelete = ent.IsDelete;
            return bo;
        }
    }
}