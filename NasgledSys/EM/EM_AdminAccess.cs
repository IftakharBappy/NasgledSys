using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public class EM_AdminAccess
    {
        public static void SetUserAccess(Guid id)
        {
            try
            {
                NasgledDBEntities db = new NasgledDBEntities();
                GlobalClass.FormList = new List<UserFormClass>();
                var tf = (from x in db.UserGroupForm
                         where x.UserGroupKey == id && x.Forms.IsDelete == false
                         orderby x.Forms.FormLevel
                         select new UserFormClass
                         {
                             FormID=x.FormKey,
                             ModuleID=x.ModuleKey,
                             FormName=x.Forms.FormName,
                             FormLevel=x.Forms.FormLevel,
                             FormController = x.Forms.FormController,
                             FormView = x.Forms.FormView,
                             FormCss = x.Forms.FormCss,

                         });
                GlobalClass.FormList = tf.ToList();
                GlobalClass.ModuleList = new List<UserModuleClass>();
                var mm = (from x in db.UserGroupModule
                          where x.UserGroupKey == id && x.Modules.IsDelete == false
                          orderby x.Modules.ModuleLevel
                          select new UserModuleClass
                          {
                              UserGroupModuleKey = x.UserGroupModuleKey,
                              UserGroupKey = x.UserGroupKey,
                              ModuleKey = x.ModuleKey,
                              ModuleName = x.Modules.ModuleName,
                              Level = x.Modules.ModuleLevel,
                              formList = ((from g in db.UserGroupForm
                                          where g.UserGroupKey == x.UserGroupKey && g.ModuleKey==x.ModuleKey && g.Forms.IsDelete == false
                                          orderby g.Forms.FormLevel
                                          select new UserFormClass
                                          {
                                              FormID = g.FormKey,
                                              ModuleID = g.ModuleKey,
                                              FormName = g.Forms.FormName,
                                              FormLevel = g.Forms.FormLevel,
                                              FormController = g.Forms.FormController,
                                              FormView = g.Forms.FormView,
                                              FormCss = g.Forms.FormCss,

                                          })).ToList()

                          });
                GlobalClass.ModuleList = mm.ToList();

            }
            catch(Exception ex)
            {
                string temp = ex.Message.ToString();
            }
        }
    }
}