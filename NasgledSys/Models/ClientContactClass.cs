using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ClientContactClass
    {
        public System.Guid ContactKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string OfficePhone { get; set; }
        public string Address { get; set; }
        public Nullable<System.Guid> ProfileKey { get; set; }
        public string JobTitle { get; set; }
        public string WebAddress { get; set; }
        public string CellPhone { get; set; }
        public string FaxPhone { get; set; }
        public string Address2 { get; set; }
        public Nullable<int> CityKey { get; set; }
        public Nullable<int> StateKey { get; set; }
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