//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NasgledSys.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientCompany()
        {
            this.Project = new HashSet<Project>();
            this.Team = new HashSet<Team>();
        }
    
        public System.Guid ClientCompanyKey { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> IndustryTypeKey { get; set; }
        public Nullable<System.Guid> ProfileKey { get; set; }
        public Nullable<int> NoOfSalesPerson { get; set; }
        public string OfficePhone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string BillingContactName { get; set; }
        public string BillingContactEMail { get; set; }
        public Nullable<bool> IsAddressSameAsOffice { get; set; }
        public string ProposalIntro { get; set; }
        public string ProposalTeam { get; set; }
        public string ProposalLegal { get; set; }
        public string ProposalDisclaimer { get; set; }
        public string ProposalReference { get; set; }
        public string EstimateFooter { get; set; }
        public string MarkupOrMargin { get; set; }
        public Nullable<decimal> MarkupOrMarginPercentage { get; set; }
        public Nullable<int> CityKey { get; set; }
        public Nullable<int> StateKey { get; set; }
    
        public virtual CityList CityList { get; set; }
        public virtual IndustryType IndustryType { get; set; }
        public virtual StateList StateList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Team { get; set; }
    }
}
