using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.EM
{
    public class EM_ReportTemplate
    {

        public static ReportTemplateModel ConvertToModel(ReportTemplate ent)
        {
            ReportTemplateModel bo = new ReportTemplateModel();

            bo.TemplateKey = ent.TemplateKey;
            bo.FactorName = ent.FactorName;
            bo.TLevel = ent.TLevel;
            return bo;
        }

        public static ReportTemplate ConvertToEntity(ReportTemplateModel bo)
        {
            ReportTemplate ent = new ReportTemplate();
            bo.TemplateKey = ent.TemplateKey;
            bo.FactorName = ent.FactorName;
            bo.TLevel = ent.TLevel;
            return ent;
        }
    }
}