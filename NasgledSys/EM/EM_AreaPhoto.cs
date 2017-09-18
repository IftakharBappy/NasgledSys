using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.EM
{
    public static class EM_AreaPhoto
    {
        public static AreaPhotoModel ConvertToModel(AreaPhoto ent)
        {
            AreaPhotoModel bo = new AreaPhotoModel();

            bo.PhotoKey = ent.PhotoKey;
            bo.AreaKey = ent.AreaKey;
            bo.FileType = ent.FileType;
            bo.FileContent = ent.FileContent;
            bo.FileName = ent.FileName;
            bo.Description = ent.Description;
          

            return bo;
        }

        public static AreaPhoto ConvertToEntity(AreaPhotoModel bo)
        {
            AreaPhoto ent = new AreaPhoto();
            ent.PhotoKey = bo.PhotoKey;
            ent.AreaKey = bo.AreaKey;
            ent.FileType = bo.FileType;
            ent.FileContent = bo.FileContent;
            ent.FileName = bo.FileName;
            ent.Description = bo.Description;

            return ent;
        }
    }
}