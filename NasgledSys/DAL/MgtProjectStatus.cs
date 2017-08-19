using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtProjectStatus
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ProjectStatusClass> ListAll()
        {
            List<ProjectStatusClass> obj = new List<ProjectStatusClass>();
            var temp = (from x in db.ProjectStatus
                        where x.IsDelete == false
                        select new ProjectStatusClass
                        {
                            ProjectStatusKey = x.ProjectStatusKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ProjectStatusClass obj)
        {
            int i = 1;
            try
            {
                ProjectStatus model = new ProjectStatus();
                model.ProjectStatusKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.ProjectStatus.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ProjectStatusClass obj)
        {
            int i = 1;
            try
            {
                ProjectStatus model = db.ProjectStatus.Find(obj.ProjectStatusKey);
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Delete(Guid ID)
        {
            int i = 1;
            try
            {
                ProjectStatus model = db.ProjectStatus.Find(ID);
                model.IsDelete = true;
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