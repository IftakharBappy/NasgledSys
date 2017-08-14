using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.DAL
{
    public class MgtCityList
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<CityClass> ListAll()
        {
            List<CityClass> obj = new List<CityClass>();
            var temp = (from x in db.CityList
                       where x.IsDelete == false && x.CityKey>30200
                       select new CityClass
                       {
                           CityKey=x.CityKey,
                           CityName=x.CityName,
                           StateCode=x.StateCode,
                           StateName=x.StateList.StateName,
                           IsDelete=x.IsDelete
                       }).OrderBy(m=>m.CityName);
            obj = temp.ToList();
            return obj;
        }
        public int Add(CityClass obj)
        {
            int i = 1;
            try
            {
                CityList model = new CityList();
                model.CityName = obj.CityName;
                model.StateCode = obj.StateCode;
                model.IsDelete = false;
                db.CityList.Add(model);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(CityClass obj)
        {
            int i = 1;
            try
            {
                CityList model = db.CityList.Find(obj.CityKey);
                model.CityName = obj.CityName;
                model.StateCode = obj.StateCode;
              
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }
        public int Delete(int obj)
        {
            int i = 1;
            try
            {
                CityList model = db.CityList.Find(obj);
                model.IsDelete = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }
    }
}