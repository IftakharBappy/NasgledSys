using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class UserModuleClass
    {
        public Guid? UserGroupModuleKey { get; set; }
        public Guid? UserGroupKey { get; set; }
        public Guid? ModuleKey { get; set; }
        public string ModuleName { get; set; }
        public int? Level { get; set; }
        public List<UserFormClass> formList { get; set; }
    }
    public class UserFormClass
    {
        public Guid? FormID { get; set; }
        public Guid? ModuleID { get; set; }
        public string FormName { get; set; }
        public Nullable<int> FormLevel { get; set; }
        public string FormController { get; set; }
        public string FormView { get; set; }
        public string FormCss { get; set; }
    }
}