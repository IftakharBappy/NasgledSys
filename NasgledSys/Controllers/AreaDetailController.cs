using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Linq;
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
                model.AreaKey = id;
                try
                {
                
                    if (db.AreaDetail.Any(o => o.AreaKey == id))
                    {
                        AreaDetail entityModel = db.AreaDetail.FirstOrDefault(m => m.AreaKey == id);
                        model = EM_AreaDetail.ConvertToModel(entityModel);
                        ViewBag.heading = model.Description;
                    }

                    else
                    {
                        model.AreaKey = id;
                        model.DetailKey = Guid.Empty;
                        model.LightingSatisfactionKey = Guid.Empty;
                    }

                    Session["GlobalMessege"] = "";

                    ViewBag.LightingSatisfactionKey = new SelectList(db.LightingSatisfactionFactor.Where(m => m.IsDelete == false).OrderBy(m => m.FactorName), "PKey", "FactorName");

                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Edit", "AreaDetail"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }


        [HttpPost]
        public ActionResult Edit(AreaDetailViewModel model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.AverageIlluminnce == null) model.AverageIlluminnce = 0;

                        if (model.LightingSatisfactionKey == null || model.LightingSatisfactionKey == Guid.Empty)
                        {
                            model.LightingSatisfactionKey = null;
                        }

                        if (model.CeilingHeight == null) model.CeilingHeight = 0;

                        if (model.Reflectance == null) model.Reflectance = 0;

                        if (model.AreaWidth == null) model.AreaWidth = 0;

                        if (model.Length == null) model.Length = 0;

                        model.DetailKey = Guid.NewGuid();

                        AreaDetail entity = new AreaDetail();
                        entity = EM_AreaDetail.ConvertToEntity(model);

                        db.AreaDetail.Add(entity);
                        db.SaveChanges();
                       
                        Session["GlobalMessege"] = "Area Details was successfully Created.";
                        GlobalClass.AreaHeading = model.Description;
                        return RedirectToAction("Edit", "AreaDetail", new { id = model.AreaKey });
                    }

                    ViewBag.LightingSatisfactionKey = new SelectList(db.LightingSatisfactionFactor.Where(m => m.IsDelete == false).OrderBy(m => m.FactorName), "PKey", "FactorName");

                    return View(model);
                }
                catch (Exception e)
                {
                    Session["GlobalMessege"] = e.Message.ToString();
                    return View(model);
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