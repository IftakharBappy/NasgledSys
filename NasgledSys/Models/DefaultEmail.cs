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
    
    public partial class DefaultEmail
    {
        public System.Guid ID { get; set; }
        public string EmailAddress { get; set; }
        public string SenderName { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public Nullable<bool> IsSMTPssl { get; set; }
        public string POPServer { get; set; }
        public string IncomingPort { get; set; }
        public string POPUsername { get; set; }
        public string POPpassword { get; set; }
        public Nullable<bool> IsPOPssl { get; set; }
        public string Fullname { get; set; }
        public string Detail { get; set; }
    }
}
