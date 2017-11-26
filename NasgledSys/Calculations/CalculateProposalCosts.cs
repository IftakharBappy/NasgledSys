using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.Calculations
{
    public class CalculateProposalCosts
    {
        public decimal GetProductCost(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.MarkupPercentage == null) p.MarkupPercentage = 0;
            if (p.ProductMargin == null) p.ProductMargin = 0;

            var productList = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.IsOn == 1 select x;
            foreach(var item in productList)
            {
                if (item.MarkupPercentage == null) item.MarkupPercentage = 0;// p.MarkupPercentage;

                decimal newcount = (decimal)((item.ReplaceQty / item.ForEveryMainQty) * item.Count);
                decimal TotalProductCost = (newcount * (decimal)item.ProductCost);
                //decimal markup = TotalProductCost + ((decimal)(item.MarkupPercentage/100)* TotalProductCost);
                // decimal margin = TotalProductCost * (100/ (decimal)p.ProductMargin);

                result = result + TotalProductCost;// markup;// + margin;
            }
            db.Dispose();
            return result;

        }

        public decimal GetProjectSaving(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            decimal? NewItemLifecost = 0;
            decimal? OldItemLifeCost = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.MarkupPercentage == null) p.MarkupPercentage = 0;
            if (p.ProductMargin == null) p.ProductMargin = 0;

            var productList = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.IsOn == 1 select x;
            foreach (var item in productList)
            {
                ProfileProduct old = db.ProfileProduct.Find(item.FixtureKey);
                Area area = db.Area.Find(item.AreaKey);
                OldItemLifeCost = OldItemLifeCost + ((old.LampLife*old.Watt*item.Count*item.OperatingSchedule.AnnualOperationHour*area.NewRateSchedule.BlendElectricityRate)/100);
                ProfileProduct newitem = db.ProfileProduct.FirstOrDefault(m=>m.ModelNo==item.ModelNo && m.ProductName==item.ProductName);
                decimal newcount = (decimal)((item.ReplaceQty / item.ForEveryMainQty) * item.Count);
                if (newitem != null)
                {
                    NewItemLifecost = NewItemLifecost + ((newitem.LampLife * item.WattPerProduct * newcount * item.OperatingSchedule.AnnualOperationHour * area.NewRateSchedule.BlendElectricityRate) / 100);
                }
               
            }
            result = (decimal)(OldItemLifeCost - NewItemLifecost);
            db.Dispose();
            return result;

        }
        public decimal GetGrossMarginAmount(decimal? MarginPercentage,decimal? COG)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            if (MarginPercentage == 0) result = 0;
            else  result = result + ((decimal)COG * (100 / (decimal)MarginPercentage));
          
            return result;

        }
        public decimal GetMarkupAmount(decimal? MarginPercentage, decimal? COG)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            if (MarginPercentage == 0) result = 0;
            else result = (decimal)COG + ((decimal)(MarginPercentage / 100) * (decimal)COG);

            return result;

        }
        public decimal GetLaborCost(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.LaborCost == null) p.LaborCost = 0;
            result = result +(decimal)p.LaborCost;
            db.Dispose();
            return result;
        }

        public decimal GetShippingCost(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.ShippingCost == null) p.ShippingCost = 0;          

            var productList = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.IsOn == 1 select x;
            foreach (var item in productList)
            {
                if (item.ShipingCost != null) result = result + (decimal)item.ShipingCost;
            }
            result = result + (decimal)p.ShippingCost;
            db.Dispose();
            return result;

        }
        public decimal GetMiscCost(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.MiscCost == null) p.MiscCost = 0;

            var productList = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.IsOn == 1 select x;
            foreach (var item in productList)
            {
                if (item.MiscCost != null) result = result + (decimal)item.MiscCost;
            }
            result = result + (decimal)p.MiscCost;
            db.Dispose();
            return result;

        }
        public decimal GetEstimatedIncentiveRebate(Guid? ProposalKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Proposal p = db.Proposal.Find(ProposalKey);
            if (p.TaxIncentives == null) p.TaxIncentives = 0;
            if (p.Incentives == null) p.Incentives = 0;

            var productList = from x in db.AreaProduct where x.ProjectKey == p.ProjectKey && x.IsDelete == false && x.IsOn == 1 select x;
            foreach (var item in productList)
            {
                if (item.IncentiveAmount == null) item.IncentiveAmount = 0;
                if (item.IncentiveMaxAmount == null) item.IncentiveMaxAmount = 0;

                decimal newcount = (decimal)((item.ReplaceQty / item.ForEveryMainQty) * item.Count);
                decimal incentiv = (newcount * (decimal)item.IncentiveAmount);
                decimal incentivMAX = (newcount * (decimal)item.IncentiveMaxAmount);

                result = result + incentiv+ incentivMAX;
            }
            db.Dispose();
            result = result +(decimal)p.Incentives;
            return result;

        }

        public decimal GetExistingCost(Guid? ProjectKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Project p = db.Project.Find(ProjectKey);
            if (p.TaxIncentives == null) p.TaxIncentives = 0;
            var areaList = from x in db.Area where x.ProjectKey == p.ProjectKey && x.IsDelete == false select x;


           
            foreach (var item in areaList)
            {
                var productList = from x in db.AreaProduct where x.AreaKey == item.AreaKey && x.IsDelete == false  select x;
                foreach (var asd in productList)
                {
                    var detailList = from x in db.AreaProductDetail where x.ProductKey == asd.ProductKey select x;
                    foreach (var zx in detailList)
                    {
                       
                        if (zx.ReplacementCost == null) zx.ReplacementCost = 0;
                        if (zx.AnnualMaintenance == null) zx.AnnualMaintenance = 0;
                        if (zx.WorkingFixtureCount == null) zx.WorkingFixtureCount = asd.Count;

                       // decimal rp = (decimal)(zx.ReplacementCost * asd.Count);
                        decimal am = (decimal)(zx.AnnualMaintenance * asd.Count);
                       
                        result = result  + am;
                    }
                    ProfileProduct fix = db.ProfileProduct.Find(asd.FixtureKey);
                    decimal operatinghr =(decimal)(asd.OperatingSchedule.AnnualOperationHour * asd.Count*item.NewRateSchedule.BlendElectricityRate*fix.Watt)/1000;
                    result = result + operatinghr;
                }
            }
            db.Dispose();
            
            return result;

        }

        public decimal GetNewCost(Guid? ProjectKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Project p = db.Project.Find(ProjectKey);
           
            var areaList = from x in db.Area where x.ProjectKey == p.ProjectKey && x.IsDelete == false select x;

            foreach (var item in areaList)
            {
                var productList = from x in db.AreaProduct where x.AreaKey == item.AreaKey && x.IsDelete == false && x.IsOn ==1 select x;
                foreach (var asd in productList)
                {
                    decimal newcount = (decimal)((asd.ReplaceQty / asd.ForEveryMainQty) * asd.Count);
                    decimal operatinghr = (decimal)(asd.OperatingSchedule.AnnualOperationHour * newcount * item.NewRateSchedule.BlendElectricityRate * asd.WattPerProduct) / 1000; 
                    result = result + operatinghr;
                }
            }
            db.Dispose();

            return result;

        }

        public decimal GetExistingEnergy(Guid? ProjectKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Project p = db.Project.Find(ProjectKey);
            if (p.TaxIncentives == null) p.TaxIncentives = 0;
            var areaList = from x in db.Area where x.ProjectKey == p.ProjectKey && x.IsDelete == false select x;



            foreach (var item in areaList)
            {
                var productList = from x in db.AreaProduct where x.AreaKey == item.AreaKey && x.IsDelete == false select x;
                foreach (var asd in productList)
                {
                    
                    ProfileProduct fix = db.ProfileProduct.Find(asd.FixtureKey);
                    decimal operatinghr = (decimal)(asd.OperatingSchedule.AnnualOperationHour * asd.Count * fix.Watt) / 1000; 
                    result = result + operatinghr;
                }
            }
            db.Dispose();

            return result;

        }

        public decimal GetNewEnergy(Guid? ProjectKey)
        {
            NasgledDBEntities db = new NasgledDBEntities();

            decimal result = 0;
            Project p = db.Project.Find(ProjectKey);

            var areaList = from x in db.Area where x.ProjectKey == p.ProjectKey && x.IsDelete == false select x;

            foreach (var item in areaList)
            {
                var productList = from x in db.AreaProduct where x.AreaKey == item.AreaKey && x.IsDelete == false && x.IsOn == 1 select x;
                foreach (var asd in productList)
                {
                    decimal newcount = (decimal)((asd.ReplaceQty / asd.ForEveryMainQty) * asd.Count);
                    decimal operatinghr = (decimal)(asd.OperatingSchedule.AnnualOperationHour * newcount * asd.WattPerProduct) / 1000;
                    result = result + operatinghr;
                }
            }
            db.Dispose();

            return result;

        }
    }
}