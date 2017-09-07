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

        public List<ClientContactViewModel> ListAll()
        {
            List<ClientContactViewModel> obj = new List<ClientContactViewModel>();
            var temp = (from x in db.ClientContact
                        where x.IsActive == true && x.ProfileKey==GlobalClass.ProfileUser.ProfileKey
                        select new ClientContactViewModel
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

        public int Create(ClientContactViewModel obj)
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

        public static ClientContactViewModel ConvertToModel(ClientContact ent)
        {
            ClientContactViewModel bo = new ClientContactViewModel();

            bo.ContactKey = ent.ContactKey;
            bo.FirstName = ent.FirstName;
            bo.LastName = ent.LastName;
            bo.Email = ent.Email;
            bo.WebAddress = ent.WebAddress;
            bo.CityKey = ent.CityKey;
            bo.Address = ent.Address;
            bo.Address2 = ent.Address2;
            bo.JobTitle = ent.JobTitle;
            bo.StateKey = ent.StateKey;
            bo.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
            bo.CellPhone = ent.CellPhone;
            bo.OfficePhone = ent.OfficePhone;
            bo.InternalNote = ent.InternalNote;
            bo.GeneralNote = ent.GeneralNote;
            bo.Zipcode = ent.Zipcode;
            bo.FaxPhone = ent.FaxPhone;
            bo.IsActive = ent.IsActive;

            return bo;
        }
        public List<ClientContactViewModel> SearchList(String searchTerm)
        {
            List<ClientContactViewModel> obj = new List<ClientContactViewModel>();
            var temp = (from x in db.ClientContact
                        where x.FirstName.Contains(searchTerm)
                        select new ClientContactViewModel
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

    }
}