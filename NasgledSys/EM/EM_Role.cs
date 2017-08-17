using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public static class EM_Role
    {
        public static CityViewModel ConvertToCityModel(CityList ent)
        {
            CityViewModel bo = new CityViewModel();

            bo.CityKey = ent.CityKey;
            bo.CityName = ent.CityName;
            bo.StateName = ent.StateList.StateName;
            bo.StateCode = ent.StateCode;
            bo.IsDelete = ent.IsDelete;

            return bo;
        }

        public static CityList ConvertCityToEntity(CityViewModel bo)
        {
            CityList ent = new CityList();
            if (bo.CityKey == null) ent.CityKey = -99;
            else  ent.CityKey =(int)bo.CityKey;
            ent.CityName = bo.CityName;
            ent.StateCode = bo.StateCode;
            ent.IsDelete = bo.IsDelete;

            return ent;
        }
        public static RoleViewModel ConvertToModel(UserRole ent)
        {
            RoleViewModel bo = new RoleViewModel();

            bo.RoleKey = ent.RoleKey;
            bo.RoleName = ent.RoleName;
            bo.Rlevel = ent.Rlevel;          
            bo.IsDelete = ent.IsDelete;

            return bo;
        }
        public static UserRole ConvertToEntity(RoleViewModel ent)
        {
            UserRole bo = new UserRole();
            if (ent.RoleKey == null) bo.RoleKey = Guid.NewGuid();
            else  bo.RoleKey =(Guid)ent.RoleKey;
            bo.RoleName = ent.RoleName;
            bo.Rlevel = ent.Rlevel;
            bo.IsDelete = ent.IsDelete;

            return bo;
        }
    }
}