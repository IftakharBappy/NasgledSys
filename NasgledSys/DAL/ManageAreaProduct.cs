using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;

namespace NasgledSys.DAL
{
    public class ManageAreaProduct
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        public string GetAllFixtureNamesForExistingKey(Guid? id)
        {
            ProfileProduct area = db.ProfileProduct.Find(id);
            return id == null ? " " : (GetAllFixtureNamesForExistingKey(area.MainItemKey) + "||" + area.ProductName);

        }
    }
}