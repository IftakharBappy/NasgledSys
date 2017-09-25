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
        public string IterateandRepeatThroughItem1(BrowseItemClass model)
        {
            string temp = "";


            var sublist = (from asset in db.ProfileProduct
                           where asset.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                           && asset.MainItemKey == model.FixtureKey
                           select new BrowseItemClass
                           {
                               FixtureKey = asset.FixtureKey,
                               TypeCount = asset.TypeCount,
                               ProductName = asset.ProductName,
                               ModelNo = asset.ModelNo,
                               MainItemKey = asset.MainItemKey
                           }).OrderBy(m => m.ProductName).ToList();
            if (sublist.Count() > 0)
            {
                temp = temp + "<li><a href= '#' class='fix' discussKey='" + model.FixtureKey + "'>" + model.ProductName + "(" + model.TypeCount + ")</a>";
                temp = temp + "<ul>";
                foreach (var item in sublist)
                {
                    temp = temp + IterateandRepeatThroughItem1(item);
                }
                temp = temp + "</ul></li>";
            }
            else
            {

                temp = temp + "<li><a href= '#' class='fix' discussKey='" + model.FixtureKey + "' >" + model.ProductName + "(" + model.TypeCount + ")</a></li>";
            }


            return temp;
        }
        public string CreateProductListHTML1()
        {
            var list = (from asset in db.ProfileProduct
                        where asset.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                        && asset.MainItemKey == null
                        select new BrowseItemClass
                        {

                            FixtureKey = asset.FixtureKey,
                            TypeCount = asset.TypeCount,
                            ProductName = asset.ProductName,
                            ModelNo = asset.ModelNo
                        }).OrderBy(m => m.ProductName).ToList();

            //string content = "<h3>Browse for Items</h3>";
            string content = "";
           //content = content + " <div class='sidebar-main sidebar-default'><div class='sidebar-fixed'><div class='sidebar-content'><div class='sidebar-category sidebar-category-visible'><div class='category-content no-padding'>";
           //content = content + "<ul class='navigation navigation-main navigation-accordion'>";
            foreach (var item in list)
            {

                ProfileProduct pr = db.ProfileProduct.Find(item.FixtureKey);
                var sublist = (from asset in db.ProfileProduct
                               where asset.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                               && asset.MainItemKey == item.FixtureKey
                               select new BrowseItemClass
                               {

                                   FixtureKey = asset.FixtureKey,
                                   TypeCount = asset.TypeCount,
                                   ProductName = asset.ProductName,
                                   ModelNo = asset.ModelNo,
                                   MainItemKey = asset.MainItemKey
                               }).OrderBy(m => m.ProductName).ToList();
                if (sublist.Count() > 0)
                {
                    content = content + "<li><a href= '#' class='fix' discussKey='" + item.FixtureKey + "'>" + item.ProductName + "(" + item.TypeCount + ")</a>";
                    content = content + "<ul>";
                    foreach (var sitem in sublist)
                    {
                        content = content + IterateandRepeatThroughItem1(sitem);
                    }
                    content = content + "</ul></li>";
                   
                }
                else
                {
                    content = content + "<li><a href= '#' class='fix' discussKey='" + item.FixtureKey + "' >" + item.ProductName + "(" + item.TypeCount + ")</a></li>";
                }
            }
            //content = content + "</ul>";
            //content = content + "</div></div></div></div></div>";
            return content;
        }


      
        public string GetSuggestiveItem(Guid ProjectKey)
        {
            var list = (from x in db.AreaProduct
                        where x.ProjectKey == ProjectKey && x.IsDelete==false
                        group x by x.ProductKey into g
                        select new
                        {

                            FixtureKey = g.FirstOrDefault().ProductKey,
                            TypeCount = g.FirstOrDefault().ProfileProduct.TypeCount,
                            ProductName = g.FirstOrDefault().ProfileProduct.ProductName,
                            ModelNo = g.FirstOrDefault().ProfileProduct.ModelNo
                        }).OrderBy(m => m.ProductName).ToList();

            string content = "<h3>Existing Products used in this Project</h3>";
            content = content + "<ul>";
            foreach (var item in list)
            {

                content = content + "<li><a href= '#' class='fix' discussKey='" + item.FixtureKey + "' >" + item.ProductName + "(" + item.TypeCount + ")</a></li>";
            }               
                content = content + "</ul>";

            return content;
        }
       
    }
}