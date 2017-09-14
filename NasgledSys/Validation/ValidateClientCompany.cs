using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.Validation
{
    public class ValidateClientCompany
    {
        public ClientCompany ConvertVIewModelToEntityMOdelFOrCreate(ClientCompanyViewModel viewModel)
        {
            ClientCompany model = new ClientCompany();
            model.ClientCompanyKey = Guid.NewGuid();
            //# convert to model
            model.CompanyName = viewModel.CompanyName;
            model.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
            model.IndustryTypeKey = viewModel.IndustryTypeKey;
            model.NoOfSalesPerson = viewModel.NoOfSalesPerson;
            if (string.IsNullOrEmpty(viewModel.Description))
            {
                model.Description = "N/A";
            }
            else
            {
                model.Description = viewModel.Description;
            }
            model.OfficePhone = viewModel.OfficePhone;
            model.Address = viewModel.Address;
            if (string.IsNullOrEmpty(viewModel.Address2))
            {
                model.Address2 = "N/A";
            }
            else
            {
                model.Address2 = viewModel.Address2;
            }
            model.CityKey = viewModel.CityKey;
            model.StateKey = viewModel.StateKey;
            model.Zipcode = viewModel.Zipcode;
            if (string.IsNullOrEmpty(viewModel.BillingContactName))
            {
                model.BillingContactName = "N/A";
            }
            else
            {
                model.BillingContactName = viewModel.BillingContactName;
            }
            if (string.IsNullOrEmpty(viewModel.BillingContactEMail))
            {
                model.BillingContactEMail = "N/A";
            }
            else
            {
                model.BillingContactEMail = viewModel.BillingContactEMail;
            }
            model.IsAddressSameAsOffice = viewModel.IsAddressSameAsOffice;
            if (string.IsNullOrEmpty(viewModel.ProposalIntro))
            {
                model.ProposalIntro = "N/A";
            }
            else
            {
                model.ProposalIntro = viewModel.ProposalIntro;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalTeam))
            {
                model.ProposalTeam = "N/A";
            }
            else
            {
                model.ProposalTeam = viewModel.ProposalTeam;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalLegal))
            {
                model.ProposalLegal = "N/A";
            }
            else
            {
                model.ProposalLegal = viewModel.ProposalLegal;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalDisclaimer))
            {
                model.ProposalDisclaimer = "N/A";
            }
            else
            {
                model.ProposalDisclaimer = viewModel.ProposalDisclaimer;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalReference))
            {
                model.ProposalReference = "N/A";
            }
            else
            {
                model.ProposalReference = viewModel.ProposalReference;
            }
            if (string.IsNullOrEmpty(viewModel.EstimateFooter))
            {
                model.EstimateFooter = "N/A";
            }
            else
            {
                model.EstimateFooter = viewModel.EstimateFooter;
            }
            model.MarkupOrMargin = viewModel.MarkupOrMargin;
            model.MarkupOrMarginPercentage = viewModel.MarkupOrMarginPercentage;

            return model;
        }

        public ClientCompany ConvertVIewModelToEntityMOdelFOrEdit(ClientCompany model,ClientCompanyViewModel viewModel)
        {
            model.CompanyName = viewModel.CompanyName;
            model.IndustryTypeKey = viewModel.IndustryTypeKey;
            model.NoOfSalesPerson = viewModel.NoOfSalesPerson;
            if (string.IsNullOrEmpty(viewModel.Description))
            {
                model.Description = "N/A";
            }
            else
            {
                model.Description = viewModel.Description;
            }
            model.OfficePhone = viewModel.OfficePhone;
            model.Address = viewModel.Address;
            if (string.IsNullOrEmpty(viewModel.Address2))
            {
                model.Address2 = "N/A";
            }
            else
            {
                model.Address2 = viewModel.Address2;
            }
            model.CityKey = viewModel.CityKey;
            model.StateKey = viewModel.StateKey;
            model.Zipcode = viewModel.Zipcode;
            if (string.IsNullOrEmpty(viewModel.BillingContactName))
            {
                model.BillingContactName = "N/A";
            }
            else
            {
                model.BillingContactName = viewModel.BillingContactName;
            }
            if (string.IsNullOrEmpty(viewModel.BillingContactEMail))
            {
                model.BillingContactEMail = "N/A";
            }
            else
            {
                model.BillingContactEMail = viewModel.BillingContactEMail;
            }
            model.IsAddressSameAsOffice = viewModel.IsAddressSameAsOffice;
            if (string.IsNullOrEmpty(viewModel.ProposalIntro))
            {
                model.ProposalIntro = "N/A";
            }
            else
            {
                model.ProposalIntro = viewModel.ProposalIntro;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalTeam))
            {
                model.ProposalTeam = "N/A";
            }
            else
            {
                model.ProposalTeam = viewModel.ProposalTeam;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalLegal))
            {
                model.ProposalLegal = "N/A";
            }
            else
            {
                model.ProposalLegal = viewModel.ProposalLegal;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalDisclaimer))
            {
                model.ProposalDisclaimer = "N/A";
            }
            else
            {
                model.ProposalDisclaimer = viewModel.ProposalDisclaimer;
            }
            if (string.IsNullOrEmpty(viewModel.ProposalReference))
            {
                model.ProposalReference = "N/A";
            }
            else
            {
                model.ProposalReference = viewModel.ProposalReference;
            }
            if (string.IsNullOrEmpty(viewModel.EstimateFooter))
            {
                model.EstimateFooter = "N/A";
            }
            else
            {
                model.EstimateFooter = viewModel.EstimateFooter;
            }
            model.MarkupOrMargin = viewModel.MarkupOrMargin;
            model.MarkupOrMarginPercentage = viewModel.MarkupOrMarginPercentage;

            return model;
        }
    }
}