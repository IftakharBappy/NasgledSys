using NasgledSys.Models;

namespace NasgledSys.EM
{
    public static class EM_ProposalTemplate
    {
        public static ProposalTemplate ConvertToEntity(ProposalTemplateModel bo)
        {
            ProposalTemplate ent = new ProposalTemplate();
            ent.ReportKey = bo.ReportKey;
            ent.ProjectKey = bo.ProjectKey;
            ent.ProposalKey = bo.ProposalKey;
            ent.FactorName = bo.FactorName;
            ent.DisplayIndex = bo.DisplayIndex;
            return ent;
        }
    }
}