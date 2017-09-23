using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaPhotoViewModel
    {
        public Nullable<System.Guid> AreaKey { get; set; }
        public System.Guid PhotoKey { get; set; }
        public byte[] FileContent { get; set; }

        public string Description { get; set; }


        //private List<AreaPhotoModel> _areaPhotoList = new List<AreaPhotoModel>();
        //public List<AreaPhotoModel> AreaPhotoList { get => _areaPhotoList; set => _areaPhotoList = value; }
        public List<AreaPhotoModel> AreaPhotoList { get; set; }
    }
}