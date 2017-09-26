using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaProductPhotoViewModel
    {
        public System.Guid PhotoKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public Nullable<System.Guid> FixtureKey { get; set; }
        public Nullable<System.Guid> ProductKey { get; set; }
        public byte[] FileContent { get; set; }

        public string Description { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }


        //private List<AreaPhotoModel> _areaPhotoList = new List<AreaPhotoModel>();
        //public List<AreaPhotoModel> AreaPhotoList { get => _areaPhotoList; set => _areaPhotoList = value; }
        public List<AreaProductPhotoViewModel> AreaProductPhotoList { get; set; }
    }
}