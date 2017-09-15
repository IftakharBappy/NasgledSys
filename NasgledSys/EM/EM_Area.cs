using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public class EM_Area
    {
        public static AreaClass ConvertToModelForEditArea(Area ent)
        {
            AreaClass bo = new AreaClass();
            NasgledDBEntities db = new NasgledDBEntities();
            bo.ProjectKey = ent.ProjectKey;
            bo.IsParent = true;
            bo.IsSubEdit = false;
            bo.AreaKey = ent.AreaKey;
            bo.AreaName = ent.AreaName;
            bo.Reception = ent.Reception;
          
            bo.SquareFeet = ent.SquareFeet;
            bo.OperationScheduleKey = ent.OperationScheduleKey;
            bo.NewRateScheduleKey = ent.NewRateScheduleKey;
            bo.CoolingSystemKey = ent.CoolingSystemKey;
            bo.HeatingSystemKey = ent.HeatingSystemKey;
            bo.ParentAreaKey = Guid.Empty;
            if(ent.ParentAreaKey==null || ent.ParentAreaKey == Guid.Empty)
            {
                bo.IsSubEdit = false;
            }
            GlobalClass.AreaHeading = ent.AreaName;      
            return bo;
        }
    }
}