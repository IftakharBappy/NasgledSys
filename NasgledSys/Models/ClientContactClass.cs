﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ClientContactClass
    {
        public System.Guid ContactKey { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Display(Name = "Office Phone")]
        [Required(ErrorMessage = "Your must provide a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string OfficePhone { get; set; }
        public string Address { get; set; }
        public Nullable<System.Guid> ProfileKey { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Web Address")]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Your Url is not correct")]
        public string WebAddress { get; set; }
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        [Display(Name = "Fax Phone")]
        public string FaxPhone { get; set; }
        public string Address2 { get; set; }
        public Nullable<int> CityKey { get; set; }
        public Nullable<int> StateKey { get; set; }
        [Display(Name = "Zip code")]
        public string Zipcode { get; set; }
        public string InternalNote { get; set; }
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual CityList CityList { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual StateList StateList { get; set; }

        public List<ClientContactClass> ClientContactList { get; set; }
    }
}