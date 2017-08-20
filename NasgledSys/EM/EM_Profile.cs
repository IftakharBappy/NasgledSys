using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public static class EM_Profile
    {
        public static CityViewModel ConvertToModel(CityList ent)
        {
            CityViewModel bo = new CityViewModel();

            bo.CityKey = ent.CityKey;
            bo.CityName = ent.CityName;
            bo.StateName = ent.StateList.StateName;
            bo.StateCode = ent.StateCode;
            bo.IsDelete = ent.IsDelete;

            return bo;
        }

        public static UserProfile ConvertToEntity(ProfileClass bo)
        {
            UserProfile ent = new UserProfile();
            if (bo.ProfileKey == null || bo.ProfileKey == Guid.Empty) bo.ProfileKey = Guid.NewGuid();

            ent.ProfileKey =(Guid)bo.ProfileKey;
            ent.FirstName = bo.FirstName;
            ent.LastName = bo.LastName;
            ent.CompanyName = bo.CompanyName;
            ent.JobTitle = bo.JobTitle;
            ent.CityKey = bo.CityKey;
           
            return ent;

        }
    }
}