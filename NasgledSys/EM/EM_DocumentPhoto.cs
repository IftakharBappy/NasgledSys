using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.EM
{
    public static class EM_DocumentPhoto
    {
        public static AreaDocumentModel ConvertToModel(AreaDocument ent)
        {
            AreaDocumentModel bo = new AreaDocumentModel();

            bo.DocumentKey = ent.DocumentKey;
            bo.AreaKey = ent.AreaKey;
            bo.FileType = ent.FileType;
            bo.FileContent = ent.FileContent;
            bo.FileName = ent.FileName;
            bo.Description = ent.Description;


            return bo;
        }

        public static AreaDocument ConvertToEntity(AreaDocumentModel bo)
        {
            AreaDocument ent = new AreaDocument();
            ent.DocumentKey = bo.DocumentKey;
            ent.AreaKey = bo.AreaKey;
            ent.FileType = bo.FileType;
            ent.FileContent = bo.FileContent;
            ent.FileName = bo.FileName;
            ent.Description = bo.Description;

            return ent;
        }
    }
}