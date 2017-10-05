using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class FinanceCompanyViewModel
    {
        public System.Guid FinanceKey { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company Name is required.")]
        public string FinancingCompanyName { get; set; }
        [Display(Name = "Introduction")]
        [Required(ErrorMessage = "Introduction is required.")]
        public string IntroText { get; set; }
        public string Hyperlink { get; set; }
        public string LogoType { get; set; }
        public byte[] Logo { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase PostedLogo { get; set; }
        public List<FinanceCompanyViewModel> FinanceCompanyViewModelList { get; set; }
    }
}