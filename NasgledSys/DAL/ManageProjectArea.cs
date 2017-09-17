using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.DAL
{
    public class ManageProjectArea
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        public string GetAllAreaNamesForSubArea(Guid? id)
        {          
                Area area = db.Area.Find(id);
            return id==null?" ":(area.AreaName + "||" + GetAllAreaNamesForSubArea(area.ParentAreaKey));                
          
        }

        public Guid? GetOperatingScheduleKey()
        {
           
            try
            {
                OperatingSchedule obj = (from x in db.OperatingSchedule where x.ProjectKey == GlobalClass.Project.ProjectKey && x.IsDefault == true select x).FirstOrDefault();
                db.Dispose();
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return obj.PKey;
                }
            }
            catch (Exception ex)
            {              
               string temp= ex.Message.ToString();
                return null;
            }
          
        }
        public Guid? GetNewRateSchedule()
        {
            try
            {
                NewRateSchedule obj = (from x in db.NewRateSchedule where x.ProjectKey == GlobalClass.Project.ProjectKey && x.IsDefault == true select x).FirstOrDefault();
                db.Dispose();
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return obj.PKey;
                }
                
            }
            catch (Exception ex)
            {
                string temp = ex.Message.ToString();
                return null;
            }

        }
        public Guid? GetCoolingSystem()
        {
            try
            {
                CoolingSystem obj = (from x in db.CoolingSystem where x.ProjectKey == GlobalClass.Project.ProjectKey && x.IsDefault == true select x).FirstOrDefault();
                db.Dispose();
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return obj.CoolingSystemKey;
                }

            }
            catch (Exception ex)
            {
                string temp = ex.Message.ToString();
                return null;
            }

        }
        public Guid? GetHeatingSystem()
        {
            try
            {
                HeatingSystem obj = (from x in db.HeatingSystem where x.ProjectKey == GlobalClass.Project.ProjectKey && x.IsDefault == true select x).FirstOrDefault();
                db.Dispose();
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return obj.HeatingSystemKey;
                }

            }
            catch (Exception ex)
            {
                string temp = ex.Message.ToString();
                return null;
            }

        }
    }
}