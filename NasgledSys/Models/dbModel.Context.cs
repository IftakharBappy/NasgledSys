﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NasgledDBEntities : DbContext
    {
        public NasgledDBEntities()
            : base("name=NasgledDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CityList> CityList { get; set; }
        public virtual DbSet<Forms> Forms { get; set; }
        public virtual DbSet<ImageFile> ImageFile { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<StaffList> StaffList { get; set; }
        public virtual DbSet<StateList> StateList { get; set; }
        public virtual DbSet<Usergroup> Usergroup { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AreaDetail> AreaDetail { get; set; }
        public virtual DbSet<AreaDocument> AreaDocument { get; set; }
        public virtual DbSet<AreaNote> AreaNote { get; set; }
        public virtual DbSet<AreaPhoto> AreaPhoto { get; set; }
        public virtual DbSet<AreaProduct> AreaProduct { get; set; }
        public virtual DbSet<AreaProductDetail> AreaProductDetail { get; set; }
        public virtual DbSet<AreaProductDocument> AreaProductDocument { get; set; }
        public virtual DbSet<AreaProductNote> AreaProductNote { get; set; }
        public virtual DbSet<AreaProductPhoto> AreaProductPhoto { get; set; }
        public virtual DbSet<ClientCompany> ClientCompany { get; set; }
        public virtual DbSet<ClientContact> ClientContact { get; set; }
        public virtual DbSet<CoolingEfficientyType> CoolingEfficientyType { get; set; }
        public virtual DbSet<CoolingSystem> CoolingSystem { get; set; }
        public virtual DbSet<CoolingSystemType> CoolingSystemType { get; set; }
        public virtual DbSet<FuelType> FuelType { get; set; }
        public virtual DbSet<HeatingEfficiencyType> HeatingEfficiencyType { get; set; }
        public virtual DbSet<HeatingSystem> HeatingSystem { get; set; }
        public virtual DbSet<HeatingSystemType> HeatingSystemType { get; set; }
        public virtual DbSet<IncentiveMaxType> IncentiveMaxType { get; set; }
        public virtual DbSet<IncentiveType> IncentiveType { get; set; }
        public virtual DbSet<IndustryType> IndustryType { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ItemCatelogue> ItemCatelogue { get; set; }
        public virtual DbSet<ItemSubcategory> ItemSubcategory { get; set; }
        public virtual DbSet<ItemType> ItemType { get; set; }
        public virtual DbSet<JobFunction> JobFunction { get; set; }
        public virtual DbSet<LightingSatisfactionFactor> LightingSatisfactionFactor { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<NewRateSchedule> NewRateSchedule { get; set; }
        public virtual DbSet<OperatingSchedule> OperatingSchedule { get; set; }
        public virtual DbSet<ProfileProduct> ProfileProduct { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Proposal> Proposal { get; set; }
        public virtual DbSet<ProposalAdditional> ProposalAdditional { get; set; }
        public virtual DbSet<ProposalLoanTerms> ProposalLoanTerms { get; set; }
        public virtual DbSet<ProposalNote> ProposalNote { get; set; }
        public virtual DbSet<ProposalTemplate> ProposalTemplate { get; set; }
        public virtual DbSet<ReportTemplate> ReportTemplate { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ZIPList> ZIPList { get; set; }
        public virtual DbSet<UserGroupForm> UserGroupForm { get; set; }
        public virtual DbSet<UserGroupModule> UserGroupModule { get; set; }
        public virtual DbSet<DefaultEmail> DefaultEmail { get; set; }
        public virtual DbSet<AnnualSalesRevenueSetup> AnnualSalesRevenueSetup { get; set; }
        public virtual DbSet<SalesReachSetup> SalesReachSetup { get; set; }
        public virtual DbSet<MainProduct> MainProduct { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
    }
}
