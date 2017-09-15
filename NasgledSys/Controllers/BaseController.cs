using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base
        public NasgledDBEntities db;

        public BaseController()
        {
            db = new NasgledDBEntities();
        }
    }
}