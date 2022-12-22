using SV19T1081011.BusinessLayers;
using SV19T1081011.DomainModels;
using SV19T1081011.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.UserInterfaceOutput model = new Models.UserInterfaceOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                TrendingPosts = ContentService.TrendingList(),
                FeaturedPosts = ContentService.FeaturedList()
            };
            return View(model);
        }

        public ActionResult Category(string categoryUrlName)
        {
            Models.UserInterfaceOutput model = new Models.UserInterfaceOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                Category = ContentService.GetCategory(categoryUrlName),
                List = ContentService.ListPosts(), // theo category
            };
            return View(model);
        }
    }
}