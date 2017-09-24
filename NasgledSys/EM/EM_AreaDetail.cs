using NasgledSys.Models;

namespace NasgledSys.EM
{
    public static class EM_AreaDetail
    {
        public static AreaDetailViewModel ConvertToModel(AreaDetail ent)
        {
            AreaDetailViewModel bo = new AreaDetailViewModel();

            bo.DetailKey = ent.DetailKey;
            bo.AreaKey = ent.AreaKey;
            bo.Description = ent.Description;
            bo.AverageIlluminnce = ent.AverageIlluminnce;
            bo.LightingSatisfactionKey = ent.LightingSatisfactionKey;
            bo.CeilingHeight = ent.CeilingHeight;
            bo.Reflectance = ent.Reflectance;
            bo.AreaWidth = ent.AreaWidth;
            bo.Length = ent.Length;

            return bo;
        }

        public static AreaDetail ConvertToEntity(AreaDetailViewModel bo)
        {
            AreaDetail ent = new AreaDetail();
            ent.DetailKey = bo.DetailKey;
            ent.AreaKey = bo.AreaKey;
            ent.Description = bo.Description;
            ent.AverageIlluminnce = bo.AverageIlluminnce;
            ent.LightingSatisfactionKey = bo.LightingSatisfactionKey;
            ent.CeilingHeight = bo.CeilingHeight;
            ent.Reflectance = bo.Reflectance;
            ent.AreaWidth = bo.AreaWidth;
            ent.Length = bo.Length;
             
            return ent;
        }
       
    }
}