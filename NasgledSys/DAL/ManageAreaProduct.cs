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
        public string IterateandRepeatThroughItem(BrowseItemClass model)
        {
            string temp = "";
            temp = temp + "<div class='panel panel-white'>";
                    temp = temp + "<div class='panel-heading'>";
                    temp = temp + "<h6 class='panel-title'>";
                    temp = temp + "<a data-toggle='collapse' data-parent='#accordion1' href='#"+model.FixtureKey+"'>"+model.ProductName+" "+model.ModelNo+"("+model.TypeCount+")</a>";
                    temp=temp+"</h6></div>";
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
                foreach(var item in sublist)
                {
                    temp = temp + "<div id='"+ model.FixtureKey+"' class='panel-collapse collapse'>";
                    temp = temp + "<div class='panel-body'>";
                    temp = temp + "<button type='button' class='btn btn-default btn-xlg col-md-6' value='" + model.FixtureKey + "' id='fixture' name='fixture'>" + model.ProductName + " " + model.ModelNo + "(" + model.TypeCount + ")</button><br />";
                    temp = temp + "</div>";
                    temp = temp + IterateandRepeatThroughItem(item);
                    temp = temp + "</div>";
                   
                }
            }
            else
            {
                temp = temp + "<div id='" + model.FixtureKey + "' class='panel-collapse collapse'>";
                temp = temp + "<div class='panel-body'>";
                temp = temp + "<button type='button' class='btn btn-default btn-xlg col-md-6' value='" + model.FixtureKey + "' id='fixture' name='fixture'>" + model.ProductName + " " + model.ModelNo + "(" + model.TypeCount + ")</button><br />";
                temp = temp + "</div>";               
                temp = temp + "</div>";
            }

            temp = temp + "</div>";
            return temp;
        }
        public string CreateProductListHTML()
        {
            var list = (from asset in db.ProfileProduct
                        where asset.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                        && asset.MainItemKey == null
                        select new
                        {

                            FixtureKey = asset.FixtureKey,
                            TypeCount = asset.TypeCount,
                            ProductName = asset.ProductName,
                            ModelNo = asset.ModelNo
                        }).OrderBy(m => m.ProductName).ToList();

            string content = "<h3>Browse for Items</h3>";
            content = content + "<div class='panel-group content-group-lg' id='accordion1'>";
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
                    content = content + "<div class='panel panel-white'>";

                    content = content + "<div class='panel-heading'>";
                    content = content + "<h6 class='panel-title'>";
                    content = content + "<a data-toggle='collapse' data-parent='#accordion1' href='#" + item.FixtureKey + "'>" + item.ProductName+"(" + item.TypeCount + ")</a>";
                    content = content + "</h6>";
                    content = content + "</div>";

                    content = content + "<div id='" + item.FixtureKey + "' class='panel-collapse collapse'>";
                    content = content + "<div class='panel-body'>";
                    content = content + "<button type='button' class='btn btn-default btn-xlg col-md-6' value='" + item.FixtureKey + "' id='fixture' name='fixture'>" + item.ProductName + " " + item.ModelNo+"(" + item.TypeCount + ")</button><br />";
                    content = content + "</div>";
                    foreach (var sitem in sublist)
                    {
                        content = content + IterateandRepeatThroughItem(sitem);
                    }
                    content = content + "</div>";

                    content = content + "</div>";
                    
                }
                else
                {
                    content = content + "<div class='panel panel-white'>";

                    content = content + "<div class='panel-heading'>";
                    content = content + "<h6 class='panel-title'>";
                    content = content + "<a data-toggle='collapse' data-parent='#accordion1' href='#" + item.FixtureKey + "'>" + item.ProductName + "</a>";
                    content = content + "</h6>";
                    content = content + "</div>";

                    content = content + "<div id='" + item.FixtureKey + "' class='panel-collapse collapse'>";
                    content = content + "<div class='panel-body'>";
                    content = content + "<button type='button' class='btn btn-default btn-xlg col-md-6' value='" + item.FixtureKey + "' id='fixture' name='fixture'>" + item.ProductName + " " + item.ModelNo + "</button>";
                    content = content + "</div>";
                    content = content + "</div>";

                    content = content + "</div>";
                }
                content = content + "</div>";
               

            }
            return content;
        }
    }
}