using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.EM
{
    public static class EM_City
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

        public static CityList ConvertToEntity(CityViewModel bo)
        {
            CityList ent = new CityList();
            //ent.CityKey = bo.CityKey;
            ent.CityName = bo.CityName;
            ent.StateCode = bo.StateCode;
            ent.IsDelete = bo.IsDelete;

            return ent;
        }
    }
}