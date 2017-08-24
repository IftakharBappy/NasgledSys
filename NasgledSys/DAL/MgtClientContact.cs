using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtClientContact
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        public List<ClientContactClass> ListAll()
        {
            List<ClientContactClass> obj = new List<ClientContactClass>();
            var temp = (from x in db.ClientContact
                        where x.IsActive == true && x.ProfileKey==GlobalClass.ProfileUser.ProfileKey
                        select new ClientContactClass
                        {
                            ContactKey = x.ContactKey,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Address = x.Address,
                            IsActive = x.IsActive
                        }).OrderBy(m => m.Email);
            obj = temp.ToList();
            return obj;
        }

        public int Create(ClientContactClass obj)
        {
            int i = 1;
            try
            {
                
                ClientContact model = new ClientContact();
                model.ContactKey = Guid.NewGuid();
                model.FirstName = obj.FirstName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
                model.WebAddress = obj.WebAddress;
                model.CityKey = obj.CityKey;
                model.Address = obj.Address;
                model.Address2 = obj.Address2;
                model.JobTitle = obj.JobTitle;
                model.StateKey = obj.StateKey;
                model.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                model.CellPhone = obj.CellPhone;
                model.OfficePhone = obj.OfficePhone;
                model.InternalNote = obj.InternalNote;
                model.GeneralNote = obj.GeneralNote;
                model.Zipcode = obj.Zipcode;
                model.FaxPhone = obj.FaxPhone;
                model.IsActive = true;
                db.ClientContact.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

    }
}