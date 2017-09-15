using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class AreaDetailController : BaseController
    {
         
        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaDetailViewModel model = new AreaDetailViewModel();
                try
                {
                    //if (GlobalClass.AreaGuidForSubArea == null || GlobalClass.AreaGuidForSubArea == Guid.Empty)
                    //{
                    //    AreaDetail entity = db.AreaDetail.Find(id);
                    //    //ViewBag.heading = ar.AreaName;
                    //    model = EM_AreaDetail.ConvertToModel(entity);
                    //}
                    //else
                    //{
                    //    model.AreaKey = id;
                    //    model.DetailKey = Guid.Empty;
                    //    model.LightingSatisfactionKey = Guid.Empty;
                    //}

                    model.AreaKey = id;
                    model.DetailKey = Guid.Empty;
                    model.LightingSatisfactionKey = Guid.Empty;

                    Session["GlobalMessege"] = "";
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Created", "MgtProject"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
    }
}