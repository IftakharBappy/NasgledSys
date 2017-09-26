﻿using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class AreaNoteController : BaseController
    {
        // GET: AreaNote
        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaNoteViewModel model = new AreaNoteViewModel();
                model.AreaKey = id;
                try
                {

                    if (db.AreaNote.Any(o => o.AreaKey == id))
                    {
                        AreaNote entityrem = db.AreaNote.Find(model.AreaKey);
                        model = EM_AreaNote.ConvertToModel(entityrem);
                    }

                    else
                    {
                        model.AreaKey = id;
                        model.NoteKey = Guid.Empty;

                    }

                    Session["GlobalMessege"] = "";

                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Edit", "AreaNote"));
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