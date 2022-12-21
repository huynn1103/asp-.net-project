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
            Models.HomeOutput model = new Models.HomeOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                TrendingPosts = ContentService.TrendingList(),
                FeaturedPosts = ContentService.FeaturedList()
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}