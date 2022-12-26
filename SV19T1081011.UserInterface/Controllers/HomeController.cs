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
        public ActionResult Category(string categoryUrlName, string searchValue = "", string page = "1")
        {
            PostCategory category = ContentService.GetCategory(categoryUrlName);
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            int rowCount = 0, pageNumber = Converter.ToInt(page);
            Models.UserInterfaceOutput model = new Models.UserInterfaceOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                Category = category,
                List = ContentService.ListPosts(pageNumber, 5, searchValue, category.CategoryId, out rowCount),
                RowCount = rowCount
            };
            return View(model);
        }

        public ActionResult Post(string categoryUrlName, string urlTitle, string commentContent = "")
        {
            Post post = ContentService.GetPost(urlTitle);
            if (post == null || (post != null && post.Category.CategoryUrlName != categoryUrlName))
            {
                return RedirectToAction("Index");
            }


            if (!string.IsNullOrWhiteSpace(commentContent))
            {
                PostComment data = new PostComment
                {
                    CreatedTime = DateTime.Now,
                    CommentContent = commentContent,
                    IsAccepted = false,
                    UserId = Converter.ToLong(this.User.GetUserData().UserId),
                    PostId = post.PostId
                };
                ContentService.AddComment(data);
            }

            int rowCount = 0;
            Models.UserInterfaceOutput model = new Models.UserInterfaceOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                Post = post,
                Comments = ContentService.ListComments(1, 0, "", post.PostId, out rowCount),
            };

            model.Comments.Reverse();

            return View(model);
        }
    }
}