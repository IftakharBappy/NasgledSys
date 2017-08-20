using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtJobFunction
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<JobFunctionViewModel> ListAll()
        {
            List<JobFunctionViewModel> obj = new List<JobFunctionViewModel>();
            var temp = (from x in db.JobFunction
                        where x.IsDelete == false
                        select new JobFunctionViewModel
                        {
                            JobFunctionKey = x.JobFunctionKey,
                            FunctionName = x.FunctionName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.FunctionName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(JobFunctionViewModel obj)
        {
            int i = 1;
            try
            {
                JobFunction model = new JobFunction();
                model.JobFunctionKey = Guid.NewGuid();
                model.FunctionName = obj.FunctionName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.JobFunction.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(JobFunctionViewModel obj)
        {
            int i = 1;
            try
            {
                JobFunction model = db.JobFunction.Find(obj.JobFunctionKey);
                model.FunctionName = obj.FunctionName;
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
                JobFunction model = db.JobFunction.Find(ID);
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